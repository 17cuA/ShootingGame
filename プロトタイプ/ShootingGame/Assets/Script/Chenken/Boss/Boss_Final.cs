using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Boss_Final : character_status
{
	public LastBossStateType stateType;


	[Header("攻撃関連")]
	public GameObject deathBullet;

	private StateManager<LastBossStateType> stateManager;
	private Enemy_DestroyParts eyes;
	private Enemy_DestroyParts battery;
	private BoxCollider body;

	private new Rigidbody rigidbody;


	[Header("ステート関連")]
	public float debutDuration;
	public float waitDuration;
	public float normalDuration;

	private void Awake()
	{
		Init();
	}

	private void Init()
	{
		//===== ラストボス　ステートマシン　確認 =====
		stateManager = new StateManager<LastBossStateType>();

		//===== ラストボス　当たり判定領域確認 =====
		body = GetComponent<BoxCollider>();

		//===== ラストボス　動力装置確認 =====
		rigidbody = GetComponent<Rigidbody>();
		rigidbody.useGravity = false;

		//=====　ラストボス　パタン確認 =====
		//=====　出場
		//=====　普通
		//=====　怒
		//=====　狂
		//=====　死
		GenerateNewState(stateManager, debutDuration, LastBossStateType.DEBUT, Debut_Enter, Debut_Update, Debut_Exit);
		GenerateNewState(stateManager, waitDuration, LastBossStateType.WAIT, Wait_Enter, Wait_Update, Wait_Enter);
		GenerateNewState(stateManager, normalDuration, LastBossStateType.NORMAL, Normal_Enter, Normal_Update, Normal_Exit);
		GenerateNewState(stateManager, 5f, LastBossStateType.DEATH, Death_Enter, Death_Update, Death_Exit);
	}


	private new void Start()
	{
		base.Start(); 
		stateManager.Start(LastBossStateType.DEBUT);

	}

	#region 便利用関数
	#region 便利用　ターゲット探す関数
	private Transform FindTarget(string targetName)
	{
		return GameObject.Find(targetName).transform.GetChild(0).transform;
	}
	#endregion

	#region 便利用関数 新しいステートの生成
	private void GenerateNewState(StateManager<LastBossStateType> manager, float duration, LastBossStateType type, StateCallBack enter, StateCallBack update, StateCallBack exit)
	{
		var newState = new StateBase<LastBossStateType>(duration, type);
		newState.EnterCallBack = enter;
		newState.UpdateCallBack = update;
		newState.ExitCallBack = exit;
		manager.Add(newState);
	}
	#endregion
	#endregion

	private new void Update()
	{
		//===== ターゲット　発見 =====
		
		stateManager.Update();
		stateType = stateManager.Current.StateType;

		base.Update();
	}

	private void FreeMove()
	{
		//if (atkTarget == null)
		//	return;

		//if(updateTimer <= updateTime)
		//{
		//	var pos = (1 - updateTimer / updateTime) * (1 - updateTimer / updateTime) * controllerPosStart.position + 2 * (updateTimer / updateTime) * (1 - updateTimer / updateTime) * controllerPos.position + (updateTimer / updateTime) * (updateTimer / updateTime) * controllerPosEnd.position;

		//	transform.position = pos;

		//	updateTimer += Time.deltaTime;
		//}

	}

	#region ===== IN DEBUT STATE =====
	private void Debut_Enter()
	{
		//===== 出場　当たり判定なしだよーーー ======
		DebugManager.OperationDebug("ラストボス出場!", "ラストボス");
		body.enabled = false;
	}

	private void Debut_Update()
	{
		if(stateManager.Current.IsDone)
		{
			stateManager.ChangeState(LastBossStateType.WAIT);
			return;
		}
	}

	private void Debut_Exit()
	{

	}
	#region ===== IN WAIT STATE =====
	private void Wait_Enter()
	{

	}
	
	private void Wait_Update()
	{
		if (stateManager.Current.IsDone)
		{
			stateManager.ChangeState(LastBossStateType.NORMAL);
			return;
		}
	}

	private void Wait_Exit()
	{

	}
	#endregion
	#endregion

	#region ===== IN NORMAL STATE =====
	private void Normal_Enter()
	{
		body.enabled = true;
	}

	private void Normal_Update()
	{
		if(base.hp <= 0)
		{
			stateManager.ChangeState(LastBossStateType.DEATH);
			return;
		}
	}

	private void Normal_Exit()
	{

	}
	#endregion

	#region ===== IN DEATH STATE =====
	private void Death_Enter()
	{
		//var leftTop = Instantiate(deathBullet, transform.position, Quaternion.identity);
		//leftTop.GetComponent<Boss_ReflectedBullet>().direction = new Vector3(-1, 1, 0);
		//leftTop.transform.localEulerAngles = new Vector3(0, 0, 135f);

		//var rightTop = Instantiate(deathBullet, transform.position, Quaternion.identity);
		//rightTop.GetComponent<Boss_ReflectedBullet>().direction = new Vector3(1, 1, 0);
		//rightTop.transform.localEulerAngles = new Vector3(0, 0, 45f);

		//var leftDown = Instantiate(deathBullet, transform.position, Quaternion.identity);
		//leftDown.GetComponent<Boss_ReflectedBullet>().direction = new Vector3(-1, -1, 0);
		//leftDown.transform.localEulerAngles = new Vector3(0, 0, 225f);

		//var rightDown = Instantiate(deathBullet, transform.position, Quaternion.identity);
		//rightDown.GetComponent<Boss_ReflectedBullet>().direction = new Vector3(1, -1, 0);
		//rightDown.transform.localEulerAngles = new Vector3(0, 0, 315f);

		//var left = Instantiate(deathBullet, transform.position, Quaternion.identity);
		//left.GetComponent<Boss_ReflectedBullet>().direction = new Vector3(-1, 0, 0);
		//left.GetComponent<Boss_ReflectedBullet>().isCloseReflect = true;
		//left.transform.localEulerAngles = new Vector3(0, 0, 180f);

		//var right = Instantiate(deathBullet, transform.position, Quaternion.identity);
		//right.GetComponent<Boss_ReflectedBullet>().direction = new Vector3(1, 0, 0);
		//right.GetComponent<Boss_ReflectedBullet>().isCloseReflect = true;
		//right.transform.localEulerAngles = new Vector3(0, 0, 0);

		//var top = Instantiate(deathBullet, transform.position, Quaternion.identity);
		//top.GetComponent<Boss_ReflectedBullet>().direction = new Vector3(0, 1, 0);
		//top.GetComponent<Boss_ReflectedBullet>().isCloseReflect = true;
		//top.transform.localEulerAngles = new Vector3(0, 0, 90);

		//var down = Instantiate(deathBullet, transform.position, Quaternion.identity);
		//down.GetComponent<Boss_ReflectedBullet>().direction = new Vector3(0, -1, 0);
		//down.GetComponent<Boss_ReflectedBullet>().isCloseReflect = true;
		//down.transform.localEulerAngles = new Vector3(0, 0, 270);

		Died_Process();
	}

	private void Death_Update()
	{

	}

	private void Death_Exit()
	{

	}
	#endregion
}
