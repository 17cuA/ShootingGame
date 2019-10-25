using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

 [DefaultExecutionOrder(599)]
 [RequireComponent(typeof(AudioSource))]
class Device_LaserEmitter : MonoBehaviour
{
	#region ##### DEBUG #####
	[Header("DEBUG　設定")]
	public bool isPlayerUseAudio = false;
	public KeyCode firekey;							//発射キー
	public string fireButtonName;                   //発射ボタン
	#endregion

	#region ##### レーザー　詳細設定 #####
	//--------------------------- 直線型レーザー（タイプ１） ----------------------------------
	[Header("--------直線型レーザー--------")]
	[SerializeField] [Range(0.1f,0.025f)] private float straightLaserWidth            = 0.05f;
	[SerializeField] [Range(20,70)]       private int straightLaserNodeMax            = 50;
	[SerializeField] [Range(0.2f,0.6f)]   private float straightLaserOverloadDuration = 0.4f;
	[SerializeField] [Range(0.005f,0.05f)]private float straightLaserLaunchInterval   = 0.01f;
	[SerializeField] private float straightTrailWidth = 0.1f;
	[SerializeField] private Material straightLaserMaterial;
	private GameObject straightLaserGeneratorParent;
	//--------------------------------------------------------------------------------------------
	#endregion

	#region ##### レーザー音	 #####
	private AudioSource audioSource;
	[SerializeField] private AudioClip laserBegin;
	[SerializeField] private AudioClip laserContinuing;
	[SerializeField] private AudioClip laserEnd;
	#endregion

	#region ##### 回転装置 #####
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
	#endregion

	#region ##### よそ者 #####
	private EmitterLaunchCore emitterLaunchCore;
	private LaunchDevice straightLaunchDevive;

	private InputManagerObject inputManager1P;
	private InputManagerObject inputManager2P;
	private GameObject parentObj;
	private Bit_Formation_3 bf;
	private string parentname;
	bool isOption;
	#endregion


	private void Awake()
	{
		//初期化

		var straightLaserParent = new GameObject("Device_StrightLaserParent");
		straightLaserParent.transform.SetParent(this.transform);
		straightLaserParent.transform.localPosition = Vector3.zero;
		this.straightLaserGeneratorParent = straightLaserParent;

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


		this.emitterLaunchCore = new EmitterLaunchCore(straightLaunchDevive);

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
		//レーザー装置が非アクティブ状態時実行、余ったレーザーオブジェクトを消す（無理矢理、処理負荷を減らすため）
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

		if (emitterLaunchCore.CurrentDevice.CurrentGenerator != null)
		{
			emitterLaunchCore.CurrentDevice.CurrentGenerator.gameObject.SetActive(false);
			emitterLaunchCore.CurrentDevice.ResetGenerator();
		}
    }
    private void Update()
	{
		//現在使用するレーザー装置取得
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

		//-----------------------------------------------------------------入力 検索----------------------------------------------------------------------
		//発射ボタン/キー　が　押されたとき
		if (Input.GetButtonDown(fireButtonName) || Input.GetKeyDown(firekey))
		{
			//レンダリングオブジェクトを生成
		    emitterLaunchCore.GenerateLine();

			if (audioSource.isPlaying) audioSource.Stop();
			audioSource.clip = laserBegin;
			audioSource.Play();
		}

		//発射ボタン/キー　が　押されているとき
		if (Input.GetButton(fireButtonName) || Input.GetKey(firekey))
		{
			if (launchDevice.CurrentGenerator == null) { emitterLaunchCore.GenerateLine(); }
            else
            { 
			    if (Time.time >= launchDevice.CanLaunchTime)
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
					emitterLaunchCore.LaunchNode();
			    }
            }
		}

		//発射ボタン/キー が　離したとき
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
		//---------------------------------------------------------------------------------------------------------------------------------------------------
		

		//------------------------------------------------------------レーザー発射装置使いすぎ------------------------------------------------------
		if(launchDevice.CurrentGenerator != null)
		{
			if (launchDevice.CurrentGenerator.IsOverLoad)
			{
				launchDevice.ResetGenerator();

				launchDevice.UpdateCanLaunchTime();
			}
			else if(launchDevice.CurrentGenerator.pointCount == straightLaserNodeMax / 3 * 2)
			{
				if (isPlayerUseAudio)
				{
					if (audioSource.isPlaying) audioSource.Stop();
					audioSource.clip = laserEnd;
					audioSource.Play();
				}
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
	}
}

