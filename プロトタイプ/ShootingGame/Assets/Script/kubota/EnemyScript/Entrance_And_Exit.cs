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
	public float Z_speed;
	private Vector3 pos;                //複雑な動きをするときに計算結果をxyzごとに入れまとめて動かす
	private Vector3 start_pos;
	float first_Time;
	float pre_Time;
	float now_Time;
	private int count;
	private float movetime;
	public float _return;


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
		pos = new Vector3(0,transform.position.y,0); //中心を決めます。今回は(0,0,0)
		Calc_ExitPosition();
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
				pos.x += transform.position.x + Mathf.Sin(movetime * X_speed) * _return;
				if (count >= 2)
				{
					type = Move_Type.Front;
				}
				break;
			case Move_Type.None:
				break;
			default:
				break;
		}

		if (transform.position.x > -32.0f && count < 2 && type != Move_Type.Back)
		{
			type = Move_Type.Back;
			start_pos = transform.position;
			movetime = 0f;
		}

		if (transform.position.z < 0 )
		{
			Z_Move();
		}
	}
	private void X_Move()
	{
		pos.x += transform.position.x + X_speed * Time.deltaTime ;
	}

	private void Z_Move()
	{
		pos.z += transform.position.z + Z_speed * Time.deltaTime;
	}

	private void Calc_ExitPosition()
	{
		now_Time = Mathf.Sin(movetime * X_speed) * _return;
		if(first_Time == 0)
		{
			first_Time = now_Time;
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
	}
}
