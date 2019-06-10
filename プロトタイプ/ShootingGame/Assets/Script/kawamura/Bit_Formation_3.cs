using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bit_Formation_3 : MonoBehaviour
{
	enum BitState
	{
		Circular,       //初期位置（円運動）
		Stay,           //停止状態
		Follow,         //追従状態
		Oblique,        //斜め撃ち状態
		Return,         //戻ってきている状態
	}

	[SerializeField]
	BitState bState;

	GameObject playerObj;
	public GameObject parentObj;           //親のオブジェクト
	public GameObject followPosObj;        //プレイヤーを追従するときの位置オブジェクト
	GameObject otherBitObj_1;
	GameObject obliquePosObj;
	ObliquePosTop opt;
	ObliquePosUnder opu;

	GameObject bitsObj;

	Bit_Formation otherBF_1;
	Bits bts;
	public float speed;             //戻るときのスピード
	float step;                     //スピードを計算して入れる

	int num;

	string myName;
	private Quaternion Direction;   //オブジェクトの向きを変更する時に使う

	bool isMove = false;
	bool isStay = false;            //留まっている状態かどうか
	bool isReturn = false;          //元の位置に戻ってくる状態かどうか
	bool isCircular = false;
	bool isFollow = false;          //プレイヤーを追従する位置に向かっているかどうか
	bool isOblique = false;         //斜め撃ちの位置に向かっているかどうか

	void Start()
	{
		speed = 50;
		num = 0;
		//bitsObj = GameObject.Find("Bits");
		//bts = bitsObj.GetComponent<Bits>();

		bState = BitState.Circular;

		playerObj = GameObject.FindGameObjectWithTag("Player");
		//親のオブジェクト取得
		parentObj = transform.parent.gameObject;
		//自分の名前取得
		myName = gameObject.name;

		if (myName == "Bit_Top")
		{
			followPosObj = GameObject.Find("FollowPosFirst");

			otherBitObj_1 = GameObject.Find("Bit_Under");
			otherBF_1 = otherBitObj_1.GetComponent<Bit_Formation>();

			obliquePosObj = GameObject.Find("ObliquePosTop");
			opt = obliquePosObj.GetComponent<ObliquePosTop>();
			opu = null;
		}
		else if (myName == "Bit_Under")
		{
			followPosObj = GameObject.Find("FollowPosSecond");

			otherBitObj_1 = GameObject.Find("Bit_Top");
			otherBF_1 = otherBitObj_1.GetComponent<Bit_Formation>();

			obliquePosObj = GameObject.Find("ObliquePosUnder");
			opu = obliquePosObj.GetComponent<ObliquePosUnder>();
			opt = null;
		}
	}

	void Update()
	{
		//スピード計算
		step = speed * Time.deltaTime;

		if (bState != BitState.Return)
		{
			if(Input.GetKeyDown(KeyCode.T) || Input.GetButtonDown("Fire3"))
			{
				num++;
				if (num > 2)
				{
					num = 0;
				}
				ChangeState();
			}
		}

		//戻ってくる状態なら
		if (bState == BitState.Circular)
		{
			if(isCircular)
			{
				//親設定解除
				transform.parent = null;
				//指定したスピードで親の位置（元の位置）に戻る【parentObjの位置までstepのスピードで戻る】
				transform.position = Vector3.MoveTowards(transform.position, parentObj.transform.position, step);

			}

			if(isMove)
			{
				//親と同じ位置に戻ったら
				if (transform.position == parentObj.transform.position)
				{
					isCircular = false;

					//戻ってくる状態false
					isMove = false;
					//親子関係を戻す
					transform.parent = parentObj.transform;
				}
			}
		}
		//追従する位置へ向かう状態
		else if (bState == BitState.Follow)
		{
			if (isFollow)
			{
				//親設定解除
				transform.parent = null;
				transform.rotation = Quaternion.Euler(0, 0, 0);

				//指定したスピードで追従する位置に行く【followPosObjの位置までstepのスピードで】
				transform.position = Vector3.MoveTowards(transform.position, followPosObj.transform.position, step);

			}
			if(isMove)
			{
				//追従する位置に行ったら
				if (transform.position == followPosObj.transform.position)
				{
					isFollow = false;

					//追従する位置へ向かう状態false
					isMove = false;
					//追従する位置のオブジェクトを親に設定
					transform.parent = followPosObj.transform;
					transform.rotation = followPosObj.transform.rotation;
				}
			}
		}
		//斜め撃ち
		else if (bState == BitState.Oblique)
		{
			if (isOblique)
			{
				//親設定解除
				transform.parent = null;
				//指定したスピードで追従する位置に行く【followPosObjの位置までstepのスピードで】
				transform.position = Vector3.MoveTowards(transform.position, obliquePosObj.transform.position, step);

			}

			if(isMove)
			{
				//追従する位置に行ったら
				if (transform.position == obliquePosObj.transform.position)
				{
					isOblique = false;

					//斜め撃ちの位置へ向かう状態false
					isMove = false;
					//追従する位置のオブジェクトを親に設定
					transform.parent = obliquePosObj.transform;
					transform.rotation = obliquePosObj.transform.rotation;
					//if (opt == null)
					//{
					//	transform.rotation = Quaternion.Euler(0, playerObj.transform.rotation.y, opu.rotaZ);

					//}
					//else if (opu == null)
					//{
					//	transform.rotation = Quaternion.Euler(0, playerObj.transform.rotation.y, opt.rotaZ);

					//}

				}
			}

		}

		if (Input.GetKeyDown(KeyCode.V) || Input.GetButtonDown("Fire3"))
		{
			if (transform.parent != parentObj.transform)
			{
				//方向転換させる関数の呼び出し
				Change_In_Direction();
			}
		}

	}

	//ビットンの方向転換
	private void Change_In_Direction()
	{
		//方向に−１をかけて反転した物を入れる
		transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
	}

	void ChangeState()
	{
		switch(num)
		{
			case 0:
				bState = BitState.Circular;
				isCircular = true;
				isMove = true;
				break;

			case 1:
				bState = BitState.Oblique;
				isOblique = true;
				isMove = true;
				break;

			case 2:
				bState = BitState.Follow;
				isFollow = true;
				isMove = true;
				break;

		}
	}
}