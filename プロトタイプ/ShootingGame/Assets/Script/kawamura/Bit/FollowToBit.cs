using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowToBit : MonoBehaviour
{
	public GameObject playerObj;
	GameObject previousBitObj;

	public Vector3[] previousBitPos;
	public Vector3 pos;
	private Quaternion Direction;   //オブジェクトの向きを変更する時に使う  

	public int cnt;
	int array_Num;

	public string myName;

	bool one = true;
	bool once = true;
	public bool check = false;		//配列すべてに値が入っているかの判定
	public bool isMove = false;
	void Start()
	{
		Direction = transform.rotation;

		myName = gameObject.name;
		if (myName == "FollowPosSecond")
		{
			previousBitObj = GameObject.Find("FollowPosFirst");
		}
		array_Num = 8;
		previousBitPos = new Vector3[array_Num];

		once = true;
		pos = previousBitObj.transform.position;

	}

	void Update()
	{
		//前のビットの座標と今のビットの座標が違うとき　かつ　位置配列すべてに値が入っていないとき
		if (pos != previousBitObj.transform.position && !check)
		{
			//位置配列にビットの位置を入れる
			//playerPos[cnt] = playerObj.transform;
			previousBitPos[cnt] = previousBitObj.transform.position;
			//if(one)
			//{
			//	transform.position=previousBitPos[0];
			//	one=false;
			//}
			cnt++;
			if (cnt > array_Num - 1)
			{
				cnt = 0;
				check=true;
			}
		}
		//配列の最後まで値が入っていたら
		//if (previousBitPos[array_Num - 1] != null)
		//{
		//	check = true;
		//}

		if (once)
		{
			//前のビットの座標が動いていないとき
			if (pos == previousBitObj.transform.position)
			{
				isMove = false;
			}
			//前のビットの座標が動いていたとき
			else
			{
				isMove = true;
				//前のビットのtransform保存
				pos = previousBitObj.transform.position;

			}
		}

		//前のビットが動いていて位置配列すべてに値が入っているとき
		if (isMove && check)
		{
			//isMove = false;
			//自分の位置を配列に入っている位置に
			//transform.position = playerPos[cnt].position;
			transform.position = previousBitPos[cnt];

			//自分の位置を移動したのでその位置を今、前のビットのいる位置で更新
			//playerPos[cnt] = playerObj.transform;
			previousBitPos[cnt] = previousBitObj.transform.position;


			cnt++;
			if (cnt > array_Num - 1)
			{
				cnt = 0;
			}
		}
		if (Input.GetKeyDown(KeyCode.V) || Input.GetButtonDown("Fire1"))
		{
			//方向転換させる関数の呼び出し
			Change_In_Direction();

		}

	}
	//ビットンの方向転換
	private void Change_In_Direction()
	{
		//方向に−１をかけて反転した物を入れる
		Direction *= new Quaternion(0, -1, 0, 0);
		transform.rotation = Direction;
	}

}
