using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFire : MonoBehaviour
{
	public bool isFire = true;
	public GameObject bullet;
	public float intervalTime;
	private float intervalTimer;

	private void Update()
	{
		if (isFire)
		{
			if (intervalTimer >= intervalTime)
			{
				Instantiate(bullet, transform.position, Quaternion.identity);
				intervalTimer = 0;
			}
			intervalTimer += Time.deltaTime;
		}
	}
}
