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

		[Header("----------タイプ2----------")]
		public float rotateSpdDivisor;
		public float r_laserSpd;
		public float r_laserWidth;
		public int r_laserNodeMax;
		public float r_laserOverloadDuration;
		public float r_laserLaunchInterval;
		public float r_laserTrailWidth;
		public Material r_laserLineMaterial;
		public Material r_laserTrailMaterial;
		public LaserType r_laserType;



		private RotateCore rotateDevice;
		private LaunchCore currentLaunchDevice;

		private LaunchCore s_laser_launch_device;
		private LaunchCore r_laser_launch_device;

		private void Awake()
		{
			this.s_laser_launch_device = new LaunchCore(s_laserType,transform.position,s_laserNodeMax, s_laserSpd, s_laserWidth, s_laserOverloadDuration, 
				                                        s_laserLaunchInterval, s_laserLineMaterial,s_laserTrailWidth, s_laserTrailMaterial);

			this.r_laser_launch_device = new LaunchCore(r_laserType,transform.position,r_laserNodeMax, r_laserSpd, r_laserWidth, r_laserOverloadDuration,
				                                        r_laserLaunchInterval, r_laserLineMaterial,r_laserTrailWidth, r_laserTrailMaterial);

			this.rotateDevice = new RotateCore(this.transform.position, rotateSpdDivisor);

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
				this.currentLaunchDevice.Update(true);
				this.currentLaunchDevice.SetDirection(rotateDevice.Direction);

				if (this.currentLaunchDevice.Type == LaserType.TYPE2)
				{
					this.rotateDevice.Update(true);
				}
			}
			else
			{
				this.currentLaunchDevice.Update();
				this.currentLaunchDevice.SetDirection(rotateDevice.Direction);

				if (this.currentLaunchDevice.Type == LaserType.TYPE2)
				{
					this.rotateDevice.Update();
				}
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
		private Vector3 originPos;
		public Vector3 Direction
		{
			get
			{
				return this.pushPos - this.originPos;
			}
		}
		private float rotateSpdDivisor;

		/// <summary>
		/// セットアップ
		/// </summary>
		/// <param name="pushPos"> 発射位置 </param>
		public RotateCore(Vector3 pushPos,float rotateSpdDivisor)
		{
			this.pushPos = pushPos;
			this.originPos = pushPos;
			this.theta = 0;
			this.rotateSpdDivisor = rotateSpdDivisor;
		}

		/// <summary>
		/// 回転
		/// </summary>
		/// <param name="angle"></param>
		private void Rotate(float angle)
		{
			this.theta += angle;
			this.pushPos.x = Mathf.Cos(this.theta);
			this.pushPos.y = Mathf.Sin(this.theta);
		}

		public void Update(bool isDebug)
		{
			InputHandle(isDebug);
		}

		public void Update()
		{		
			Update(false);
		}

		private void InputHandle(bool isDebug)
		{
			if (isDebug)
			{
				var isChange = false;
				if (Input.GetKey(KeyCode.Space))
				{
					isChange = true;
				}
				if(isChange)
				{
					Rotate(Mathf.PI / rotateSpdDivisor * Mathf.Deg2Rad);
				}
				else
				{
					Rotate(-Mathf.PI / rotateSpdDivisor * Mathf.Deg2Rad);
				}
			}
			else
			{
				if (Input.GetKey(KeyCode.LeftShift))
				{
					Rotate(Mathf.PI / rotateSpdDivisor * Mathf.Deg2Rad);
				}
				if (Input.GetKey(KeyCode.LeftControl))
				{
					Rotate(-Mathf.PI / rotateSpdDivisor * Mathf.Deg2Rad);
				}
			}
		}
	}

	/// <summary>
	/// 発射装置
	/// </summary>
	public class LaunchCore
	{
		private LaserType laserType;
		public LaserType Type
		{
			get
			{
				return this.laserType;
			}
		}
		private Vector3 laserLaunchPos;
		private Vector3 laserDirection;
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

			AdjustNodesRendering();
			ClearCheck();
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
			if(Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Fire1"))
			{
				ResetAndNew();
			}
		}

		/// <summary>
		/// レーザー再生成準備
		/// </summary>
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
			s_node.Travelling_Direction = this.laserDirection;
			s_node.Trail.Clear();
			s_node.Trail.startWidth = this.laserTrailWidth;
			s_node.Trail.endWidth = this.laserTrailWidth;
			s_node.Trail.material = this.laserTrailMaterial;
			s_node.Line.startWidth = this.laserWidth;
			s_node.Line.endWidth = this.laserWidth;
			s_node.Line.material = this.laserlineMaterial;
			this.laserCount++;
			return s_node;
		}

		/// <summary>
		/// データクリアチェック
		/// </summary>
		private void ClearCheck()
		{
			var removekeyNums = new List<int>();
			foreach(var tempLaserNodes in laserNodes)
			{
				var checkList = tempLaserNodes.Value;
				var count = 0;
				for(var j = 0; j < checkList.Count; ++j)
				{
					if(checkList[j].IsOutScreen)
					{
						count++;
					}
					else
					{
						break;
					}
				}
				if(count  == checkList.Count)
				{
					Debug.Log("レーザーリセット　：ALL False");
					for (var j = 0; j < checkList.Count; ++j)
					{
						checkList[j].SwitchComponent(true);
						checkList[j].gameObject.SetActive(false);
					}
					removekeyNums.Add(tempLaserNodes.Key);
				}
			}

			for(var i = 0; i < removekeyNums.Count; ++i)
			{
				this.laserNodes.Remove(removekeyNums[i]);
			}
		}

		/// <summary>
		/// レーザー　点オブジェクトレンダリング調整
		/// </summary>
		private void AdjustNodesRendering()
		{
			foreach (var tempLaserNodes in laserNodes)
			{
				var adjustList = tempLaserNodes.Value;
				for(var j = 0; j <adjustList.Count; ++j)
				{
					if(adjustList[j].IsOutScreen)
					{
						continue;
					}
					if(j == 0)
					{
						continue;
					}
					else
					{
						adjustList[j].Line.SetPosition(0, adjustList[j - 1].transform.position);
						adjustList[j].Line.SetPosition(1, adjustList[j].transform.position);
					}
				}
			}
		}

		/// <summary>
		/// 方向設定
		/// </summary>
		/// <param name="direction"></param>
		public void SetDirection(Vector3 direction)
		{
			this.laserDirection = direction;
		}
	}
}
