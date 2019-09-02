using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Moai : MonoBehaviour
{
    public GameObject ringBulletObj;    //弾をロードして入れる
    public GameObject saveRingBullet;   //生成したオブジェクトを入れる
    GameObject saveObj;
    public Quaternion shotRota;
    public float bulletRota_Value;  //発射する弾の角度範囲用

	public bool isMouthOpen = false;
	void Start()
    {
        ringBulletObj = Resources.Load("Bullet/Enemy_RingBullet") as GameObject;
    }


	void Update()
    {

        if (Input.GetKeyDown(KeyCode.O))
        {
            for (int i = 0; i < 10; i++)
            {
                shotRota = new Quaternion(0, 0, Random.Range(-50, 50), 0);

                saveRingBullet = Instantiate(ringBulletObj, transform.position, transform.rotation);
                saveRingBullet.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-bulletRota_Value, bulletRota_Value));
            }
        }
        shotRota = new Quaternion(0, 0, Random.Range(-50, 50), 0);
    }
}
