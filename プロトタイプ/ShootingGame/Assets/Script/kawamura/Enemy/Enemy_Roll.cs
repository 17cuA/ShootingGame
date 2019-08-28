﻿//作成者：川村良太
//オブジェクトの回転スクリプト　名前はEnemyだけど他でも使えると思う

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Roll : MonoBehaviour
{
	public float rotaX;
	public float rotaY;
	public float rotaZ;

	public float rotaX_Value = 0;
	public float rotaY_Value = 0;
	public float rotaZ_Value = 0;

	Enemy_Wave_Direction ewd;

	public string myName;

	public bool isWaveEnemy = false;

	private void Awake()
	{
		myName = gameObject.name;
		if (gameObject.name == "Enemy_Bullfight")
		{
			isWaveEnemy = true;
			ewd = gameObject.GetComponent<Enemy_Wave_Direction>();
		}

	}

	void Start()
    {
        if (myName == "Enemy_MeteorBound_Model")
        {
            rotaX_Value = Random.Range(-2f, 2f);
            rotaY_Value = Random.Range(-0.3f, 0.3f);
        }
        //else if (myName == "Bacula")
        //{
        //    rotaX_Value = Random.Range(-6f, 6f);
        //    rotaY_Value = Random.Range(-2f, 2f);

        //}
        rotaX = transform.eulerAngles.x;
		rotaY = transform.eulerAngles.y;
		rotaZ = transform.eulerAngles.z;
    }

    void Update()
    {
		if(isWaveEnemy)
		{
			transform.localRotation = Quaternion.Euler(rotaX, ewd.rotaY, rotaZ);
		}
		else
		{
			transform.localRotation = Quaternion.Euler(rotaX, rotaY, rotaZ);

		}
		rotaX += rotaX_Value;
		rotaY += rotaY_Value;
		rotaZ += rotaZ_Value;
    }
}
