//作成者：川村良太
//ミニモアイの挙動　横の移動は親がして一斉に動くのでここではしない。
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Moai_Mini : character_status
{
	GameObject parentObj;
	Enemy_Moai_Mini_Group miniMoaiGroup_Script;     //親のスクリプト

	public Vector3 startPos;
	public Vector3 endPos;
	// スピード
	public float lerpSpeed = 1.0F;
	//二点間の距離を入れる
	private float distance_two;

	public int rotate_Direction = 0;	//回転する向きを決めるのに使う（1か2でランダム：2なら回転の変化値にマイナスをかける）

	public float rotaY;         //モアイはYで横回転							

	public float rotaY_Value = 0;


	public string myName;

	public bool once = true;	
	private void Awake()
	{
		myName = gameObject.name;
		parentObj = transform.parent.gameObject;
		miniMoaiGroup_Script = parentObj.GetComponent<Enemy_Moai_Mini_Group>();

		//二点間の距離を代入(スピード調整に使う)
		distance_two = Vector3.Distance(startPos, endPos);
	}

	new void Start()
	{
		
		//rotaX = transform.eulerAngles.x;
		rotaY = transform.eulerAngles.y;
		//rotaZ = transform.eulerAngles.z;
		base.Start();
	}

	new void Update()
	{
		if(once)
		{
			startPos = transform.localPosition;
			rotaY_Value = Random.Range(2, 5);
			rotate_Direction = Random.Range(1, 3);	//intだと最大値-1が範囲になるので2ではなく3を書いている

			if (rotate_Direction == 2)
			{
				rotaY_Value *= -1;
			}
			once = false;
		}

		if (miniMoaiGroup_Script.isChildRoll)
		{
			transform.localRotation = Quaternion.Euler(0, rotaY, 0);
			//rotaX += rotaX_Value;
			rotaY += rotaY_Value;
			//rotaZ += rotaZ_Value;
		}

		if (miniMoaiGroup_Script.isChildMove)
		{
			float present_Location = (Time.time * speed) / distance_two;
			transform.localPosition = Vector3.Lerp(startPos, endPos, present_Location);
			if (transform.localPosition == endPos)
			{
				miniMoaiGroup_Script.isChildMove = false;
			}
		}

		base.Update();
	}
	void ResetMoai()
	{
		transform.localPosition = new Vector3(0, 0, 0);
		transform.rotation = Quaternion.Euler(0, -90, 0);
	}
}
