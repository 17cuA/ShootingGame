using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

namespace Laser
{
	public enum LaserType
	{
		TYPE1,
		TYPE2,
	}

	public class LaserEmitter : MonoBehaviour
	{
		[Header("DEBUGチェック")]
		public bool isDebug;

		[Header("レーザータイプ設定")]
		public LaserType selectLaserType;

		[Header("----------タイプ1----------")]
		public float s_laserSpd;
		public float s_laserWidth;
		public int s_laserNodeMax;
		public float s_laserOverloadDuration;
		public float s_laserLaunchInterval;
		public float s_laserTrailWidth;
		public Material s_laserLineMaterial;
		public Material s_laserTrailMaterial;
		public LaserType s_laserType;
		public int s_laserMax;

		[Header("----------タイプ2----------")]
		public float r_laserSpd;
		public float r_laserWidth;
		public int r_laserNodeMax;
		public float r_laserOverloadDuration;
		public float r_laserLaunchInterval;
		public float r_laserTrailWidth;
		public Material r_laserLineMaterial;
		public Material r_laserTrailMaterial;
		public LaserType r_laserType;
		public int r_laserMax;

		private RotateCore rotateDevice;
		private LaunchCore currentLaunchDevice;

		private LaunchCore s_laser_launch_device;
		private LaunchCore r_laser_launch_device;

		private void Awake()
		{
			this.s_laser_launch_device = new LaunchCore(s_laserType,transform.position,s_laserMax, s_laserSpd, s_laserWidth, s_laserOverloadDuration, 
				                                        s_laserLaunchInterval, s_laserLineMaterial,s_laserTrailWidth, s_laserTrailMaterial);

			this.r_laser_launch_device = new LaunchCore(r_laserType,transform.position,r_laserMax, r_laserSpd, r_laserWidth, r_laserOverloadDuration,
				                                        r_laserLaunchInterval, r_laserLineMaterial,r_laserTrailWidth, r_laserTrailMaterial);

			//選択によって発射装置を設定される
			switch(this.selectLaserType)
			{
				case LaserType.TYPE1: this.currentLaunchDevice = this.s_laser_launch_device; break;
				case LaserType.TYPE2: this.currentLaunchDevice = this.r_laser_launch_device; break;
			}
		}

		private void Update()
		{
			if (isDebug)
			{
				currentLaunchDevice.Update(true);
			}
			else
			{
				currentLaunchDevice.Update(false);
			}
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
		private LaserType laserType;
		private Vector3 laserLaunchPos;
		private float laserSpd;
		private float laserWidth;
		private float overloadDuration;
		private float launchInterval;
		private float canLaunchTime;
		private Material laserlineMaterial;
		private float laserTrailWidth;
		private Material laserTrailMaterial;

		private int laserMax;
		private int laserCount;
		public bool IsOverloading
		{
			get
			{
				return this.laserCount > this.laserMax;
			}
		}
		public bool CanLaunching
		{
			get
			{
				return Time.time >= this.canLaunchTime;
			}
		}
		private Dictionary<int, List<LaserNode>> laserNodes;
		private int laserIndex;

		public LaunchCore(LaserType laserType,Vector3 laserLaunchPos, int laserMax, float laserSpd, float laserWidth, float overloadDuration, float launchInterval, Material laserlineMaterial,float laserTrailWidth, Material laserTrailMaterial)
		{
			this.laserType = laserType;
			this.laserLaunchPos = laserLaunchPos;
			this.laserMax = laserMax;
			this.laserSpd = laserSpd;
			this.laserWidth = laserWidth;
			this.overloadDuration = overloadDuration;
			this.launchInterval = launchInterval;
			this.laserlineMaterial = laserlineMaterial;
			this.laserTrailWidth = laserTrailWidth;
			this.laserTrailMaterial = laserTrailMaterial;
			this.laserNodes = new Dictionary<int, List<LaserNode>>();
		}

		/// <summary>
		/// Core更新
		/// </summary>
		public void Update(bool isDebug)
		{
			InputHandle(isDebug);

			if(this.IsOverloading)
			{
				ResetAndNew();
			}

			ClearCheck();
			AdjustNodesRendering();
		}

		public void Update()
		{
			Update(false);
		}

		/// <summary>
		/// 入力処理装置
		/// </summary>
		private void InputHandle(bool isDebug)
		{
			//攻撃ボタン押されている
			if((Input.GetKey(KeyCode.Space) || Input.GetButton("Fire1") || isDebug) && CanLaunching)
			{
				if(!this.laserNodes.ContainsKey(this.laserIndex))
				{
					this.laserNodes.Add(this.laserIndex, new List<LaserNode>());
					Debug.Log("新しいレーザー作成、レーザー番号：" + this.laserIndex);
				}
				var newNode = Launch();
				this.laserNodes[this.laserIndex].Add(newNode);
				this.canLaunchTime = Time.time + this.launchInterval;
			}
			//攻撃ボタンRelease
			if(Input.GetKeyUp(KeyCode.Space) || Input.GetButton("Fire1"))
			{
				ResetAndNew();
			}
		}

		private void ResetAndNew()
		{
			this.laserCount = 0;
			this.canLaunchTime = Time.time + this.overloadDuration;
			this.laserIndex++;
		}

		/// <summary>
		/// 発射
		/// </summary>
		/// <returns></returns>
		private LaserNode Launch()
		{
			var node = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePLAYER_LASER, laserLaunchPos, Quaternion.identity);
			var s_node = node.GetComponent<LaserNode>();
			s_node.NeedFixed = (this.laserType == LaserType.TYPE1) ? true : false;
			s_node.shot_speed = this.laserSpd;
			s_node.Travelling_Direction = s_node.transform.right;
			s_node.Trail.startWidth = this.laserTrailWidth;
			s_node.Trail.endWidth = this.laserTrailWidth;
			s_node.Line.startWidth = this.laserWidth;
			s_node.Line.endWidth = this.laserWidth;
			this.laserCount++;
			return s_node;
		}

		/// <summary>
		/// データクリアチェック
		/// </summary>
		private void ClearCheck()
		{
			for(var i = 0; i < this.laserNodes.Count; ++i)
			{
				var checkList = this.laserNodes[i];
				var count = 0;
				for(var j = 0; j < checkList.Count; ++j)
				{
					if(!checkList[j].IsActive)
					{
						count++;
					}
					else
					{
						break;
					}
				}
				if(count == checkList.Count)
				{
					this.laserNodes.Remove(i);
				}
			}
		}

		/// <summary>
		/// レーザー　点オブジェクトレンダリング調整
		/// </summary>
		private void AdjustNodesRendering()
		{

		}
	}
}
