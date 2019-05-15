﻿/*
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
	public int a;
	private GameObject _parent;
	private Parent_Screw PS;
	private float distance_Z;
	private float distance_Y;

	Vector3 pos;				//複雑な動きをするときに計算結果をxyzごとに入れまとめて動かす
	private float start_pos_y;
	private void Start()
	{
		this.gameObject.tag = "Enemy";
		_parent = transform.parent.gameObject;
		PS = _parent.GetComponent<Parent_Screw>();
		distance_Z = PS.moving_distance_Z;
		distance_Y = PS.moving_distance_Y;
		start_pos_y = transform.position.y;
		pos = Vector3.zero;
	}
	void Update()
	{
		pos = new Vector3(0,start_pos_y,0); //中心を決めます。今回は(0,0,0)
        pos.z += Mathf.Sin(Time.time * X_speed + transform.localPosition.x) * distance_Z;
        pos.y += Mathf.Cos(Time.time * X_speed + transform.localPosition.x) * distance_Y;
        pos.x += transform.position.x + 1 * Time.deltaTime * -1;
        //pos.y = (1-Mathf.Cos(Mathf.PI * Time.time)) /2;
        //pos.y = (((int)(Time.time)^3)/3)-Y_speed*((int)(Time.time)^2)+((3*Y_speed - 2)*Time.time);
        //pos.z +=  transform.position.z + 1 * Time.deltaTime * -1;
        //float a = Time.time;
        //for (int i = 0; i < 3; i++) a *= Time.time;
        //float b = -(a + Z_speed) / 3.0f;
        //float c = -Y_speed * Time.time;
        //pos.x = b - c;

        //----------------------------------------
        //float x1 = (Mathf.Pow(Time.time - a,3)/2) * X_speed;
        //float x2 = Mathf.Pow(Time.time, 2) * Y_speed;
        //float x3 = Time.time * Z_speed;
        //pos.x += x1 - x2 + x3 ;
        //-----------------------------------------
        transform.position = pos;
		//Debug.Log(power_3());
	}

	float power_3()
	{
		return Mathf.Pow(2,3);
	}
}
