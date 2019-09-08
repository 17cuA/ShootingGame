//作成者：川村良太
//ビートルの挙動

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Beetle : character_status
{
	public enum State
	{
		Front,
		Behind,
	}

	public State eState;

	GameObject smallBeamObj;		//弾取得用
    GameObject saveObj;
    GameObject childObj;
	GameObject muzzleObj;			//発射位置用
	Vector3 velocity;		
	Vector3 defaultPos;				//初期位置セーブ

	//--------------------------------------------------------------
	//主に上に上がる挙動（登場挙動）の時に使う
	//[Header("入力用　Xスピード")]
	public float speedX;
    public float default_SpeedX;
	[Header("入力用　Xスピード")]
	public float defaultSpeedX_Value;
	[Header("入力用　Yスピード")]
	public float speedY;
	public float defaultSpeedY_Value;
	[Header("入力用　Y移動速度を減速し始める大きさ")]
	public float decelerationY_Start;        //回転の減速開始をする角度
	public float speedZ;
	[Header("入力用　Zスピード")]
	public float speedZ_Value;      //Zスピードの値
	[Header("入力用　Yの移動する距離")]
	public float moveY_Max;			//Yの最大移動値
    public float default_MoveY_Max;
	public float savePosY;          //前のY座標を入れる（移動量を求めるため）
    //--------------------------------------------------------------

    //--------------------------------------------------------------
    //登場後に使う
    float moveX_DelayCnt;
    [Header("入力用　登場後動き出すまでの時間フレーム")]
    public float moveX_DelayMax;

    public float shot_DelayCnt;
    [Header("入力用　攻撃間隔の時間フレーム")]
    public float shot_DelayMax;
    public float shotRotaZ;
    public float default_Shot_DelayCnt;
    //--------------------------------------------------------------

    public bool once;				//一回だけ行う処理
    public bool isUP;				//上に上がるとき
    public bool isMove;             //登場後の動き始め「

	new void Start()
    {
        default_SpeedX = speedX;
        default_Shot_DelayCnt = shot_DelayCnt;
        smallBeamObj = Resources.Load("Bullet/SmallBeam_Bullet") as GameObject;
        muzzleObj = transform.GetChild(1).gameObject;
		defaultSpeedY_Value = speedY;
		//defaultSpeedX_Value = speedX;
		defaultPos = transform.localPosition;
		isUP = true;
        once = true;
        isMove = false;
		base.Start();
    }

    new void Update()
    {
		if (once)
		{
			transform.localPosition = defaultPos;
			switch (eState)
			{
				case State.Front:
					//transform.rotation = Quaternion.Euler(0, -90, 90);
					if (defaultSpeedX_Value > 0)
					{
						defaultSpeedX_Value *= -1;
					}
					break;

				case State.Behind:
					transform.rotation = Quaternion.Euler(0, 180, 0);
					if (defaultSpeedX_Value < 0)
					{
						defaultSpeedX_Value *= -1;
					}

					break;
			}
            once = false;
		}

		if (isUP)
		{
			savePosY = transform.position.y;

			velocity = gameObject.transform.rotation * new Vector3(speedX, speedY, -speedZ);
			gameObject.transform.position += velocity * Time.deltaTime;

			moveY_Max -= transform.position.y - savePosY;

			if (moveY_Max < decelerationY_Start)
			{
				speedY = defaultSpeedY_Value * moveY_Max / decelerationY_Start;
			}
			else if (moveY_Max < 3)
			{
				speedZ = speedZ_Value;
				speedX = defaultSpeedX_Value;
			}
			if (transform.position.z < 0)
			{
				speedZ = 0;
				speedX = 0;
				transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                isUP = false;
			}
		}
		else
		{
			velocity = gameObject.transform.rotation * new Vector3(speedX, 0, 0);
			gameObject.transform.position += velocity * Time.deltaTime;

            if (isMove)
            {
                shot_DelayCnt++;
                if (shot_DelayCnt > shot_DelayMax)
                {
                    shot_DelayCnt = 0;
                    shotRotaZ = 30f;
                    for (int i = 0; i < 3; i++)
                    {
                        saveObj = Instantiate(smallBeamObj, muzzleObj.transform.position, new Quaternion(0, 0, 0, 0));
                        saveObj.transform.rotation = Quaternion.Euler(0, 0, shotRotaZ);
                        shotRotaZ -= 30f;
                    }
                }
            }
            else if (moveX_DelayCnt > moveX_DelayMax)
            {
                isMove= true;
                switch(eState)
                {
                    case State.Front:
                        speedX = -1.5f;
                        break;

                    case State.Behind:
                        speedX = 1.5f;
                        break;

                }
            }
            else
            {
                moveX_DelayCnt++;
            }

        }
		HSV_Change();
        if (hp < 1)
        {
            Died_Process();
        }
        if (transform.localPosition.x < -35)
        {
            Destroy(this.gameObject);
        }
		base.Update();
	}
    void ResetEnemy()
    {
        speedX = default_SpeedX;
        speedY = defaultSpeedY_Value;
        moveY_Max = default_MoveY_Max;
        shot_DelayCnt = default_Shot_DelayCnt;
    }
}
