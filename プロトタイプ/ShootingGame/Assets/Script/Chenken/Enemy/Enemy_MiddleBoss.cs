using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(500)]
public class Enemy_MiddleBoss : MonoBehaviour
{
	public Transform player;
	public GameObject bullet;
	public StateType type;
	public float speed;
	public float advanceBackSpeed;
	public float escapeSpeed;
	public List<int> advanceActionSlot = new List<int>();
	public int excapeActionSlot;
	private int currentSlot;
	private int backActionSlot;
	public float moveDuration = 1;
	public float advanceDuration = 0.75f;
	public float backDuration = 0.75f;
	public float stopDuration = 1f;
	public float escapeDuration = 2f;
	private Vector3 moveDirection;
	private StateManager<StateType> stateManager;

	private void Awake()
	{
		stateManager = new StateManager<StateType>();

		var state = new StateBase<StateType>(moveDuration, StateType.MOVE);
		state.EnterCallBack = Move_Enter;
		state.UpdateCallBack = Move_Update;
		state.ExitCallBack = Move_Exit;
		stateManager.Add(state);

		state = new StateBase<StateType>(advanceDuration, StateType.ADVANCE);
		state.EnterCallBack = Advance_Enter;
		state.UpdateCallBack = Advance_Update;
		stateManager.Add(state);

		state = new StateBase<StateType>(backDuration, StateType.BACK);
		state.EnterCallBack = Back_Enter;
		state.UpdateCallBack = Back_Update;
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
		player = GameObject.Find("Player").transform.GetChild(0).transform;
		stateManager.Start(StateType.STOP);
	}

	// Update is called once per frame
	private void Update()
    {
		if (player)
		{
			stateManager.Update();
			type = stateManager.Current.StateType;
		}
	}

	private void Move_Enter()
	{
		currentSlot++;
	}

	private void Move_Update()
	{
		if (stateManager.Current.IsDone)
			stateManager.ChangeState(StateType.STOP);

		if (moveDirection == Vector3.down)
			transform.Rotate(Vector3.left * (180.0f / (moveDuration * 60f)));
		if (moveDirection == Vector3.up)
			transform.Rotate(Vector3.right * (180.0f / (moveDuration * 60f)));

		transform.position += speed * moveDirection * Time.deltaTime;
	}

	private void Move_Exit()
	{
		if (moveDirection == Vector3.down)
			transform.localEulerAngles = new Vector3(180.0f, 0, 0);
		if (moveDirection == Vector3.up)
			transform.localEulerAngles = new Vector3(0, 0, 0);
	}

	private void Advance_Enter()
	{
		moveDirection = Vector3.left;
		currentSlot++;
	}

	private void Advance_Update()
	{
		if (stateManager.Current.IsDone)
		{
			stateManager.ChangeState(StateType.STOP);
			return;
		}

		transform.position += advanceBackSpeed * moveDirection * Time.deltaTime;
	}

	private void Back_Enter()
	{
		moveDirection = Vector3.right;
		currentSlot++;
	}

	private void Back_Update()
	{
		if (stateManager.Current.IsDone)
		{
			stateManager.ChangeState(StateType.STOP);
			return;
		}

		transform.position += advanceBackSpeed * moveDirection * Time.deltaTime;
	}

	private void Escape_Enter()
	{
		moveDirection = Vector3.left;
	}

	private void Escape_Update()
	{
		transform.position += escapeSpeed * moveDirection * Time.deltaTime;
		transform.Rotate(Vector3.left * (180.0f / (escapeDuration * 60f)));
	}

	private void Stop_Enter()
	{
		moveDirection = Vector3.zero;
		GameObject go = Instantiate(bullet, transform.position + Vector3.left * 2, Quaternion.identity);
		go.GetComponent<Rigidbody>().velocity = Vector3.left * speed * 2f;
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
				stateManager.ChangeState(StateType.ADVANCE);
				backActionSlot = advanceActionSlot[0] + 1;
				advanceActionSlot.RemoveAt(0);
				return;
			}

			if (currentSlot == backActionSlot)
			{
				stateManager.ChangeState(StateType.BACK);
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
