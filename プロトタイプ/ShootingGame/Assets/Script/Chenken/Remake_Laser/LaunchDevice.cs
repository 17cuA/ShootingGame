using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DeviceType
{
	TYPE_1_STRAIGHT,
	TYPE_2_ROTATE,
}
public abstract class LaunchDevice
{
	public abstract DeviceType Type { get; }
	public abstract Instance_Laser_Node_Generator CurrentGenerator { get; }
	public abstract float CanLaunchTime { get; }
	public abstract void GenerateLine();
	public abstract void LaunchNode();
	public abstract void ResetGenerator();
	public abstract void UpdateCanLaunchTime();
	public abstract void Reset();
}

public class StraightLaunchDevive : LaunchDevice
{
	private float overloadDuration;
	private float launchInterval;
	private float canLaunchTime;
	private float laserWidth;
	private Material laserMaterial;
	private int pointMax;
	private GameObject emitterInstance;

	private Instance_Laser_Node_Generator currentGenerator;
	private List<Instance_Laser_Node_Generator> generators;

	public StraightLaunchDevive(float overloadDuration, float launchInterval,float laserWidth,Material laserMaterial, int pointMax, GameObject emitterInstance)
	{
		this.overloadDuration = overloadDuration;
		this.launchInterval = launchInterval;
		this.canLaunchTime = 0f;
		this.laserWidth = laserWidth;
		this.laserMaterial = laserMaterial;
		this.pointMax = pointMax;
		this.emitterInstance = emitterInstance;
		this.currentGenerator = null;
		this.generators = new List<Instance_Laser_Node_Generator>();
	}

	public override DeviceType Type
	{
		get
		{
			return DeviceType.TYPE_1_STRAIGHT;
		}
	}

	public override Instance_Laser_Node_Generator CurrentGenerator
	{
		get
		{
			return currentGenerator;
		}
	}

	public override float CanLaunchTime
	{
		get
		{
			return canLaunchTime;
		}
	}

	public override void GenerateLine()
	{
		for(var i = 0; i < generators.Count; ++i)
		{
			if(!this.generators[i].gameObject.activeSelf)
			{
				currentGenerator = generators[i];
				currentGenerator.ResetLineRenderer();
				currentGenerator.IsFixed = true;
				currentGenerator.gameObject.SetActive(true);
				return;
			}
		}

		var generatorGo = new GameObject("Generator");
		var generator = generatorGo.AddComponent<Instance_Laser_Node_Generator>();

		generator.Setting(laserWidth, laserMaterial, pointMax);
		generator.IsFixed = true;

		generatorGo.transform.SetParent(emitterInstance.transform);
		generatorGo.transform.localPosition = Vector3.zero;

		currentGenerator = generator;
		generators.Add(currentGenerator);
	}

	public override void LaunchNode()
	{
		this.currentGenerator.LaunchNode(false);
		canLaunchTime = Time.time + launchInterval;
	}

	public override void ResetGenerator()
	{
		currentGenerator.IsFixed = false;
		currentGenerator = null;
	}

	public override void UpdateCanLaunchTime()
	{
		canLaunchTime = Time.time + overloadDuration;
	}

	public override void Reset()
	{
		canLaunchTime = 0;
		currentGenerator.ResetGenerator();
		currentGenerator.ResetLineRenderer();
		currentGenerator.gameObject.SetActive(false);
		currentGenerator = null;
		for(var i = 0; i < generators.Count; ++i)
		{
			GameObject.Destroy(generators[i].gameObject);
		}

		generators.Clear();

	}
}

public class RotateLaunchDevice : LaunchDevice
{
	private float overloadDuration;
	private float launchInterval;
	private float canLaunchTime;
	private float laserWidth;
	private Material laserMaterial;
	private int pointMax;
	private GameObject emitterInstance;

	private Instance_Laser_Node_Generator currentGenerator;
	private List<Instance_Laser_Node_Generator> generators;

	public RotateLaunchDevice(float overloadDuration, float launchInterval, float laserWidth, Material laserMaterial, int pointMax, GameObject emitterInstance)
	{
		this.overloadDuration = overloadDuration;
		this.launchInterval = launchInterval;
		this.canLaunchTime = 0f;
		this.laserWidth = laserWidth;
		this.laserMaterial = laserMaterial;
		this.pointMax = pointMax;
		this.emitterInstance = emitterInstance;
		this.currentGenerator = null;
		this.generators = new List<Instance_Laser_Node_Generator>();
	}

	public override DeviceType Type
	{
		get
		{
			return DeviceType.TYPE_2_ROTATE;
		}
	}

	public override Instance_Laser_Node_Generator CurrentGenerator
	{
		get
		{
			return currentGenerator;
		}
	}

	public override float CanLaunchTime
	{
		get
		{
			return canLaunchTime;
		}
	}

	public override void GenerateLine()
	{
		for (var i = 0; i < generators.Count; ++i)
		{
			if (!this.generators[i].gameObject.activeSelf)
			{
				currentGenerator = generators[i];
				currentGenerator.ResetLineRenderer();
				currentGenerator.IsFixed = false;
				currentGenerator.gameObject.SetActive(true);
				return;
			}
		}

		var generatorGo = new GameObject("Generator");
		var generator = generatorGo.AddComponent<Instance_Laser_Node_Generator>();

		generator.Setting(laserWidth, laserMaterial, pointMax);
		generator.IsFixed = false;

		generatorGo.transform.SetParent(emitterInstance.transform);
		generatorGo.transform.localPosition = Vector3.zero;

		currentGenerator = generator;
		generators.Add(currentGenerator);
	}

	public override void LaunchNode()
	{
		this.currentGenerator.LaunchNode(true);
		canLaunchTime = Time.time + launchInterval;
	}

	public override void ResetGenerator()
	{
		currentGenerator.IsFixed = false;
		currentGenerator = null;
	}

	public override void UpdateCanLaunchTime()
	{
		canLaunchTime = Time.time + overloadDuration;
	}

	public override void Reset()
	{
		canLaunchTime = 0;
		currentGenerator = null;
		for (var i = 0; i < generators.Count; ++i)
		{
			GameObject.Destroy(generators[i].gameObject);
		}

		generators.Clear();

	}
}
