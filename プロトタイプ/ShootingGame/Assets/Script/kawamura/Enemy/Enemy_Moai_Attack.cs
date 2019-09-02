using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Moai_Attack : MonoBehaviour
{
    public GameObject ringBulletObj;    //弾をロードして入れる
    public GameObject saveRingBullet;   //生成したオブジェクトを入れる
    GameObject saveObj;
    public Quaternion shotRota;

    [Header("入力用　発射する弾の角度範囲設定")]
    public float bulletRota_Value;      //発射する弾の角度範囲用
    public float ringShot_DelayCnt;     //弾発射のディレイカウント
    [Header("入力用　弾発射の間隔(秒)")]
    public float ringShot_DelayMax;     //弾発射のディレイマックス

    Find_Angle find_Angle_Script;

    public bool isMouthOpen = false;
    void Start()
    {
        find_Angle_Script = gameObject.GetComponent<Find_Angle>();
        ringBulletObj = Resources.Load("Bullet/Enemy_RingBullet") as GameObject;
    }


    void Update()
    {

        if(isMouthOpen)
        {
            ringShot_DelayCnt += Time.deltaTime;
            if (ringShot_DelayCnt > ringShot_DelayMax)
            {
                RingShot();
            }
        }
        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    for (int i = 0; i < 10; i++)
        //    {
        //        shotRota = new Quaternion(0, 0, Random.Range(-50, 50), 0);

        //        saveRingBullet = Instantiate(ringBulletObj, transform.position, transform.rotation);
        //        saveRingBullet.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-bulletRota_Value, bulletRota_Value));
        //    }
        //}
        //shotRota = new Quaternion(0, 0, Random.Range(-50, 50), 0);
    }

    void RingShot()
    {
        for (int i = 0; i < 10; i++)
        {
            //shotRota = new Quaternion(0, 0, Random.Range(-50, 50), 0);

            saveRingBullet = Instantiate(ringBulletObj, transform.position, transform.rotation);
            saveRingBullet.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-(180f - find_Angle_Script.degree - bulletRota_Value), -(180f - find_Angle_Script.degree + bulletRota_Value)));
        }
        ringShot_DelayCnt = 0;
    }
}
