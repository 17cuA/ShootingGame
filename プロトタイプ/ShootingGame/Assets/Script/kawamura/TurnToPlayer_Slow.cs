using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnToPlayer_Slow : MonoBehaviour
{
	public GameObject playerObj; // 注視したいオブジェクトをInspectorから入れておく

	// Update is called once per frame
	void Update()
	{
		if (playerObj == null)
		{
			playerObj = GameObject.FindGameObjectWithTag("Player");
		}

		// 補完スピードを決める
		float speed = 0.1f;
		// ターゲット方向のベクトルを取得
		Vector3 relativePos = playerObj.transform.position - this.transform.position;
		// 方向を、回転情報に変換
		Quaternion rotation = Quaternion.LookRotation(relativePos);
		// 現在の回転情報と、ターゲット方向の回転情報を補完する
		transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, speed);

	}
}

