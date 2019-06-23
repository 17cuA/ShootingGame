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
	public float speedX;
	public float aaaa;

	float radian;           //ラジアン
	public float degree;    //角度
	public float degree_plus;
    public int rollDelay;

    public float watchZ;
	float frameCnt;

	public float saveDeg;
	public float saveDig_plus;

	bool isFollow = false;
	bool once = true;
	public bool isInc;
	public bool isDec;
	public bool isPositive;
	public bool isNegative;
	public bool isPlus;
	public bool isMinus;
    bool isDelay = false;
	private void Start()
	{
		frameCnt = 0;
		//speedX = 3.0f;
		//speed = 0.2f;
		//transform.rotation = Quaternion.Euler(0, 0, 180);
		saveDeg = 180;

	}
	void Update()
	{
		frameCnt++;
		if(frameCnt>60)
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
        velocity = gameObject.transform.rotation * new Vector3(speedX, 0, -0);
        gameObject.transform.position += velocity * Time.deltaTime;

        //プレイヤーの方向へ向く角度の値を更新前の値として保存
        saveDig_plus = degree_plus;
		//プレイヤーの方向角度の値更新
		DegreeCalculation();

		//角度の値が増えたか減ったかを判定
		if (degree_plus > saveDig_plus)
		{
			isPlus = true;
			isMinus = false;
		}
		else if (degree_plus < saveDig_plus)
		{
			isMinus = true;
			isPlus = false;
		}

		//角度を変え始める
		if (isFollow)
		{
			//一回のみ行う
			if (once)
			{
				if (playerObj.transform.position.y > transform.position.y)
				{
					isInc = false;
					isDec = true;
				}
				else
				{
					isDec = false;
					isInc = true;
				}
				once = false;
			}
		}

		//角度が増えているとき（向く方向が今の角度より大きい）
		if (isInc)
		{
			//角度を増やす
			transform.Rotate(0, 0, 1.0f);
            watchZ = transform.eulerAngles.z;

            if (transform.eulerAngles.z <= 180)
            {
                isPositive = true;
                isNegative = false;
            }
            else if (transform.eulerAngles.z > 180)
            {
                isNegative = true;
                isPositive = false;
            }

            //角度の値が減っていてdegreeが正の値の時
            //         if (isMinus && isPositive)
            //{
            //	//プレイヤーの方向に向く角度の値が今の角度の値より小さくなった時
            //	if (saveDig_plus < transform.eulerAngles.z)
            //	{
            //		//角度を減らす判定true
            //		isDec = true;
            //		isInc = false;
            //	}
            //}
            ////角度が減っていてdegreeが負の値の時
            //else if (isMinus && isNegative)
            //{
            //	//プレイヤーの方向に向く角度の値が今の角度の値より小さくなった時
            //	if (saveDig_plus < transform.eulerAngles.z)
            //	{
            //		isDec = true;
            //		isInc = false;
            //	}
            //}
            //if (isPositive)
            //{
            //    //プレイヤーの方向に向く角度の値が今の角度の値より小さくなった時
            //    if (saveDig_plus < transform.eulerAngles.z)
            //    {
            //        //角度を減らす判定true
            //        isDec = true;
            //        isInc = false;
            //    }
            //}
            ////角度が減っていてdegreeが負の値の時
            //else if (isNegative)
            //{
            //    //プレイヤーの方向に向く角度の値が今の角度の値より小さくなった時
            //    if (saveDig_plus < transform.eulerAngles.z)
            //    {
            //        isDec = true;
            //        isInc = false;
            //    }
            //}


        }
        else if (isDec)
		{
			transform.Rotate(0, 0, -1.0f);
            watchZ = transform.eulerAngles.z;


            if (transform.eulerAngles.z <= 180)
			{
				isPositive = true;
				isNegative = false;
			}
			else if (transform.eulerAngles.z > 180)
			{
				isNegative = true;
				isPositive = false;
			}

            //if (isPlus && isPositive)
            //{
            //	if (saveDig_plus > transform.eulerAngles.z)
            //	{
            //		isInc = true;
            //		isDec = false;
            //	}
            //}
            //else if (isPlus && isNegative)
            //{
            //	if (saveDig_plus > transform.eulerAngles.z)
            //	{
            //		isInc = true;
            //		isDec = false;
            //	}
            //}
            //if (isPositive)
            //{
            //    if (saveDig_plus > transform.eulerAngles.z)
            //    {
            //        isInc = true;
            //        isDec = false;
            //    }
            //}
            //else if (isNegative)
            //{
            //    if (saveDig_plus > transform.eulerAngles.z)
            //    {
            //        isInc = true;
            //        isDec = false;
            //    }
            //}


        }

        //if (isPlus && isPositive)
        //{
        //	if (degree_plus > transform.rotation.z)
        //	{
        //		isInc = true;
        //		isDec = false;
        //	}
        //}
        //else if (isMinus && isPositive)
        //{

        //}

        if(isDelay)
        {
            rollDelay++;
            if(rollDelay>5)
            {
                rollDelay = 0;
                isDelay = false;

                if (isInc)
                {
                    isInc = false;
                    isDec = true;
                }
                else if (isDec)
                {
                    isDec = false;
                    isInc = true;
                }

            }
        }

	}
	void DegreeCalculation()
	{
		//座標の差を入れる
		dif = playerObj.transform.position - transform.position;

		//ラジアンを求める
		radian = Mathf.Atan2(dif.y, dif.x);

		//角度を求める
		degree = radian * Mathf.Rad2Deg;

		//if (degree >= 0)
		//{
		//	isPositive = true;
		//	isNegative = false;
		//}
		//else if (degree < 0)
		//{
		//	isNegative = true;
		//	isPositive = false;
		//}
		if (degree < 0)
		{
			degree_plus = degree + 360.0f;
		}
		else
		{
			degree_plus = degree;
		}
	}

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag=="Player")
        {
            isDelay = true;

        }  
    }

}

