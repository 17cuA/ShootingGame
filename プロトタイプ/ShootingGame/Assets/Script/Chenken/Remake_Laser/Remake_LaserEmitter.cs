using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Remake_LaserEmitter : MonoBehaviour
{
	[Header("■■■■■　デバッグ設定　■■■■■")]
	[SerializeField] private bool isUseAudio = false;
	[SerializeField] private KeyCode fireKey = KeyCode.Space;
	[SerializeField] private string fireButtonName;

	[Header("■■■■■■■■■　音設定 ■■■■■■■■■")]
	[SerializeField] private AudioClip laserBegin;
	[SerializeField] private AudioClip laserContinuing;
	[SerializeField] private AudioClip laserEnd;
	[SerializeField] [Range(0.3f, 1f)] private float audioCheckLength = 0.6f;
	private AudioSource audioSource;
	
	[Header("■■■■■ Type-1(直線型) ■■■■■")]
	[SerializeField] [Range(10, 70)]    private int s_laserNodeMax = 50;
	[SerializeField] [Range(0, 0.6f)]   private float s_laserOverLoadDuration = 0.4f;
	[SerializeField] [Range(0f, 0.05f)] private float s_laserLaunchInterval = 0.01f;
	[SerializeField] [Range(0.01f, 0.1f)] private float s_laserWidth;
	[SerializeField] private Material s_laserMaterial;
	private GameObject s_laserGeneratorParent;

	[Header("■■■■■ Type-2(曲線型) ■■■■■")]
	[SerializeField] [Range(10, 100)]  private int r_laserNodeMax = 50;
	[SerializeField] [Range(0, 0.6f)]  private float r_laserOverloadDuration = 0.4f;
	[SerializeField] [Range(0, 0.05f)] private float r_laserLaunchInterval = 0.01f;
	[SerializeField] [Range(0.01f, 0.1f)] private float r_laserWidth;
	[SerializeField] private Material r_laserMaterial;
	private GameObject r_laserGeneratorParent;

	//■■■■■■■■■ 魔物が潜り込んでいます■■■■■■■■■■
	private GameObject parentObj;
	public string parentname;
	private Bit_Formation_3 bitFormation;
	private bool isOption;
	private InputManagerObject inputManager1P;
	private InputManagerObject inputManager2P;
	//■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■

	[SerializeField] private bool isType1;
	[SerializeField] private bool isType2;

	//回転
	private float theta;
	private Vector3 pushPosition;

	private float currentOverloadDuration;
	private float currentLaunchInterval;
	private float currentCanLaunchTime;
	private GameObject currentGeneratorParent;
	private Instance_Laser_Node_Generator currentGenertor;
	private List<Instance_Laser_Node_Generator> currentGenerators = new List<Instance_Laser_Node_Generator>();

	private void Awake()
	{
		//レーサータイプ設定
		if (isType1)
		{
			var type1LaserParent = new GameObject("Type1_Laser");
			type1LaserParent.transform.SetParent(this.transform);
			type1LaserParent.transform.localPosition = Vector3.zero;
			this.s_laserGeneratorParent = type1LaserParent;

			Type1LaserSetting(type1LaserParent);
		}
		if(isType2)
		{
			var type2LaserParent = new GameObject("Type2_Laser");
			type2LaserParent.transform.SetParent(this.transform);
			type2LaserParent.transform.localPosition = Vector3.zero;
			this.r_laserGeneratorParent = type2LaserParent;

			Type2LaserSetting(type2LaserParent);
		}

		inputManager1P = GameObject.Find("InputManager_1P").GetComponent<InputManagerObject>();
		inputManager2P = GameObject.Find("InputManager_2P").GetComponent<InputManagerObject>();

		//メディア設定
		if(this.isUseAudio)
		{
			this.audioSource = GetComponent<AudioSource>();
		}

		//■■■■■■■■■ 勇者設定 ■■■■■■■■■■
		this.parentObj = transform.parent.gameObject;
		this.parentname = parentObj.name;
		if(parentObj.name == "Option(Clone)")
		{
			this.bitFormation = parentObj.GetComponent<Bit_Formation_3>();
			this.isOption = true;
		}
		else if(parentObj.name == "Player(Clone)")
		{
			fireButtonName = inputManager1P.Manager.Button["Shot"];
			isOption = false;
		}
		else if(parentObj.name == "Player2(Clone)")
		{
			fireButtonName = inputManager2P.Manager.Button["Shot"];
			isOption = false;
		}
		//■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
	}

	private void OnDisable()
	{
		//非アクティブ状態にレーサーラインプーリングオブジェクト消す処理（軽くする為）
		if(Obj_Storage.Storage_Data.Laser_Line != null)
		{
			for(var i = 29; i < Obj_Storage.Storage_Data.Laser_Line.Get_Obj().Count; ++i)
			{
				if (Obj_Storage.Storage_Data.Laser_Line.Get_Obj()[i].gameObject != null)
				{
					if(!Obj_Storage.Storage_Data.Laser_Line.Get_Obj()[i].gameObject.activeSelf)
					{
						Destroy(Obj_Storage.Storage_Data.Laser_Line.Get_Obj()[i]);
						Obj_Storage.Storage_Data.Laser_Line.Get_Obj().RemoveAt(i);
					}
				}
			}
		}
	}

	private void Update()
	{
		if(isOption)
		{
			if(bitFormation.bState == Bit_Formation_3.BitState.Player1)
			{
				fireButtonName = inputManager1P.Manager.Button["Shot"];
			}
			else if(bitFormation.bState == Bit_Formation_3.BitState.Player2)
			{
				fireButtonName = inputManager2P.Manager.Button["Shot"];
			}
		}

		//■■■■■■■■■ ボタンチェック ■■■■■■■■■■
		if(Input.GetButtonDown(fireButtonName) || Input.GetKeyDown(fireKey))
		{
			//ここはレーサー♪
			if(audioSource.isPlaying)
			{
				audioSource.Stop();
			}
			audioSource.clip = laserBegin;
			audioSource.Play();

			//レーサーをレンダリングする親を生成（レーサーの見た目
			if(isType1)
			{
				GenerateLaserRenderingParent(s_laserWidth,s_laserMaterial, s_laserNodeMax);
			}
			else if(isType2)
			{
				GenerateLaserRenderingParent(r_laserWidth,r_laserMaterial, r_laserNodeMax);
			}
		}

		if(Input.GetButton(fireButtonName) || Input.GetKey(fireKey))
		{
			if(currentGenertor == null)
			{
				if (isType1)
				{
					GenerateLaserRenderingParent(s_laserWidth, s_laserMaterial, s_laserNodeMax);
				}
				else if (isType2)
				{
					GenerateLaserRenderingParent(r_laserWidth, r_laserMaterial, r_laserNodeMax);
				}
			}
			else
			{
				if(Time.time >= currentCanLaunchTime)
				{
					if(isUseAudio)
					{
						if(audioSource.time >= laserBegin.length * audioCheckLength && audioSource.clip == laserBegin)
						{
							audioSource.clip = laserContinuing;
							audioSource.loop = true;
							audioSource.Play();
						}
						if(audioSource.clip == laserEnd && audioSource.time >= laserEnd.length * audioCheckLength)
						{
							if(audioSource.isPlaying)
							{
								audioSource.Stop();
							}
							audioSource.clip = laserBegin;
							audioSource.Play();
						}
					}

					//
					if (isType1)
					{
						LaunchNode(true); 
					}
					else if (isType2)
					{
						LaunchNode(false);
					}
				}
			}
		}

		if(Input.GetButtonUp(fireButtonName) || Input.GetKeyUp(fireKey))
		{
			if(currentGenertor != null)
			{
				currentGenertor.IsFixed = false;
				currentGenertor = null;
			}

			if(isUseAudio)
			{
				if(audioSource.isPlaying)
				{
					audioSource.Stop();
				}
				audioSource.clip = laserEnd;
				audioSource.loop = false;
				audioSource.Play();
			}
		}
	}

	private void Type1LaserSetting(GameObject genertorParent)
	{
		this.currentOverloadDuration = s_laserOverLoadDuration;
		this.currentLaunchInterval = s_laserLaunchInterval;
		this.currentCanLaunchTime = 0f;

		this.currentGeneratorParent = genertorParent;
	}


	private void Type2LaserSetting(GameObject generatorParent)
	{
		this.currentOverloadDuration = r_laserOverloadDuration;
		this.currentLaunchInterval = r_laserLaunchInterval;
		this.currentCanLaunchTime = 0f;

		this.currentGeneratorParent = generatorParent;
	}

	private void GenerateLaserRenderingParent(float lineWidth, Material material,int pointMax)
	{
		for(var i = 0; i < currentGenerators.Count; ++i)
		{
			if(!currentGenerators[i].gameObject.activeSelf)
			{
				currentGenertor = currentGenerators[i];
				currentGenertor.ResetLineRenderer();
				currentGenertor.IsFixed = true;
				currentGenertor.gameObject.SetActive(true);

				return;
			}
		}

		var generatorGo = new GameObject("Generator");
		var generator = generatorGo.AddComponent<Instance_Laser_Node_Generator>();

		generator.Setting(lineWidth,material,pointMax);
		generator.IsFixed = true;

		generatorGo.transform.SetParent(currentGeneratorParent.transform);
		generatorGo.transform.localPosition = Vector3.zero;

		currentGenertor = generator;
		currentGenerators.Add(currentGenertor);
	}

	private void LaunchNode(bool isRotate)
	{
		currentGenertor.LaunchNode(isRotate);
		currentCanLaunchTime = Time.time + currentLaunchInterval;
	}
}
