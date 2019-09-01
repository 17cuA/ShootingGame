//作成者：川村良太
//敵を出すスクリプト

//2019/08/03改修

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class EnemyCreate : MonoBehaviour
{
	//生成位置上側（Tなので上側、mTはマイナスなのでmがついてる、17が左右の最大）
	#region CreatePosTop
	public GameObject createPosT17;
	public GameObject createPosT16;
	public GameObject createPosT15;
	public GameObject createPosT14;
	public GameObject createPosT13;
	public GameObject createPosT12;
	public GameObject createPosT11;
	public GameObject createPosT10;
	public GameObject createPosT9;
	public GameObject createPosT8;
	public GameObject createPosT7;
	public GameObject createPosT6;
	public GameObject createPosT5;
	public GameObject createPosT4;
	public GameObject createPosT3;
	public GameObject createPosT2;
	public GameObject createPosT1;
	public GameObject createPosT0;
	public GameObject createPosTm1;
	public GameObject createPosTm2;
	public GameObject createPosTm3;
	public GameObject createPosTm4;
	public GameObject createPosTm5;
	public GameObject createPosTm6;
	public GameObject createPosTm7;
	public GameObject createPosTm8;
	public GameObject createPosTm9;
	public GameObject createPosTm10;
	public GameObject createPosTm11;
	public GameObject createPosTm12;
	public GameObject createPosTm13;
	public GameObject createPosTm14;
	public GameObject createPosTm15;
	public GameObject createPosTm16;
	public GameObject createPosTm17;
	#endregion

	//生成位置下側（Uなので上側、mUはマイナスなのでmがついてる、17が左右の最大）
	#region CreatePosUnder
	public GameObject createPosU17;
	public GameObject createPosU16;
	public GameObject createPosU15;
	public GameObject createPosU14;
	public GameObject createPosU13;
	public GameObject createPosU12;
	public GameObject createPosU11;
	public GameObject createPosU10;
	public GameObject createPosU9;
	public GameObject createPosU8;
	public GameObject createPosU7;
	public GameObject createPosU6;
	public GameObject createPosU5;
	public GameObject createPosU4;
	public GameObject createPosU3;
	public GameObject createPosU2;
	public GameObject createPosU1;
	public GameObject createPosU0;
	public GameObject createPosUm1;
	public GameObject createPosUm2;
	public GameObject createPosUm3;
	public GameObject createPosUm4;
	public GameObject createPosUm5;
	public GameObject createPosUm6;
	public GameObject createPosUm7;
	public GameObject createPosUm8;
	public GameObject createPosUm9;
	public GameObject createPosUm10;
	public GameObject createPosUm11;
	public GameObject createPosUm12;
	public GameObject createPosUm13;
	public GameObject createPosUm14;
	public GameObject createPosUm15;
	public GameObject createPosUm16;
	public GameObject createPosUm17;
	#endregion

	//生成位置右側（Rなので右側、mRはマイナスなのでmがついてる、5が上下の最大）
	#region CreatePosRight
	public GameObject createPosR5;				
    public GameObject createPosR4;
    public GameObject createPosR3;
    public GameObject createPosR2;
    public GameObject createPosR1;
    public GameObject createPosR0;
	public GameObject createPos_FourGroupR;
    public GameObject createPosRm1;
    public GameObject createPosRm2;
    public GameObject createPosRm3;
    public GameObject createPosRm4;
    public GameObject createPosRm5;
	#endregion

	//生成位置左側
	#region CreatePosLeft
	public GameObject createPosL5;
    public GameObject createPosL4;
    public GameObject createPosL3;
    public GameObject createPosL2;
    public GameObject createPosL1;
    public GameObject createPosL0;
    public GameObject createPos_FourGroupL;
    public GameObject createPosLm1;
    public GameObject createPosLm2;
    public GameObject createPosLm3;
    public GameObject createPosLm4;
    public GameObject createPosLm5;
	#endregion

	//中ボス位置と戦艦位置
	public GameObject createMiddleBossPos;
    public GameObject createBattleShipPos;

    //バキュラ位置
    public GameObject createBaculaGroupPos;

	//隕石生成位置
    public GameObject createMeteorPosR4_814;
    public GameObject createMeteorPosR2_988;
    public GameObject createMeteorPosR1_494;
    public GameObject createMeteorPosR0;
    public GameObject createMeteorPosRm1_162;
    public GameObject createMeteorPosRm2_822;
	public GameObject createMeteorPosRm3_57;
	public GameObject createMeteorPosRm4_814;

	//public GameObject enemy_UFO_Group;
	//public GameObject enemy_ClamChowder_Group_Four;
	public GameObject enemy_ClamChowder_Group_Four_NoItem;

	public GameObject enemy_ClamChowder_Group_Five;
    public GameObject enemy_ClamChowder_Group_Five_NoItem;
    public GameObject enemy_ClamChowder_Group_FourBehind;
	public GameObject enemy_ClamChowder_Group_TwoWaveOnlyUp;
	public GameObject enemy_ClamChowder_Group_TwoWaveOnlyDown;
	public GameObject enemy_ClamChowder_Group_TwoWaveOnlyUp_Item;
	public GameObject enemy_ClamChowder_Group_TwoWaveOnlyDown_Item;
	public GameObject enemy_ClamChowder_Group_Three;
	//public GameObject enemy_ClamChowder_Group_Three_Item;
	public GameObject enemy_ClamChowder_Group_SevenWave;
	public GameObject enemy_Clamchowder_Group_Straight;
	public GameObject enemy_Clamchowder_Group_StraightBehind;
    //public GameObject enemy_MiddleBoss_Father;
    //public GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp;
    //public GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown;
    //public GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item;
    //public GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item;
    public GameObject enemy_BattleShip;
	public GameObject enemy_Beelzebub_Group_FourNomal;
    public GameObject enemy_Beelzebub_Group_FourBack;
    public GameObject enemy_Bacula_Sixteen;
    public GameObject enemy_Bacula_FourOnly;
    public GameObject enemy_Meteor;
	public GameObject enemy_MEteor_Under;
    public GameObject enemy_Meteors;
    public GameObject enemy_Meteor_Mini;
	public GameObject enemy_MeteorWaveGroup;
	public GameObject enemy_SlowFollow;
    public GameObject Enemy_BoundMeteors;

	public GameObject saveEnemyObj;

	public int PreviousCount = 0;

	public int frameCnt = 0;	//フレームカウント：これの値で生成のタイミングをはかる
    public int groupCnt = 1;	//画面に出す群れのカウント
    public int nowGroupCnt;
    public int[] groupCntArray;
    //float plusNum = 60;			//
    //float plusNum2 = 60;		//この三つは強引に表示のフレームをずらすために使ったので消した方がいいけど面倒
    //float plusNum3 = 60;        //

	public int turning_frame = 180;
	public string nextEnemy;

    GameObject oneBossOBj;
    One_Boss oneBoss_Script;
	GameObject mistEffectObj;
	ParticleSystem mistParticle;
	public BackgroundActivation backActive_Script;

	GameObject middleBossOBj;
    Enemy_MiddleBoss middleBoss_Script;

    public bool isCreate;       //表示するときにtrueにする
	public bool isBaculaDestroy = false;
    public bool isMiddleBossDead = false;
    public bool isOneBossDead = false;
	void Start()
    {
		//位置オブジェクト取得
		//上側取得
		#region CreatePosTop
		createPosT17 = GameObject.Find("CreatePos_Top_17");
		createPosT16 = GameObject.Find("CreatePos_Top_16");
		createPosT15 = GameObject.Find("CreatePos_Top_15");
		createPosT14 = GameObject.Find("CreatePos_Top_14");
		createPosT13 = GameObject.Find("CreatePos_Top_13");
		createPosT12 = GameObject.Find("CreatePos_Top_12");
		createPosT11 = GameObject.Find("CreatePos_Top_11");
		createPosT10 = GameObject.Find("CreatePos_Top_10");
		createPosT9 = GameObject.Find("CreatePos_Top_9");
		createPosT8 = GameObject.Find("CreatePos_Top_8");
		createPosT7 = GameObject.Find("CreatePos_Top_7");
		createPosT6 = GameObject.Find("CreatePos_Top_6");
		createPosT5 = GameObject.Find("CreatePos_Top_5");
		createPosT4 = GameObject.Find("CreatePos_Top_4");
		createPosT3 = GameObject.Find("CreatePos_Top_3");
		createPosT2 = GameObject.Find("CreatePos_Top_2");
		createPosT1 = GameObject.Find("CreatePos_Top_1");
		createPosT0 = GameObject.Find("CreatePos_Top_0");
		createPosTm1 = GameObject.Find("CreatePos_Top_-1");
		createPosTm2 = GameObject.Find("CreatePos_Top_-2");
		createPosTm3 = GameObject.Find("CreatePos_Top_-3");
		createPosTm4 = GameObject.Find("CreatePos_Top_-4");
		createPosTm5 = GameObject.Find("CreatePos_Top_-5");
		createPosTm6 = GameObject.Find("CreatePos_Top_-6");
		createPosTm7 = GameObject.Find("CreatePos_Top_-7");
		createPosTm8 = GameObject.Find("CreatePos_Top_-8");
		createPosTm9 = GameObject.Find("CreatePos_Top_-9");
		createPosTm10 = GameObject.Find("CreatePos_Top_-10");
		createPosTm11 = GameObject.Find("CreatePos_Top_-11");
		createPosTm12 = GameObject.Find("CreatePos_Top_-12");
		createPosTm13 = GameObject.Find("CreatePos_Top_-13");
		createPosTm14 = GameObject.Find("CreatePos_Top_-14");
		createPosTm15 = GameObject.Find("CreatePos_Top_-15");
		createPosTm16 = GameObject.Find("CreatePos_Top_-16");
		createPosTm17 = GameObject.Find("CreatePos_Top_-17");
		#endregion

		//下側取得
		#region CreatePosUnder
		createPosU17 = GameObject.Find("CreatePos_Under_17");
		createPosU16 = GameObject.Find("CreatePos_Under_16");
		createPosU15 = GameObject.Find("CreatePos_Under_15");
		createPosU14 = GameObject.Find("CreatePos_Under_14");
		createPosU13 = GameObject.Find("CreatePos_Under_13");
		createPosU12 = GameObject.Find("CreatePos_Under_12");
		createPosU11 = GameObject.Find("CreatePos_Under_11");
		createPosU10 = GameObject.Find("CreatePos_Under_10");
		createPosU9 = GameObject.Find("CreatePos_Under_9");
		createPosU8 = GameObject.Find("CreatePos_Under_8");
		createPosU7 = GameObject.Find("CreatePos_Under_7");
		createPosU6 = GameObject.Find("CreatePos_Under_6");
		createPosU5 = GameObject.Find("CreatePos_Under_5");
		createPosU4 = GameObject.Find("CreatePos_Under_4");
		createPosU3 = GameObject.Find("CreatePos_Under_3");
		createPosU2 = GameObject.Find("CreatePos_Under_2");
		createPosU1 = GameObject.Find("CreatePos_Under_1");
		createPosU0 = GameObject.Find("CreatePos_Under_0");
		createPosUm1 = GameObject.Find("CreatePos_Under_-1");
		createPosUm2 = GameObject.Find("CreatePos_Under_-2");
		createPosUm3 = GameObject.Find("CreatePos_Under_-3");
		createPosUm4 = GameObject.Find("CreatePos_Under_-4");
		createPosUm5 = GameObject.Find("CreatePos_Under_-5");
		createPosUm6 = GameObject.Find("CreatePos_Under_-6");
		createPosUm7 = GameObject.Find("CreatePos_Under_-7");
		createPosUm8 = GameObject.Find("CreatePos_Under_-8");
		createPosUm9 = GameObject.Find("CreatePos_Under_-9");
		createPosUm10 = GameObject.Find("CreatePos_Under_-10");
		createPosUm11 = GameObject.Find("CreatePos_Under_-11");
		createPosUm12 = GameObject.Find("CreatePos_Under_-12");
		createPosUm13 = GameObject.Find("CreatePos_Under_-13");
		createPosUm14 = GameObject.Find("CreatePos_Under_-14");
		createPosUm15 = GameObject.Find("CreatePos_Under_-15");
		createPosUm16 = GameObject.Find("CreatePos_Under_-16");
		createPosUm17 = GameObject.Find("CreatePos_Under_-17");
		#endregion

		//右側取得
		#region CreatePosRight
		createPosR5 = GameObject.Find("CreatePos_Right_5");
        createPosR4 = GameObject.Find("CreatePos_Right_4");
        createPosR3 = GameObject.Find("CreatePos_Right_3");
        createPosR2 = GameObject.Find("CreatePos_Right_2");
        createPosR1 = GameObject.Find("CreatePos_Right_1");
        createPosR0 = GameObject.Find("CreatePos_Right_0");
		createPos_FourGroupR = GameObject.Find("CreatePos_FourGroupR");
        createPosRm1 = GameObject.Find("CreatePos_Right_-1");
        createPosRm2 = GameObject.Find("CreatePos_Right_-2");
        createPosRm3 = GameObject.Find("CreatePos_Right_-3");
        createPosRm4 = GameObject.Find("CreatePos_Right_-4");
        createPosRm5 = GameObject.Find("CreatePos_Right_-5");
		#endregion

		//左側取得
		#region CreatePosLeft
		createPosL5 = GameObject.Find("CreatePos_Left_5");
        createPosL4 = GameObject.Find("CreatePos_Left_4");
        createPosL3 = GameObject.Find("CreatePos_Left_3");
        createPosL2 = GameObject.Find("CreatePos_Left_2");
        createPosL1 = GameObject.Find("CreatePos_Left_1");
        createPosL0 = GameObject.Find("CreatePos_Left_0");
        createPos_FourGroupL = GameObject.Find("CreatePos_FourGroupL");
        createPosLm1 = GameObject.Find("CreatePos_Left_-1");
        createPosLm2 = GameObject.Find("CreatePos_Left_-2");
        createPosLm3 = GameObject.Find("CreatePos_Left_-3");
        createPosLm4 = GameObject.Find("CreatePos_Left_-4");
        createPosLm5 = GameObject.Find("CreatePos_Left_-5");
		#endregion

		createMiddleBossPos = GameObject.Find("CreateMiddleBossPos");
        createBattleShipPos = GameObject.Find("CreateBattleshipPos");

        createBaculaGroupPos = GameObject.Find("CreateBaculaGroupPos");

        createMeteorPosR4_814 = GameObject.Find("CreateMeteorPos_Right_4.814");
        createMeteorPosR2_988 = GameObject.Find("CreateMeteorPos_Right_2.988");
        createMeteorPosR1_494 = GameObject.Find("CreateMeteorPos_Right_1.494");
        createMeteorPosR0 = GameObject.Find("CreateMeteorPos_Right_0");
        createMeteorPosRm1_162 = GameObject.Find("CreateMeteorPos_Right_-1.162");
        createMeteorPosRm2_822 = GameObject.Find("CreateMeteorPos_Right_-2.822");
		createMeteorPosRm3_57 = GameObject.Find("CreateMeteorPos_Right_-3.57");
		createMeteorPosRm4_814 = GameObject.Find("CreateMeteorPos_Right_-4.814");

		//enemy_UFO_Group = Resources.Load("Enemy/Enemy_UFO_Group") as GameObject;
		//enemy_ClamChowder_Group_Four = Resources.Load("Enemy/Enemy_ClamChowder_Group_Four") as GameObject;
		enemy_ClamChowder_Group_Four_NoItem = Resources.Load("Enemy/Enemy_ClamChowder_Group_Four_NoItem") as GameObject;
		enemy_ClamChowder_Group_Five = Resources.Load("Enemy/Enemy_ClamChowder_Group_Five") as GameObject;
        enemy_ClamChowder_Group_Five_NoItem = Resources.Load("Enemy/Enemy_ClamChowder_Group_Five_NoItem") as GameObject;
        enemy_ClamChowder_Group_FourBehind = Resources.Load("Enemy/Enemy_ClamChowder_Group_FourBehind") as GameObject;
        enemy_ClamChowder_Group_TwoWaveOnlyUp = Resources.Load("Enemy/Enemy_ClamChowder_Group_TwoWaveOnlyUp") as GameObject;
        enemy_ClamChowder_Group_TwoWaveOnlyDown = Resources.Load("Enemy/Enemy_ClamChowder_Group_TwoWaveOnlyDown") as GameObject;
		enemy_ClamChowder_Group_TwoWaveOnlyUp_Item = Resources.Load("Enemy/Enemy_ClamChowder_Group_TwoWaveOnlyUp_Item") as GameObject;
        enemy_ClamChowder_Group_TwoWaveOnlyDown_Item = Resources.Load("Enemy/Enemy_ClamChowder_Group_TwoWaveOnlyDown_Item") as GameObject;
		enemy_ClamChowder_Group_Three = Resources.Load("Enemy/Enemy_ClamChowder_Group_Three") as GameObject;
		//enemy_ClamChowder_Group_Three_Item = Resources.Load("Enemy/Enemy_ClamChowder_Group_Three_Item") as GameObject;
		enemy_ClamChowder_Group_SevenWave = Resources.Load("Enemy/Enemy_ClamChowder_Group_Seven") as GameObject;
		enemy_Clamchowder_Group_Straight = Resources.Load("Enemy/Enemy_ClamChowder_Group_Straight") as GameObject;
		enemy_Clamchowder_Group_StraightBehind = Resources.Load("Enemy/Enemy_ClamChowder_Group_StraightBehind") as GameObject;
		//enemy_MiddleBoss_Father = Resources.Load("Enemy/Enemy_MiddleBoss_Father") as GameObject;
		//enemy_ClamChowder_Group_ThreeWaveOnlyUp = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyUp") as GameObject;
		//enemy_ClamChowder_Group_ThreeWaveOnlyDown = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyDown") as GameObject;
		//enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item") as GameObject;
		//enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item") as GameObject;
		enemy_BattleShip = Resources.Load("Enemy/BattleshipType_Enemy") as GameObject;
        enemy_Beelzebub_Group_FourNomal = Resources.Load("Enemy/Enemy_Beelzebub_Group_FourNomal") as GameObject;
        enemy_Beelzebub_Group_FourBack = Resources.Load("Enemy/Enemy_Beelzebub_Group_FourBack") as GameObject;
        enemy_Bacula_Sixteen = Resources.Load("Enemy/Enemy_Bacula_Sixteen") as GameObject;
        enemy_Bacula_FourOnly = Resources.Load("Enemy/Enemy_Bacula_FourOnly") as GameObject;
        
        enemy_Meteor = Resources.Load("Enemy/Enemy_Meteor") as GameObject;
        enemy_Meteor_Mini = Resources.Load("Enemy/Enemy_Meteor_Mini") as GameObject;
		enemy_MEteor_Under = Resources.Load("Enemy/Enemy_Meteor_Under") as GameObject;
        enemy_Meteors = Resources.Load("Enemy/Meteors") as GameObject;
		enemy_MeteorWaveGroup = Resources.Load("Enemy/Enemy_MeteorWaveGroup") as GameObject;
		enemy_SlowFollow = Resources.Load("Enemy/Enemy_SlowFollow") as GameObject;
        Enemy_BoundMeteors = Resources.Load("Enemy/BoundMeteors") as GameObject;

		mistEffectObj = Resources.Load("Effects/Other/O004") as GameObject;
		mistParticle = mistEffectObj.GetComponent<ParticleSystem>();
		//backActive_Script = mistEffectObj.GetComponent<BackgroundActivation>();

		//群れカウント初期化
		groupCnt = 1;
        nowGroupCnt = 1;
        for (int i = 0; i < groupCntArray.Length; i++)
        {
            groupCntArray[i] = i;
        }

        middleBossOBj = Obj_Storage.Storage_Data.GetMiddleBoss();
        middleBoss_Script = middleBossOBj.GetComponent<Enemy_MiddleBoss>();

        oneBossOBj = Obj_Storage.Storage_Data.GetBoss(1);
        oneBoss_Script = oneBossOBj.GetComponent<One_Boss>();
    }

    void Update()
    {
		PreviousCount = frameCnt;
		frameCnt++;
        if (Input.GetKeyDown(KeyCode.N))
        {
            frameCnt = turning_frame;
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            turning_frame = 5010;
            frameCnt = 5010;
            groupCnt = 17;
            //nowGroupCnt = 17;
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            turning_frame = 6750;
            frameCnt = 6750;
            groupCnt = 24;
            //nowGroupCnt = 21;
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            turning_frame = 10550;
            frameCnt = 10550;
            groupCnt = 41;
            //nowGroupCnt = 36;
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            turning_frame = 17750;
            frameCnt = 17750;
            groupCnt = 42;

        }
        if (saveEnemyObj != null)
		{

		}

        if (isMiddleBossDead)
        {
            if (frameCnt < 7450)
            {
                frameCnt = 7450;
                turning_frame = 7450;

                groupCnt = 24;
            }
            isMiddleBossDead = false;
        }
        else if (middleBoss_Script != null)
        {
            if(middleBoss_Script.Is_Dead)
            {
                if (frameCnt < 7450)
                {
                    frameCnt = 7450;
                    turning_frame = 7450;
                    groupCnt = 24;
                }
                isMiddleBossDead = false;

            }
        }

        if (isOneBossDead)
        {
            if (frameCnt < 40780)
            {
                frameCnt = 40780;
                //turning_frame = 40930;
            }
            isOneBossDead = false;
        }
        else if (oneBoss_Script != null)
        {
			if (oneBoss_Script.Is_Dead)
            {
                if (frameCnt < 40780)
                {
                    if(backActive_Script)
                    {
                        backActive_Script.TransparencyChangeTrigger();
                    }
                    frameCnt = 40780;
                    //turning_frame = 40930;
                }
                isOneBossDead = false;

                //if(frame > 180) SceneManager.LoadScene("GameClear");
                //if (frame > 120) Scene_Manager.Manager.Screen_Transition_To_Clear();
            }
        }

		if(isBaculaDestroy)
		{
			if (frameCnt < turning_frame)
			{
				frameCnt = turning_frame;
			}
			isBaculaDestroy = false;
		}
        //CreateCheck();
        CreateEnemyGroup_01();
		//switch(Scene_Manager.Manager.Now_Scene)
		//{
  //          case Scene_Manager.SCENE_NAME.eSTAGE_01:
  //              CreateEnemyGroup_01();

  //          case Scene_Manager.SCENE_NAME.eSTAGE_01:
		//		CreateEnemyGroup_01();
		//		break;
		//	case Scene_Manager.SCENE_NAME.eSTAGE_02:
		//		CreateEnemyGroup_02();
		//		break;
		//	default:
		//		break;
		//}
    }

	//--------------------------------------------------------------------

	//敵を出す関数
	void CreateEnemyGroup_01()
	{
		#region 保留
		//if (isCreate)
		//      {
		//          switch (groupCnt)
		//          {
		//              case 1:						//円盤の群れを１つ右上から出す
		//                  isCreate = false;
		//                  groupCnt++;
		//                  //Instantiate(Obj_Storage.Storage_Data.enemy_UFO_Group_prefab, createPosR4.transform.position, transform.rotation);
		//                  GameObject enemy_UFO_Group = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
		//                  enemy_UFO_Group.transform.position = createPosR3.transform.position;
		//                  enemy_UFO_Group.transform.rotation = transform.rotation;
		//                  break;

		//              case 2:						//円盤の群れを１つ右下から出す
		//                  isCreate = false;
		//                  groupCnt++;
		//                  //Instantiate(enemy_UFO_Group, createPosRm4.transform.position, transform.rotation);
		//                  GameObject enemy_UFO_Group2 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
		//                  enemy_UFO_Group2.transform.position = createPosRm3.transform.position;
		//                  enemy_UFO_Group2.transform.rotation = transform.rotation;
		//                  break;

		//              case 3:						//円盤の群れを１つ右上から出す
		//			isCreate = false;
		//                  groupCnt++;
		//                  //Instantiate(enemy_UFO_Group, createPosR4.transform.position, transform.rotation);
		//                  GameObject enemy_UFO_Group3 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
		//                  enemy_UFO_Group3.transform.position = createPosR3.transform.position;
		//                  enemy_UFO_Group3.transform.rotation = transform.rotation;
		//                  break;

		//              case 4:						//円盤の群れを１つ右下から出す
		//			isCreate = false;
		//                  groupCnt++;
		//                  //Instantiate(enemy_UFO_Group, createPosRm4.transform.position, transform.rotation);
		//                  GameObject enemy_UFO_Group4 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
		//                  enemy_UFO_Group4.transform.position = createPosRm3.transform.position;
		//                  enemy_UFO_Group4.transform.rotation = transform.rotation;
		//                  break;

		//              case 5:						//奥からくる斜めに並んだ闘牛型の群れを出す
		//                  isCreate = false;
		//                  groupCnt++;
		//                  //Instantiate(enemy_ClamChowder_Group_Four, createPos_FourGroup.transform.position, transform.rotation);
		//                  GameObject enemy_ClamChowder_Group_Four = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Four.Active_Obj();
		//                  enemy_ClamChowder_Group_Four.transform.position = createPos_FourGroup.transform.position;
		//                  enemy_ClamChowder_Group_Four.transform.rotation = transform.rotation;
		//                  break;

		//              case 6:						//円盤の群れを右上と右下から１つずつ出す
		//                  isCreate = false;
		//                  groupCnt++;
		//                  //Instantiate(enemy_UFO_Group, createPosR4.transform.position, transform.rotation);
		//                  GameObject enemy_UFO_Group5 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
		//                  enemy_UFO_Group5.transform.position = createPosR3.transform.position;
		//                  enemy_UFO_Group5.transform.rotation = transform.rotation;

		//                  //Instantiate(enemy_UFO_Group, createPosRm4.transform.position, transform.rotation);
		//                  GameObject enemy_UFO_Group6 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
		//                  enemy_UFO_Group6.transform.position = createPosRm3.transform.position;
		//                  enemy_UFO_Group6.transform.rotation = transform.rotation;

		//                  break;

		//              case 7:						//円盤の群れを右側から中央寄りで2つ出す
		//                  isCreate = false;
		//                  groupCnt++;
		//                  //Instantiate(enemy_UFO_Group, createPosR1.transform.position, transform.rotation);
		//                  GameObject enemy_UFO_Group7 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
		//                  enemy_UFO_Group7.transform.position = createPosR1.transform.position;
		//                  enemy_UFO_Group7.transform.rotation = transform.rotation;

		//                  //Instantiate(enemy_UFO_Group, createPosRm1.transform.position, transform.rotation);
		//                  GameObject enemy_UFO_Group8 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
		//                  enemy_UFO_Group8.transform.position = createPosRm1.transform.position;
		//                  enemy_UFO_Group8.transform.rotation = transform.rotation;

		//                  break;

		//              case 8:						//円盤の群れを右上と右下から１つずつ出す
		//			isCreate = false;
		//                  groupCnt++;
		//                  //Instantiate(enemy_UFO_Group, createPosR4.transform.position, transform.rotation);
		//                  GameObject enemy_UFO_Group9 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
		//                  enemy_UFO_Group9.transform.position = createPosR3.transform.position;
		//                  enemy_UFO_Group9.transform.rotation = transform.rotation;

		//                  //Instantiate(enemy_UFO_Group, createPosRm4.transform.position, transform.rotation);
		//                  GameObject enemy_UFO_Group10 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
		//                  enemy_UFO_Group10.transform.position = createPosRm3.transform.position;
		//                  enemy_UFO_Group10.transform.rotation = transform.rotation;

		//                  break;

		//              case 9:						//奥からくる闘牛型が縦に2つ並んだ敵の群れを２つ出す
		//                  isCreate = false;
		//                  groupCnt++;
		//                  //Instantiate(enemy_ClamChowder_Group_Two_Top, createPos_FourGroup.transform.position, transform.rotation);
		//                  GameObject enemy_ClamChowder_Group_Two_Top = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Two_Top.Active_Obj();
		//                  enemy_ClamChowder_Group_Two_Top.transform.position = createPos_FourGroup.transform.position;
		//                  enemy_ClamChowder_Group_Two_Top.transform.rotation = transform.rotation;

		//                  //Instantiate(enemy_ClamChowder_Group_Two_Under, createPos_FourGroup.transform.position, transform.rotation);
		//                  GameObject enemy_ClamChowder_Group_Two_Under = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Two_Under.Active_Obj();
		//                  enemy_ClamChowder_Group_Two_Under.transform.position = createPos_FourGroup.transform.position;
		//                  enemy_ClamChowder_Group_Two_Under.transform.rotation = transform.rotation;

		//                  break;

		//              case 10:					//奥からくる闘牛型が縦に3つ並んだ敵の群れを１つ出す（真ん中がアイテムを落とす敵）
		//			isCreate = false;
		//                  groupCnt++;
		//                  //GameObject Battle_Ship2 = enemy_BattleShip2;
		//                  //Instantiate(enemy_BattleShip2, createBattleShipPos.transform.position, enemy_BattleShip2.transform.rotation);

		//                  //GameObject Battle_Ship1 = enemy_BattleShip;
		//                  //Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
		//			//Instantiate(enemy_ClamChowder_Group_Three_Item, createPos_FourGroup.transform.position, transform.rotation);
		//			GameObject enemy_ClamChowder_Group_Three_Item = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Three_Item.Active_Obj();
		//			enemy_ClamChowder_Group_Three_Item.transform.position = createPos_FourGroup.transform.position;
		//			enemy_ClamChowder_Group_Three_Item.transform.rotation = transform.rotation;

		//			break;

		//		case 11:					//戦艦を2体出す
		//			isCreate = false;
		//			groupCnt++;
		//			//GameObject Battle_Ship2 = enemy_BattleShip2;
		//			GameObject Battle_Ship1 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
		//			BattleshipType_Enemy b1 = Battle_Ship1.GetComponent<BattleshipType_Enemy>();
		//			b1.Is_up = false;

		//			GameObject Battle_Ship2 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
		//			BattleshipType_Enemy b2 = Battle_Ship2.GetComponent<BattleshipType_Enemy>();
		//			b2.Is_up = true;

		//			break;

		//		case 12: //戦艦を2体出す2回目
		//			isCreate = false;
		//			groupCnt++;
		//			//GameObject Battle_Ship2 = enemy_BattleShip2;
		//			GameObject Battle_Ship3 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
		//			BattleshipType_Enemy b3 = Battle_Ship3.GetComponent<BattleshipType_Enemy>();
		//			b3.Is_up = false;

		//			GameObject Battle_Ship4 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
		//			BattleshipType_Enemy b4 = Battle_Ship4.GetComponent<BattleshipType_Enemy>();
		//			b4.Is_up = true;

		//			break;

		//		case 13:					//奥からくる闘牛型が縦7つに並んだ群れを一つ出す
		//                  isCreate = false;
		//                  groupCnt++;
		//                  //Instantiate(enemy_ClamChowder_Group_Seven, createPos_FourGroup.transform.position, transform.rotation);
		//                  GameObject enemy_ClamChowder_Group_Seven = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Seven.Active_Obj();
		//                  enemy_ClamChowder_Group_Seven.transform.position = createPos_FourGroup.transform.position;
		//                  enemy_ClamChowder_Group_Seven.transform.rotation = transform.rotation;

		//                  break;
		//		case 14:					//奥からくる闘牛型が縦7つに並んだ群れを一つ出す
		//                  isCreate = false;
		//                  groupCnt++;
		//                  //Instantiate(enemy_ClamChowder_Group_Seven, createPos_FourGroup.transform.position, transform.rotation);
		//                  GameObject enemy_ClamChowder_Group_Seven1 = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Seven.Active_Obj();
		//                  enemy_ClamChowder_Group_Seven1.transform.position = createPos_FourGroup.transform.position;
		//                  enemy_ClamChowder_Group_Seven1.transform.rotation = transform.rotation;

		//                  break;
		//		case 15:					//奥からくる闘牛型が縦7つに並んだ群れを一つ出す
		//                  isCreate = false;
		//                  groupCnt++;
		//                  //Instantiate(enemy_ClamChowder_Group_Seven, createPos_FourGroup.transform.position, transform.rotation);
		//                  GameObject enemy_ClamChowder_Group_Seven2 = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Seven.Active_Obj();
		//                  enemy_ClamChowder_Group_Seven2.transform.position = createPos_FourGroup.transform.position;
		//                  enemy_ClamChowder_Group_Seven2.transform.rotation = transform.rotation;

		//                  break;

		//              case 16:					//中ボス出す
		//                  isCreate = false;
		//                  groupCnt++;
		//                  //Instantiate(enemy_MiddleBoss_Father, createMiddleBossPos.transform.position, transform.rotation);
		//                  GameObject Boss_Middle = Obj_Storage.Storage_Data.Boss_Middle.Active_Obj();
		//                  Boss_Middle.transform.position = createMiddleBossPos.transform.position;
		//                  Boss_Middle.transform.rotation = transform.rotation;

		//                  break;

		//              case 17:					//右上と右下に闘牛型が3つ縦に並んだ群れを出す
		//                  isCreate = false;
		//                  groupCnt++;
		//                  //Instantiate(enemy_ClamChowder_Group_ThreeWaveOnlyUp, createPosR3.transform.position, transform.rotation);
		//                  GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyUp.Active_Obj();
		//                  enemy_ClamChowder_Group_ThreeWaveOnlyUp.transform.position = createPosR3.transform.position;
		//                  enemy_ClamChowder_Group_ThreeWaveOnlyUp.transform.rotation = transform.rotation;

		//                  //Instantiate(enemy_ClamChowder_Group_ThreeWaveOnlyDown, createPosRm3.transform.position, transform.rotation);
		//                  GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyDown.Active_Obj();
		//                  enemy_ClamChowder_Group_ThreeWaveOnlyDown.transform.position = createPosRm3.transform.position;
		//                  enemy_ClamChowder_Group_ThreeWaveOnlyDown.transform.rotation = transform.rotation;


		//                  break;

		//              case 18:						//右上と右下に闘牛型が3つ縦に並んだ群れを出す
		//			isCreate = false;
		//                  groupCnt++;
		//                  //Instantiate(enemy_ClamChowder_Group_ThreeWaveOnlyUp, createPosR3.transform.position, transform.rotation);
		//                  GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp2 = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyUp.Active_Obj();
		//                  enemy_ClamChowder_Group_ThreeWaveOnlyUp2.transform.position = createPosR3.transform.position;
		//                  enemy_ClamChowder_Group_ThreeWaveOnlyUp2.transform.rotation = transform.rotation;

		//                  //Instantiate(enemy_ClamChowder_Group_ThreeWaveOnlyDown, createPosRm3.transform.position, transform.rotation);
		//                  GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown2 = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyDown.Active_Obj();
		//                  enemy_ClamChowder_Group_ThreeWaveOnlyDown2.transform.position = createPosRm3.transform.position;
		//                  enemy_ClamChowder_Group_ThreeWaveOnlyDown2.transform.rotation = transform.rotation;

		//                  break;

		//              case 19:						//右上と右下に闘牛型が3つ縦に並んだ群れを出す（アイテム落とす敵入り）
		//			isCreate = false;
		//                  groupCnt++;
		//                  //Instantiate(enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item, createPosR3.transform.position, transform.rotation);
		//                  GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item.Active_Obj();
		//                  enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item.transform.position = createPosR3.transform.position;
		//                  enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item.transform.rotation = transform.rotation;

		//                  //Instantiate(enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item, createPosRm3.transform.position, transform.rotation);
		//                  GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item.Active_Obj();
		//                  enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item.transform.position = createPosRm3.transform.position;
		//                  enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item.transform.rotation = transform.rotation;

		//                  break;

		//              case 20:						//右上と右下に闘牛型が3つ縦に並んだ群れを出す
		//			isCreate = false;
		//                  groupCnt++;
		//                  //Instantiate(enemy_ClamChowder_Group_ThreeWaveOnlyUp, createPosR3.transform.position, transform.rotation);
		//                  GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp3 = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyUp.Active_Obj();
		//                  enemy_ClamChowder_Group_ThreeWaveOnlyUp3.transform.position = createPosR3.transform.position;
		//                  enemy_ClamChowder_Group_ThreeWaveOnlyUp3.transform.rotation = transform.rotation;

		//                  //Instantiate(enemy_ClamChowder_Group_ThreeWaveOnlyDown, createPosRm3.transform.position, transform.rotation);
		//                  GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown3 = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyDown.Active_Obj();
		//                  enemy_ClamChowder_Group_ThreeWaveOnlyDown3.transform.position = createPosRm3.transform.position;
		//                  enemy_ClamChowder_Group_ThreeWaveOnlyDown3.transform.rotation = transform.rotation;

		//                  break;

		//              case 21:						//右上と右下に闘牛型が3つ縦に並んだ群れを出す
		//			isCreate = false;
		//                  groupCnt++;
		//                  //Instantiate(enemy_ClamChowder_Group_ThreeWaveOnlyUp, createPosR3.transform.position, transform.rotation);
		//                  GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp4 = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyUp.Active_Obj();
		//                  enemy_ClamChowder_Group_ThreeWaveOnlyUp4.transform.position = createPosR3.transform.position;
		//                  enemy_ClamChowder_Group_ThreeWaveOnlyUp4.transform.rotation = transform.rotation;

		//                  //Instantiate(enemy_ClamChowder_Group_ThreeWaveOnlyDown, createPosRm3.transform.position, transform.rotation);
		//                  GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown4 = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyDown.Active_Obj();
		//                  enemy_ClamChowder_Group_ThreeWaveOnlyDown4.transform.position = createPosRm3.transform.position;
		//                  enemy_ClamChowder_Group_ThreeWaveOnlyDown4.transform.rotation = transform.rotation;
		//                  break;

		//		case 22:
		//			isCreate = false;
		//			groupCnt++;
		//			GameObject Boss_01 = Obj_Storage.Storage_Data.Boss_1.Active_Obj();
		//			Boss_01.transform.position = Vector3.zero;
		//			break;
		//          }
		//      }
		#endregion
		//円盤の群れを１つ右上から出す		(180)
		if (Is_A_Specified_Frame(turning_frame) && groupCnt == 1)
		{
			GameObject enemy_UFO_Group = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group.transform.position = createPosR3.transform.position + new Vector3(8.5f, 0, 0);
			enemy_UFO_Group.transform.rotation = transform.rotation;

			nextEnemy = "右下円盤";
			Next_Condition(460);
            nowGroupCnt++;
		}
		//円盤の群れを１つ右下から出す(610)
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 2)
		{
			GameObject enemy_UFO_Group2 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group2.transform.position = createPosRm3.transform.position + new Vector3(8.5f, 0, 0);
			enemy_UFO_Group2.transform.rotation = transform.rotation;

			nextEnemy = "右上円盤";

			Next_Condition(460);
            nowGroupCnt++;
        }
        //円盤の群れを１つ右上から出す(680)
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 3)
		{
			GameObject enemy_UFO_Group3 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group3.transform.position = createPosR3.transform.position + new Vector3(8.5f, 0, 0);
			enemy_UFO_Group3.transform.rotation = transform.rotation;

			nextEnemy = "右下円盤";
			Next_Condition(460);
            nowGroupCnt++;
        }
        //円盤の群れを１つ右下から出す(930)
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 4)
		{
			GameObject enemy_UFO_Group4 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group4.transform.position = createPosRm3.transform.position + new Vector3(8.5f, 0, 0);
			enemy_UFO_Group4.transform.rotation = transform.rotation;

			Next_Condition(180);
            nextEnemy = "突進闘牛";

            nowGroupCnt++;
        }
        //突進 1020
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 5)
		{
			GameObject enemy_ClamChowder_Group_Four = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Four.Active_Obj();
			enemy_ClamChowder_Group_Four.transform.position = createPos_FourGroupL.transform.position;
			enemy_ClamChowder_Group_Four.transform.rotation = transform.rotation;

			Next_Condition(180);
            nextEnemy = "突進";
                    nowGroupCnt++;
}
        //突進 1320
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 6)
		{
            //Instantiate(enemy_ClamChowder_Group_FourBehind, createPos_FourGroupR.transform.position, transform.rotation);
            //GameObject enemy_ClamChowder_Group_Four = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Four.Active_Obj();
            //enemy_ClamChowder_Group_Four.transform.position = createPos_FourGroupL.transform.position;
            //enemy_ClamChowder_Group_Four.transform.rotation = transform.rotation;

            Instantiate(enemy_ClamChowder_Group_Four_NoItem, createPos_FourGroupL.transform.position, transform.rotation);

            nextEnemy = "闘牛三体真ん中アイテム";
			Next_Condition(200);
		}

		//奥からくる闘牛型が縦に3つ並んだ敵の群れを１つ出す（真ん中がアイテムを落とす敵） 1530
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 7)
		{
			GameObject enemy_ClamChowder_Group_Three_Item = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Three_Item.Active_Obj();
			enemy_ClamChowder_Group_Three_Item.transform.position = createPos_FourGroupL.transform.position;
			enemy_ClamChowder_Group_Three_Item.transform.rotation = transform.rotation;

			nextEnemy = "縦2体の闘牛上下で";
			Next_Condition(40);
		}
		//奥からくる闘牛型が縦に2つ並んだ敵の群れを２つ出す 1570
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 8)
		{
			GameObject enemy_ClamChowder_Group_Two_Top = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Two_Top.Active_Obj();
			enemy_ClamChowder_Group_Two_Top.transform.position = createPos_FourGroupL.transform.position;
			enemy_ClamChowder_Group_Two_Top.transform.rotation = transform.rotation;

			GameObject enemy_ClamChowder_Group_Two_Under = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Two_Under.Active_Obj();
			enemy_ClamChowder_Group_Two_Under.transform.position = createPos_FourGroupL.transform.position;
			enemy_ClamChowder_Group_Two_Under.transform.rotation = transform.rotation;

			nextEnemy = "闘牛縦に5体並んだ";
			Next_Condition(40);
		}
        //闘牛縦5体
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 9)
        {
            Instantiate(enemy_ClamChowder_Group_Five_NoItem, createPos_FourGroupL.transform.position, transform.rotation);

            nextEnemy = "右上＆右下円盤間隔広め";
            Next_Condition(260);
        }

        //円盤の群れを右上と右下から１つずつ出す 1930
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 10)
		{
			GameObject enemy_UFO_Group5 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group5.transform.position = createPosR3.transform.position + new Vector3(8.5f, 0, 0);
			enemy_UFO_Group5.transform.rotation = transform.rotation;

			GameObject enemy_UFO_Group6 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group6.transform.position = createPosRm3.transform.position + new Vector3(8.5f, 0, 0);
			enemy_UFO_Group6.transform.rotation = transform.rotation;

			nextEnemy = "右上＆右下円盤間隔狭め";
			Next_Condition(210);
		}
		//円盤の群れを右側から中央寄りで2つ出す 2050
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 11)
		{
			GameObject enemy_UFO_Group7 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group7.transform.position = createPosR1.transform.position + new Vector3(8.5f, 0, 0);
			enemy_UFO_Group7.transform.rotation = transform.rotation;

			GameObject enemy_UFO_Group8 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group8.transform.position = createPosRm1.transform.position + new Vector3(8.5f, 0, 0);
			enemy_UFO_Group8.transform.rotation = transform.rotation;

			nextEnemy = "右上＆右下円盤間隔広め";
			Next_Condition(210);
		}
		//円盤の群れを右上と右下から１つずつ出す 2170
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 12)
		{
			GameObject enemy_UFO_Group9 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group9.transform.position = createPosR4.transform.position + new Vector3(8.5f, 0, 0);
			enemy_UFO_Group9.transform.rotation = transform.rotation;

			GameObject enemy_UFO_Group10 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group10.transform.position = createPosRm4.transform.position + new Vector3(8.5f, 0, 0);
			enemy_UFO_Group10.transform.rotation = transform.rotation;

			nextEnemy = "戦艦2体";
			Next_Condition(290);
		}
		//戦艦を2体出す 2370
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 13)
		{
			GameObject Battle_Ship1 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
			BattleshipType_Enemy b1 = Battle_Ship1.GetComponent<BattleshipType_Enemy>();
			b1.Is_up = false;

			GameObject Battle_Ship2 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
			BattleshipType_Enemy b2 = Battle_Ship2.GetComponent<BattleshipType_Enemy>();
			b2.Is_up = true;

			nextEnemy = "戦艦2体（2回目）";
			Next_Condition(550);
		}
		//戦艦を2体出す2回目 2920
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 14)
		{
			GameObject Battle_Ship1 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
			BattleshipType_Enemy b1 = Battle_Ship1.GetComponent<BattleshipType_Enemy>();
			b1.Is_up = false;

			GameObject Battle_Ship2 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
			BattleshipType_Enemy b2 = Battle_Ship2.GetComponent<BattleshipType_Enemy>();
			b2.Is_up = true;

			nextEnemy = "縦7体の闘牛1回目";
			Next_Condition(650);
		}
		//奥からくる闘牛型が縦5つに並んだ群れを一つ出す 3570
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 15)
		{
			Instantiate(enemy_ClamChowder_Group_Five, createPos_FourGroupL.transform.position, transform.rotation);
			//GameObject enemy_ClamChowder_Group_Seven1 = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Seven.Active_Obj();
			//enemy_ClamChowder_Group_Seven1.transform.position = createPos_FourGroupL.transform.position;
			//enemy_ClamChowder_Group_Seven1.transform.rotation = transform.rotation;

			nextEnemy = "縦7体の闘牛2回目";
			Next_Condition(40);
		}
		//奥からくる闘牛型が縦5つに並んだ群れを一つ出す 3610
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 16)
		{
			Instantiate(enemy_ClamChowder_Group_Five, createPos_FourGroupL.transform.position, transform.rotation);
			//GameObject enemy_ClamChowder_Group_Seven1 = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Seven.Active_Obj();
			//enemy_ClamChowder_Group_Seven1.transform.position = createPos_FourGroupL.transform.position;
			//enemy_ClamChowder_Group_Seven1.transform.rotation = transform.rotation;

			nextEnemy = "縦7体の闘牛3回目";
			Next_Condition(40);
		}
		//奥からくる闘牛型が縦5つに並んだ群れを一つ出す 3650
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 17)
		{
			Instantiate(enemy_ClamChowder_Group_Five, createPos_FourGroupL.transform.position, transform.rotation);

			//GameObject enemy_ClamChowder_Group_Seven1 = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Seven.Active_Obj();
			//enemy_ClamChowder_Group_Seven1.transform.position = createPos_FourGroupL.transform.position;
			//enemy_ClamChowder_Group_Seven1.transform.rotation = transform.rotation;

			nextEnemy = "！！！！！中ボス！！！！";
			Next_Condition(640);
		}
		//中ボス出す 4290
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 18)
		{
			GameObject Boss_Middle = Obj_Storage.Storage_Data.Boss_Middle.Active_Obj();
			saveEnemyObj = Boss_Middle;
			Boss_Middle.transform.position = createMiddleBossPos.transform.position;
			Boss_Middle.transform.rotation = transform.rotation;

			nextEnemy = "右上と右下に縦3の闘牛";
			Next_Condition(180);
		}
		//右上と右下に闘牛型が3つ縦に並んだ群れを出す 4470
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 19)
		{
			Instantiate(enemy_ClamChowder_Group_TwoWaveOnlyDown, createPosR4.transform.position, transform.rotation);
			Instantiate(enemy_ClamChowder_Group_TwoWaveOnlyUp, createPosRm4.transform.position, transform.rotation);
			//GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Two_Top.Active_Obj();
			//enemy_ClamChowder_Group_ThreeWaveOnlyUp.transform.position = createPosR3.transform.position;
			//enemy_ClamChowder_Group_ThreeWaveOnlyUp.transform.rotation = transform.rotation;

			//GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Two_Under.Active_Obj();
			//enemy_ClamChowder_Group_ThreeWaveOnlyDown.transform.position = createPosRm3.transform.position;
			//enemy_ClamChowder_Group_ThreeWaveOnlyDown.transform.rotation = transform.rotation;

			nextEnemy = "右上と右下に縦3の闘牛2回目";
			Next_Condition(180);
		}
		//右上と右下に闘牛型が3つ縦に並んだ群れを出す 4650
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 20)
		{
			Instantiate(enemy_ClamChowder_Group_TwoWaveOnlyDown, createPosR4.transform.position, transform.rotation);
			Instantiate(enemy_ClamChowder_Group_TwoWaveOnlyUp, createPosRm4.transform.position, transform.rotation);

			//GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp2 = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Two_Top.Active_Obj();
			//enemy_ClamChowder_Group_ThreeWaveOnlyUp2.transform.position = createPosR3.transform.position;
			//enemy_ClamChowder_Group_ThreeWaveOnlyUp2.transform.rotation = transform.rotation;

			//GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown2 = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Two_Under.Active_Obj();
			//enemy_ClamChowder_Group_ThreeWaveOnlyDown2.transform.position = createPosRm3.transform.position;
			//enemy_ClamChowder_Group_ThreeWaveOnlyDown2.transform.rotation = transform.rotation;

			nextEnemy = "右上と右下に真ん中がアイテムの縦3の闘牛";
			Next_Condition(180);
		}
		//右上と右下に闘牛型が3つ縦に並んだ群れを出す（アイテム落とす敵入り） 4830

		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 21)
		{
			Instantiate(enemy_ClamChowder_Group_TwoWaveOnlyDown, createPosR4.transform.position, transform.rotation);
			Instantiate(enemy_ClamChowder_Group_TwoWaveOnlyUp, createPosRm4.transform.position, transform.rotation);
			//GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Two_Top.Active_Obj();
			//         enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item.transform.position = createPosR3.transform.position;
			//         enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item.transform.rotation = transform.rotation;

			//         GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Two_Under.Active_Obj();
			//         enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item.transform.position = createPosRm3.transform.position;
			//         enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item.transform.rotation = transform.rotation;

			nextEnemy = "右上と右下に縦3の闘牛3回目";
			Next_Condition(240);
		}
		//右上と右下に闘牛型が3つ縦に並んだ群れを出す 5070
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 22)
		{
			Instantiate(enemy_ClamChowder_Group_TwoWaveOnlyDown, createPosR4.transform.position, transform.rotation);
			Instantiate(enemy_ClamChowder_Group_TwoWaveOnlyUp, createPosRm4.transform.position, transform.rotation);
			//GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp3 = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Two_Top.Active_Obj();
			//         enemy_ClamChowder_Group_ThreeWaveOnlyUp3.transform.position = createPosR3.transform.position;
			//         enemy_ClamChowder_Group_ThreeWaveOnlyUp3.transform.rotation = transform.rotation;

			//         GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown3 = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Two_Under.Active_Obj();
			//         enemy_ClamChowder_Group_ThreeWaveOnlyDown3.transform.position = createPosRm3.transform.position;
			//         enemy_ClamChowder_Group_ThreeWaveOnlyDown3.transform.rotation = transform.rotation;

			nextEnemy = "右上と右下に縦3の闘牛4回目";
			Next_Condition(180);
		}
		// 右上と右下に闘牛型が3つ縦に並んだ群れを出す 5250
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 23)
		{
			Instantiate(enemy_ClamChowder_Group_TwoWaveOnlyDown, createPosR4.transform.position, transform.rotation);
			Instantiate(enemy_ClamChowder_Group_TwoWaveOnlyUp, createPosRm4.transform.position, transform.rotation);
			//         Instantiate(enemy_ClamChowder_Group_TwoWaveOnlyUp, createPosRm3.transform.position, transform.rotation);
			//GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp4 = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Two_Top.Active_Obj();
			//         enemy_ClamChowder_Group_ThreeWaveOnlyUp4.transform.position = createPosR3.transform.position;
			//         enemy_ClamChowder_Group_ThreeWaveOnlyUp4.transform.rotation = transform.rotation;

			//         GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown4 = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Two_Under.Active_Obj();
			//         enemy_ClamChowder_Group_ThreeWaveOnlyDown4.transform.position = createPosRm3.transform.position;
			//         enemy_ClamChowder_Group_ThreeWaveOnlyDown4.transform.rotation = transform.rotation;

			nextEnemy = "直進闘牛真ん中から";
			Next_Condition(700);
		}
		//直進の闘牛を右真ん中（後ろからくる）5950
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 24)
		{
			Instantiate(enemy_Clamchowder_Group_Straight, createPosR0.transform.position, transform.rotation);

			//Instantiate(enemy_Clamchowder_Group_Straight, createPosR3.transform.position, transform.rotation);
			//Instantiate(enemy_Clamchowder_Group_Straight, createPosRm3.transform.position, transform.rotation);

			//Instantiate(enemy_Clamchowder_Group_StraightBehind, createPosL3.transform.position, transform.rotation);
			//Instantiate(enemy_Clamchowder_Group_StraightBehind, createPosLm3.transform.position, transform.rotation);

			nextEnemy = "直進の闘牛を右上と右下から（前からくる）";
			Next_Condition(60);
		}
		//直進の闘牛を右上と右下（前からくる） 6010
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 25)
		{
			//Instantiate(enemy_Clamchowder_Group_Straight, createPosR0.transform.position, transform.rotation);
			Instantiate(enemy_Clamchowder_Group_Straight, createPosR3.transform.position, transform.rotation);
			Instantiate(enemy_Clamchowder_Group_Straight, createPosRm3.transform.position, transform.rotation);

			//Instantiate(enemy_Clamchowder_Group_Straight, createPosRm2.transform.position, transform.rotation);
			nextEnemy = "左右からハエ型4体の群れ出す";
			Next_Condition(290);

		}

		//ここの間に入れる（ヒトデを複数ポンポン出す予定）

		//左右からハエ型5体の群れ出す 6300　
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 26)
		{
			Instantiate(enemy_Beelzebub_Group_FourNomal, createPosR0.transform.position, transform.rotation);
			//Instantiate(enemy_Beelzebub_Group_FourBack, createPosL0.transform.position, transform.rotation);


			//Instantiate(enemy_Clamchowder_Group_Straight, createPosRm2.transform.position, transform.rotation);
			nextEnemy = "突進闘牛後ろから";
			Next_Condition(130);

		}
		//闘牛6430
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 27)
		{
			//Instantiate(enemy_ClamChowder_Group_FourBehind, createPos_FourGroupR.transform.position, transform.rotation);


			//Instantiate(enemy_Clamchowder_Group_Straight, createPosRm2.transform.position, transform.rotation);
			nextEnemy = "ハエ";
			Next_Condition(90);

		}
		//ハエ6520
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 28)
		{
			Instantiate(enemy_Beelzebub_Group_FourNomal, createPosR0.transform.position, transform.rotation);


			//Instantiate(enemy_Clamchowder_Group_Straight, createPosRm2.transform.position, transform.rotation);
			nextEnemy = "戦艦二体";
			Next_Condition(260);

		}
		//戦艦２体　6780
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 29)
		{
			GameObject Battle_Ship1 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
			BattleshipType_Enemy b1 = Battle_Ship1.GetComponent<BattleshipType_Enemy>();
			b1.Is_up = false;

			GameObject Battle_Ship2 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
			BattleshipType_Enemy b2 = Battle_Ship2.GetComponent<BattleshipType_Enemy>();
			b2.Is_up = true;

			nextEnemy = "直線闘牛を右真ん中から";
			Next_Condition(380);

		}
		//直線闘牛右真ん中　7160
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 30)
		{
			Instantiate(enemy_Clamchowder_Group_Straight, createPosR0.transform.position, transform.rotation);

			nextEnemy = "戦艦右真ん中から";
			Next_Condition(260);

		}

		//ここの間に追加（奥からくる闘牛縦3体x3）
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 31)
		{
			GameObject waveThreeEnemy = Instantiate(enemy_ClamChowder_Group_Three, createPosL0.transform.position, transform.rotation);

			nextEnemy = "右上と右下に奥からくる縦に3体の闘牛";
			Next_Condition(40);
		}



		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 32)
		{
			GameObject waveThreeEnemy = Instantiate(enemy_ClamChowder_Group_Three, createPosL2.transform.position, transform.rotation);
			GameObject waveThreeEnemy2 = Instantiate(enemy_ClamChowder_Group_Three, createPosLm2.transform.position, transform.rotation);

			nextEnemy = "戦艦真ん中1体";
			Next_Condition(160);
		}
		//else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 32)
		//{
		//	GameObject waveThreeEnemy = Instantiate(enemy_ClamChowder_Group_Four_NoItem, createPos_FourGroupL.transform.position, transform.rotation);

		//	nextEnemy = "右上と右下に奥からくる縦に3体の闘牛";
		//	Next_Condition(330);
		//}


		//戦艦1体右真ん中　7580
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 33)
        {
            GameObject Battle_Ship1 = Instantiate(enemy_BattleShip, createPosR0.transform.position, enemy_BattleShip.transform.rotation);
            Battle_Ship1.transform.position = createPosR0.transform.position;
            BattleshipType_Enemy b1 = Battle_Ship1.GetComponent<BattleshipType_Enemy>();
            b1.is_sandwich = false;
            b1.Is_up = false;


            nextEnemy = "直線闘牛を右上と右下から";
            Next_Condition(260);

        }
        //直線闘牛右真ん中　7840
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 34)
        {
            Instantiate(enemy_Clamchowder_Group_Straight, createPosR3.transform.position, transform.rotation);

            Instantiate(enemy_Clamchowder_Group_Straight, createPosRm3.transform.position, transform.rotation);

            nextEnemy = "直線闘牛を右上と右下から";
            Next_Condition(150);

        }
        //直線闘牛右真ん中　7990（ハヤブサにかえる何体か出す）
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 35)
        {
            Instantiate(enemy_Clamchowder_Group_Straight, createPosR3.transform.position, transform.rotation);

            Instantiate(enemy_Clamchowder_Group_Straight, createPosRm3.transform.position, transform.rotation);

            nextEnemy = "円盤群1つ右真ん中から";
            Next_Condition(360);

        }

        //円盤真ん中8350　（これ削除　次の2つの円盤を詰めて出す）
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 36)
        {
            GameObject enemy_UFO_Group = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
            enemy_UFO_Group.transform.position = createPosR0.transform.position;
            nextEnemy = "右上と右下から円盤";
            Next_Condition(150);
        }
        //円盤右上右下中央より8500
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 37)
        {
            GameObject enemy_UFO_Group = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
            enemy_UFO_Group.transform.position = createPosR2.transform.position;

            GameObject enemy_UFO_Group2 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
            enemy_UFO_Group2.transform.position = createPosRm2.transform.position;

            nextEnemy = "右上と右下から円盤2群";
            Next_Condition(150);
        }
        //円盤右上右下8650
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 38)
        {
            GameObject enemy_UFO_Group = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
            enemy_UFO_Group.transform.position = createPosR4.transform.position;

            GameObject enemy_UFO_Group2 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
            enemy_UFO_Group2.transform.position = createPosRm4.transform.position;

            nextEnemy = "闘牛型が縦7つに並んだ群れ";
            Next_Condition(300);
        }

		//ここにビートル5体挿入

		//ビートル後、新闘牛縦4体の並び


        //奥からくる闘牛型が縦7つに並んだ群れを一つ出す 8950
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 39)
        {
            //Instantiate(enemy_ClamChowder_Group_SevenWave, createPos_FourGroupL.transform.position, transform.rotation);
            Instantiate(enemy_ClamChowder_Group_Five, createPos_FourGroupL.transform.position, transform.rotation);
			//GameObject enemy_ClamChowder_Group_Seven = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Seven.Active_Obj();
            //enemy_ClamChowder_Group_Seven.transform.position = createPos_FourGroupL.transform.position;
            //enemy_ClamChowder_Group_Seven.transform.rotation = transform.rotation;

            nextEnemy = "縦7体の闘牛2回目";
            Next_Condition(40);
        }
        //奥からくる闘牛型が縦7つに並んだ群れを一つ出す 8990　（この2回目は削除）
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 40)
        {
            //Instantiate(enemy_ClamChowder_Group_SevenWave, createPos_FourGroupL.transform.position, transform.rotation);
			Instantiate(enemy_ClamChowder_Group_Five, createPos_FourGroupL.transform.position, transform.rotation);

            //GameObject enemy_ClamChowder_Group_Seven = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Seven.Active_Obj();
            //enemy_ClamChowder_Group_Seven.transform.position = createPos_FourGroupL.transform.position;
            //enemy_ClamChowder_Group_Seven.transform.rotation = transform.rotation;

            nextEnemy = "縦7体の闘牛3回目";
            Next_Condition(40);
        }
        //奥からくる闘牛型が縦7つに並んだ群れを一つ出す 9030
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 41)
        {
            //Instantiate(enemy_ClamChowder_Group_SevenWave, createPos_FourGroupL.transform.position, transform.rotation);
			Instantiate(enemy_ClamChowder_Group_Five, createPos_FourGroupL.transform.position, transform.rotation);

            //GameObject enemy_ClamChowder_Group_Seven = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Seven.Active_Obj();
            //enemy_ClamChowder_Group_Seven.transform.position = createPos_FourGroupL.transform.position;
            //enemy_ClamChowder_Group_Seven.transform.rotation = transform.rotation;

            nextEnemy = "大ボス！！！";
            Next_Condition(800);
        }

        // 大ボス(10810)
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 42)
        {
			GameObject Boss_01 = Obj_Storage.Storage_Data.Boss_1.Active_Obj();
			Boss_01.transform.position = new Vector3(10.0f, 0.0f, 0.0f);

			GameObject mistSaveObj = Instantiate(mistEffectObj, transform.position, transform.rotation);
			mistEffectObj.transform.position = new Vector3(0, 0, 3);
			mistParticle = mistSaveObj.GetComponent<ParticleSystem>();
			backActive_Script = mistSaveObj.GetComponent<BackgroundActivation>();
			mistParticle.Play();
			backActive_Script.TransparencyChangeTrigger();



            nextEnemy = "バキュラ群";
            Next_Condition(30000);
        }
        //バキュラ群 17030
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 43)
        {
            Instantiate(enemy_Bacula_Sixteen, createBaculaGroupPos.transform.position, transform.rotation);

            nextEnemy = "隕石群(5つ)";
            Next_Condition(1400);
        }
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 44)
        {
            Instantiate(enemy_Bacula_FourOnly, createBaculaGroupPos.transform.position, transform.rotation);

            nextEnemy = "バウンド隕石";
            Next_Condition(60);
        }
		//バウンド隕石1
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 45)
        {
            Instantiate(Enemy_BoundMeteors, createMeteorPosR0.transform.position, transform.rotation);

            nextEnemy = "バウンド隕石2";
            Next_Condition(240);

        }
		//バウンド隕石2
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 46)
		{
			Instantiate(Enemy_BoundMeteors, createMeteorPosR0.transform.position, transform.rotation);

			nextEnemy = "バウンド隕石3";
			Next_Condition(240);

		}
		//バウンド隕石3
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 47)
		{
			Instantiate(Enemy_BoundMeteors, createMeteorPosR0.transform.position, transform.rotation);

			nextEnemy = "バウンド隕石4";
			Next_Condition(240);

		}
		//バウンド隕石4
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 48)
		{
			Instantiate(Enemy_BoundMeteors, createMeteorPosR0.transform.position, transform.rotation);

			nextEnemy = "バウンド隕石5";
			Next_Condition(240);

		}
		//バウンド隕石5
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 49)
		{
			Instantiate(Enemy_BoundMeteors, createMeteorPosR0.transform.position, transform.rotation);

			nextEnemy = "バウンド隕石6";
			Next_Condition(240);

		}
		//バウンド隕石6
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 50)
        {
            Instantiate(Enemy_BoundMeteors, createMeteorPosR0.transform.position, transform.rotation);

            nextEnemy = " ";
            Next_Condition(1200);

        }

        //隕石群(5つ) 18950
        //      else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 41)
        //      {
        //          Instantiate(enemy_Meteors, createMeteorPosR0.transform.position, transform.rotation);

        //          nextEnemy = "隕石2";
        //          Next_Condition(1730);
        //      }
        ////隕石の間を行く敵 21120
        //else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 42)
        //{
        //	Instantiate(enemy_MeteorWaveGroup, createPosR3.transform.position, transform.rotation);

        //	nextEnemy = " ";
        //	Next_Condition(1800);
        //}
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 51)
        {
            Scene_Manager.Manager.Screen_Transition_To_Clear();
        }
		////3隕石 21780
		//else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 43)
		//{
		//    Instantiate(enemy_MEteor_Under, createMeteorPosRm3_57.transform.position, transform.rotation);

			//    nextEnemy = "隕石4";
			//    Next_Condition(300);
			//}
			////4隕石 22170
			//else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 44)
			//{
			//    Instantiate(enemy_Meteor, createMeteorPosR2_988.transform.position, transform.rotation);

			//    nextEnemy = "隕石5";
			//    Next_Condition(180);
			//}
			////5隕石 22470
			//else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 45)
			//{
			//    Instantiate(enemy_Meteor_Mini, createMeteorPosRm2_822.transform.position, transform.rotation);

			//    nextEnemy = "隕石";
			//    Next_Condition(380);
			//}

			//22300
	}

	void CreateEnemyGroup_02()
	{
		//円盤の群れを１つ右上から出す
		if (Is_A_Specified_Frame(turning_frame) && groupCnt == 1)
		{
			GameObject enemy_UFO_Group = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group.transform.position = createPosR3.transform.position;
			enemy_UFO_Group.transform.rotation = transform.rotation;

			Next_Condition(240);
		}
		//円盤の群れを１つ右上から出す		
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 2)
		{
			GameObject enemy_UFO_Group = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group.transform.position = createPosR3.transform.position;
			enemy_UFO_Group.transform.rotation = transform.rotation;

			Next_Condition(240);
		}
		//円盤の群れを１つ右上から出す		
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 3)
		{
			GameObject enemy_UFO_Group = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group.transform.position = createPosR3.transform.position;
			enemy_UFO_Group.transform.rotation = transform.rotation;

			Next_Condition(240);
		}

		//ボス仮置きを出す
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 4)
		{
			GameObject Boss = Obj_Storage.Storage_Data.Boss_2.Active_Obj();
			Boss.transform.position = new Vector3(10.0f, 0.0f, 0.0f);
			Next_Condition(240);
		}
	}
	//---------------------------------------------------------------------

	//---------------------------------------------------------------------
	void CreateCheck()
	{
		//groupCntが1の時のを出すタイミング
		if (Is_A_Specified_Frame(180))
		{
			isCreate = true;
		}
		//groupCntが2の時のを出すタイミング
		else if (Is_A_Specified_Frame(1000))
		{
			isCreate = true;
		}
		//groupCntが3の時のを出すタイミング
		else if (Is_A_Specified_Frame(720))
		{
			isCreate = true;
		}
		//groupCntが4の時のを出すタイミング
		else if (Is_A_Specified_Frame(1020))
		{
			isCreate = true;
		}
		//groupCntが5の時のを出すタイミング
		else if (Is_A_Specified_Frame(1140))
		{
			isCreate = true;
		}
		//groupCntが6の時のを出すタイミング
		else if (Is_A_Specified_Frame(1500))
		{
			isCreate = true;
		}
		//groupCntが7の時のを出すタイミング
		else if (Is_A_Specified_Frame(1620))
		{
			isCreate = true;
		}
		//groupCntが8の時のを出すタイミング
		else if (Is_A_Specified_Frame(1740))
		{
			isCreate = true;
		}
		//groupCntが9の時のを出すタイミング
		else if (Is_A_Specified_Frame(1920))
		{
			isCreate = true;
		}
		//groupCntが10の時のを出すタイミング
		else if (Is_A_Specified_Frame(1860))
		{
			isCreate = true;
		}
		//groupCntが11の時のを出すタイミング
		else if (Is_A_Specified_Frame(2010))
		{
			isCreate = true;
		}
		//groupCntが12の時のを出すタイミング
		else if (Is_A_Specified_Frame(2660))
		{
			isCreate = true;
		}
		//groupCntが13の時のを出すタイミング
		else if (Is_A_Specified_Frame(3780))
		{
			isCreate = true;
		}
		//groupCntが14の時のを出すタイミング
		else if (Is_A_Specified_Frame(3820))
		{
			isCreate = true;
		}
		//groupCntが15の時のを出すタイミング
		else if (Is_A_Specified_Frame(3860))
		{
			isCreate = true;
		}
		//groupCntが16の時のを出すタイミング
		else if (Is_A_Specified_Frame(4230))
        {
            isCreate = true;
		}
		//groupCntが17の時のを出すタイミング
		else if (Is_A_Specified_Frame(4410))
		{
			isCreate = true;
		}
		//groupCntが18の時のを出すタイミング
		else if (Is_A_Specified_Frame(4590))
		{
			isCreate = true;
		}
		//groupCntが19の時のを出すタイミング
		else if (Is_A_Specified_Frame(4600))
		{
			isCreate = true;
		}
		//groupCntが20の時のを出すタイミング
		else if (Is_A_Specified_Frame(5070))
		{
			isCreate = true;
		}
		// 21
		else if (Is_A_Specified_Frame(5250))
		{
			isCreate = true;
		}
		// 22
		else if (Is_A_Specified_Frame(5450))
		{
			isCreate = true;
		}
	}
	#region
	//  void CreateCheck()
	//  {
	//if(second >= 4300 + 470 + 180 + 300 + 200 && 4300 + 470 + 180 + 300 + 200 >= psecond)
	//{
	//	isCreate = true;
	//}
	//// 21
	//else if (second >= 4300 + 470 + 180 + 300 && 4300 + 470 + 180 + 300 >= psecond)
	//{
	//	isCreate = true;
	//}
	////groupCntが20の時のを出すタイミング
	//else if (second >= 4300 + 470 + 300 && 4300 + 470 + 300 >= psecond)
	//      {
	//          isCreate = true;
	//}
	////groupCntが19の時のを出すタイミング
	//else if (second >= 3820 + 470 + 300 && 3820 + 470 + 300 >= psecond)
	//      {
	//          isCreate = true;
	//}
	////groupCntが18の時のを出すタイミング
	//else if (second >= 3820 + 470 + 300 && 3820 + 470 + 300 >= psecond)
	//      {
	//          isCreate = true;
	//}
	////groupCntが17の時のを出すタイミング
	//else if (second >= 3640 + 470 + 300 && 3640 + 470 + 300 >= psecond)
	//      {
	//          isCreate = true;
	//}
	////groupCntが16の時のを出すタイミング
	//else if (second >= 3460 + 470 +300 && 3460 + 470 + 300 >= psecond)
	//      {
	//          isCreate = true;
	//}
	////groupCntが15の時のを出すタイミング
	//else if (second >= 3430 + 470+80-120 && 3430 + 470 + 80 - 120 >= psecond)
	//{
	//	isCreate = true;
	//}
	////groupCntが14の時のを出すタイミング
	//else if (second >= 3430 + 470+40-120 && 3430 + 470 + 40 - 120 >= psecond)
	//{
	//	isCreate = true;
	//}
	////groupCntが13の時のを出すタイミング
	//else if (second >= 3430 + 470-120 && 3430 + 470 - 120 >= psecond)
	//      {
	//          isCreate = true;
	//}
	////groupCntが12の時のを出すタイミング
	//else if (second >= 2890 + 470 - 700 && 2890 + 470 - 700 >= psecond)
	//      {
	//          isCreate = true;
	//}
	////groupCntが11の時のを出すタイミング
	//else if (second >= 2120 - 260 && 2120 - 260 >= psecond)
	//      {
	//          isCreate = true;
	//}
	////groupCntが10の時のを出すタイミング
	//else if (second >= 2010 && 2010 >= psecond)
	//      {
	//          isCreate = true;
	//}
	////groupCntが9の時のを出すタイミング
	//else if (second >= 1920 && 1920 >= psecond)
	//      {
	//          isCreate = true;
	//}
	////groupCntが8の時のを出すタイミング
	//else if (second >= 1740 && 1740 >= psecond)
	//      {
	//          isCreate = true;
	//}
	////groupCntが7の時のを出すタイミング
	//else if (second >= 1620 && 1620 >= psecond)
	//      {
	//          isCreate = true;
	//}
	////groupCntが6の時のを出すタイミング
	//else if (second >= 1500 && 1500 >= psecond)
	//      {
	//          isCreate = true;
	//}
	////groupCntが5の時のを出すタイミング
	//else if (second >= 1140 && 1140 >= psecond)
	//      {
	//          isCreate = true;
	//}
	////groupCntが4の時のを出すタイミング
	//else if (second >= 1020 && 1020 >= psecond)
	//      {
	//          isCreate = true;
	//}
	////groupCntが3の時のを出すタイミング
	//else if (second >= 720 && 720 >= psecond)
	//      {
	//          isCreate = true;
	//}
	////groupCntが2の時のを出すタイミング
	//else if (second >= 420 && 420 >= psecond)
	//      {
	//          isCreate = true;
	//}
	////groupCntが1の時のを出すタイミング
	//else if (second >= 120 && 120 >= psecond)
	//      {
	//          isCreate = true;
	//}
	//  }
	//---------------------------------------------------------------------
	#endregion

	/// <summary>
	/// 指定されたフレームかどうか
	/// </summary>
	/// <param name="specified_frame"> 指定フレーム </param>
	/// <returns> あっているか </returns>
	private bool Is_A_Specified_Frame( int specified_frame )
	{
		return frameCnt >= specified_frame && specified_frame >= PreviousCount;
	}

	private void Next_Condition(int add_frame)
	{
		groupCnt++;
        //nowGroupCnt++;
		turning_frame += add_frame;
	}
}
