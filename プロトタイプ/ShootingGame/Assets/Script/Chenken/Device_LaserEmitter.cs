using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChenkenLaser;
using UnityEngine;

 [DefaultExecutionOrder(599)]
class Device_LaserEmitter : MonoBehaviour
{
	public bool isClose = false;
	//--------------------------- 直線型レーザー（タイプ１） ----------------------------------
	[Header("--------直線型レーザー--------")]
	[SerializeField] [Range(0.5f,1.2f)]   private float straightLaserShotSpeed        = 0.8f;
	[SerializeField] [Range(0.1f,0.025f)] private float straightLaserWidth            = 0.05f;
	[SerializeField] [Range(20,70)]       private int straightLaserNodeMax            = 50;
	[SerializeField] [Range(0.2f,0.6f)]   private float straightLaserOverloadDuration = 0.4f;
	[SerializeField] [Range(0.005f,0.05f)]private float straightLaserLaunchInterval   = 0.01f;
	[SerializeField] private float straightTrailWidth = 0.1f;
	[SerializeField] private Material straightLaserMaterial;
	private GameObject straightLaserGeneratorParent;

	//---------------------------曲線型レーザー（タイプ２）------------------------------------
	[Header("--------曲線型レーザー--------")]
	[SerializeField] private float rotateLaserShotSpeed = 0.8f;
	[SerializeField] private float rotateLaserWidth     = 0.05f;
	[SerializeField] private int rotateLaserNodeMax     = 60;
	[SerializeField] private float rotateLaserOverloadDuration = 0.3f;
	[SerializeField] private float rotateLaserLaunchInterval   = 0.01f;
	[SerializeField] private float rotateTrailWidth = 0.1f;
	[SerializeField] private Material rotateLaserMaterial;
	private GameObject rotateLaserGeneratorParent;

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

	/// <summary>
	/// 発射装置
	/// </summary>
	public class EmitterLaunchCore
	{
		public ILaunchDevice currentLaunchDevice;

		public EmitterLaunchCore(ILaunchDevice defaultLaunchDevice)
		{
			this.currentLaunchDevice = defaultLaunchDevice;
		}

		public void SetDevice(ILaunchDevice newLaunchDevice)
		{
			this.currentLaunchDevice = newLaunchDevice;
		}

		public void GenerateLine(float laserShotSpeed, float laserWidth, Material laserMaterial, int pointMax)
		{
			currentLaunchDevice.GenerateLine(laserShotSpeed, laserWidth, laserMaterial, pointMax);
		}

		public void LaunchNode(float trailWidth)
		{
			currentLaunchDevice.LaunchNode(trailWidth);
		}

	}

	/// <summary>
	/// 発射専用部品インタフェース
	/// </summary>
	public interface ILaunchDevice
	{
		Instance_Laser_Node_Generator CurrentGenerator { get; set; }
		List<Instance_Laser_Node_Generator> generators { get; set; }

		float OverloadDuration          { get; set; }
		float LaunchInterval            { get; set; }
		float CanLaunchTime             { get; set; }
		GameObject EmitterInstance      { get; set; }

		void GenerateLine(float laserShotSpeed, float laserWidth, Material laserMaterial,int pointMax);
		void LaunchNode(float trailWidth);
	}

	/// <summary>
	/// 直線発射部品
	/// </summary>
	public class StraightLaunchDevice : ILaunchDevice
	{
		public float OverloadDuration          { get; set; }
		public float LaunchInterval            { get; set; }
		public float CanLaunchTime             { get; set; }
		public GameObject EmitterInstance      { get; set; }

		public Instance_Laser_Node_Generator CurrentGenerator { get; set; }
		public List<Instance_Laser_Node_Generator> generators { get; set; }

		public StraightLaunchDevice(float overloadDuration,float launchInterval, GameObject emitterInstance)
		{
			this.OverloadDuration = overloadDuration;
			this.LaunchInterval   = launchInterval;
			this.CanLaunchTime    = 0f;
			this.EmitterInstance  = emitterInstance;

			this.CurrentGenerator     = null;
			this.generators           = new List<Instance_Laser_Node_Generator>();
		}

