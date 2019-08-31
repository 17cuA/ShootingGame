using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Moai : MonoBehaviour
{
    GameObject ringBulletObj;
    GameObject saveObj;
    public Quaternion shotRota;

	public bool isMouthOpen = false;
	void Start()
    {
        ringBulletObj = Resources.Load("Bulet/Enemy_RingBullet") as GameObject;
    }


	void Update()
    {
        shotRota = new Quaternion(0, 0, Random.Range(-50, 50), 0);
    }
}
