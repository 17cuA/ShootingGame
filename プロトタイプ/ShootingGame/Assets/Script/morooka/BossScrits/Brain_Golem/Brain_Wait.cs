//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2019/12/20
//----------------------------------------------------------------------------------------------
// 2面ボス本体
//----------------------------------------------------------------------------------------------
// 2019/12/20　パーツの死亡判定確認
//----------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

public class Brain_Wait : character_status
{
	[SerializeField, Tooltip("ダメージ受けるパーツ")] private List<Brain_Parts> damagedParts;
	[SerializeField, Tooltip("触手のパーツ")] private List<Tentacles> tentacles;
	[SerializeField, Tooltip("顔のアニメーション")] private Animation FaceAnimation;
	[SerializeField, Tooltip("タイムライン制御")] private PlayableDirector playable_Map;
    [SerializeField, Tooltip("エネミータイムライン制御")] public PlayableDirector playable_Create;
    [SerializeField, Tooltip("レーザー")] private GameObject lasear;
	[SerializeField, Tooltip("レーザーのためエフェクト")] private Boss_One_A111 lasear_EFPS;
	[SerializeField, Tooltip("レーザーのインターバル時間")] private float lasearInterval_Max;
	[SerializeField, Tooltip("ボスの目")] GameObject Eyes;
	[SerializeField, Tooltip("爆発エフェクト")]GameObject effect;

	private bool Is_Active { get; set; }									// 行動可能か
	private bool Is_Laser { get; set; }									// レーザーを撃てるか
	protected int ActionStep { get; set; }								// 攻撃手順指示番号
	private float DeathTime_Cnt { get; set; }							// 死ぬ時間カウンター
	private float DeathTime_Max { get; set; }						// 死ぬ時間
	private List<Collider> colliders { get; set; }						// コライダー軍
	private List<Transform> All_Transforms { get; set; }		// 自分、子供、孫含めたトランスフォーム
	private float lasearinterval_Cnt { get; set; }                      // レーザーのインターバル計測
	private bool oneFlag;

	new private void Start()
	{
		colliders = new List<Collider>(transform.GetComponentsInChildren<Collider>(false));
		All_Transforms = new List<Transform>(transform.GetComponentsInChildren<Transform>(false));
		All_Transforms.Remove(transform);
		Boss_DriveSwitch(false);
		Is_Laser = true;

		ActionStep = 0;
		DeathTime_Max = 60.0f * 3.0f;
		lasearinterval_Cnt = lasearInterval_Max;
		Is_Dead = false;
		oneFlag = false;
	}

    new private void Update()
    {
		#region 起動状態(仮)確認
		if (!Is_Active)
		{
			// 画面内に入る少し前で
			if (transform.position.x < 20.0f)
			{
				// 起動
				foreach (Transform obj in All_Transforms)
				{
					obj.gameObject.SetActive(true);
				}

				if(playable_Map.state == PlayState.Paused)
				{
					Boss_DriveSwitch(true);
					// 全体
					Is_Active = true;
				}
			}

			return;
		}
		#endregion

		#region 自動死亡時間
		DeathTime_Cnt += Time.deltaTime;
		if (DeathTime_Max < DeathTime_Cnt || Input.GetKey(KeyCode.O) && Input.GetKey(KeyCode.H))
		{
			if (playable_Map.state == PlayState.Paused)
			{
				// パーツ破壊
				foreach(var damage in damagedParts)
				{
					if (damage.Is_Dead)
					{
						continue;
					}
					damage.Died_Process();
				}
			}
		}
		#endregion

		#region パーツの死亡確認とボスの破壊確認
		// パーツが死んでいるとき
		if (!Is_PartsAlive())
		{
			// 管理しているタイムラインがポーズ状態のとき
			if (playable_Map.state == PlayState.Paused && !oneFlag)
			{
				SE_Manager.SE_Obj.SE_Explosion(Obj_Storage.Storage_Data.audio_se[22]);
				if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eONE_PLAYER)
				{
					Game_Master.MY.Score_Addition(Parameter.Get_Score, (int)Game_Master.PLAYER_NUM.eONE_PLAYER);
				}
				else if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
				{
					Game_Master.MY.Score_Addition(Parameter.Get_Score / 2, (int)Game_Master.PLAYER_NUM.eONE_PLAYER);
					Game_Master.MY.Score_Addition(Parameter.Get_Score / 2, (int)Game_Master.PLAYER_NUM.eTWO_PLAYER);
				}
				playable_Map.time = 324.3;
                playable_Create.time = 324.3;
				Is_Dead = true;
				Instantiate(effect, transform.position, transform.rotation);
				Boss_DriveSwitch(false);

				Wireless_sinario.Is_using_wireless = true;
				oneFlag = true;
			}
			// タイムラインの再生時間を指定後、再生
			if (Wireless_sinario.IsFinishWireless_BrainBattle())
			{
				playable_Map.Play();
                playable_Create.Play();
			}
			// 画面外
			if (transform.position.x < -20.0f)
			{
				Destroy(gameObject);
			}
		}
		#endregion

