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
	public float X_speed;
	Vector3 pos;
	Vector3 spos;
		private void Start()
	{
		spos = transform.position;
	}
	void Update()
	{
		pos = Vector3.zero; //中心を決めます。今回は(0,0,0)
		//pos.z += Mathf.Sin(Time.time * X_speed + transform.localPosition.x) * 2f;
		pos.y += Mathf.Cos(Time.time * X_speed + transform.localPosition.x) * 2f;
		pos.x +=  transform.position.x + 1 * Time.deltaTime;
		transform.position = pos;
	}
}
