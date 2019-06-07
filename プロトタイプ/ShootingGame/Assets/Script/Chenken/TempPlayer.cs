using System;
using System.Collections.Generic;
using UnityEngine;
using Power;

/// <summary>
/// プレイヤーに付け足し、修正箇所をこのスクリプトを参照
/// </summary>
public class TempPlayer : MonoBehaviour
{
	public void Awake()
	{
		//ここでプレイヤーが取得できる全てのパワーをパワーマネージャーに入れとく
		PowerManager.Instance.AddPower(new Power_Shield(PowerType.POWER_SHIELD, 3));
		PowerManager.Instance.AddPower(new Power_BulletUpgrade(PowerType.POWER_BULLET_UPGRADE, 5));

		//説明は42行目に移行
		PowerManager.Instance.GetPower(PowerType.POWER_SHIELD).onPickCallBack += () => { Debug.Log("イベント発生！依頼関数実行"); };
	}

	/// <summary>
	/// 更新
	/// </summary>
	public void Update()
	{
		//パワーマネージャー更新
		PowerManager.Instance.OnUpdate(Time.deltaTime);
	}

	/// <summary>
	/// 当たり判定
	/// すり抜く場合
	/// </summary>
	/// <param name="collision"></param>
	private void OnTriggerEnter(Collider collision)
	{
		//アイテムの場合
		if (collision.tag == "Item")
		{
			//アイテムのパワータイプを取得
			PowerType type = collision.GetComponent<Item>().powerType;

			//外からのアイテム再取得時の処理　
			//() => { Debug.Log("イベント発生！依頼関数実行"); };
			//上記部分を含め処理する

			//PowerManager.Instance.Pick(type);実行する前に、依頼関数をイベントに入れておけば、同時に実行することができる
			//パワー内部　＋　パワー外部　同時に実行
			//何故なら、パワーアップする時、内部データに影響するだけでなく、外部（エフェクト、音再生とか）も影響する

			//新たに生成したパワーをパワーマネージャーで管理
			PowerManager.Instance.Pick(type);		
		}

		//弾の場合
		if(collision.tag == "Enemy_Bullet")
		{
			//シールドがある場合
			if(PowerManager.Instance.HasPower(PowerType.POWER_SHIELD))
			{
				//シールドまだ消滅してない場合
				if (!PowerManager.Instance.GetPower(PowerType.POWER_SHIELD).IsLost)
				{
					//シールドのHp　-1
					//変更必要
					int value = PowerManager.Instance.GetPower(PowerType.POWER_SHIELD).value--;
					Debug.Log(value);
				}
			}
		}

		if(collision.tag == "Enemy")
		{
			//シールドがある場合
			if (PowerManager.Instance.HasPower(PowerType.POWER_SHIELD))
			{
				//シールドまだ消滅してない場合
				if (!PowerManager.Instance.GetPower(PowerType.POWER_SHIELD).IsLost)
				{
					//シールドのHp　-1
					//変更必要
					int value = PowerManager.Instance.GetPower(PowerType.POWER_SHIELD).value--;
					Debug.Log(value);
				}
			}
		}
	}
}

