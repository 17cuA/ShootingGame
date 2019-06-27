using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnToPlayer_Slow : MonoBehaviour
{
	public GameObject playerObj; // 注視したいオブジェクトをInspectorから入れておく
	List<GameObject> colList = new List<GameObject>();

	Vector3 dif;            //対象と自分の座標の差を入れる変数
	Vector3 velocity;

	public float speed;
	public float speedX;
	public float rollSpeed;

	float radian;           //ラジアン
	public float degree;    //角度
	public float degree_plus;
    public int rollDelay;

	public float frameCnt;
	public float followStartTime;
	public float followEndTime;

	public float saveDeg;
	public float saveDig_plus;

	public bool isFollow = false;
	bool once;
	public bool isInc = false;
	public bool isDec = false;
	public bool isPositive;
	public bool isNegative;
	public bool isPlus;
	public bool isMinus;
	private void Start()
	{
		once = true;
		frameCnt = 0;
		saveDeg = 180;

		transform.rotation = Quaternion.Euler(0, 0, 180.0f);
	}
	void Update()
	{
		frameCnt++;
		if(frameCnt> followStartTime)
		{
			isFollow = true;
			frameCnt = 0;
		}
		if (playerObj == null)
		{
			playerObj = GameObject.FindGameObjectWithTag("Player");
		}

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

		//追従を始める
		if (isFollow)
		{
			//一回のみ行う
			if (once)
			{
				if (playerObj.transform.position.y > transform.position.y)
				{
					isDec = true;
					isInc = false;

				}
				else
				{
					isInc = true;
					isDec = false;

				}
				once = false;
			}
		}

		//角度が増えているとき（向く方向が今の角度より大きい）
		if (isInc)
		{
			//角度を増やす
			transform.Rotate(0, 0, rollSpeed);

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
        }
        else if (isDec)
		{
			transform.Rotate(0, 0, -rollSpeed);
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
        }

		if(!isInc && !isDec)
		{
			if(isFollow)
			{
				//if(degree_plus>transform.eulerAngles.z+3)
				//{
				//	isInc = true;
				//}


				if ((degree_plus < transform.eulerAngles.z - 3 && degree_plus - transform.eulerAngles.z < 20) || degree_plus - transform.eulerAngles.z > 20)
				{
					isDec = true;
					isInc = false;

				
				}
				if ((degree_plus > transform.eulerAngles.z + 3 && transform.eulerAngles.z - degree_plus < 20) || transform.eulerAngles.z - degree_plus > 20)
				{

					isInc = true;
					isDec = false;
				}

				//else if (degree_plus > transform.eulerAngles.z + 3)
				//{
					
				//	isInc = true;
				//	isDec = false;
				//}
			}
		}

		if (saveDig_plus - 3 <= transform.eulerAngles.z && saveDig_plus + 3 >= transform.eulerAngles.z)
		{
			if(isFollow)
			{
				isInc = false;
				isDec = false;
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

		//if (colList.Count > 0)
		//{
		//	foreach(GameObject checkObj in colList)
		//	{
		//		if(checkObj.tag=="Player")
		//		{
		//			if (isFollow)
		//			{
		//				if (isPlus)
		//				{
		//					isDec = false;
		//					isInc = true;
		//				}
		//				else if (isMinus)
		//				{
		//					isInc = false;
		//					isDec = true;

		//				}
		//			}
		//		}
		//	}
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

 //   private void OnTriggerStay(Collider col)
 //   {
	//	colList.Add(col.gameObject);
	//	if (col.gameObject.tag=="Player")
 //       {
	//		isInc = false;
	//		isDec = false;

 //       }  
 //   }

	//private void OnTriggerExit(Collider col)
	//{
	//	colList.Remove(col.gameObject);
	//	if (col.gameObject.tag == "Player")
	//	{
	//		if(isFollow)
	//		{
	//			if (isPlus)
	//			{
	//				isDec = false;
	//				isInc = true;
	//			}
	//			else if (isMinus)
	//			{
	//				isInc = false;
	//				isDec = true;

	//			}
	//		}
	//	}
	//}
}

