using System;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class TempPlayer : MonoBehaviour
{
	[Header("移動　プロパティ")]
	public float speed;
	public float SpeedUpModifier;

	[Header("ミサイル　プロパティ")]
	public float missleWaitTime;
	public bool  activeMissile;

	private float canPlayMissileTime;

	private void OnEnable()
	{
		//プール化したため、ここでイベント発生時の処理を入れとく
		PowerUpManager.Instance.AddEvent(PowerUpType.PowerUp_SpeedUp, SpeedUp);
		PowerUpManager.Instance.AddEvent(PowerUpType.PowerUp_Missile, ActiveMissile);
	}

	private void OnDisable()
	{
		PowerUpManager.Instance.RemoveEvent(PowerUpType.PowerUp_SpeedUp, SpeedUp);
		PowerUpManager.Instance.RemoveEvent(PowerUpType.PowerUp_Missile, ActiveMissile);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			PowerUpManager.Instance.Excute();
		}

		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");
		transform.position = transform.position + new Vector3(x, y, 0) * Time.deltaTime * speed;

		if(Input.GetKey(KeyCode.Space))
		{
			//ミサイル発射パワーアップ導入され、そして発射できる場合
			if(Time.time >= canPlayMissileTime && activeMissile)
			{
				//ミサイル発射
				Object_Instantiation.Object_Reboot("Player_Missile", transform.position, transform.right);
				//次の発射時間を設定する
				canPlayMissileTime = missleWaitTime + Time.time;
			}
		}		
	}

	private void OnTriggerEnter(Collider col)
	{
		if(col.tag == "Item")
		{
			var item = col.GetComponent<Item>();
			if (item.itemType != ItemType.Item_KillAllEnemy)
				PowerUpManager.Instance.ApplyPowerUpSelection();
			else
				PowerUpManager.Instance.KillingExcute();
		}
	}

	/// <summary>
	/// スピード変更関数
	/// ここに引数なしに注目してください。
	/// でないと、AddEvent()に入れられない
	/// </summary>
	private void SpeedUp()
	{
		speed *= SpeedUpModifier;
		Debug.Log("加速!");
	}

	private void ActiveMissile()
	{
		activeMissile = true;
		Debug.Log("ミサイル導入");
	}
}

