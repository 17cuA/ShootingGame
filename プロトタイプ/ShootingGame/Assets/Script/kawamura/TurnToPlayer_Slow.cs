using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnToPlayer_Slow : MonoBehaviour
{
	public GameObject playerObj; // 注視したいオブジェクトをInspectorから入れておく
	public Quaternion qqq;
	Transform ttt;

	Vector3 dif;            //対象と自分の座標の差を入れる変数
	Vector3 velocity;
	Vector3 vvv;

	public float speed;
	float speedX;
	public float aaaa;

	float radian;           //ラジアン
	public float degree;    //角度
	public float degree_plus;

	float frameCnt;

	public float saveDeg;

	bool isFollow = false;
	bool once = true;
	public bool isInc;
	public bool isDec;
	// Update is called once per frame
	private void Start()
	{
		frameCnt = 0;
		speedX = 3.0f;
		//speed = 0.2f;
		//transform.rotation = Quaternion.Euler(0, 0, 180);
		saveDeg = 180;

	}
	void Update()
	{
		frameCnt++;
		if(frameCnt>180)
		{
			isFollow = true;
		}
		if (playerObj == null)
		{
			playerObj = GameObject.FindGameObjectWithTag("Player");
		}

		//----
		//Vector3 targetDir = new Vector3(transform.position.x, transform.position.y, playerObj.transform.position.z) - transform.position;
		//Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, speed * Time.deltaTime, 0f);
		//transform.rotation = Quaternion.LookRotation(newDir);
		//----

		//----
		//velocity = gameObject.transform.rotation * new Vector3(speedX, 0, -0);
		//gameObject.transform.position += velocity * Time.deltaTime;

		DegreeCalculation();

		//if(isFollow)
		//{
		//	if(once)
		//	{
		//		if (playerObj.transform.position.y > transform.position.y)
		//		{
		//			isInc = false;
		//			isDec = true;
		//		}
		//		else
		//		{
		//			isDec = false;
		//			isInc = true;
		//		}
		//		once = false;
		//	}


		//}

		//if(isInc)
		//{
		//	transform.Rotate(0, 0, 1.0f);

		//}
		//else if(isDec)
		//{
		//	transform.Rotate(0, 0, -1.0f);

		//}

		//if (degree == saveDeg)
		//{

		//}
		//else if (degree < 0)
		//{
		//	if (degree < saveDeg)
		//	{
		//		saveDeg -= speed;
		//		transform.rotation = Quaternion.Euler(0, 0, saveDeg);
		//	}
		//	else if (degree > saveDeg)
		//	{
		//		saveDeg += speed;
		//		transform.rotation = Quaternion.Euler(0, 0, saveDeg);

		//	}
		//}
		//else if (degree > 0)
		//{
		//	if (degree > saveDeg)
		//	{
		//		saveDeg += speed;
		//		transform.rotation = Quaternion.Euler(0, 0, saveDeg);

		//	}
		//	else if (degree < saveDeg)
		//	{
		//		saveDeg -= speed;
		//		transform.rotation = Quaternion.Euler(0, 0, saveDeg);

		//	}
		//}

		qqq = new Quaternion(0, 0, degree,180);
		vvv=new Vector3(0,0,degree);
		//ttt.rotation = Quaternion.Euler(0, 0, degree);
		//----
		// 補完スピードを決める

		// ターゲット方向のベクトルを取得
		Vector3 relativePos = playerObj.transform.position - this.transform.position;
		// 方向を、回転情報に変換
		Quaternion rotation = Quaternion.LookRotation(relativePos);
		// 現在の回転情報と、ターゲット方向の回転情報を補完する
		transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, speed);

		//transform.rotation = qqq;
		////----

		//-----
		//float dx = playerObj.transform.position.x - transform.position.x;
		//float dy = playerObj.transform.position.y - transform.position.y;
		//float rad = Mathf.Atan2(dy, dx);
		//aaaa = rad * Mathf.Rad2Deg;
		//-----


		// ターゲット方向のベクトルを取得
		//Vector3 relativePos = playerObj.transform.position - this.transform.position;

		// 方向を、回転情報に変換
		//Quaternion rotation = Quaternion.LookRotation(relativePos);

		// 現在の回転情報と、ターゲット方向の回転情報を補完する
		//transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, speed);
		//transform.rotation = Quaternion.Slerp(this.transform.rotation, qqq, speed);
	}
	void DegreeCalculation()
	{
		//座標の差を入れる
		dif = playerObj.transform.position - transform.position;

		//ラジアンを求める
		radian = Mathf.Atan2(dif.y, dif.x);

		//角度を求める
		degree = radian * Mathf.Rad2Deg;

		if (degree < 0)
		{
			degree_plus = degree + 360.0f;
		}
		else
		{
			degree_plus = degree;
		}
	}

}

