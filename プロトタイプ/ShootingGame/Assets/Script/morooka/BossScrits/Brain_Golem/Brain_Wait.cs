﻿//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2019/12/20
//----------------------------------------------------------------------------------------------
// 2面ボス本体
//----------------------------------------------------------------------------------------------
// 2019/12/20　パーツの死亡判定確認
//----------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Brain_Wait : character_status
{
	[SerializeField, Tooltip("ダメージ受けるパーツ")] private List<Brain_Parts> damagedParts;
	[SerializeField, Tooltip("触手のパーツ_バルカン")] private List<Brain_Parts> balkanTentacles;
	[SerializeField, Tooltip("触手のパーツ_コンテナ")] private List<Brain_Parts> containerTentacles;
	[SerializeField, Tooltip("顔のアニメーション")] private Animation FaceAnimation;
	[SerializeField, Tooltip("タイムライン制御")] private PlayableDirector playable_Map;
	//[SerializeField, Tooltip("タイムラインストッパー")] private 

	private bool Is_Active { get; set; }
	WaitLoopTrigger waitLoopTrigger = null;

	private bool Is_Laser { get; set; }
	protected int ActionStep { get; set; }                              // 攻撃手順指示番号
	private float DeathTime_Cnt { get; set; }		// 死ぬ時間カウンター
	private float DeathTime_Max { get; set; }       // 死ぬ時間

	new private void Start()
	{
		foreach (Transform obj in transform)
		{
			obj.gameObject.SetActive(false);
		}
		Is_Active = false;
		Is_Laser = false;
		waitLoopTrigger = FindObjectOfType<WaitLoopTrigger>();
		ActionStep = 0;
		DeathTime_Max = 60.0f * 3.0f;
	}

    new private void Update()
    {
		#region 自動死亡時間
		DeathTime_Cnt += Time.deltaTime;
		if (DeathTime_Max < DeathTime_Cnt)
		{
			if (playable_Map.state == PlayState.Paused)
			{
				playable_Map.time = 285.25f;
				playable_Map.Play();
			}
		}
		#endregion

		#region 起動状態(仮)確認
		if (!Is_Active)
		{
			if (transform.position.x < 10.0f)
			{
				foreach (Transform obj in transform)
				{
					obj.gameObject.SetActive(true);
				}
				Is_Active = true;
			}
			else
			{
				return;
			}
		}
		#endregion
		if (Is_PartsNotAlive())
		{
			if (playable_Map.state == PlayState.Paused)
			{
				playable_Map.time = 285.25f;
				playable_Map.Play();
			}
		}

		// レーザーの攻撃
		if (Is_Laser)
		{
			// 口開く、エネルギー溜めパーティクル再生
			if (ActionStep == 0)
			{
				FaceAnimation.Play("Open");
				ActionStep++;
			}
			// パーティクル終了時
			else if (ActionStep == 1)
			{
				if (!FaceAnimation.IsPlaying("Open"))
				{
					ActionStep++;
				}
			}
			// 口閉じる
			else if (ActionStep == 2)
			{
				FaceAnimation.Play("Close");
				ActionStep++;
			}
			else if (ActionStep == 3)
			{
				if (!FaceAnimation.IsPlaying("Close"))
				{
					ActionStep = 0;
					Is_Laser = false;
					GetComponent<Collider>().enabled = true;
				}
			}
		}
    }

	private bool Is_PartsNotAlive()
	{
		bool flag = false;

		// 生存パーツリストの確認
		foreach(var parts in damagedParts)
		{
			if(parts.Is_Dead)
			{
				continue;
			}
			if(parts.hp > 0)
			{
				flag = false;
			}
			else
			{
				flag = true;
				parts.Died_Process();
			}
		}
		return flag;
	}

	new private void OnTriggerEnter(Collider other)
	{
		if(!Is_Laser && other.tag == "Player")
		{
			Is_Laser = true;
			GetComponent<Collider>().enabled = false;
		}
	}
}