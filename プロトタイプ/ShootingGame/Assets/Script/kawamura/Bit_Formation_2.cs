using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bit_Formation_2 : MonoBehaviour
{
	enum BitState
	{
		Circular,		//初期位置（円運動）
		Stay,			//停止状態
		Follow,			//追従状態
		Oblique,		//斜め撃ち状態
		Return,			//戻ってきている状態
	}

	[SerializeField]
	BitState bState;

    public GameObject parentObj;           //親のオブジェクト
    public GameObject followPosObj;        //プレイヤーを追従するときの位置オブジェクト
	GameObject otherBitObj_1;
	GameObject obliquePosObj;

	GameObject bitsObj;

	Bit_Formation otherBF_1;
	Bits bts;
	public float speed;             //戻るときのスピード
    float step;                     //スピードを計算して入れる

	int num;

    string myName;
	private Quaternion Direction;   //オブジェクトの向きを変更する時に使う  

	bool isStay = false;            //留まっている状態かどうか
    bool isReturn = false;			//元の位置に戻ってくる状態かどうか
    bool isFollow = false;          //プレイヤーを追従する位置に向かっているかどうか
	bool isOblique = false;			//斜め撃ちの位置に向かっているかどうか

    void Start()
    {
		//bitsObj = GameObject.Find("Bits");
		//bts = bitsObj.GetComponent<Bits>();

		bState = BitState.Circular;
        //親のオブジェクト取得
        parentObj = transform.parent.gameObject;
        //自分の名前取得
        myName = gameObject.name;

        if(myName=="Bit_Top")
        {
            followPosObj = GameObject.Find("FollowPosFirst");

			otherBitObj_1 = GameObject.Find("Bit_Under");
			otherBF_1 = otherBitObj_1.GetComponent<Bit_Formation>();

			obliquePosObj=GameObject.Find("ObliquePosTop");
		}
		else if (myName == "Bit_Under")
        {
            followPosObj = GameObject.Find("FollowPosSecond");

			otherBitObj_1 = GameObject.Find("Bit_Top");
			otherBF_1 = otherBitObj_1.GetComponent<Bit_Formation>();

			obliquePosObj=GameObject.Find("ObliquePosUnder");

		}
	}

    void Update()
    {
		//スピード計算
		step = speed * Time.deltaTime;

		if (bState != BitState.Return)
		{
			//Tキーで留めるまたは戻ってこさせる
			if (Input.GetKeyDown(KeyCode.T))
			{
				Bit_Stay();
			}
			else if (Input.GetKeyDown(KeyCode.Y))
			{
				Bit_Follow();
			}
			else if(Input.GetKeyDown(KeyCode.R))
			{
				Bit_Oblique();
			}
		}

		//戻ってくる状態なら
		if (bState==BitState.Return)
		{
			//指定したスピードで親の位置（元の位置）に戻る【parentObjの位置までstepのスピードで戻る】
			transform.position = Vector3.MoveTowards(transform.position, parentObj.transform.position, step);

			//親と同じ位置に戻ったら
			if (transform.position == parentObj.transform.position)
			{
				//戻ってくる状態false
				//親子関係を戻す
				transform.parent = parentObj.transform;
				transform.rotation=Quaternion.Euler(0,0,0);
				bState=BitState.Circular;
				
			}
		}
		//追従する位置へ向かう状態
		else if(bState==BitState.Follow)
		{
			if(isFollow)
			{
				//親設定解除
				transform.parent = null;
				//指定したスピードで追従する位置に行く【followPosObjの位置までstepのスピードで】
				transform.position = Vector3.MoveTowards(transform.position, followPosObj.transform.position, step);

			}
			//追従する位置に行ったら
			if (transform.position == followPosObj.transform.position)
			{
				//追従する位置へ向かう状態false
				isFollow = false;
				//追従する位置のオブジェクトを親に設定
				transform.parent = followPosObj.transform;
			}
		}
		//斜め撃ち
		else if(bState==BitState.Oblique)
		{
			if(isOblique)
			{
				//親設定解除
				transform.parent = null;
				//指定したスピードで追従する位置に行く【followPosObjの位置までstepのスピードで】
				transform.position = Vector3.MoveTowards(transform.position, obliquePosObj.transform.position, step);

			}

			//追従する位置に行ったら
			if (transform.position == obliquePosObj.transform.position)
			{
				//斜め撃ちの位置へ向かう状態false
				isOblique=false;
				//追従する位置のオブジェクトを親に設定
				transform.parent = obliquePosObj.transform;
				transform.rotation=obliquePosObj.transform.rotation;

			}

		}
		
		if (Input.GetKeyDown(KeyCode.V) || Input.GetButtonDown("Fire3"))
		{
			if(transform.parent!=parentObj.transform)
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
		transform.localScale = new Vector3 ( transform.localScale.x, transform.localScale.y*-1, transform.localScale.z);
	}

	void Bit_Stay()
    {
        //戻ってくる状態じゃなかったとき
        if (bState!=BitState.Return)
        {
			//留まる状態じゃないとき
			if (bState != BitState.Stay)
			{
				//親子関係を解除。また、戻って来ない状態にする
				bState = BitState.Stay;
				transform.parent = null;
				isReturn = false;
			}
			//留まっている状態のとき
			else
			{
				//戻ってくる状態true
				bState = BitState.Return;
				isReturn = true;
			}
		}
	}

	//追従状態にするまたは追従状態の解除をする関数
	void Bit_Follow()
	{
		//戻ってきている状態じゃないとき
		if (bState != BitState.Return)
		{
			//追従状態ではなかったとき
			if (bState != BitState.Follow)
			{
				transform.rotation = Quaternion.Euler(0,0,0);
				//追従状態にする
				isFollow = true;
				bState = BitState.Follow;
			}

			//追従状態だった時
			else if (bState == BitState.Follow)
			{
				//追従を解除して戻ってこさせる
				//isFollow = false;
				transform.parent = null;
				bState = BitState.Return;
				isReturn = true;
			}			
		}
	}

	//斜め撃ち状態にするまたは斜め撃ち状態を解除する関数
	void Bit_Oblique()
	{
		//戻ってくる状態じゃないとき
		if(bState!=BitState.Return)
		{
			//斜め撃ち状態時ではなかったとき
			if(bState!=BitState.Oblique)
			{
				//斜め撃ち状態にする
				bState=BitState.Oblique;
				isOblique=true;
			}
			//斜め撃ち状態だったら
			else if(bState==BitState.Oblique)
			{
				//戻ってくる状態にする
				bState=BitState.Return;
				//親子関係を解除
				transform.parent=null;
			}
		}
	}
}
