using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn_Slow : MonoBehaviour
{
	GameObject playerObj;

	Vector3 velocity;

	[SerializeField]
	[Header("移動速度")]
	private float speed;
	[SerializeField]
	[Header("回転時間")]
	private float rotation_time;

	//private Transform Player_Transform { get; set; }        // プレイヤーのデータ

	void Start()
	{

	}

	void Update()
	{
		if (playerObj == null)
		{
			playerObj = GameObject.FindGameObjectWithTag("Player");
		}
		else
		{
			// 向きを切り替える処理
			float step = speed * Time.deltaTime;

			Vector3 Direction = playerObj.transform.position - transform.position;

			//指定した方向にゆっくり回転する場合
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(Direction), step);

			transform.position = transform.right * speed;

		}

	}
}
