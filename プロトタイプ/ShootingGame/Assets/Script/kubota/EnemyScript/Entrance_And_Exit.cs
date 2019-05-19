using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance_And_Exit : MonoBehaviour
{
	public enum Move_Type
	{
		Front,
		Back,
		None,
	}
	public Move_Type type;
	public float X_speed;       //Ｘ軸の行動の時のスピード

	public Vector3 pos;                //複雑な動きをするときに計算結果をxyzごとに入れまとめて動かす
	public Vector3 start_pos;
	public float b;
	float first_Time;
	float pre_Time;
	float now_Time;
	public int count;
	private float movetime;
	void Start()
    {
		start_pos = transform.position;
		first_Time = 0;
		now_Time = 0;
		pre_Time = 0;
		type = Move_Type.Front;
		count = 0;
		movetime = 0;

	}

	void Update()
    {
		movetime += Time.deltaTime;
		pos = Vector3.zero; //中心を決めます。今回は(0,0,0)
		//pos = Vector3.zero; //中心を決めます。今回は(0,0,0)
		now_Time = Mathf.Sin(movetime * X_speed) * 0.5f;
		if(first_Time == 0)
		{
			first_Time = 0;
		}
		//if (now_Time < pre_Time)
		//{
		//	float kari = now_Time;
		//	now_Time = pre_Time;
		//	pre_Time = kari;
		//}
		if (pre_Time < first_Time && first_Time < now_Time && type == Move_Type.Back)
		{
			count += 1;
		}
		pre_Time = now_Time;
		Move();
		transform.position = pos;

	}
	private void Move()
	{
		switch (type)
		{
			case Move_Type.Front:
				X_Move();
				break;
			case Move_Type.Back:
				pos.x += transform.position.x + Mathf.Sin(movetime * X_speed) * 0.5f;
				if (count >= 1)
				{
					type = Move_Type.Front;
				}
				break;
			case Move_Type.None:
				break;
			default:
				break;
		}

		if (transform.position.x > -25.0f && count < 1 && type != Move_Type.Back)
		{
			type = Move_Type.Back;
			start_pos = transform.position;
			movetime = 0f;
		}

		if (transform.position.z < 0)
		{
			pos.z += transform.position.z + 3 * Time.deltaTime;
		}
	}
	private void X_Move()
	{
		pos.x += transform.position.x + 1 * Time.deltaTime ;
	}
}
