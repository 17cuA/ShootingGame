using System;
using System.Collections.Generic;
using UnityEngine;

namespace Laser
{
	[RequireComponent(typeof(LineRenderer))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(TrailRenderer))]
	public class LaserNode : bullet_status
	{
		private LineRenderer lineRenderer;
		private  CapsuleCollider capsuleCollider;
		private TrailRenderer trailRenderer;
		private bool needFixed = false;
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

		public bool IsActive
		{
			get
			{
				return this.gameObject.activeSelf;
			}
		}

		private void Awake()
		{
			this.lineRenderer = GetComponent<LineRenderer>();
			this.capsuleCollider = GetComponent<CapsuleCollider>();
			this.trailRenderer = GetComponent<TrailRenderer>();
		}
	}
}

