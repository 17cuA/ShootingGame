using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChenkenLaser;
using UnityEngine;

 [DefaultExecutionOrder(599)]
 [RequireComponent(typeof(AudioSource))]
class Device_LaserEmitter : MonoBehaviour
{
	public bool isPlayerUseAudio = false;
	public bool isClose = false;

	[Header("Fireボタン/キー　設定")]
	public KeyCode firekey;
	public string fireButtonName;
	InputManagerObject inputManager1P;
	InputManagerObject inputManager2P;

	//--------------------------- 直線型レーザー（タイプ１） ----------------------------------
	[Header("--------直線型レーザー--------")]
	[SerializeField] [Range(0.1f,0.025f)] private float straightLaserWidth            = 0.05f;
	[SerializeField] [Range(20,70)]       private int straightLaserNodeMax            = 50;
	[SerializeField] [Range(0.2f,0.6f)]   private float straightLaserOverloadDuration = 0.4f;
	[SerializeField] [Range(0.005f,0.05f)]private float straightLaserLaunchInterval   = 0.01f;
	[SerializeField] private float straightTrailWidth = 0.1f;
	[SerializeField] private Material straightLaserMaterial;
	private GameObject straightLaserGeneratorParent;

	//--------------------------- 直線型レーザー（タイプ2） ----------------------------------
	[Header("--------回転型レーザー--------")]
	[SerializeField] [Range(0.1f, 0.025f)] private float rotateLaserWidth = 0.05f;
	[SerializeField] [Range(20, 70)] private int rotateLaserNodeMax = 50;
	[SerializeField] [Range(0.2f, 0.6f)] private float rotateLaserOverloadDuration = 0.4f;
	[SerializeField] [Range(0.005f, 0.05f)] private float rotateLaserLaunchInterval = 0.01f;
	[SerializeField] private float rotateTrailWidth = 0.1f;
	[SerializeField] private Material rotateLaserMaterial;
	private GameObject rotateLaserGeneratorParent;
	[SerializeField] private float rotateSpeed = 10;

	public GameObject parentObj;
	public Bit_Formation_3 bf;
	public string parentname;
	bool isOption;
	private float endTimer;
	private bool isEnd = false;

	/// <summary>
	/// 回転装置
	/// </summary>
	public class EmitterRotateCore
	{
		public float theta;					
		public Vector3 pushPosition;

		public EmitterRotateCore(Vector3 pushPosition)
		{
			this.pushPosition = pushPosition;
			this.theta = 0;
		}

		public void Rotate(float angle)
		{
			this.theta += angle;
			this.pushPosition.x = Mathf.Cos(this.theta);
			this.pushPosition.y = Mathf.Sin(this.theta);
		}

		public void Reset()
		{
			theta = 0;
		}
	}


	private EmitterLaunchCore emitterLaunchCore;
	private EmitterRotateCore emitterRotateCore;

	private LaunchDevice straightLaunchDevive;
	private LaunchDevice rotateLaunchDevice;

	private AudioSource audioSource;
	[SerializeField] private AudioClip laserBegin;
	[SerializeField] private AudioClip laserContinuing;
	[SerializeField] private AudioClip laserEnd;
	private float firePressTime;//

	private void OnEnable()
	{
		this.isClose = false;
	}