		public void GenerateLine(float laserShotSpeed, float laserWidth, Material laserMaterial, int pointMax)
		{
			Debug.Log("StraightLaserGeneratorアクティブ");
			for(var i = 0; i < this.generators.Count; ++i)
			{
				if(!this.generators[i].gameObject.activeSelf)
				{
					this.CurrentGenerator = this.generators[i];
					this.CurrentGenerator.ResetLineRenderer();
					this.CurrentGenerator.IsFixed = true;
					this.CurrentGenerator.gameObject.SetActive(true);
					return;
				}
			}

			var generatorGo = new GameObject("Generator");
			var generator = generatorGo.AddComponent<Instance_Laser_Node_Generator>();

			generator.Setting(laserShotSpeed, laserWidth, laserMaterial,pointMax);
			generator.IsFixed = true;

			generatorGo.transform.SetParent(EmitterInstance.transform);
			generatorGo.transform.localPosition = Vector3.zero;

			this.CurrentGenerator = generator;
			this.generators.Add(generator);
		}

		public void LaunchNode(float trailWidth)
		{
			this.CurrentGenerator.LaunchNode(trailWidth,false);
			this.CanLaunchTime = Time.time + LaunchInterval;
		}
	}

	public class RotateLaunchDevice : ILaunchDevice
	{
		public Instance_Laser_Node_Generator CurrentGenerator { get; set; }
		public List<Instance_Laser_Node_Generator> generators { get; set; }
		public float OverloadDuration     { get; set; }
		public float LaunchInterval       { get; set; }
		public float CanLaunchTime        { get; set; }
		public GameObject EmitterInstance { get; set;}

		public RotateLaunchDevice(float overloadDuration, float launchInterval,GameObject emitterInstance)
		{
			this.OverloadDuration = overloadDuration;
			this.LaunchInterval   = launchInterval;
			this.CanLaunchTime    = 0f;
			this.EmitterInstance  = emitterInstance;

			this.CurrentGenerator = null;
			this.generators       = new List<Instance_Laser_Node_Generator>();
		}

		public void GenerateLine(float laserShotSpeed, float laserWidth, Material laserMaterial, int pointMax)
		{
			Debug.Log("RotateLaserGeneratorアクティブ");
			for(var i = 0; i < this.generators.Count; ++i)
			{
				if(!this.generators[i].gameObject.activeSelf)
				{
					this.CurrentGenerator = this.generators[i];
					this.CurrentGenerator.ResetLineRenderer();
					this.CurrentGenerator.Setting(laserShotSpeed, laserWidth, laserMaterial, pointMax);
					this.CurrentGenerator.IsFixed = false;
					this.CurrentGenerator.gameObject.SetActive(true);
					return;
				}
			}

			var generatorGo = new GameObject("Generator");
			var generator = generatorGo.AddComponent<Instance_Laser_Node_Generator>();


			generatorGo.transform.SetParent(EmitterInstance.transform);
			generatorGo.transform.localPosition = Vector3.zero;

			generator.IsFixed = false;
			generator.Setting(laserShotSpeed, laserWidth, laserMaterial, pointMax);



			this.CurrentGenerator = generator;
			this.generators.Add(generator);
		}

		public void LaunchNode(float trailWidth)
		{
			this.CurrentGenerator.LaunchNode(trailWidth,true);
			this.CanLaunchTime = Time.time + LaunchInterval;
		}
	}

	private EmitterRotateCore emitterRotateCore;
	private EmitterLaunchCore emitterLaunchCore;


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

		var rotateLaserParent = new GameObject("Device_RotateLaserParnt");
		rotateLaserParent.transform.SetParent(this.transform);
		rotateLaserParent.transform.localPosition = Vector3.zero;
		this.rotateLaserGeneratorParent = rotateLaserParent;

