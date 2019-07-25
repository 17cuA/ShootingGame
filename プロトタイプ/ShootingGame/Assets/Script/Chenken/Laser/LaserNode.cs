using System;
using System.Collections.Generic;
using UnityEngine;

namespace Laser
{
	[RequireComponent(typeof(LineRenderer))]
	[RequireComponent(typeof(CapsuleCollider))]
	public class LaserNode : character_status
	{
		private LineRenderer lineRenderer;
		private new CapsuleCollider capsuleCollider;
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

		private void Awake()
		{
			this.lineRenderer = GetComponent<LineRenderer>();
			this.capsuleCollider = GetComponent<CapsuleCollider>();
		}
	}
}

