//作成日2019年04月22日
//バレットを一定方向に動かすスクリプト
//作成者:佐藤翼
using UnityEngine;
using System.Collections;

public class bullet: MonoBehaviour
{
　　　
	public Vector2 speed = new Vector2(10, 10);//スピードの変数
	public Vector2 direction = new Vector2(-1, 0);//スピードの倍率、方向を変更する

	// Update is called once per frame
	void Update()
	{
		Vector3 movement = new Vector3(speed.x * direction.x, speed.y * direction.y, 0);//スピードに方向の値をかける

		movement *= Time.deltaTime;//移動距離をフレーム（秒)でかけていく

		transform.Translate(movement);//移動処理
	}
}