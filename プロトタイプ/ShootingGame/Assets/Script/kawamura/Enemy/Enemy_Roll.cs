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

	bool isWaveEnemy = false;

    void Start()
    {
		if(gameObject.name== "Enemy_Bullfight")
		{
			isWaveEnemy = true;
			ewd = gameObject.GetComponent<Enemy_Wave_Direction>();
		}
		rotaX = transform.eulerAngles.x;
		rotaY = transform.eulerAngles.y;
		rotaZ = transform.eulerAngles.z;
    }

    void Update()
    {
		if(isWaveEnemy)
		{
			transform.rotation = Quaternion.Euler(rotaX, ewd.rotaY, rotaZ);
		}
		else
		{
			transform.rotation = Quaternion.Euler(rotaX, rotaY, rotaZ);

		}
		rotaX += rotaX_Value;
		rotaY += rotaY_Value;
		rotaZ += rotaZ_Value;
    }
}
