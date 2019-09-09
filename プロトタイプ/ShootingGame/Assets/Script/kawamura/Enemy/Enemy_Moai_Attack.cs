using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Moai_Attack : MonoBehaviour
{
    public GameObject parentObj;
    public GameObject ringBulletObj;    //弾をロードして入れる
    public GameObject miniMoaiGroupObj;		//ミニモアイ群をロードして入れる
    public GameObject saveRingBullet;   //生成したオブジェクトを入れる
    GameObject saveObj;
    public GameObject miniMoaiPos;

    public Enemy_Moai moai_Script;
    public Quaternion shotRota;

    public float miniMoai_DelayCnt;
    public float miniMoai_DelayMax;

    public string parentName;

    [Header("入力用　発射する弾の角度範囲設定")]
    public float bulletRota_Value;      //発射する弾の角度範囲用
    public float ringShot_DelayCnt;     //弾発射のディレイカウント
    [Header("入力用　弾発射の間隔(秒)")]
    public float ringShot_DelayMax;     //弾発射のディレイマックス

    //レーザー---------------------------------------
    public GameObject moaiLaserObj;
    [SerializeField, Tooltip("エネルギーため用のパーティクル用")] private Boss_One_A111[] supply;
    [SerializeField, Tooltip("レーザーの発射位置")] private GameObject[] laser_muzzle;

    private int Attack_Step { get; set; }           // 関数内 攻撃ステップ
    private bool Is_Attack_Now { get; set; }            // 現在攻撃しているか

    //レーザー音追加
    [SerializeField] private AudioClip laserBegin;
    [SerializeField] private AudioClip laserContinuing;
    [SerializeField] private AudioClip laserEnd;
    private AudioSource audioSource;

    //-----------------------------------------------


    Find_Angle find_Angle_Script;

    public bool isMouthOpen = false;
    void Start()
    {

        parentObj = transform.parent.gameObject;
        parentName = parentObj.name;
        moai_Script = parentObj.GetComponent<Enemy_Moai>();
        find_Angle_Script = gameObject.GetComponent<Find_Angle>();
        //miniMoaiPos = transform.GetChild(0).gameObject;
        ringBulletObj = Resources.Load("Bullet/Enemy_RingBullet") as GameObject;
        miniMoaiGroupObj = Resources.Load("Enemy/Enemy_Moai_MiniGroup") as GameObject;
        moaiLaserObj= Resources.Load("Bullet/Moai_Laser") as GameObject;
        //-------------追加--------------
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = laserEnd;
        audioSource.playOnAwake = false;
        //-------------------------------
    }


    void Update()
    {

        if (moai_Script.isMouthOpen)
        {
            if (parentName == "Enemy_Moai_RingShot")
            {
                ringShot_DelayCnt += Time.deltaTime;
                if (ringShot_DelayCnt > ringShot_DelayMax)
                {
                    RingShot();
                }
            }
            else if (parentName == "Enemy_Moai")
            {
                Laser_Attack();
            }
            else if (parentName == "Enemy_Moai_MiniMoaiDischarge")
            {
                miniMoai_DelayCnt++;
                if (miniMoai_DelayCnt > miniMoai_DelayMax)
                {
                    MiniMoaiCreate();
                    miniMoai_DelayCnt = 0;
                }
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

    void MiniMoaiCreate()
    {
        GameObject save = Instantiate(miniMoaiGroupObj, miniMoaiPos.transform.position, Quaternion.Euler(0, 0, 0));
        save.transform.position = miniMoaiPos.transform.position;
    }

    void Laser_Attack()
    {
        // レーザーチャージ
        if (Attack_Step == 0)
        {
            // 攻撃開始
            Is_Attack_Now = true;

            // チャージ音
            if (audioSource.clip == laserEnd)
            {
                audioSource.clip = laserBegin;
                audioSource.Stop();
                audioSource.loop = true;
                audioSource.Play();
            }

            // チャージエフェクト再生
            if (!supply[0].gameObject.activeSelf)// && !supply[1].gameObject.activeSelf)
            {
                supply[0].gameObject.SetActive(true);
                //supply[1].gameObject.SetActive(true);

                supply[0].SetUp();
                //supply[1].SetUp();
            }
            // チャージエフェクト終了
            if (supply[0].Completion_Confirmation())// && supply[1].Completion_Confirmation())
            {
                supply[0].gameObject.SetActive(false);
                //supply[1].gameObject.SetActive(false);

                Attack_Step++;
            }
        }
        else if (Attack_Step == 1)
        {
            //--------------追加-----------------
            if (audioSource.clip == laserBegin)
            {
                audioSource.clip = laserContinuing;
                audioSource.Stop();
                audioSource.loop = true;
                audioSource.Play();
            }
            //-----------------------------------

            LaserCreate();
        }
    }

    void LaserCreate()
    {
        //foreach (GameObject obj in laser_muzzle)
        //{
        //    if (obj.activeSelf)
        //    {
        //        GameObject save = Instantiate(moaiLaserObj, obj.transform.position, transform.rotation);
        //        Moai_Laser laser = save.GetComponent<Moai_Laser>();
        //        laser.Manual_Start(obj.transform);

        //        //Moai_Laser laser = Instantiate(moaiLaserObj, obj.transform.position, transform.rotation).GetComponent<Moai_Laser>();
        //        //Boss_One_Laser laser = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eONE_BOSS_LASER, obj.transform.position, transform.right).GetComponent<Boss_One_Laser>();
        //        //laser.Manual_Start(obj.transform);
        //    }
        //}
        GameObject save = Instantiate(moaiLaserObj, transform.position, transform.rotation);
        save.transform.rotation = Quaternion.Euler(0, 180, transform.rotation.z);
        Moai_Laser laser = save.GetComponent<Moai_Laser>();
        //laser.Manual_Start(transform);

    }
}
