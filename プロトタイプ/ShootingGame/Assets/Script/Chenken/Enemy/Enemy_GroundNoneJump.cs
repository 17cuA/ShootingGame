using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_GroundNoneJump : character_status
{
	public enum State { Fire, Move}

	public float fireRadius = 2f;
	public float moveRadius = 1f;
	public float moveDuration = 0.5f;
	public float groundCheckDistance = 1.0f;

	private float moveTime;
	private Vector3 normal;
	private Vector3 alongGround;
	private MeshCollider meshCollider;

	private void Awake()
	{
		meshCollider = GetComponent<MeshCollider>();

		base.Type = Chara_Type.Enemy;
		base.HP_Setting();
	}

	private void FixedUpdate()
	{
		//RaycastHit hitInfo;
		//meshCollider.Raycast(new Ray(transform.position, Vector3.down), out hitInfo, groundCheckDistance);
		//if (hitInfo.collider != null)
		//{
		//	normal = hitInfo.normal;
		//	alongGround = new Vector3(normal.y, -normal.x);
		//}

		//transform.position += alongGround * speed * Time.deltaTime;
	}
}

