using System;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class TempPlayer : MonoBehaviour
{
	public float speed;
	public float SpeedUpModifier;

	private float missileDelay;
	public float missileDelayMax;
	public bool canPlayMissile = true;
	public bool activeMissile;


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
		if (Input.GetKeyDown(KeyCode.Q))
		{
			PowerUpManager.Instance.ApplyPowerUpSelection();
		}

		if (Input.GetKeyDown(KeyCode.E))
		{
			PowerUpManager.Instance.Excute();
		}

		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");
		transform.position = transform.position + new Vector3(x, y, 0) * Time.deltaTime * speed;

		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(activeMissile)
			{
				if(canPlayMissile)
				{
					//var missile = Obj_Storage.Storage_Data.PlayerMissile.Active_Obj();
					//missile.transform.position = transform.position;
					Object_Instantiation.Object_Reboot("Player_Missile", transform.position, Quaternion.identity);
				}
			}
		}
		
		if(!canPlayMissile)
		{
			missileDelay += Time.deltaTime;
			if(missileDelay >= missileDelayMax)
			{
				missileDelay = missileDelayMax;
				canPlayMissile = true;
			}
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

