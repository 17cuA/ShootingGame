using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Boss_Final : character_status
{
	public LastBossStateType stateType;

	[Header("プレイヤーを中心にし、円を描く動き")]
	public float freeMoveSpd;
	public float freeMoveRadius;
	public float freeDistanceLimit;

	[Header("攻撃関連")]
	public float bulletShotTime;
	private float bulletShotTimer;
	public float laserShotTime;
	private float laserShotTimer;
	public float eyeLaserShotTime;
	private float eyeLaserShotTimer;
	public float fullPowerRotateSpd;
	[SerializeField] private bool canUseLaser = false;
	[SerializeField] private bool canUseEyeLaser = false;
	[SerializeField] private bool canUseFullBulletPower = false;

	private StateManager<LastBossStateType> stateManager;
	[SerializeField] private BoxCollider brainDamageBox;
	private Enemy_DestroyParts eyes;
	private Enemy_DestroyParts battery;

	private new Rigidbody rigidbody;

	private Transform atkTarget;

	[Header("ステート関連")]
	public float debutDuration;
	public float waitDuration;
	public float normalDuration;
	public float angerDuration;
	public float crazyDuration;

	private void Awake()
	{
		Init();
	}

	private void Init()
	{
		//===== ラストボス　自走装置　確認 =====
		stateManager = new StateManager<LastBossStateType>();

		//===== ラストボス　当たり判定領域確認 =====
		brainDamageBox = transform.Find("Brain").GetComponent<BoxCollider>();
		eyes    = transform.Find("Eye_L").GetComponent<Enemy_DestroyParts>();
		battery = transform.Find("houshin").GetComponent<Enemy_DestroyParts>();

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
		GenerateNewState(stateManager, angerDuration, LastBossStateType.ANGER, Anger_Enter, Anger_Update, Anger_Exit);
		GenerateNewState(stateManager, crazyDuration, LastBossStateType.CRAZY, Crazy_Enter, Crazy_Update, Crazy_Exit);
		GenerateNewState(stateManager, 5f, LastBossStateType.DEATH, Death_Enter, Death_Update, Death_Exit);
	}


	private new void Start()
	{
		//=====  プレイヤー確認 =====
		atkTarget = FindTarget("Player");
		//=====　確認失敗、プレイヤー2確認開始
		if (!atkTarget.gameObject.activeSelf)
			atkTarget = FindTarget("Player_2");

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
		if(atkTarget.gameObject.activeSelf)
		{
			stateManager.Update();
			stateType = stateManager.Current.StateType;
		}
		else
		{
			//===== 再びターゲット　探す =====
			atkTarget = FindTarget("Player");
			//=====　確認失敗、プレイヤー2確認開始
			if (!atkTarget.gameObject.activeSelf)
				atkTarget = FindTarget("Player_2");
		}

		base.Update();
	}

	#region ===== IN DEBUT STATE =====
	private void Debut_Enter()
	{
		//===== 出場　当たり判定なしだよーーー ======
		DebugManager.OperationDebug("ラストボス出場!", "ラストボス");

		eyes.GetComponent<BoxCollider>().enabled = false;
		battery.GetComponent<BoxCollider>().enabled = false;
		brainDamageBox.enabled = false;
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

	}

	private void Wait_Exit()
	{

	}
	#endregion
	#endregion

	#region ===== IN NORMAL STATE =====
	private void Normal_Enter()
	{

	}

	private void Normal_Update()
	{

	}

	private void Normal_Exit()
	{

	}
	#endregion

	#region ===== IN ANGER STATE =====
	private void Anger_Enter()
	{

	}

	private void Anger_Update()
	{

	}

	private void Anger_Exit()
	{

	}
	#endregion

	#region ===== IN CRAZY STATE =====
	private void Crazy_Enter()
	{

	}

	private void Crazy_Update()
	{

	}

	private void Crazy_Exit()
	{

	}
	#endregion

	#region ===== IN DEATH STATE =====
	private void Death_Enter()
	{

	}

	private void Death_Update()
	{

	}

	private void Death_Exit()
	{

	}
	#endregion
}
