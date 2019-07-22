using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChenkenLaser;
using UnityEngine;

 [DefaultExecutionOrder(599)]
class Device_LaserEmitter : MonoBehaviour
{
	[SerializeField] private float straightLaserShotSpeed = 0.8f;
	[SerializeField] private float straightLaserWidth = 0.2f;
	[SerializeField] private Material straightLaserMaterial;
	[SerializeField] private int straightLaserNodeMax = 50;
	[SerializeField] private float straightLaserOverloadDuration = 0.4f;
	[SerializeField] private float straightLaserLaunchInterval = 0.1f;
	private GameObject straightLaserGeneratorParent;


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

		public void LaunchNode()
		{
			currentLaunchDevice.LaunchNode();
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
		void LaunchNode();
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
			Debug.Log("LaserGeneratorアクティブ");
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

		public void LaunchNode()
		{
			this.CurrentGenerator.LaunchNode();
			this.CanLaunchTime = Time.time + LaunchInterval;
		}
	}

	private EmitterRotateCore emitterRotateCore;
	private EmitterLaunchCore emitterLaunchCore;

	private void Awake()
	{
		var straightLaserParent = new GameObject("Device_StrightLaserParent");
		straightLaserParent.transform.SetParent(this.transform);
		straightLaserParent.transform.localPosition = Vector3.zero;
		this.straightLaserGeneratorParent = straightLaserParent;


		this.emitterRotateCore = new EmitterRotateCore(this.transform.position);
		this.emitterLaunchCore = new EmitterLaunchCore(new StraightLaunchDevice(this.straightLaserOverloadDuration, this.straightLaserLaunchInterval, this.straightLaserGeneratorParent));
	}

	private void Update()
	{
		var launchDevice = emitterLaunchCore.currentLaunchDevice;

		//-----------------------------------------------------------------入力 検索----------------------------------------------------------------------
		if(Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
		{
			if(launchDevice is StraightLaunchDevice)
				this.emitterLaunchCore.GenerateLine(straightLaserShotSpeed,straightLaserWidth,straightLaserMaterial,straightLaserNodeMax);
		}

		if(Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space))
		{
			if(launchDevice.CurrentGenerator == null)
			{
				this.emitterLaunchCore.GenerateLine(straightLaserShotSpeed,straightLaserWidth,straightLaserMaterial,straightLaserNodeMax);
			}

			if(Time.time > launchDevice.CanLaunchTime && launchDevice.CurrentGenerator != null)
			{
				this.emitterLaunchCore.LaunchNode();
			}
		}

		if(Input.GetButtonUp("Fire1") || Input.GetKeyUp(KeyCode.Space) && launchDevice.CurrentGenerator != null)
		{
			launchDevice.CurrentGenerator.IsFixed = false;
			launchDevice.CurrentGenerator = null;
		}
		//---------------------------------------------------------------------------------------------------------------------------------------------------

		if(launchDevice.CurrentGenerator != null && launchDevice.CurrentGenerator.IsOverLoad)
		{
			launchDevice.CurrentGenerator.ResetGenerator();

			launchDevice.CurrentGenerator.IsFixed = false;
			launchDevice.CurrentGenerator = null;

			launchDevice.CanLaunchTime = Time.time + launchDevice.OverloadDuration;		
		}

		if(!this.transform.parent.gameObject.activeSelf && launchDevice.CurrentGenerator != null)
		{
			launchDevice.CurrentGenerator.IsFixed = false;
			launchDevice.CurrentGenerator = null;
		}
	}
}

