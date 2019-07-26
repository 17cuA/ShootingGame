using System;
using System.Collections.Generic;
using UnityEngine;

namespace Laser
{
	[RequireComponent(typeof(LineRenderer))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(TrailRenderer))]
	public class LaserNode : Player_Bullet
	{
		public float lifeTime;
		public bool NeedFixed
		{
			get
			{
				return this.needFixed;
			}
			set
			{
				this.needFixed = value;
			}
		}
		public TrailRenderer Trail
		{
			get
			{
				return this.trailRenderer;
			}
			set
			{
				this.trailRenderer = value;
			}
		}

		public LineRenderer Line
		{
			get
			{
				return this.lineRenderer;
			}
			set
			{
				this.lineRenderer = value;
			}
		}

		public bool IsOutScreen
		{
			get
			{
				return transform.position.x >= 25.0f || transform.position.x <= -25.0f || transform.position.y >= 8.5f || transform.position.y <= -8.5f;
			}
		}
		private LineRenderer lineRenderer;
		private CapsuleCollider capsuleCollider;
		private TrailRenderer trailRenderer;
		private bool needFixed = false;
		private float lifeTimer;
		

		private void Awake()
		{
			this.lineRenderer = GetComponent<LineRenderer>();
			this.capsuleCollider = GetComponent<CapsuleCollider>();
			this.trailRenderer = GetComponent<TrailRenderer>();

			base.attack_damage = 1;
		}

		private new void Start()
		{
			base.Start();
		}

		private new void Update()
		{
			//画面外
			if (IsOutScreen)
			{
				SwitchComponent(false);
				this.lifeTimer = 0;
			}
			else
			{
				this.lifeTimer += Time.deltaTime;
				this.Trail.startWidth = Mathf.Lerp(0, 28, this.lifeTimer / lifeTime);
				this.Trail.endWidth = Mathf.Lerp(0, 28, this.lifeTimer / lifeTime);
				this.trailRenderer.time = Mathf.Lerp(0.6f, -0.6f, lifeTimer / lifeTime);
			}

	     	base.Moving_To_Travelling_Direction();
		}

		private new void OnTriggerEnter(Collider col)
		{
		    if (col.gameObject.tag == "Enemy")
			{
				SwitchComponent(false);
				this.lifeTimer = 0;
				//add:0513_takada 爆発エフェクトのテスト
				//AddExplosionProcess();
				GameObject effect = Obj_Storage.Storage_Data.Effects[11].Active_Obj();
				ParticleSystem particle = effect.GetComponent<ParticleSystem>();
				effect.transform.position = gameObject.transform.position;
				particle.Play();
			}

		}

		/// <summary>
		/// コンポーネントだけ操作する
		/// </summary>
		/// <param name="flag"></param>
		public void SwitchComponent(bool flag)
		{
			this.lineRenderer.enabled = flag;
			this.capsuleCollider.enabled = flag;
			this.trailRenderer.enabled = flag;
		}

	}
}

