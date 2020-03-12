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
	public ParticleSystem explosionEffect;
	public ParticleSystem blackSmokeEffect;


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
		GenerateNewState(stateManager, 13f, LastBossStateType.DEATH, Death_Enter, Death_Update, Death_Exit);
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
		if(hp < 1)
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
		
		var changeTransfrom = transform.GetChild(0);
		for (var i = 0; i < changeTransfrom.childCount; ++i)
		{
			changeTransfrom.GetChild(i).gameObject.layer = LayerMask.NameToLayer("Explosion");
		}

		explosionEffect.gameObject.SetActive(true);
		blackSmokeEffect.gameObject.SetActive(true);
		body.enabled = false;

		Game_Master.MY.Score_Addition(Parameter.Get_Score, Opponent);
		SE_Manager.SE_Obj.SE_Explosion(Obj_Storage.Storage_Data.audio_se[22]);
		GetComponent<Floating>().enabled = false;

		//無線
		Wireless_sinario.Is_using_wireless = true;
	}

	private void Death_Update()
	{
		var y = Mathf.Lerp(0, -30f, (stateManager.Current.Duration - stateManager.Current.Timer) / stateManager.Current.Duration);
		var z = Mathf.Lerp(0, -20f, (stateManager.Current.Duration - stateManager.Current.Timer) / stateManager.Current.Duration);

		transform.localEulerAngles = new Vector3(0, y, z);

		transform.position += speed * 0.1f * Time.deltaTime * Vector3.down;

		if (stateManager.Current.Timer <= 7f)
		{
			if(body.transform.GetChild(0).gameObject.activeSelf)
				body.transform.GetChild(0).gameObject.SetActive(false);
		}
		if (stateManager.Current.IsDone)
		{
			Is_Dead = true;
			Reset_Status();
			this.gameObject.SetActive(false);
			explosionEffect.gameObject.SetActive(false);
		}
	}

	private void Death_Exit()
	{

	}
	#endregion
}
