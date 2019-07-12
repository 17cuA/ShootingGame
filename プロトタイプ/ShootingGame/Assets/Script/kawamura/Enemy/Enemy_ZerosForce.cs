using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ZerosForce : character_status
{
	public int saveHP;


	float scale;
	float scaleNum;
	float scaleNum_Maximum;     //スケールが最大の時に縮小させる値
	public float noDamageTime;
	public bool isSmall;
	public bool isBig;
	public bool isDamage = false;
    void Start()
    {
		isBig = true;
		scale = 3.0f;
		scaleNum = 0.05f;
		scaleNum_Maximum = 0.007f;
		noDamageTime = 0;

		HP_Setting();
		saveHP = hp;
    }

    void Update()
    {
		if (saveHP > hp)
		{
			isDamage = true;
			saveHP = hp;
		}

		if (isDamage && noDamageTime <= 30)
		{
			scale -= scaleNum;
			if (scale < 3.0f)
			{
				scale = 3.0f;
			}
		}

		if (saveHP == hp)
		{
			noDamageTime++;
		}
		else
		{
			noDamageTime = 0;
			saveHP = hp;
		}

		if (noDamageTime > 30 && scale < 9.0f)
		{
			hp++;
			if (hp > 100)
			{
				hp = 100;
			}
			scale += scaleNum;
		}

		if (scale >= 9.0f)
		{
			if(isBig)
			{
				scale += scaleNum_Maximum;
				if (scale > 9.3f)
				{
					scale = 9.3f;
					isBig = false;
					isSmall = true;
				}
			}
			else if(isSmall)
			{
				scale -= scaleNum_Maximum;
				if (scale < 9.0f)
				{
					scale = 9.0f;
					isSmall = false;
					isBig = true;
				}
			}
		}
		transform.localScale = new Vector3(scale, scale, scale);

	}
}