		#region レーザー
		lasearinterval_Cnt += Time.deltaTime;
		// レーザーの攻撃
		if (Is_Laser)
		{
			// 口開く、エネルギー溜めパーティクル再生
			if (ActionStep == 0)
			{
				FaceAnimation.Play("Open");
				lasear_EFPS.gameObject.SetActive(true);
				ActionStep++;
			}
			// パーティクル終了時
			else if (ActionStep == 1)
			{
				// 口開くアニメーションが終わったとき
				if (!FaceAnimation.IsPlaying("Open") && lasear_EFPS.Completion_Confirmation())
				{
					lasear_EFPS.gameObject.SetActive(false);
					ActionStep++;
				}
			}
			// レーザー撃ちだし
			else if (ActionStep == 2)
			{
				lasear.SetActive(true);
				ActionStep++;
			}
			// 口閉じる
			else if (ActionStep == 3)
			{
				// レーザーが起動しなくなったとき
				if (!lasear.activeSelf)
				{
					FaceAnimation.Play("Close");
					ActionStep++;
				}
			}
			else if (ActionStep == 4)
			{
				if (!FaceAnimation.IsPlaying("Close"))
				{
					ActionStep = 0;
					Is_Laser = false;
					GetComponent<Collider>().enabled = true;
					lasearinterval_Cnt = 0.0f;
				}
			}
		}
		#endregion

		#region 目の動き
		Vector3 p_pos = Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eONE_PLAYER ? Obj_Storage.Storage_Data.GetPlayer().transform.position : Obj_Storage.Storage_Data.GetPlayer().activeSelf ? Obj_Storage.Storage_Data.GetPlayer().transform.position : Obj_Storage.Storage_Data.GetPlayer2().transform.position;
		Vector3 tempPos = p_pos - Eyes.transform.position;
		tempPos.z = 0.0f;
		Eyes.transform.forward = tempPos;
		#endregion
	}

	/// <summary>
	/// パーツが生きているのか確認
	/// </summary>
	/// <returns> 生きていれば true </returns>
	private bool Is_PartsAlive()
	{
		bool flag = false;

		// 生存パーツリストの確認
		foreach(var parts in damagedParts)
		{
			// パーツが死んでいるとき
			if(parts.Is_Dead)
			{
				continue;
			}
			// パーツのHPが0以上のとき
			if(parts.hp > 0)
			{
				flag = true;
			}
			// パーツがHP0のとき
			else
			{
				// 死亡判定
				parts.Died_Process();
			}
		}
		return flag;
	}

	new private void OnTriggerEnter(Collider other)
	{
		if(!Is_Laser && other.tag == "Player" && lasearinterval_Cnt > lasearInterval_Max)
		{
			Is_Laser = true;
			GetComponent<Collider>().enabled = false;
		}
	}

	/// <summary>
	/// ボスの駆動スイッチ
	/// </summary>
	/// <param name="b"> ON OFFの状態 </param>
	private void Boss_DriveSwitch(bool b)
	{
		// 触手
		foreach (var tenp in tentacles)
		{
			tenp.enabled = b;
		}

		// コライダー
		foreach (Collider col in colliders)
		{
			col.enabled = b;
		}

		// ゲームオブジェクト
		foreach (Transform obj in All_Transforms)
		{
			obj.gameObject.SetActive(b);
		}
		// レーザー
		Is_Laser = b;
		lasear.SetActive(false);
	}
}