	private void Awake()
	{
		var straightLaserParent = new GameObject("Device_StrightLaserParent");
		straightLaserParent.transform.SetParent(this.transform);
		straightLaserParent.transform.localPosition = Vector3.zero;
		this.straightLaserGeneratorParent = straightLaserParent;

		var rotateLaserParent = new GameObject("Device_RotateLaserParent");
		rotateLaserParent.transform.SetParent(this.transform);
		rotateLaserParent.transform.localPosition = Vector3.zero;
		this.rotateLaserGeneratorParent = rotateLaserParent;

		straightLaunchDevive
			= new StraightLaunchDevive
			(
				straightLaserOverloadDuration,
				straightLaserLaunchInterval,
				straightLaserWidth,
				straightLaserMaterial,
				straightLaserNodeMax,
				straightLaserGeneratorParent
			);

		rotateLaunchDevice
			= new RotateLaunchDevice
			(
				rotateLaserOverloadDuration,
				rotateLaserLaunchInterval,
				rotateLaserWidth,
				rotateLaserMaterial,
				rotateLaserNodeMax,
				rotateLaserGeneratorParent
			);

		this.emitterLaunchCore = new EmitterLaunchCore(straightLaunchDevive);
		this.emitterRotateCore = new EmitterRotateCore(transform.position);

		inputManager1P = GameObject.Find("InputManager_1P").GetComponent<InputManagerObject>();
		inputManager2P = GameObject.Find("InputManager_2P").GetComponent<InputManagerObject>();

		if (isPlayerUseAudio)
		{
			this.audioSource = GetComponent<AudioSource>();
		}
		parentObj = transform.parent.gameObject;
		parentname = parentObj.name;
		if (parentObj.name == "Option(Clone)")
		{
			bf = parentObj.GetComponent<Bit_Formation_3>();
			isOption = true;
		}
		else if (parentObj.name == "Player(Clone)")
		{

			fireButtonName = inputManager1P.Manager.Button["Shot"];
			isOption = false;
		}
		else if (parentObj.name == "Player2(Clone)")
		{
			fireButtonName = inputManager2P.Manager.Button["Shot"];
			isOption = false;
		}

	}

