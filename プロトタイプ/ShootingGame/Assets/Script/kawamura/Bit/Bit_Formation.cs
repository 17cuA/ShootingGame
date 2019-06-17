//ビットンを任意の位置に留めたり、元の位置（プレイヤーの周りを回る）に戻したりするスクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bit_Formation : MonoBehaviour
{
    GameObject parentObj;           //親のオブジェクト
    GameObject followPosObj;        //プレイヤーを追従するときの位置オブジェクト

    public float speed;             //戻るときのスピード
    float step;                     //スピードを計算して入れる

    string myName;
	private Quaternion Direction;   //オブジェクトの向きを変更する時に使う  

	bool isStay = false;            //留まっている状態かどうか
    bool isBack = false;			//元の位置に戻ってくる状態かどうか
    bool isFollow = false;          //プレイヤーを追従する状態かどうか

    void Start()
    {
        //親のオブジェクト取得
        parentObj = transform.parent.gameObject;
        //自分の名前取得
        myName = gameObject.name;

        if(myName=="Bit_Top")
        {
            followPosObj = GameObject.Find("FollowPosFirst");
        }
        else if (myName == "Bit_Left")
        {
            followPosObj = GameObject.Find("FollowPosSecond");
        }
        else if (myName == "Bit_Under")
        {
            followPosObj = GameObject.Find("FollowPosThird");
        }
        else if (myName == "Bit_Right")
        {
            followPosObj = GameObject.Find("FollowPosFourth");
        }

    }

    void Update()
    {
        //スピード計算
        step = speed * Time.deltaTime;

		if (Input.GetKeyDown(KeyCode.V) || Input.GetButtonDown("Fire1"))
		{
			if(transform.parent!=parentObj.transform)
			{
				//方向転換させる関数の呼び出し
				Change_In_Direction();
			}
		}


		//Tキーで留めるまたは戻ってこさせる
		if (Input.GetKeyDown(KeyCode.T))
        {

            Bit_Stay();
        }

        if(Input.GetKeyDown(KeyCode.Y))
        {
            Bit_Follow();
        }

        //戻ってくる状態なら
        if (isBack)
        {
            //指定したスピードで親の位置（元の位置）に戻る【parentObjの位置までstepのスピードで戻る】
            transform.position = Vector3.MoveTowards(transform.position, parentObj.transform.position, step);

            //親と同じ位置に戻ったら
            if (transform.position == parentObj.transform.position)
            {
                //戻ってくる状態false
                isBack = false;
                //親子関係を戻す
                transform.parent = parentObj.transform;
            }
        }
		//追従する位置へ向かう状態
        else if(isFollow)
        {
			//親設定解除
            transform.parent = null;
			//指定したスピードで追従する位置に行く【followPosObjの位置までstepのスピードで】
			transform.position = Vector3.MoveTowards(transform.position, followPosObj.transform.position, step);

            //追従する位置に行ったら
            if (transform.position == followPosObj.transform.position)
            {
				//追従する位置へ向かう状態false
				isFollow = false;
                //追従する位置のオブジェクトを親に設定
                transform.parent = followPosObj.transform;
            }
        }

    }

	//ビットンの方向転換
	private void Change_In_Direction()
	{
		//方向に−１をかけて反転した物を入れる
		Direction *= new Quaternion(0, -1, 0, 0);
		transform.rotation = Direction;
	}

	void Bit_Stay()
    {
        //親の設定がされていた時
        if (transform.parent)
        {
            //親子関係を解除。また、戻って来ない状態にする
            transform.parent = null;
            isBack = false;
        }
        //親の設定がされていないとき
        else
        {
            //戻ってくる状態true
            isBack = true;
        }

    }

    void Bit_Follow()
    {
        //親の設定がされていた時
        if (transform.parent)
        {
            if(transform.parent==followPosObj.transform)
            {
                isFollow = false;
                transform.parent = null;
                isBack = true;
            }
            else
            {
                isFollow = true;
            }
        }
        else
        {
            isFollow = true;
        }

    }
}
