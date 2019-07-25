using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

namespace Laser
{
	public class LaserEmitter : MonoBehaviour
	{
		[Header("----------タイプ1----------")]
		[SerializeField] private float s_laserSpd;
		[SerializeField] private float s_laserWidth;
		[SerializeField] private int s_laserNodeMax;
		[SerializeField] private float s_laserOverloadDuration;
		[SerializeField] private float s_laserLaunchInterval;
		[SerializeField] private float s_laserTrailWidth;
		[SerializeField] private Material s_laserLineMaterial;
		[SerializeField] private Material s_laserTrailMaterial;

		[Header("----------タイプ2----------")]
		[SerializeField] private float r_laserSpd;
		[SerializeField] private float r_laserWidth;
		[SerializeField] private int r_laserNodeMax;
		[SerializeField] private float r_laserOverloadDuration;
		[SerializeField] private float r_laserLaunchInterval;
		[SerializeField] private float r_laserTrailWidth;
		[SerializeField] private Material r_laserLineMaterial;
		[SerializeField] private Material r_laserTrailMaterial;

		private List<LaserNode> laserNodes;
		private RotateCore rotateDevice;
		private LaunchCore currentLaunchDevice;

		private LaunchCore s_laser_launch_device;
		private LaunchCore r_laser_launch_device;

		private void Awake()
		{
			this.s_laser_launch_device = new LaunchCore(s_laserSpd, s_laserWidth, s_laserOverloadDuration, 
				                                        s_laserLaunchInterval, s_laserLineMaterial, s_laserTrailMaterial);

			this.r_laser_launch_device = new LaunchCore(r_laserSpd, r_laserWidth, r_laserOverloadDuration,
				                                        r_laserLaunchInterval, r_laserLineMaterial, r_laserTrailMaterial);

			

			this.laserNodes = new List<LaserNode>();
		}
	}

	/// <summary>
	/// 回転装置
	/// </summary>
	public class RotateCore
	{
		private float theta;
		private Vector3 pushPos;

		/// <summary>
		/// セットアップ
		/// </summary>
		/// <param name="pushPos"> 発射位置 </param>
		public RotateCore(Vector3 pushPos)
		{
			this.pushPos = pushPos;
			this.theta = 0;
		}

		/// <summary>
		/// 回転
		/// </summary>
		/// <param name="angle"></param>
		public void Rotate(float angle)
		{
			this.theta += angle;
			this.pushPos.x = Mathf.Cos(this.theta);
			this.pushPos.x = Mathf.Sin(this.theta);
		}
	}

	/// <summary>
	/// 発射装置
	/// </summary>
	public class LaunchCore
	{
		private float laserSpd;
		private float laserWidth;
		private float overloadDuration;
		private float launchInterval;
		private float canLaunchTime;
		private Material laserlineMaterial;
		private Material laserTrailMaterial;
		
		public LaunchCore(float laserSpd, float laserWidth, float overloadDuration, float launchInterval, Material laserlineMaterial, Material laserTrailMaterial)
		{
			this.laserSpd = laserSpd;
			this.laserWidth = laserWidth;
			this.overloadDuration = overloadDuration;
			this.launchInterval = launchInterval;
			this.laserlineMaterial = laserlineMaterial;
			this.laserTrailMaterial = laserTrailMaterial;
		}

		public void Launch(bool needFixed)
		{

		}
	}
}
