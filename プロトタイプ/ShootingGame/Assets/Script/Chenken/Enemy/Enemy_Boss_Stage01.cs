using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss_Stage01 : MonoBehaviour
{
	public enum Boss_Stage_01_StateType
	{
		Debut,
		FirstShot,
		SpreadShot,
		LaserShot,
		Escape,
	}

	public Boss_Stage_01_StateType type;
	[Header("行動持続時間　（単位：秒）")]
	public float firstShotDuration = 8f;
	public float SpreadShotDuration = 15f;
	public float laserShotDuration = 10f;

	[Header("最大回転回数　（単位：回）")]
	public int maxRotateCount = 4;

	[Header("回数方向")]
	public bool isRotatingRight = false;

	[Header("弾種類ごとの発射可能かどうかのフラグ（デバッグ用）")]
	public bool isBubbleShot = false;
	public bool isSpreadShot = false;
	public bool isLaserShot = false;

	private int laserRotationCount;
	private StateManager<Boss_Stage_01_StateType> stateManager;
	private Animator animator;
	private Rigidbody rb;

	private void Awake()
	{
		stateManager  = new StateManager<Boss_Stage_01_StateType>();
		animator      = GetComponentInChildren<Animator>();
		rb            = GetComponent<Rigidbody>();
		rb.useGravity = false;

		var state = new StateBase<Boss_Stage_01_StateType>(0,Boss_Stage_01_StateType.Debut);
		state.EnterCallBack = Debut_Enter;
		state.UpdateCallBack = Debut_Update;
		state.ExitCallBack = Debut_Exit;
		stateManager.Add(state);

		state = new StateBase<Boss_Stage_01_StateType>(firstShotDuration, Boss_Stage_01_StateType.FirstShot);
		state.EnterCallBack = FirstShot_Enter;
		state.UpdateCallBack = FirstShot_Update;
		state.ExitCallBack = FirstShot_Exit;
		stateManager.Add(state);

		state = new StateBase<Boss_Stage_01_StateType>(SpreadShotDuration, Boss_Stage_01_StateType.SpreadShot);
		state.EnterCallBack = SpreadShot_Enter;
		state.UpdateCallBack = SpreadShot_Update;
		state.ExitCallBack = SpreadShot_Exit;
		stateManager.Add(state);

		state = new StateBase<Boss_Stage_01_StateType>(laserShotDuration, Boss_Stage_01_StateType.LaserShot);
		state.EnterCallBack = LaserShot_Enter;
		state.UpdateCallBack = LaserShot_Update;
		state.ExitCallBack = LaserShot_Exit;
		stateManager.Add(state);

		state = new StateBase<Boss_Stage_01_StateType>(0, Boss_Stage_01_StateType.Escape);
		state.EnterCallBack = Escape_Enter;
		state.UpdateCallBack = Escape_Update;
		state.ExitCallBack = Escape_Exit;
		stateManager.Add(state);
	}

	private void Start()
	{
		stateManager.Start(Boss_Stage_01_StateType.Debut);
	}

	private void Update()
	{
		stateManager.Update();
		type = stateManager.Current.StateType;
	}

	private void Debut_Enter()
	{
		animator.Play("Debut");
	}

	private void Debut_Update()
	{
		if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
		{
			stateManager.ChangeState(Boss_Stage_01_StateType.FirstShot);
			return;
		}
	}

	private void Debut_Exit()
	{
		animator.Play("None");
		transform.position = transform.GetChild(0).transform.position;
		transform.GetChild(0).localPosition = Vector3.zero;
	}

	private void FirstShot_Enter()
	{
		isBubbleShot = true;
	}

	private void FirstShot_Update()
	{
		if(stateManager.Current.IsDone)
		{
			stateManager.ChangeState(Boss_Stage_01_StateType.SpreadShot);
			return;
		}
	}

	private void FirstShot_Exit()
	{

	}

	private void SpreadShot_Enter()
	{
		isSpreadShot = true;
	}

	private void SpreadShot_Update()
	{
		if(stateManager.Current.IsDone)
		{
			stateManager.ChangeState(Boss_Stage_01_StateType.LaserShot);
			return;
		}
	}

	private void SpreadShot_Exit()
	{
		isSpreadShot = false;
	}

	private void LaserShot_Enter()
	{
		isLaserShot = true;
		laserRotationCount++;
		isRotatingRight = (laserRotationCount % 2 != 0) ? true : false;
	}

	private void LaserShot_Update()
	{
		if(stateManager.Current.IsDone)
		{
			if(laserRotationCount > maxRotateCount)
			{
				stateManager.ChangeState(Boss_Stage_01_StateType.Escape);
				return;
			}

			stateManager.ChangeState(Boss_Stage_01_StateType.SpreadShot);
			return;
		}

		if (isRotatingRight) transform.Rotate(Vector3.forward * (540.0f / (laserShotDuration / Time.deltaTime)));
		else				 transform.Rotate(Vector3.back * (540.0f / (laserShotDuration / Time.deltaTime)));
	}

	private void LaserShot_Exit()
	{
		isLaserShot = false;
		isRotatingRight = false;
	}

	private void Escape_Enter()
	{
		animator.Play("Escape");

		//念のため弾発射フラグをFalse にする
		isBubbleShot = false;
		isSpreadShot = false;
		isLaserShot = false;
	}

	private void Escape_Update()
	{
		if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
		{
			gameObject.SetActive(false);
		}
	}

	private void Escape_Exit()
	{

	}
}

