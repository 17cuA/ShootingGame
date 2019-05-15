using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round_Trip_Enemy : MonoBehaviour
{
	public float X_speed;		//Ｘ軸の行動の時のスピード
	public float Y_speed;		//Ｙ軸の行動の時のスピード
	public float Z_speed;       //Ｚ軸の行動の時のスピード
	public int a;
	Vector3 pos;				//複雑な動きをするときに計算結果をxyzごとに入れまとめて動かす
	private float start_pos_x;
    void Start()
    {
		start_pos_x = transform.position.x;
		pos = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
		//pos = Vector3.zero;
		pos = new Vector3(start_pos_x,0,0); //中心を決めます。今回は(0,0,0)
		float x1 = (Mathf.Pow(Time.time - a, 3) / 2) * X_speed;
		float x2 = Mathf.Pow(Time.time, 2) * Y_speed;
		float x3 = Time.time * Z_speed;
		pos.x += x1 - x2 ;
		transform.position = -pos;
    }
}