    private void OnDisable()
    {

        if(Obj_Storage.Storage_Data.Laser_Line != null)
        { 
             for (int i = 29; i < Obj_Storage.Storage_Data.Laser_Line.Get_Obj().Count; i++)
             {
                 if(Obj_Storage.Storage_Data.Laser_Line.Get_Obj()[i].gameObject != null && !Obj_Storage.Storage_Data.Laser_Line.Get_Obj()[i].gameObject.activeSelf )
                 { 
                     Destroy(Obj_Storage.Storage_Data.Laser_Line.Get_Obj()[i]);
                     Obj_Storage.Storage_Data.Laser_Line.Get_Obj().RemoveAt(i);
                 }
            }
        }
    }
    private void Update()
	{

		var launchDevice = emitterLaunchCore.CurrentDevice;
		if (isOption)
		{
			if (bf.bState == Bit_Formation_3.BitState.Player1)
			{
				fireButtonName = inputManager1P.Manager.Button["Shot"];
			}
			else if (bf.bState == Bit_Formation_3.BitState.Player2)
			{
				fireButtonName = inputManager2P.Manager.Button["Shot"];
			}
		}

		if (this.isClose)
		{
			if(launchDevice.Type == DeviceType.TYPE_1_STRAIGHT)
			{
				var num = transform.GetChild(0).childCount;
				for(var i = 0; i < num; ++i)
				{
					if(transform.GetChild(0).GetChild(i).gameObject.activeSelf)
					{
						return;
					}
				}
				this.gameObject.SetActive(false);
			}
		}
		else
		{
			//-----------------------------------------------------------------入力 検索----------------------------------------------------------------------
			if (Input.GetButtonDown(fireButtonName) || Input.GetKeyDown(firekey))
			{
			    this.emitterLaunchCore.GenerateLine();

				if (audioSource.isPlaying) audioSource.Stop();
				audioSource.clip = laserBegin;
				audioSource.Play();

				isEnd = false;
			}

			if (Input.GetButton(fireButtonName) || Input.GetKey(firekey))
			{
				if (launchDevice.CurrentGenerator == null)
				{
					this.emitterLaunchCore.GenerateLine();			
				}
                else
                { 
				    if (Time.time >= launchDevice.CanLaunchTime && launchDevice.CurrentGenerator != null)
				    {
					
					    if (isPlayerUseAudio)
					    {
						
						    //記念すべきAセット by Johnny Yamazaki
						    // ドトールのミラノサンドはおいしいよ
						    if (audioSource.time >= laserBegin.length * 0.6f && audioSource.clip == laserBegin && audioSource.clip != laserEnd)
						    {
							    audioSource.clip = laserContinuing;
							    audioSource.loop = true;
							    audioSource.Play();
						    }
						    if(audioSource.clip == laserEnd && audioSource.time >= laserEnd.length * 0.6f)
						    {
							    if (audioSource.isPlaying) audioSource.Stop();
							    audioSource.clip = laserBegin;
							    audioSource.Play();
						    }

					    }

						 this.emitterLaunchCore.LaunchNode();
				    }
                }
			}

			if (Input.GetButtonUp(fireButtonName) || Input.GetKeyUp(firekey))
			{
				if (launchDevice.CurrentGenerator != null)
				{
					launchDevice.CurrentGenerator.IsFixed = false;
					launchDevice.ResetGenerator();
				}

				if (isPlayerUseAudio)
				{
					if (audioSource.isPlaying) audioSource.Stop();
					audioSource.clip = laserEnd;
                    audioSource.loop = false;
					audioSource.Play();
				}
			}
		}
		//---------------------------------------------------------------------------------------------------------------------------------------------------
		

		//------------------------------------------------------------レーザー発射装置使いすぎ------------------------------------------------------
		if(launchDevice.CurrentGenerator != null && launchDevice.CurrentGenerator.IsOverLoad)
		{
			launchDevice.ResetGenerator();

			launchDevice.UpdateCanLaunchTime();
			//if (isPlayerUseAudio)
			//{
			//	if (audioSource.loop) audioSource.loop = false;
			//	if (audioSource.isPlaying) audioSource.Stop();
			//	audioSource.PlayOneShot(laserEnd);

			//}
		}

		if(launchDevice.CurrentGenerator != null && launchDevice.CurrentGenerator.pointCount ==  straightLaserNodeMax / 3 * 2)
		{
			if (isPlayerUseAudio)
			{
				if (audioSource.isPlaying) audioSource.Stop();
				audioSource.clip = laserEnd;
				audioSource.Play();
			}
		}
		//------------------------------------------------------------------------------------------------------------------------------------------------


		if(!this.transform.parent.gameObject.activeSelf && launchDevice.CurrentGenerator != null)
		{
			launchDevice.ResetGenerator();
		}


		if (launchDevice.Type == DeviceType.TYPE_1_STRAIGHT)
		{
			this.transform.gameObject.transform.localEulerAngles = Vector3.zero;
		}

		if(Input.GetKeyDown(KeyCode.Q))
		{
			this.isClose = !this.isClose;
		}

		if(Input.GetKeyDown(KeyCode.Z))
		{
			if (emitterLaunchCore.CurrentDevice.Type == DeviceType.TYPE_1_STRAIGHT)
			{
				emitterRotateCore.Reset();
				emitterLaunchCore.SetDevice(rotateLaunchDevice);
			}

			else if (emitterLaunchCore.CurrentDevice.Type == DeviceType.TYPE_2_ROTATE)
			{
				emitterRotateCore.Reset();
				emitterRotateCore.Reset();
				emitterLaunchCore.SetDevice(straightLaunchDevive);
			}
		}


		if (emitterLaunchCore.CurrentDevice.Type == DeviceType.TYPE_2_ROTATE && Input.GetKey(KeyCode.LeftShift))
		{
			//this.emitterRotateCore.Rotate(Mathf.PI / 12 * Mathf.Deg2Rad);
			this.transform.gameObject.transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed);
		}

		if (emitterLaunchCore.CurrentDevice.Type == DeviceType.TYPE_2_ROTATE && Input.GetKey(KeyCode.LeftControl))
		{
			//this.emitterRotateCore.Rotate(Mathf.PI / 12 * Mathf.Deg2Rad);
			this.transform.gameObject.transform.Rotate(-Vector3.forward * Time.deltaTime * rotateSpeed);
		}
	}
}

