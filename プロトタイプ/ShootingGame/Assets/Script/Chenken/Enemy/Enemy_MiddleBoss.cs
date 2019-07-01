﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

[RequireComponent(typeof(Rigidbody))]
[DefaultExecutionOrder(500)]
public class Enemy_MiddleBoss : character_status
{
	public StateType type;
	public float advanceBackSpeed;
	public float escapeSpeed;
	public List<int> advanceActionSlot = new List<int>();
	public int excapeActionSlot;
	private int currentSlot;
	private int backActionSlot;
	public float debutDuration = 2f;
	public float moveDuration = 1;
	public float advanceBackDuration = 2f;
	public float stopDuration = 1f;
	public float escapeDuration = 2f;
	private Vector3 moveDirection;
	private StateManager<StateType> stateManager;

	private CapsuleCollider capsuleCollider;                    
	private Rigidbody rigidbody;
	private Transform player;
	private Animator animator;
	private bool canAdvanceAttack;

	private void Awake()
	{
		stateManager = new StateManager<StateType>();
		rigidbody = GetComponent<Rigidbody>() ;
		animator = GetComponentInChildren<Animator>();
		capsuleCollider = GetComponent<CapsuleCollider>();
		rigidbody.useGravity = false;

		var state = new StateBase<StateType>(moveDuration, StateType.MOVE);
		state.EnterCallBack = Move_Enter;
		state.UpdateCallBack = Move_Update;
		state.ExitCallBack = Move_Exit;
		stateManager.Add(state);

		state = new StateBase<StateType>(0, StateType.DEBUT);
		state.EnterCallBack = Debut_Enter;
		state.UpdateCallBack = Debut_Update;
		state.ExitCallBack = Debut_Exit;
		stateManager.Add(state);

		state = new StateBase<StateType>(advanceBackDuration, StateType.ADVANCE_AND_BACK);
		state.EnterCallBack = AdvanceBack_Enter;
		state.UpdateCallBack = AdvanceBack_Update;
		stateManager.Add(state);


		state = new StateBase<StateType>(stopDuration, StateType.STOP);
		state.EnterCallBack = Stop_Enter;
		state.UpdateCallBack = Stop_Update;
		state.ExitCallBack = Stop_Exit;
		stateManager.Add(state);

		state = new StateBase<StateType>(escapeDuration, StateType.ESCAPE);
		state.EnterCallBack = Escape_Enter;
		state.UpdateCallBack = Escape_Update;
		stateManager.Add(state);
	}

	// Start is called before the first frame update
	private void Start()
    {
		base.HP_Setting();

		player = GameObject.Find("Player").transform.GetChild(0).transform;
		stateManager.Start(StateType.DEBUT);
	}

	// Update is called once per frame
	private void Update()
    {
		if (player)
		{
			stateManager.Update();
			type = stateManager.Current.StateType;
		}

		base.Died_Process();
	}

	private void Debut_Enter()
	{
		animator.Play("Debut");
		capsuleCollider.enabled = false;
	}

	private void Debut_Update()
	{
		if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
		{
			stateManager.ChangeState(StateType.STOP);
			return;
		}

	}

	private void Debut_Exit()
	{
		animator.Play("None");
		transform.position = transform.GetChild(0).transform.position;
		transform.GetChild(0).localPosition = Vector3.zero;
		capsuleCollider.enabled = true;
	}

	private void Move_Enter()
	{
		currentSlot++;
	}

	private void Move_Update()
	{
		if (stateManager.Current.IsDone)
		{
			stateManager.ChangeState(StateType.STOP);
			return;
		}
			
		if (moveDirection == Vector3.down)
			transform.Rotate(Vector3.left * (180.0f / (moveDuration * 60f)));
		if (moveDirection == Vector3.up)
			transform.Rotate(Vector3.right * (180.0f / (moveDuration * 60f)));

		transform.position += speed  * moveDirection * Time.deltaTime;
	}

	private void Move_Exit()
	{
		if (moveDirection == Vector3.down)
			transform.localEulerAngles = new Vector3(180.0f, 0, 0);
		if (moveDirection == Vector3.up)
			transform.localEulerAngles = new Vector3(0, 0, 0);
	}

	private void AdvanceBack_Enter()
	{
		canAdvanceAttack = true;
		currentSlot += 2;
	}

	private void AdvanceBack_Update()
	{
		if (stateManager.Current.IsDone)
		{
			stateManager.ChangeState(StateType.STOP);
			return;
		}

		if (stateManager.Current.Timer >= 0.625 * stateManager.Current.Duration)
		{
			moveDirection = Vector3.left;
		}
		else if (stateManager.Current.Timer >= 0.375 * stateManager.Current.Duration)
		{
			if (canAdvanceAttack)
			{
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, transform.position + new Vector3(1, 1, 0), Vector3.left);
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, transform.position + new Vector3(0, 0.5f, 0), Vector3.left);
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, transform.position + new Vector3(0, -0.5f, 0), Vector3.left);
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, transform.position + new Vector3(1, -1, 0), Vector3.left);
				canAdvanceAttack = false;
			}
			moveDirection = Vector3.zero;
		}
		else 
		{
			moveDirection = Vector3.right;
		}

		transform.position += advanceBackSpeed * moveDirection * Time.deltaTime;
	}


	private void Escape_Enter()
	{
		moveDirection = Vector3.left;
		base.hp = 10000;
	}

	private void Escape_Update()
	{
		transform.position += escapeSpeed * moveDirection * Time.deltaTime;
		transform.Rotate(Vector3.left * (180.0f / (escapeDuration * 60f)));
	}

	private void Stop_Enter()
	{
		moveDirection = Vector3.zero;
		Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, transform.position + new Vector3(1, 1, 0), Vector3.left);
		Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, transform.position + new Vector3(0, 0.5f, 0), Vector3.left);
		Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, transform.position + new Vector3(0, -0.5f, 0), Vector3.left);
		Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, transform.position + new Vector3(1, -1, 0), Vector3.left);
	}

	private void Stop_Update()
	{
		if (stateManager.Current.IsDone)
		{
			if ((advanceActionSlot.Count > 0 && currentSlot != advanceActionSlot[0] || advanceActionSlot.Count == 0) && currentSlot != excapeActionSlot && currentSlot != backActionSlot || (backActionSlot == 0 && currentSlot == backActionSlot))
			{
				stateManager.ChangeState(StateType.MOVE);
				return;
			}

			if (advanceActionSlot.Count > 0 && currentSlot == advanceActionSlot[0])
			{
				stateManager.ChangeState(StateType.ADVANCE_AND_BACK);
				backActionSlot = advanceActionSlot[0] + 1;
				advanceActionSlot.RemoveAt(0);
				return;
			}

			if (currentSlot == excapeActionSlot)
			{
				stateManager.ChangeState(StateType.ESCAPE);
				return;
			}
		}
	}

	private void Stop_Exit()
	{
		if (player.transform.position.y >= transform.position.y)
			moveDirection = Vector3.up;
		else
			moveDirection = Vector3.down;
	}
}
