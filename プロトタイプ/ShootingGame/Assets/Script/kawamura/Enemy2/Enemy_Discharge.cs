using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Discharge : MonoBehaviour
{
	public GameObject createObj;

	[Header("敵排出間隔MAX")]
	public int createDelayMax;
	private int createDelayCnt;

	void Start()
    {
		createDelayCnt = 0;
	}

    void Update()
    {
		createDelayCnt++;

		if (createDelayCnt > createDelayMax)
		{
			Instantiate(createObj, transform.position, transform.rotation);
			createDelayCnt = 0;
		}
    }
}
