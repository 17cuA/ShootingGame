using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChenkenLaser;
using UnityEngine;

class Device_LaserEmitter : MonoBehaviour
{
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

		}

		public void GenerateLine()
		{

		}

		public void LaunchPoint()
		{

		}

	}

	/// <summary>
	/// 発射専用部品インタフェース
	/// </summary>
	public interface ILaunchDevice
	{
		ChenkenLaser.Laser CurrentLaser { get; set; }
		 List<ChenkenLaser.Laser> Lasers { get; set; }
		int PointMax { get; }
		int PointCount { get; set; }
		float OverloadDuration { get; }
		float CanLaunchTime { get; set; }
		void LaunchPoint();
		void ResetLaunchDevice();
	}

	/// <summary>
	/// 直線発射部品
	/// </summary>
	public class StraightLaunchDevice : ILaunchDevice
	{
		private int pointMax;
		private float overLoadDuration;

		public int PointMax { get { return pointMax; } }
		public int PointCount { get; set; }
		public float OverloadDuration { get { return overLoadDuration; } }
		public float CanLaunchTime { get; set; }
		public ChenkenLaser.Laser CurrentLaser { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public List<ChenkenLaser.Laser> Lasers { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public void LaunchPoint()
		{
			this.PointCount++;
		}

		public void ResetLaunchDevice()
		{

		}
	}
}

