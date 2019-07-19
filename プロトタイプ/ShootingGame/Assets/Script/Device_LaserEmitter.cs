using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Device_LaserEmitter : MonoBehaviour
{
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

	public class EmitterLaunchCore
	{
		public ILaunchDevice currentLaunchDevice;
		public ChenkenLaser.Laser currentLaser;
		public List<ChenkenLaser.Laser> lasers;

		public EmitterLaunchCore(ILaunchDevice defaultLaunchDevice)
		{
			this.currentLaunchDevice = defaultLaunchDevice;
			this.currentLaser = null;
			this.lasers = new List<ChenkenLaser.Laser>();
		}

		public void SetDevice(ILaunchDevice newLaunchDevice)
		{

		}

		public void GenerateLine()
		{

		}

		public void LaunchPoint()
		{

		}

	}

	public interface ILaunchDevice
	{
		int PointMax { get; }
		int PointCount { get; set; }
		float OverloadDuration { get; }
		float CanLaunchTime { get; set; }
		void LaunchPoint();
		void ResetLaunchDevice();
	}

	public class StraightLaunchDevice : ILaunchDevice
	{
		private int pointMax;
		private float overLoadDuration;

		public int PointMax { get { return pointMax; } }
		public int PointCount { get; set; }
		public float OverloadDuration { get { return overLoadDuration; } }
		public float CanLaunchTime { get; set; }

		public void LaunchPoint()
		{

		}

		public void ResetLaunchDevice()
		{

		}
	}
}

