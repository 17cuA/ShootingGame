using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Discharge : MonoBehaviour
{
	public GameObject createObj;

	[Header("入力用　グループ排出数")]
	public int createGroupNum = 0;
	public int createGroupCnt = 0;
	[Header("入力用　グループ内排出数")]
	public int createNum = 0;
	public int createCnt = 0;
	[Header("入力用　グループ排出間隔MAX(フレーム)")]
	public int createGroupDelayMax = 0;
	public int createGroupDelayCnt = 0;
	[Header("入力用　グループ内間隔MAX(フレーム)")]
	public int createDelayMax = 0;
	public int createDelayCnt = 0;

	void Start()
    {

	}

    void Update()
    {
		if (createGroupDelayCnt > createGroupDelayMax)
		{
			createDelayCnt++;
			//ひとまとまりの排出がまだ残っていたら
			if (createGroupNum > createGroupCnt)
			{
				//ひとまとまり内の排出数が残っていたら
				if (createNum > createCnt)
				{
					if (createDelayCnt > createDelayMax)
					{
						Instantiate(createObj, transform.position, transform.rotation);
						createDelayCnt = 0;
						createCnt++;
					}
				}
				if (createCnt == createNum)
				{
					createGroupCnt++;
					createGroupDelayCnt = 0;
					createCnt = 0;
				}
			}
		}
		else
		{
			createGroupDelayCnt++;
		}

		//if (createDelayCnt > createDelayMax)
		//{
		//	Instantiate(createObj, transform.position, transform.rotation);
		//	createDelayCnt = 0;
		//}
    }
}
