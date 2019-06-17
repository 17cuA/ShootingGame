using System;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class TempPlayer : MonoBehaviour
{
	[Header("移動　プロパティ")]
	public float speed;				//移動速度
	public float speedUpModifier;	//スピードアップ変更率(1.5 -> 150% UP!!!)

	[Header("ミサイル　プロパティ")]
	public float missleWaitTime;	//ミサイル発射間隔時間
	public bool  activeMissile;     //ミサイルは導入されたかどうか


	private float canPlayMissileTime;

	private GameObject[] bitsPrefabs = new GameObject[4];
	private GameObject[] bitGameObject = new GameObject[4];
	public int bitIndex = 0;

	private void Start()
	{	
		bitsPrefabs[0] = Resources.Load("Bit_First")  as GameObject;
		bitsPrefabs[1] = Resources.Load("Bit_Second") as GameObject;
		bitsPrefabs[2] = Resources.Load("Bit_Third")  as GameObject;
		bitsPrefabs[3] = Resources.Load("Bit_Fourth") as GameObject;
	}

	private void OnEnable()
	{
		//プール化したため、ここでイベント発生時の処理を入れとく
		PowerUpManager.Instance.AddEvent(PowerUpType.PowerUp_SpeedUp, SpeedUp);
		PowerUpManager.Instance.AddEvent(PowerUpType.PowerUp_Missile, ActiveMissile);
		PowerUpManager.Instance.AddEvent(PowerUpType.PowerUp_Option, CreateBit);
	}

	private void OnDisable()
	{
		PowerUpManager.Instance.RemoveEvent(PowerUpType.PowerUp_SpeedUp, SpeedUp);
		PowerUpManager.Instance.RemoveEvent(PowerUpType.PowerUp_Missile, ActiveMissile);
		PowerUpManager.Instance.RemoveEvent(PowerUpType.PowerUp_Option, CreateBit);
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

		if(Input.GetKey(KeyCode.Q))
		{
			//ミサイル発射パワーアップ導入され、そして発射できる場合
			if(Time.time >= canPlayMissileTime && activeMissile)
			{
				//ミサイル発射
				Object_Instantiation.Object_Reboot("Player_Missile", transform.position, transform.right);

				for(var i = 0; i < bitIndex; ++i)
				{
					Object_Instantiation.Object_Reboot("Player_Missile", bitGameObject[i].transform.position, bitGameObject[i].transform.right);
				}
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
		speed *= speedUpModifier;
		Debug.Log("加速!");
	}

	private void ActiveMissile()
	{
		activeMissile = true;
		Debug.Log("ミサイル導入");
	}

	private void CreateBit()
	{
		switch(bitIndex)
		{
			case 0:
			case 1:
			case 2:
			case 3:
				bitGameObject[bitIndex] = Instantiate(bitsPrefabs[bitIndex],transform.position,transform.rotation) as GameObject;
				bitIndex++;
				break;
		}
	}
}

