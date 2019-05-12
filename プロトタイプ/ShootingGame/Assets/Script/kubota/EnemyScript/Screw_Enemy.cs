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
	public float X_speed;		//Ｘ軸の行動の時のスピード
	public float Y_speed;		//Ｙ軸の行動の時のスピード
	public float Z_speed;       //Ｚ軸の行動の時のスピード

	private GameObject _parent;
	private Parent_Screw PS;

	Vector3 pos;				//複雑な動きをするときに計算結果をxyzごとに入れまとめて動かす
	private void Start()
	{
		
	}
	void Update()
	{
		pos = Vector3.zero; //中心を決めます。今回は(0,0,0)
		pos.z += Mathf.Sin(Time.time * X_speed + transform.localPosition.x) * 2f;
		pos.y += Mathf.Cos(Time.time * X_speed + transform.localPosition.x) *6f;
		pos.x +=  transform.position.x + 1 * Time.deltaTime ;
		transform.position = pos;
	}
}