		this.emitterRotateCore = new EmitterRotateCore(this.transform.parent.position);
		this.emitterLaunchCore = new EmitterLaunchCore(new StraightLaunchDevice(this.straightLaserOverloadDuration, this.straightLaserLaunchInterval, this.straightLaserGeneratorParent));
	}

	private void Update()
	{
		var launchDevice = emitterLaunchCore.currentLaunchDevice;
		if (this.isClose)
		{
			if(launchDevice is StraightLaunchDevice)
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
			if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
			{
				if (launchDevice is StraightLaunchDevice)
					this.emitterLaunchCore.GenerateLine(straightLaserShotSpeed, straightLaserWidth, straightLaserMaterial, straightLaserNodeMax);
				else
					this.emitterLaunchCore.GenerateLine(rotateLaserShotSpeed, rotateLaserWidth, rotateLaserMaterial, rotateLaserNodeMax);
			}

			if (Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space))
			{
				if (launchDevice.CurrentGenerator == null)
				{
					if (launchDevice is StraightLaunchDevice)
						this.emitterLaunchCore.GenerateLine(straightLaserShotSpeed, straightLaserWidth, straightLaserMaterial, straightLaserNodeMax);
					else
						this.emitterLaunchCore.GenerateLine(rotateLaserShotSpeed, rotateLaserWidth, rotateLaserMaterial, rotateLaserNodeMax);
				}

				if (Time.time >= launchDevice.CanLaunchTime && launchDevice.CurrentGenerator != null)
				{
					if (launchDevice is StraightLaunchDevice)
						this.emitterLaunchCore.LaunchNode(straightTrailWidth);
					else
						this.emitterLaunchCore.LaunchNode(rotateTrailWidth);
				}
			}

			if (Input.GetButtonUp("Fire1") || Input.GetKeyUp(KeyCode.Space) && launchDevice.CurrentGenerator != null)
			{
				launchDevice.CurrentGenerator.IsFixed = false;
				launchDevice.CurrentGenerator = null;
			}
		}
		//---------------------------------------------------------------------------------------------------------------------------------------------------
		

		//------------------------------------------------------------レーザー発射装置使いすぎ------------------------------------------------------
		if(launchDevice.CurrentGenerator != null && launchDevice.CurrentGenerator.IsOverLoad)
		{
			launchDevice.CurrentGenerator.IsFixed = false;
			launchDevice.CurrentGenerator = null;

			launchDevice.CanLaunchTime = Time.time + launchDevice.OverloadDuration;		
		}
		//------------------------------------------------------------------------------------------------------------------------------------------------



		if(!this.transform.parent.gameObject.activeSelf && launchDevice.CurrentGenerator != null)
		{
			launchDevice.CurrentGenerator.IsFixed = false;
			launchDevice.CurrentGenerator = null;
		}

		if(Input.GetKeyDown(KeyCode.Z))
		{
			if (emitterLaunchCore.currentLaunchDevice is StraightLaunchDevice)
				emitterLaunchCore.SetDevice(new RotateLaunchDevice(this.rotateLaserOverloadDuration, this.rotateLaserLaunchInterval, this.rotateLaserGeneratorParent));
			else if (emitterLaunchCore.currentLaunchDevice is RotateLaunchDevice)
			{
				emitterRotateCore.Reset();
				emitterLaunchCore.SetDevice(new StraightLaunchDevice(this.straightLaserOverloadDuration, this.straightLaserLaunchInterval, this.straightLaserGeneratorParent));
			}
		}

		if(Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.LeftShift) && launchDevice.CurrentGenerator != null)
		{
			launchDevice.CurrentGenerator.IsFixed = false;
			launchDevice.CurrentGenerator = null;
		}

		if(emitterLaunchCore.currentLaunchDevice is RotateLaunchDevice && Input.GetKey(KeyCode.LeftShift))
		{
			//this.emitterRotateCore.Rotate(Mathf.PI / 12 * Mathf.Deg2Rad);
			this.transform.gameObject.transform.Rotate(Vector3.forward * Time.deltaTime * 250);
			
			for(var i = 0;i < emitterLaunchCore.currentLaunchDevice.CurrentGenerator?.nodes.Count; ++i)
			{
				emitterLaunchCore.currentLaunchDevice.CurrentGenerator.nodes[i].GetComponent<LaserLine>().ischangLaserWidth = true;
			}
		}

		if (emitterLaunchCore.currentLaunchDevice is RotateLaunchDevice && Input.GetKey(KeyCode.LeftControl))
		{
			//this.emitterRotateCore.Rotate(Mathf.PI / 12 * Mathf.Deg2Rad);
			this.transform.gameObject.transform.Rotate(-Vector3.forward * Time.deltaTime * 250);

			for (var i = 0; i < emitterLaunchCore.currentLaunchDevice.CurrentGenerator?.nodes.Count; ++i)
			{
				emitterLaunchCore.currentLaunchDevice.CurrentGenerator.nodes[i].GetComponent<LaserLine>().ischangLaserWidth = true;
			}
		}

		if (emitterLaunchCore.currentLaunchDevice is StraightLaunchDevice)
		{
			this.transform.gameObject.transform.localEulerAngles = Vector3.zero;
		}

		if(Input.GetKeyDown(KeyCode.Q))
		{
			this.isClose = !this.isClose;
		}
	}
}

