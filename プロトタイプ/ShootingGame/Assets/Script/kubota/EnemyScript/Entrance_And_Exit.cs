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

	Vector3 pos;                //複雑な動きをするときに計算結果をxyzごとに入れまとめて動かす
	private Vector3 start_pos;
	public float b;
	float first_Time;
	float pre_Time;
	float now_Time;
	public int count;
	void Start()
    {
		start_pos = transform.position;
		first_Time = 0;
		now_Time = 0;
		pre_Time = 0;
		type = Move_Type.Front;
		count = 0;
    }

    void Update()
    {
		pos = new Vector3(0, start_pos.y, 0); //中心を決めます。今回は(0,0,0)
											  //pos = new Vector3(start_pos.x, 0, 0); //中心を決めます。今回は(0,0,0)
		now_Time = Mathf.Sin(Time.time * X_speed);
		if(first_Time == 0)
		{
			first_Time = now_Time;
		}
		if(now_Time < pre_Time)
		{
			float kari = now_Time;
			now_Time = pre_Time;
			pre_Time = kari;
		}
		if(pre_Time < first_Time && first_Time < now_Time && type == Move_Type.Back)
		{
			count += 1;
			Debug.Log("hollo");
		}

		if(count == 2)
		{
			type = Move_Type.Front;
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
				pos.x += Mathf.Sin(Time.time * X_speed) * 1;
				break;
			case Move_Type.None:
				break;
			default:
				break;
		}
		if (transform.position.z < 0)
		{
			pos.z += transform.position.z + 3 * Time.deltaTime;
		}
		if (transform.position.x > start_pos.x && transform.position.x < -20)
		{
			type = Move_Type.Back;
		}
	}
	private void X_Move()
	{
		pos.x += transform.position.x + X_speed * Time.deltaTime;
	}
}
