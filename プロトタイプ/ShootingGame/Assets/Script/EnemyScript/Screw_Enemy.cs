/*
 * 回転死ながら出現する敵のスクリプト
 * 主に、サインカーブ、コサインカーブを使う
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screw_Enemy : MonoBehaviour
{
	public float speed;
	void Update()
	{
		//float f = 1.0f / speed;
		//float sin = Mathf.Sin(2 * Mathf.PI * f * Time.time) / 1;
		//transform.position = new Vector3(0, sin, 0);
		Vector3 pos = Vector3.zero; //中心を決めます。今回は(0,0,0)

		//範囲を指定してあげると大きな円、小さな円を実装できます。
		//pos.y += Mathf.Sin(Time.time * speed) * 2f;
		pos.x += Mathf.Sin(Time.time * speed) * 2f;
		pos.y += Mathf.Cos(Time.time * speed) * 2f;
		pos.z += 0.1f;
		transform.position += pos;
	}
}
