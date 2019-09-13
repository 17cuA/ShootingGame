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
    //public GameObject createMeteorPosR4_814;
    //public GameObject createMeteorPosR2_988;
    //public GameObject createMeteorPosR1_494;
    public GameObject createMeteorPosR0;
    //public GameObject createMeteorPosRm1_162;
    //public GameObject createMeteorPosRm2_822;
    //public GameObject createMeteorPosRm3_57;
    //public GameObject createMeteorPosRm4_814;

    //public GameObject enemy_UFO_Group;
    public GameObject enemy_UFO_Group_Five;
    public GameObject enemy_ClamChowder_Group_Four;
    public GameObject enemy_ClamChowder_Group_Four_NoItem;

    public GameObject enemy_ClamChowder_Group_Five;
    public GameObject enemy_ClamChowder_Group_Five_NoItem;
    public GameObject enemy_ClamChowder_Group_FourBehind;
    public GameObject enemy_ClamChowder_Group_Two;
    public GameObject enemy_ClamChowder_Group_Two_Top;
    public GameObject enemy_ClamChowder_Group_Two_Under;
    public GameObject enemy_ClamChowder_Group_TwoWaveOnlyUp;
    public GameObject enemy_ClamChowder_Group_TwoWaveOnlyDown;
    public GameObject enemy_ClamChowder_Group_TwoWaveOnlyUp_Item;
    public GameObject enemy_ClamChowder_Group_TwoWaveOnlyDown_Item;
    public GameObject enemy_ClamChowder_Group_Three;
    public GameObject enemy_ClamChowder_Group_Three_Item;
    public GameObject enemy_ClamChowder_Group_SevenWave;
    public GameObject enemy_Clamchowder_Group_Straight;
    public GameObject enemy_Clamchowder_Group_StraightBehind;
    public GameObject enemy_ClamChowder_Group_FourTriangle;
    public GameObject enemy_ClamChowder_Group_FourTriangle_NoItem;
    public GameObject enemy_ClamChowder_Group_TwelveStraight;
    public GameObject enemy_ClamChowder_Group_SevenStraight;
    public GameObject enemy_ClamChowder_Group_SixStraight;
    public GameObject enemy_ClamChowder_Group_UpSevenDiagonal;
    public GameObject enemy_ClamChowder_Group_DownSevenDiagonal;
    public GameObject enemy_ClamChowder_Group_TenStraight;
    //public GameObject enemy_MiddleBoss_Father;
    //public GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp;
    //public GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown;
    //public GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item;
    //public GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item;
    public GameObject enemy_BattleShip;
    public GameObject enemy_Beelzebub_Group_FourNomal;
    public GameObject enemy_Beelzebub_Group_FourBack;
    public GameObject enemy_Beelzebub_Group_FourWide;
    public GameObject enemy_Beelzebub_Group_FourWide_Item;
    public GameObject enemy_Beelzebub_Group_EightNormal_Item;
    public GameObject enemy_Bacula_Sixteen;
    public GameObject enemy_Bacula_FourOnly;
    //public GameObject enemy_Meteor;
    //public GameObject enemy_Meteor_Top;
    //public GameObject enemy_Meteor_Under;
    //public GameObject enemy_Meteors;
    //public GameObject enemy_Meteor_Mini;
    //public GameObject enemy_MeteorWaveGroup;
    public GameObject enemy_SlowFollow;
    public GameObject Enemy_BoundMeteors;
    public GameObject enemy_Star_Fish_Spowner;

    public GameObject enemy_Beetle_Group;
    public GameObject enemy_Beetle_Group_Three;
    public GameObject enemy_Beetle_Group_Seven;

    public GameObject enemy_MoaiBossGroup;


    public GameObject saveEnemyObj;

    public int PreviousCount = 0;

    public int frameCnt = 0;    //フレームカウント：これの値で生成のタイミングをはかる
    public int groupCnt = 1;    //画面に出す群れのカウント
    public int nowGroupCnt;
    public int[] groupCntArray;
    //float plusNum = 60;			//
    //float plusNum2 = 60;		//この三つは強引に表示のフレームをずらすために使ったので消した方がいいけど面倒
    //float plusNum3 = 60;        //

    public int turning_frame = 180;
    public string nextEnemy;

    public EnemyGroupManage group_Script;

    GameObject middleBossOBj;
    Enemy_MiddleBoss middleBoss_Script;

    GameObject oneBossOBj;
    One_Boss oneBoss_Script;
    GameObject mistEffectObj;
    ParticleSystem mistParticle;
    public BackgroundActivation backActive_Script;

    GameObject twoBossObj;
    Two_Boss twoBoss_Script;

    GameObject moaiObj;
    Enemy_Moai moai_Script;


    public bool isCreate;       //表示するときにtrueにする
    public bool isBaculaDestroy = false;
    public bool isMiddleBossDead = false;
    public bool isOneBossAlive = false;
    public bool isTwoBossAlive = false;
    public bool isMoaiAlive = false;

    public bool isMiddleBossSkip = true;

    public bool isNowOneBoss = false;
    public bool isNowTwoBoss = false;
    public bool isNowMoai = false;
    public bool isDebug = false;
    public bool aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa = false;

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

        //createMeteorPosR4_814 = GameObject.Find("CreateMeteorPos_Right_4.814");
        //createMeteorPosR2_988 = GameObject.Find("CreateMeteorPos_Right_2.988");
        //createMeteorPosR1_494 = GameObject.Find("CreateMeteorPos_Right_1.494");
        createMeteorPosR0 = GameObject.Find("CreateMeteorPos_Right_0");
        //createMeteorPosRm1_162 = GameObject.Find("CreateMeteorPos_Right_-1.162");
        //createMeteorPosRm2_822 = GameObject.Find("CreateMeteorPos_Right_-2.822");
        //createMeteorPosRm3_57 = GameObject.Find("CreateMeteorPos_Right_-3.57");
        //createMeteorPosRm4_814 = GameObject.Find("CreateMeteorPos_Right_-4.814");

        //enemy_UFO_Group = Resources.Load("Enemy/Enemy_UFO_Group") as GameObject;
        enemy_UFO_Group_Five = Resources.Load("Enemy/Enemy_UFO_Group_Five") as GameObject;
        enemy_ClamChowder_Group_Four = Resources.Load("Enemy/Enemy_ClamChowder_Group_Four") as GameObject;
        enemy_ClamChowder_Group_Four_NoItem = Resources.Load("Enemy/Enemy_ClamChowder_Group_Four_NoItem") as GameObject;
        enemy_ClamChowder_Group_Five = Resources.Load("Enemy/Enemy_ClamChowder_Group_Five") as GameObject;
        enemy_ClamChowder_Group_Five_NoItem = Resources.Load("Enemy/Enemy_ClamChowder_Group_Five_NoItem") as GameObject;
        enemy_ClamChowder_Group_FourBehind = Resources.Load("Enemy/Enemy_ClamChowder_Group_FourBehind") as GameObject;
        enemy_ClamChowder_Group_Two = Resources.Load("Enemy/Enemy_ClamChowder_Group_Two") as GameObject;
        enemy_ClamChowder_Group_Two_Top = Resources.Load("Enemy/Enemy_ClamChowder_Group_Two_Top") as GameObject;
        enemy_ClamChowder_Group_Two_Under = Resources.Load("Enemy/Enemy_ClamChowder_Group_Two_Under") as GameObject;
        enemy_ClamChowder_Group_TwoWaveOnlyUp = Resources.Load("Enemy/Enemy_ClamChowder_Group_TwoWaveOnlyUp") as GameObject;
        enemy_ClamChowder_Group_TwoWaveOnlyDown = Resources.Load("Enemy/Enemy_ClamChowder_Group_TwoWaveOnlyDown") as GameObject;
        enemy_ClamChowder_Group_TwoWaveOnlyUp_Item = Resources.Load("Enemy/Enemy_ClamChowder_Group_TwoWaveOnlyUp_Item") as GameObject;
        enemy_ClamChowder_Group_TwoWaveOnlyDown_Item = Resources.Load("Enemy/Enemy_ClamChowder_Group_TwoWaveOnlyDown_Item") as GameObject;
        enemy_ClamChowder_Group_Three = Resources.Load("Enemy/Enemy_ClamChowder_Group_Three") as GameObject;
        enemy_ClamChowder_Group_Three_Item = Resources.Load("Enemy/Enemy_ClamChowder_Group_Three_Item") as GameObject;
        enemy_ClamChowder_Group_SevenWave = Resources.Load("Enemy/Enemy_ClamChowder_Group_Seven") as GameObject;
        enemy_Clamchowder_Group_Straight = Resources.Load("Enemy/Enemy_ClamChowder_Group_Straight") as GameObject;
        enemy_Clamchowder_Group_StraightBehind = Resources.Load("Enemy/Enemy_ClamChowder_Group_StraightBehind") as GameObject;
        enemy_ClamChowder_Group_FourTriangle = Resources.Load("Enemy/Enemy_ClamChowder_Group_FourTriangle") as GameObject;
        enemy_ClamChowder_Group_FourTriangle_NoItem = Resources.Load("Enemy/Enemy_ClamChowder_Group_FourTriangle_NoItem") as GameObject;
        enemy_ClamChowder_Group_TwelveStraight = Resources.Load("Enemy/Enemy_ClamChowder_Group_TwelveStraight") as GameObject;
        enemy_ClamChowder_Group_SevenStraight = Resources.Load("Enemy/Enemy_ClamChowder_Group_SevenStraight") as GameObject;
        enemy_ClamChowder_Group_SixStraight = Resources.Load("Enemy/Enemy_ClamChowder_Group_SixStraight") as GameObject;
        enemy_ClamChowder_Group_UpSevenDiagonal = Resources.Load("Enemy/Enemy_ClamChowder_Group_UpSevenDiagonal") as GameObject;
        enemy_ClamChowder_Group_DownSevenDiagonal = Resources.Load("Enemy/Enemy_ClamChowder_Group_DownSevenDiagonal") as GameObject;
        enemy_ClamChowder_Group_TenStraight = Resources.Load("Enemy/Enemy_ClamChowder_Group_TenStraight") as GameObject;
        //enemy_MiddleBoss_Father = Resources.Load("Enemy/Enemy_MiddleBoss_Father") as GameObject;
        //enemy_ClamChowder_Group_ThreeWaveOnlyUp = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyUp") as GameObject;
        //enemy_ClamChowder_Group_ThreeWaveOnlyDown = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyDown") as GameObject;
        //enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item") as GameObject;
        //enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item") as GameObject;
        enemy_BattleShip = Resources.Load("Enemy/BattleshipType_Enemy") as GameObject;
        enemy_Beelzebub_Group_FourNomal = Resources.Load("Enemy/Enemy_Beelzebub_Group_FourNomal") as GameObject;
        enemy_Beelzebub_Group_FourBack = Resources.Load("Enemy/Enemy_Beelzebub_Group_FourBack") as GameObject;
        enemy_Beelzebub_Group_FourWide = Resources.Load("Enemy/Enemy_Beelzebub_Group_FourWide") as GameObject;
        enemy_Beelzebub_Group_FourWide_Item = Resources.Load("Enemy/Enemy_Beelzebub_Group_FourWide_Item") as GameObject;
        enemy_Beelzebub_Group_EightNormal_Item = Resources.Load("Enemy/Enemy_Beelzebub_Group_EightNormal_Item") as GameObject;
        enemy_Bacula_Sixteen = Resources.Load("Enemy/Enemy_Bacula_Sixteen") as GameObject;
        enemy_Bacula_FourOnly = Resources.Load("Enemy/Enemy_Bacula_FourOnly") as GameObject;

        //enemy_Meteor = Resources.Load("Enemy/Enemy_Meteor") as GameObject;
        //enemy_Meteor_Mini = Resources.Load("Enemy/Enemy_Meteor_Mini") as GameObject;
        //enemy_Meteor_Top = Resources.Load("Enemy/Enemy_Meteor_Top") as GameObject;
        //enemy_Meteor_Under = Resources.Load("Enemy/Enemy_Meteor_Under") as GameObject;
        //enemy_Meteors = Resources.Load("Enemy/Meteors") as GameObject;
        //enemy_MeteorWaveGroup = Resources.Load("Enemy/Enemy_MeteorWaveGroup") as GameObject;
        enemy_SlowFollow = Resources.Load("Enemy/Enemy_SlowFollow") as GameObject;
        Enemy_BoundMeteors = Resources.Load("Enemy/BoundMeteors") as GameObject;
        enemy_Star_Fish_Spowner = Resources.Load("Enemy/Enemy_Star_Fish_Spowner") as GameObject;
        enemy_Beetle_Group = Resources.Load("Enemy/Enemy_Beetle_Group") as GameObject;
        enemy_Beetle_Group_Three = Resources.Load("Enemy/Enemy_Beetle_Group_Three") as GameObject;
        enemy_Beetle_Group_Seven = Resources.Load("Enemy/Enemy_Beetle_Group_Seven") as GameObject;

        enemy_MoaiBossGroup = Resources.Load("Enemy/Enemy_MoaiBossGroup") as GameObject;

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
        isOneBossAlive = true;

        twoBossObj = Obj_Storage.Storage_Data.GetBoss(2);
        twoBoss_Script = twoBossObj.GetComponent<Two_Boss>();
        isTwoBossAlive = true;

        moaiObj = Obj_Storage.Storage_Data.GetBoss(3);
        moai_Script = moaiObj.GetComponent<Enemy_Moai>();
        isMoaiAlive = true;

    }

    void Update()
    {
        if (Game_Master.Management_In_Stage == Game_Master.CONFIGURATION_IN_STAGE.WIRELESS)
        {
            return;
        }
        PreviousCount = frameCnt;

        if (!isNowOneBoss && !isNowTwoBoss && !isNowMoai)
        {
            frameCnt++;
        }

        //次の敵を出す
        if (Input.GetKeyDown(KeyCode.N))
        {
            frameCnt = turning_frame;
        }

		//ラスボス一つ手前
		//if (Input.GetKeyDown(KeyCode.Q) && Input.GetKeyDown(KeyCode.B))
		//{
		//	turning_frame = 5010;
		//	frameCnt = 5010;
		//	groupCnt = 53;
		//}
		//中ボス
		if (Input.GetKeyDown(KeyCode.J))
		{
			turning_frame = 5010;
			frameCnt = 5010;
			groupCnt = 17;
			//nowGroupCnt = 17;
		}
		//中ボス後
		else if (Input.GetKeyDown(KeyCode.K))
		{
			turning_frame = 6750;
			frameCnt = 6750;
			groupCnt = 25;
			//nowGroupCnt = 21;
		}
		//1ボス
		else if (Input.GetKeyDown(KeyCode.M))
		{
			turning_frame = 9660;
			frameCnt = 9600;    //←上の数字から60引いた数にする
			groupCnt = 46;
			//nowGroupCnt = 36;
		}
		//1ボス後
		else if (!Input.GetKey(KeyCode.H) && Input.GetKeyDown(KeyCode.B))
		{
			turning_frame = 9660;
			frameCnt = 9600;    //←上の数字から60引いた数にする
			groupCnt = 47;
			//nowGroupCnt = 36;
		}
		// モアイ
		else if (Input.GetKeyDown(KeyCode.B))
		{
			turning_frame = 9660;
			frameCnt = 9600;    //←上の数字から60引いた数にする
			groupCnt = 55;
			//nowGroupCnt = 36;
		}
		else if (Input.GetKey(KeyCode.U) && Input.GetKeyDown(KeyCode.B))
		{
			turning_frame = 9660;
			frameCnt = 9600;    //←上の数字から60引いた数にする
			groupCnt = 56;
			//nowGroupCnt = 36;
		}
		//ラスボス
		else if (Input.GetKeyDown(KeyCode.L))
		{
			//turning_frame = 17750;
			//frameCnt = 17750;
			//groupCnt = 45;
			isDebug = true;
			turning_frame = 5010;
			frameCnt = 4950;    //←上の数字から60引いた数にする
			groupCnt = 90;

		}
		else if (Input.GetKeyDown(KeyCode.C))
		{
			//turning_frame = 17750;
			//frameCnt = 17750;
			//groupCnt = 45;
			turning_frame = 5010;
			frameCnt = 4950;    //←上の数字から60引いた数にする
			groupCnt = 82;

		}

        if (saveEnemyObj != null)
        {

        }

        //if (isMiddleBossDead)
        //{
        //    if (frameCnt < 7210)
        //    {
        //        frameCnt = 7210;
        //        turning_frame = 7210;

        //        groupCnt = 26;
        //    }
        //    isMiddleBossDead = false;
        //}

        //中ボス撃破
        if (middleBoss_Script != null)
        {
            if (middleBoss_Script.Is_Dead && isMiddleBossSkip)
            {
                if (frameCnt < 7210)
                {
                    frameCnt = 7200;
                    turning_frame = 7210; //←今爆発がでかいのでちょっと間を空けます
                    groupCnt = 25;
                    isMiddleBossSkip = false;
                }
            }
        }

        //if (isOneBossAlive)
        //{
        //    if (frameCnt < 39900)
        //    {
        //        frameCnt = 39900;
        //        //turning_frame = 40930;
        //    }
        //    isOneBossAlive = false;
        //}

        //第一ボス出現時に無線をONにする🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲
        if (groupCnt == 46 && frameCnt == turning_frame - 60f)
        {
            Wireless_sinario.Is_using_wireless = true;
        }
        //第二ボス出現時に無線をONにする🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲
        if (groupCnt == 91 && frameCnt == turning_frame - 60f)
        {
            Wireless_sinario.Is_using_wireless = true;
        }

        //第一ボスを撃破したら間隔を詰める🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲
        if (oneBoss_Script != null)
        {
            if (oneBoss_Script.Is_Dead)
            {
                if (isOneBossAlive)
                {
                    //if (frameCnt < 39660)
                    //{
                    //    if (backActive_Script)
                    //    {
                    //        backActive_Script.TransparencyChangeTrigger();
                    //        Wireless_sinario.Is_using_wireless = true;
                    //    }
                    //    frameCnt = 39630;

                    //    //turning_frame = 40930;
                    //}
                    if (backActive_Script)
                    {
                        backActive_Script.TransparencyChangeTrigger();
                        Wireless_sinario.Is_using_wireless = true;
                    }
                    isNowOneBoss = false;
                    isOneBossAlive = false;
                }

                //if(frame > 180) SceneManager.LoadScene("GameClear");
                //if (frame > 120) Scene_Manager.Manager.Screen_Transition_To_Clear();
            }
        }

        if (moai_Script != null)
        {
            if (moai_Script.Is_Dead)
            {
                if (isMoaiAlive)
                {
                    isNowMoai = false;
                    isMoaiAlive = false;
                }
            }
        }

        //第二ボスを撃破🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲
        if (twoBoss_Script != null)
        {
            if (twoBoss_Script.Is_Dead)
            {
                if (isTwoBossAlive)
                {
                    backActive_Script.TransparencyChangeTrigger();
                    isNowTwoBoss = false;
                    Wireless_sinario.Is_using_wireless = true;
                    isTwoBossAlive = false;

                }
            }
        }

        if (isBaculaDestroy)
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

    public enum EnemyType
    {
        UFO_GROUP_NONESHOT,
        UFO_GROUP_FIVE,
        CLAMCHOWDER_GROUP_FOUR,
        CLAMCHOWDER_GROUP_FOURTRIANGLE,
        CLAMCHOWDER_GROUP_THREE,
        CLAMCHOWDER_GROUP_TOPANDUNEDR,
        CLAMCHOWDER_GROUP_FIVE,
        CLAMCHOWDER_GROUP_TWOWAVEONLYDOWN,
        CLAMCHOWDER_GROUP_TWOWAVEONLYUP,
        BEETLE_GROUP_THREE,
    }

    public enum CreatePos
    {
        L4,
        L3,
        L2,
        L1,
        L0,
        Lm1,
        Lm2,
        Lm3,
        Lm4,
        FOURGROUPL,
        R4,
        R3,
        R2,
        R1,
        R0,
        Rm1,
        Rm2,
        Rm3,
        Rm4,
    }

    private void CreateEnemy(EnemyType e, CreatePos p, bool isItem = false)
    {
        Vector3 pos = Vector3.zero;
        switch (p)
        {
            case CreatePos.FOURGROUPL:
                pos = createPos_FourGroupL.transform.position;
                break;

            case CreatePos.R4:
                pos = createPosR4.transform.position;
                break;

            case CreatePos.R3:
                pos = createPosR3.transform.position;
                break;

            case CreatePos.R2:
                pos = createPosR2.transform.position;
                break;

            case CreatePos.R1:
                pos = createPosR1.transform.position;
                break;

            case CreatePos.R0:
                pos = createPosR0.transform.position;
                break;

            case CreatePos.Rm1:
                pos = createPosRm1.transform.position;
                break;

            case CreatePos.Rm2:
                pos = createPosRm2.transform.position;
                break;

            case CreatePos.Rm3:
                pos = createPosRm3.transform.position;
                break;

            case CreatePos.Rm4:
                pos = createPosRm4.transform.position;
                break;

            case CreatePos.L4:
                pos = createPosL4.transform.position;
                break;

            case CreatePos.L3:
                pos = createPosL3.transform.position;
                break;

            case CreatePos.L2:
                pos = createPosL2.transform.position;
                break;

            case CreatePos.L1:
                pos = createPosL1.transform.position;
                break;

            case CreatePos.L0:
                pos = createPosL0.transform.position;
                break;

            case CreatePos.Lm1:
                pos = createPosLm1.transform.position;
                break;

            case CreatePos.Lm2:
                pos = createPosLm2.transform.position;
                break;

            case CreatePos.Lm3:
                pos = createPosLm3.transform.position;
                break;

            case CreatePos.Lm4:
                pos = createPosLm4.transform.position;
                break;

            default:
                pos = Vector3.zero;
                break;
        }

        switch (e)
        {
            case EnemyType.UFO_GROUP_NONESHOT:
				//Object_Pooling pEnemy_UFO_Group = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group_NoneShot") as GameObject, 1, "enemy_UFO_Group");
				//GameObject enemy_UFO_Group = pEnemy_UFO_Group.Active_Obj();
				GameObject enemy_UFO_Group = Obj_Storage.Storage_Data.enemy_UFO_Group_NoneShot.Active_Obj();
                enemy_UFO_Group.transform.position = pos + new Vector3(8.5f, 0, 0);
                enemy_UFO_Group.transform.rotation = transform.rotation;
                //Object_Pooling pEnemy_UFO_Group = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group_NoneShot") as GameObject, 1, "enemy_UFO_Group");
                //GameObject enemy_UFO_Group = Obj_Storage.Storage_Data.enemy_UFO_Group_NoneShot.Active_Obj();
                //enemy_UFO_Group.transform.position = createPosR3.transform.position + new Vector3(8.5f, 0, 0);
                //enemy_UFO_Group.transform.rotation = transform.rotation;
                break;

            case EnemyType.UFO_GROUP_FIVE:
				//Object_Pooling pEnemy_UFO_Group_Five = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group_Five") as GameObject, 1, "enemy_UFO_Group");
				//GameObject enemy_UFO_Group_Five = pEnemy_UFO_Group_Five.Active_Obj();

				GameObject save_enemy_UFO_Group_Five = Instantiate(enemy_UFO_Group_Five, transform.position, transform.rotation);
				save_enemy_UFO_Group_Five.transform.position = pos + new Vector3(8.5f, 0, 0);
				save_enemy_UFO_Group_Five.transform.rotation = transform.rotation;
                //Object_Pooling pEnemy_UFO_Group = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group_NoneShot") as GameObject, 1, "enemy_UFO_Group");
                //GameObject enemy_UFO_Group = Obj_Storage.Storage_Data.enemy_UFO_Group_NoneShot.Active_Obj();
                //enemy_UFO_Group.transform.position = createPosR3.transform.position + new Vector3(8.5f, 0, 0);
                //enemy_UFO_Group.transform.rotation = transform.rotation;
                break;

            case EnemyType.CLAMCHOWDER_GROUP_FOUR:
                if (!isItem)
                {
                    //GameObject saveObjA = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Four.Active_Obj();
                    //saveObjA.transform.position = pos;
                    Instantiate(enemy_ClamChowder_Group_Four, pos, transform.rotation);
                }
                else
                {
                    //GameObject saveObj = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Four_NoItem.Active_Obj();
                    GameObject saveObjB = Instantiate(enemy_ClamChowder_Group_Four_NoItem, createPos_FourGroupL.transform.position, transform.rotation);
                    saveObjB.transform.position = createPos_FourGroupL.transform.position;
                }
                break;

            case EnemyType.CLAMCHOWDER_GROUP_FOURTRIANGLE:
                if (!isItem)
                {
                    Instantiate(enemy_ClamChowder_Group_FourTriangle_NoItem, pos, transform.rotation);
                }
                else
                {
                    Instantiate(enemy_ClamChowder_Group_FourTriangle, pos, transform.rotation);
                }
                break;

            case EnemyType.CLAMCHOWDER_GROUP_THREE:
                if (!isItem)
                {
                    //Instantiate(enemy_ClamChowder_Group_FourTriangle_NoItem, pos, transform.rotation);
                }
                else
                {
                    GameObject saveObjC = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Three_Item.Active_Obj();
                    saveObjC.transform.position = createPos_FourGroupL.transform.position;
                    //Instantiate(enemy_ClamChowder_Group_Three_Item, createPos_FourGroupL.transform.position, transform.rotation);
                }
                break;

            case EnemyType.CLAMCHOWDER_GROUP_TOPANDUNEDR:
                GameObject saveObj = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Two_Top.Active_Obj();
                GameObject saveObj2 = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Two_Under.Active_Obj();
                saveObj.transform.position = createPos_FourGroupL.transform.position;
                saveObj2.transform.position = createPos_FourGroupL.transform.position;
                //Instantiate(enemy_ClamChowder_Group_Two_Top, createPos_FourGroupL.transform.position, transform.rotation);
                //Instantiate(enemy_ClamChowder_Group_Two_Under, createPos_FourGroupL.transform.position, transform.rotation);
                break;

            case EnemyType.CLAMCHOWDER_GROUP_TWOWAVEONLYUP:
                Instantiate(enemy_ClamChowder_Group_TwoWaveOnlyUp, pos, transform.rotation);
                break;

            case EnemyType.CLAMCHOWDER_GROUP_TWOWAVEONLYDOWN:
                Instantiate(enemy_ClamChowder_Group_TwoWaveOnlyDown, pos, transform.rotation);
                break;

            case EnemyType.BEETLE_GROUP_THREE:
                GameObject beetleGroup_Three = Instantiate(enemy_Beetle_Group_Three, createPosRm3.transform.position, transform.rotation);
                beetleGroup_Three.transform.position = new Vector3(15, -8, 0);
                break;

            default:
                break;
        }
    }
    //敵を出す関数
    private void CreateEnemyGroup_01()
    {
        if (false)
        {
            CreateEnemyGroup_01_TypeA();
        }
        // デバッグ
        else
        {
            CreateEnemyGroup_01_TypeB();
        }
    }

    private void CreateEnemyGroup_01_TypeA()
    {
        // 円盤上10
        if (Is_A_Specified_Frame(turning_frame) && groupCnt == 1)
        {
            //Object_Pooling pEnemy_UFO_Group = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group_NoneShot") as GameObject, 1, "enemy_UFO_Group");
            //GameObject enemy_UFO_Group = pEnemy_UFO_Group.Active_Obj();
            GameObject enemy_UFO_Group = Obj_Storage.Storage_Data.enemy_UFO_Group_NoneShot.Active_Obj();
            enemy_UFO_Group.transform.position = createPosR3.transform.position + new Vector3(8.5f, 0, 0);
            enemy_UFO_Group.transform.rotation = transform.rotation;

            nextEnemy = "円盤下10";
            Next_Condition(240);
            nowGroupCnt++;
        }
        // 円盤下10
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 2)
        {
            //Object_Pooling pEnemy_UFO_Group = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group_NoneShot") as GameObject, 1, "enemy_UFO_Group");
            //GameObject enemy_UFO_Group = pEnemy_UFO_Group.Active_Obj();
            GameObject enemy_UFO_Group = Obj_Storage.Storage_Data.enemy_UFO_Group_NoneShot.Active_Obj();
            enemy_UFO_Group.transform.position = createPosRm3.transform.position + new Vector3(8.5f, 0, 0);
            enemy_UFO_Group.transform.rotation = transform.rotation;

            nextEnemy = "闘牛斜め配置4中央アイテム2";
            Next_Condition(180);
            nowGroupCnt++;
        }
        // 闘牛斜め配置4中央アイテム2
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 3)
        {
            GameObject saveObj = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Four.Active_Obj();
            //Instantiate(enemy_ClamChowder_Group_Four, createPos_FourGroupL.transform.position, transform.rotation);

            nextEnemy = "闘牛斜め配置4";
            Next_Condition(120);
            nowGroupCnt++;
        }
        // 闘牛斜め配置4
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 4)
        {
            GameObject saveObj = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Four_NoItem.Active_Obj();

            //Instantiate(enemy_ClamChowder_Group_Four_NoItem, createPos_FourGroupL.transform.position, transform.rotation);

            nextEnemy = "円盤上10下10";
            Next_Condition(360);
            nowGroupCnt++;
        }
        // 円盤上10下10
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 5)
        {
            //Object_Pooling pEnemy_UFO_Group = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group_NoneShot") as GameObject, 1, "enemy_UFO_Group");
            //GameObject enemy_UFO_Group = pEnemy_UFO_Group.Active_Obj();
            GameObject enemy_UFO_Group = Obj_Storage.Storage_Data.enemy_UFO_Group_NoneShot.Active_Obj();
            enemy_UFO_Group.transform.position = createPosR3.transform.position + new Vector3(8.5f, 0, 0);
            enemy_UFO_Group.transform.rotation = transform.rotation;

            //Object_Pooling pEnemy_UFO_Group2 = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group_NoneShot") as GameObject, 1, "enemy_UFO_Group");
            //GameObject enemy_UFO_Group2 = pEnemy_UFO_Group2.Active_Obj();
            GameObject enemy_UFO_Group2 = Obj_Storage.Storage_Data.enemy_UFO_Group_NoneShot.Active_Obj();
            enemy_UFO_Group2.transform.position = createPosRm3.transform.position + new Vector3(8.5f, 0, 0);
            enemy_UFO_Group2.transform.rotation = transform.rotation;

            nextEnemy = "円盤上10下10狭";
            Next_Condition(210);
            nowGroupCnt++;
        }
        // 円盤上10下10狭
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 6)
        {
            //Object_Pooling pEnemy_UFO_Group = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group_NoneShot") as GameObject, 1, "enemy_UFO_Group");
            //GameObject enemy_UFO_Group = pEnemy_UFO_Group.Active_Obj();
            GameObject enemy_UFO_Group = Obj_Storage.Storage_Data.enemy_UFO_Group_NoneShot.Active_Obj();
            enemy_UFO_Group.transform.position = createPosR1.transform.position + new Vector3(8.5f, 0, 0);
            enemy_UFO_Group.transform.rotation = transform.rotation;

            //Object_Pooling pEnemy_UFO_Group2 = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group_NoneShot") as GameObject, 1, "enemy_UFO_Group");
            //GameObject enemy_UFO_Group2 = pEnemy_UFO_Group2.Active_Obj();
            GameObject enemy_UFO_Group2 = Obj_Storage.Storage_Data.enemy_UFO_Group_NoneShot.Active_Obj();
            enemy_UFO_Group2.transform.position = createPosRm1.transform.position + new Vector3(8.5f, 0, 0);
            enemy_UFO_Group2.transform.rotation = transform.rotation;

            nextEnemy = "闘牛縦3";
            Next_Condition(270);
            nowGroupCnt++;
        }
        // 闘牛縦3
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 7)
        {
            Instantiate(enemy_ClamChowder_Group_Three, createPos_FourGroupL.transform.position, transform.rotation);

            nextEnemy = "闘牛上2下2";
            Next_Condition(45);
            nowGroupCnt++;
        }
        // 闘牛上2下2
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 8)
        {
            Instantiate(enemy_ClamChowder_Group_Two_Top, createPos_FourGroupL.transform.position, transform.rotation);
            Instantiate(enemy_ClamChowder_Group_Two_Under, createPos_FourGroupL.transform.position, transform.rotation);

            nextEnemy = "闘牛縦7中央アイテム";
            Next_Condition(45);
            nowGroupCnt++;
        }
        // 闘牛縦7中央アイテム
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 9)
        {
            Instantiate(enemy_ClamChowder_Group_Five, createPos_FourGroupL.transform.position, transform.rotation);

            nextEnemy = "闘牛縦7中央アイテム";
            Next_Condition(45);
            nowGroupCnt++;
        }
        // 闘牛縦7中央アイテム
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 10)
        {
            Instantiate(enemy_ClamChowder_Group_Five, createPos_FourGroupL.transform.position, transform.rotation);

            nextEnemy = "闘牛斜め配置4中央2アイテム";
            Next_Condition(360);
            nowGroupCnt++;
        }
        // 闘牛斜め配置4中央2アイテム
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 11)
        {
            Instantiate(enemy_ClamChowder_Group_Four, createPos_FourGroupL.transform.position, transform.rotation);

            nextEnemy = "闘牛斜め配置4";
            Next_Condition(135);
            nowGroupCnt++;
        }
        // 闘牛斜め配置4
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 12)
        {
            Instantiate(enemy_ClamChowder_Group_Four_NoItem, createPos_FourGroupL.transform.position, transform.rotation);

            nextEnemy = "円盤上10下10狭";
            Next_Condition(360);
            nowGroupCnt++;
        }
        // 円盤上10下10狭
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 13)
        {
            Object_Pooling pEnemy_UFO_Group = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group_NoneShot") as GameObject, 1, "enemy_UFO_Group");
            GameObject enemy_UFO_Group = pEnemy_UFO_Group.Active_Obj();
            enemy_UFO_Group.transform.position = createPosR1.transform.position + new Vector3(8.5f, 0, 0);
            enemy_UFO_Group.transform.rotation = transform.rotation;

            Object_Pooling pEnemy_UFO_Group2 = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group_NoneShot") as GameObject, 1, "enemy_UFO_Group");
            GameObject enemy_UFO_Group2 = pEnemy_UFO_Group2.Active_Obj();
            enemy_UFO_Group2.transform.position = createPosRm1.transform.position + new Vector3(8.5f, 0, 0);
            enemy_UFO_Group2.transform.rotation = transform.rotation;

            nextEnemy = "闘牛斜め配置4中央2アイテム";
            Next_Condition(270);
            nowGroupCnt++;
        }
        // 闘牛斜め配置4中央2アイテム
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 14)
        {
            Instantiate(enemy_ClamChowder_Group_Four, createPos_FourGroupL.transform.position, transform.rotation);

            nextEnemy = "闘牛斜め配置4";
            Next_Condition(135);
            nowGroupCnt++;
        }
        // 闘牛斜め配置4
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 15)
        {
            Instantiate(enemy_ClamChowder_Group_Four_NoItem, createPos_FourGroupL.transform.position, transform.rotation);

            nextEnemy = "闘牛縦3中央アイテム";
            Next_Condition(270);
            nowGroupCnt++;
        }
        // 闘牛縦3中央アイテム
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 16)
        {
            Instantiate(enemy_ClamChowder_Group_Three_Item, createPos_FourGroupL.transform.position, transform.rotation);

            nextEnemy = "闘牛上2下2";
            Next_Condition(45);
            nowGroupCnt++;
        }
        // 闘牛上2下2
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 17)
        {
            Instantiate(enemy_ClamChowder_Group_Two_Top, createPos_FourGroupL.transform.position, transform.rotation);
            Instantiate(enemy_ClamChowder_Group_Two_Under, createPos_FourGroupL.transform.position, transform.rotation);

            nextEnemy = "闘牛縦3中央アイテム";
            Next_Condition(45);
            nowGroupCnt++;
        }
        // 闘牛縦3中央アイテム
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 18)
        {
            Instantiate(enemy_ClamChowder_Group_Three_Item, createPos_FourGroupL.transform.position, transform.rotation);

            nextEnemy = "闘牛上2下2";
            Next_Condition(45);
            nowGroupCnt++;
        }
        // 闘牛上2下2
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 19)
        {
            Instantiate(enemy_ClamChowder_Group_Two_Top, createPos_FourGroupL.transform.position, transform.rotation);
            Instantiate(enemy_ClamChowder_Group_Two_Under, createPos_FourGroupL.transform.position, transform.rotation);

            nextEnemy = "闘牛縦7";
            Next_Condition(45);
            nowGroupCnt++;
        }
        // 闘牛縦7
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 20)
        {
            Instantiate(enemy_ClamChowder_Group_Five_NoItem, createPos_FourGroupL.transform.position, transform.rotation);

            nextEnemy = "ビッグコア";
            Next_Condition(510);
            nowGroupCnt++;
        }
        // ビッグコア??????????????????
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 21)
        {
            GameObject Boss_Middle = Obj_Storage.Storage_Data.Boss_Middle.Active_Obj();
            saveEnemyObj = Boss_Middle;
            Boss_Middle.transform.position = createMiddleBossPos.transform.position;
            Boss_Middle.transform.rotation = transform.rotation;

            nextEnemy = "闘牛上2下2";
            Next_Condition(180);
            nowGroupCnt++;
        }
        // 闘牛上2下2
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 22)
        {
            Instantiate(enemy_ClamChowder_Group_TwoWaveOnlyDown, createPosR4.transform.position, transform.rotation);
            Instantiate(enemy_ClamChowder_Group_TwoWaveOnlyUp, createPosRm4.transform.position, transform.rotation);

            nextEnemy = "闘牛上2下2";
            Next_Condition(240);
            nowGroupCnt++;
        }
        // 闘牛上2下2
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 23)
        {
            Instantiate(enemy_ClamChowder_Group_TwoWaveOnlyDown, createPosR4.transform.position, transform.rotation);
            Instantiate(enemy_ClamChowder_Group_TwoWaveOnlyUp, createPosRm4.transform.position, transform.rotation);

            nextEnemy = "闘牛上2下2";
            Next_Condition(240);
            nowGroupCnt++;
        }
        // 闘牛上2下2
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 24)
        {
            Instantiate(enemy_ClamChowder_Group_TwoWaveOnlyDown, createPosR4.transform.position, transform.rotation);
            Instantiate(enemy_ClamChowder_Group_TwoWaveOnlyUp, createPosRm4.transform.position, transform.rotation);

            nextEnemy = "闘牛上2下2";
            Next_Condition(240);
            nowGroupCnt++;
        }
        // 闘牛上2下2
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 25)
        {
            Instantiate(enemy_ClamChowder_Group_TwoWaveOnlyDown, createPosR4.transform.position, transform.rotation);
            Instantiate(enemy_ClamChowder_Group_TwoWaveOnlyUp, createPosRm4.transform.position, transform.rotation);

            nextEnemy = "闘牛上2下2";
            Next_Condition(240);
            nowGroupCnt++;
        }
        // 闘牛上2下2
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 26)
        {
            Instantiate(enemy_ClamChowder_Group_TwoWaveOnlyDown, createPosR4.transform.position, transform.rotation);
            Instantiate(enemy_ClamChowder_Group_TwoWaveOnlyUp, createPosRm4.transform.position, transform.rotation);

            nextEnemy = "ハエ上2下2広";
            Next_Condition(750);
            nowGroupCnt++;
        }
        // ハエ上2下2広
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 27)
        {
            Instantiate(enemy_Beelzebub_Group_FourWide, createPosR0.transform.position, transform.rotation);

            nextEnemy = "ハエ上2下2広右2アイテム";
            Next_Condition(240);
            nowGroupCnt++;
        }
        // ハエ上2下2広右2アイテム
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 28)
        {
            Instantiate(enemy_Beelzebub_Group_FourWide_Item, createPosR0.transform.position, transform.rotation);

            nextEnemy = "ビートル5";
            Next_Condition(270);
            nowGroupCnt++;
        }
        // ビートル3
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 29)
        {
            //GameObject beetleGroup_Three = Instantiate(enemy_Beetle_Group_Three, createPosRm3.transform.position, transform.rotation);
            //beetleGroup_Three.transform.position = new Vector3(15, -8, 0);

            nextEnemy = "戦艦";
            Next_Condition(360);
            nowGroupCnt++;
        }
        // 戦艦
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 30)
        {
            GameObject Battle_Ship1 = Instantiate(enemy_BattleShip, createPosR0.transform.position, enemy_BattleShip.transform.rotation);
            Battle_Ship1.transform.position = createPosR0.transform.position;
            BattleshipType_Enemy b1 = Battle_Ship1.GetComponent<BattleshipType_Enemy>();
            b1.is_sandwich = false;
            b1.Is_up = false;

            nextEnemy = "戦艦上下";
            Next_Condition(240);
            nowGroupCnt++;
        }
        // 戦艦上下
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 31)
        {
            GameObject Battle_Ship1 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
            BattleshipType_Enemy b1 = Battle_Ship1.GetComponent<BattleshipType_Enemy>();
            b1.Is_up = false;

            GameObject Battle_Ship2 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
            BattleshipType_Enemy b2 = Battle_Ship2.GetComponent<BattleshipType_Enemy>();
            b2.Is_up = true;

            nextEnemy = "闘牛直進12";
            Next_Condition(240);
            nowGroupCnt++;
        }
        // 闘牛直進12
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 32)
        {
            GameObject saveObj = Instantiate(enemy_Clamchowder_Group_Straight, createPosR0.transform.position, transform.rotation);
            group_Script = saveObj.GetComponent<EnemyGroupManage>();
            group_Script.isItemDrop = true;

            nextEnemy = "闘牛直進12";
            Next_Condition(270);
            nowGroupCnt++;
        }
        // 闘牛直進12
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 33)
        {
            GameObject saveObj = Instantiate(enemy_Clamchowder_Group_Straight, createPosR0.transform.position, transform.rotation);
            group_Script = saveObj.GetComponent<EnemyGroupManage>();
            group_Script.isItemDrop = true;

            nextEnemy = "戦艦";
            Next_Condition(270);
            nowGroupCnt++;
        }
        // 戦艦
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 34)
        {
            GameObject Battle_Ship1 = Instantiate(enemy_BattleShip, createPosR0.transform.position, enemy_BattleShip.transform.rotation);
            Battle_Ship1.transform.position = createPosR0.transform.position;
            BattleshipType_Enemy b1 = Battle_Ship1.GetComponent<BattleshipType_Enemy>();
            b1.is_sandwich = false;
            b1.Is_up = false;

            nextEnemy = "闘牛12直進上下";
            Next_Condition(240);
            nowGroupCnt++;
        }
        // 闘牛12直進上下
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 35)
        {
            Instantiate(enemy_Clamchowder_Group_Straight, createPosR3.transform.position, transform.rotation);
            Instantiate(enemy_Clamchowder_Group_Straight, createPosRm3.transform.position, transform.rotation);

            nextEnemy = "ハエ上2下2広右2アイテム";
            Next_Condition(330);
            nowGroupCnt++;
        }
        // ハエ上2下2広右2アイテム
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 36)
        {
            Instantiate(enemy_Beelzebub_Group_FourWide_Item, createPosR0.transform.position, transform.rotation);

            nextEnemy = "ハエ上2下2広右2アイテム";
            Next_Condition(240);
            nowGroupCnt++;
        }
        // ハエ上2下2広右2アイテム
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 37)
        {
            Instantiate(enemy_Beelzebub_Group_FourWide_Item, createPosR0.transform.position, transform.rotation);

            nextEnemy = "ビートル5";
            Next_Condition(270);
            nowGroupCnt++;
        }
        // ビートル5
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 38)
        {
            GameObject beetleGroup = Instantiate(enemy_Beetle_Group, createPosRm3.transform.position, transform.rotation);
            beetleGroup.transform.position = new Vector3(15, -8, 0);

            nextEnemy = "闘牛縦3";
            Next_Condition(360);
            nowGroupCnt++;
        }
        // 闘牛縦3
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 39)
        {
            Instantiate(enemy_ClamChowder_Group_Three, createPos_FourGroupL.transform.position, transform.rotation);

            nextEnemy = "闘牛上2下2";
            Next_Condition(45);
            nowGroupCnt++;
        }
        // 闘牛上2下2
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 40)
        {
            Instantiate(enemy_ClamChowder_Group_Two_Top, createPos_FourGroupL.transform.position, transform.rotation);
            Instantiate(enemy_ClamChowder_Group_Two_Under, createPos_FourGroupL.transform.position, transform.rotation);

            nextEnemy = "闘牛縦7中央アイテム";
            Next_Condition(45);
            nowGroupCnt++;
        }
        // 闘牛縦7中央アイテム
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 41)
        {
            Instantiate(enemy_ClamChowder_Group_Five, createPos_FourGroupL.transform.position, transform.rotation);

            nextEnemy = "闘牛縦7中央アイテム";
            Next_Condition(45);
            nowGroupCnt++;
        }
        // 闘牛縦7中央アイテム
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 42)
        {
            Instantiate(enemy_ClamChowder_Group_Five, createPos_FourGroupL.transform.position, transform.rotation);

            nextEnemy = "闘牛直進12上下";
            Next_Condition(270);
            nowGroupCnt++;
        }
        // 闘牛直進12上下
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 43)
        {
            Instantiate(enemy_Clamchowder_Group_Straight, createPosR3.transform.position, transform.rotation);
            Instantiate(enemy_Clamchowder_Group_Straight, createPosRm3.transform.position, transform.rotation);

            nextEnemy = "闘牛直進12上下";
            Next_Condition(90);
            nowGroupCnt++;
        }
        // 闘牛直進12上下
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 44)
        {
            Instantiate(enemy_Clamchowder_Group_Straight, createPosR3.transform.position, transform.rotation);
            Instantiate(enemy_Clamchowder_Group_Straight, createPosRm3.transform.position, transform.rotation);

            nextEnemy = "闘牛直進12";
            Next_Condition(90);
            nowGroupCnt++;
        }
        // 闘牛直進12
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 45)
        {
            GameObject saveObj = Instantiate(enemy_Clamchowder_Group_Straight, createPosR0.transform.position, transform.rotation);
            group_Script = saveObj.GetComponent<EnemyGroupManage>();
            group_Script.isItemDrop = true;

            nextEnemy = "ビッグコアマーク2";
            Next_Condition(270);
            nowGroupCnt++;
        }
        // ビッグコアマーク2????????????????????????????????????????????????????????????????????????????????????????????????????????
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 46)
        {
            GameObject Boss_01 = Obj_Storage.Storage_Data.Boss_1.Active_Obj();
            Boss_01.transform.position = new Vector3(10.0f, 0.0f, 0.0f);

            GameObject mistSaveObj = Instantiate(mistEffectObj, transform.position, transform.rotation);
            mistEffectObj.transform.position = new Vector3(0, 0, 3);
            mistParticle = mistSaveObj.GetComponent<ParticleSystem>();
            backActive_Script = mistSaveObj.GetComponent<BackgroundActivation>();
            mistParticle.Play();
            backActive_Script.TransparencyChangeTrigger();
            isNowOneBoss = true;

            nextEnemy = "ヒトデ12";
            Next_Condition(120);
        }
        // ヒトデ12
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 47)
        {
            Instantiate(enemy_Star_Fish_Spowner, transform.position, transform.rotation);

            nextEnemy = "バキュラ16";
            Next_Condition(240 + 240);
        }
        // バキュラ16
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 48)
        {
            Instantiate(enemy_Bacula_Sixteen, createBaculaGroupPos.transform.position, transform.rotation);

            nextEnemy = "バキュラ4";
            Next_Condition(1380);
        }
        // バキュラ4
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 49)
        {
            Instantiate(enemy_Bacula_FourOnly, createBaculaGroupPos.transform.position, transform.rotation);

            nextEnemy = "隕石20";
            Next_Condition(840);
        }
        // 隕石20
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 50)
        {
            Instantiate(Enemy_BoundMeteors, createMeteorPosR0.transform.position, transform.rotation);

            nextEnemy = "隕石20";
            Next_Condition(240);
        }
        // 隕石20
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 51)
        {
            Instantiate(Enemy_BoundMeteors, createMeteorPosR0.transform.position, transform.rotation);

            nextEnemy = "モアイ";
            Next_Condition(360);
        }
        // モアイ????????????????????????????????????????????????????????????????????????????????????????????????????
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 52)
        {
            //GameObject moai = Obj_Storage.Storage_Data.Moai.Active_Obj();
            //moai.transform.position = new Vector3(15.44f, -17.0f, 0.0f);
            //Wireless_sinario.Is_using_wireless = true;
            //isNowMoai = true;

            nextEnemy = "ヒトデ12";

            //Next_Condition(620);
            Next_Condition(620);
        }
        // ヒトデ12
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 53)
        {
            Instantiate(enemy_Star_Fish_Spowner, transform.position, transform.rotation);

            nextEnemy = "円盤上10狭下10射撃";
            Next_Condition(240 + 240);
        }
        // 円盤上10狭下10射撃
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 54)
        {
            Object_Pooling pEnemy_UFO_Group = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group") as GameObject, 1, "enemy_UFO_Group");
            GameObject enemy_UFO_Group = pEnemy_UFO_Group.Active_Obj();
            enemy_UFO_Group.transform.position = createPosR1.transform.position + new Vector3(8.5f, 0, 0);
            enemy_UFO_Group.transform.rotation = transform.rotation;

            Object_Pooling pEnemy_UFO_Group2 = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group") as GameObject, 1, "enemy_UFO_Group");
            GameObject enemy_UFO_Group2 = pEnemy_UFO_Group2.Active_Obj();
            enemy_UFO_Group2.transform.position = createPosRm3.transform.position + new Vector3(8.5f, 0, 0);
            enemy_UFO_Group2.transform.rotation = transform.rotation;

            nextEnemy = "円盤上10下10狭射撃";
            Next_Condition(180);
            nowGroupCnt++;
        }
        // 円盤上10下10狭射撃
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 55)
        {
            Object_Pooling pEnemy_UFO_Group = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group") as GameObject, 1, "enemy_UFO_Group");
            GameObject enemy_UFO_Group = pEnemy_UFO_Group.Active_Obj();
            enemy_UFO_Group.transform.position = createPosR3.transform.position + new Vector3(8.5f, 0, 0);
            enemy_UFO_Group.transform.rotation = transform.rotation;

            Object_Pooling pEnemy_UFO_Group2 = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group") as GameObject, 1, "enemy_UFO_Group");
            GameObject enemy_UFO_Group2 = pEnemy_UFO_Group2.Active_Obj();
            enemy_UFO_Group2.transform.position = createPosRm1.transform.position + new Vector3(8.5f, 0, 0);
            enemy_UFO_Group2.transform.rotation = transform.rotation;

            nextEnemy = "円盤上10狭下10射撃";
            Next_Condition(150);
            nowGroupCnt++;
        }
        // 円盤上10狭下10射撃
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 56)
        {
            Object_Pooling pEnemy_UFO_Group = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group") as GameObject, 1, "enemy_UFO_Group");
            GameObject enemy_UFO_Group = pEnemy_UFO_Group.Active_Obj();
            enemy_UFO_Group.transform.position = createPosR1.transform.position + new Vector3(8.5f, 0, 0);
            enemy_UFO_Group.transform.rotation = transform.rotation;

            Object_Pooling pEnemy_UFO_Group2 = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group") as GameObject, 1, "enemy_UFO_Group");
            GameObject enemy_UFO_Group2 = pEnemy_UFO_Group2.Active_Obj();
            enemy_UFO_Group2.transform.position = createPosRm3.transform.position + new Vector3(8.5f, 0, 0);
            enemy_UFO_Group2.transform.rotation = transform.rotation;

            nextEnemy = "闘牛斜め配置4";
            Next_Condition(180);
            nowGroupCnt++;
        }
        // ハエ上2下2広右2アイテム
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 57)
        {
            Instantiate(enemy_Beelzebub_Group_FourWide_Item, createPosR0.transform.position, transform.rotation);

            nextEnemy = "円盤上10下10射撃";
            Next_Condition(90);
            nowGroupCnt++;
        }
        // 円盤上10下10射撃
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 58)
        {
            Object_Pooling pEnemy_UFO_Group = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group") as GameObject, 1, "enemy_UFO_Group");
            GameObject enemy_UFO_Group = pEnemy_UFO_Group.Active_Obj();
            enemy_UFO_Group.transform.position = createPosR4.transform.position + new Vector3(8.5f, 0, 0);
            enemy_UFO_Group.transform.rotation = transform.rotation;

            Object_Pooling pEnemy_UFO_Group2 = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group") as GameObject, 1, "enemy_UFO_Group");
            GameObject enemy_UFO_Group2 = pEnemy_UFO_Group2.Active_Obj();
            enemy_UFO_Group2.transform.position = createPosRm4.transform.position + new Vector3(8.5f, 0, 0);
            enemy_UFO_Group2.transform.rotation = transform.rotation;

            nextEnemy = "戦艦";
            Next_Condition(90);
            nowGroupCnt++;
        }
        // 戦艦
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 59)
        {
            GameObject Battle_Ship1 = Instantiate(enemy_BattleShip, createPosR0.transform.position, enemy_BattleShip.transform.rotation);
            Battle_Ship1.transform.position = createPosR0.transform.position;
            BattleshipType_Enemy b1 = Battle_Ship1.GetComponent<BattleshipType_Enemy>();
            b1.is_sandwich = false;
            b1.Is_up = false;

            nextEnemy = "円盤上10下10射撃";
            Next_Condition(240);
            nowGroupCnt++;
        }
        // 円盤上10下10射撃
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 60)
        {
            Object_Pooling pEnemy_UFO_Group = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group") as GameObject, 1, "enemy_UFO_Group");
            GameObject enemy_UFO_Group = pEnemy_UFO_Group.Active_Obj();
            enemy_UFO_Group.transform.position = createPosR4.transform.position + new Vector3(8.5f, 0, 0);
            enemy_UFO_Group.transform.rotation = transform.rotation;

            Object_Pooling pEnemy_UFO_Group2 = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group") as GameObject, 1, "enemy_UFO_Group");
            GameObject enemy_UFO_Group2 = pEnemy_UFO_Group2.Active_Obj();
            enemy_UFO_Group2.transform.position = createPosRm4.transform.position + new Vector3(8.5f, 0, 0);
            enemy_UFO_Group2.transform.rotation = transform.rotation;

            nextEnemy = "戦艦上下";
            Next_Condition(90);
            nowGroupCnt++;
        }
        // 戦艦上下
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 61)
        {
            GameObject Battle_Ship1 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
            BattleshipType_Enemy b1 = Battle_Ship1.GetComponent<BattleshipType_Enemy>();
            b1.Is_up = false;

            GameObject Battle_Ship2 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
            BattleshipType_Enemy b2 = Battle_Ship2.GetComponent<BattleshipType_Enemy>();
            b2.Is_up = true;

            nextEnemy = "闘牛直進12";
            Next_Condition(180);
            nowGroupCnt++;
        }
        // 闘牛直進12
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 62)
        {
            GameObject saveObj = Instantiate(enemy_Clamchowder_Group_Straight, createPosR0.transform.position, transform.rotation);
            group_Script = saveObj.GetComponent<EnemyGroupManage>();
            group_Script.isItemDrop = true;

            nextEnemy = "円盤上10下10狭射撃";
            Next_Condition(90);
            nowGroupCnt++;
        }
        // 円盤上10下10狭射撃
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 63)
        {
            Object_Pooling pEnemy_UFO_Group = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group") as GameObject, 1, "enemy_UFO_Group");
            GameObject enemy_UFO_Group = pEnemy_UFO_Group.Active_Obj();
            enemy_UFO_Group.transform.position = createPosR1.transform.position + new Vector3(8.5f, 0, 0);
            enemy_UFO_Group.transform.rotation = transform.rotation;

            Object_Pooling pEnemy_UFO_Group2 = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group") as GameObject, 1, "enemy_UFO_Group");
            GameObject enemy_UFO_Group2 = pEnemy_UFO_Group2.Active_Obj();
            enemy_UFO_Group2.transform.position = createPosRm1.transform.position + new Vector3(8.5f, 0, 0);
            enemy_UFO_Group2.transform.rotation = transform.rotation;

            nextEnemy = "ハエ上2下2広右2アイテム";
            Next_Condition(240);
            nowGroupCnt++;
        }
        // ハエ上2下2広右2アイテム
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 64)
        {
            Instantiate(enemy_Beelzebub_Group_FourWide_Item, createPosR0.transform.position, transform.rotation);

            nextEnemy = "闘牛斜め配置4";
            Next_Condition(180);
            nowGroupCnt++;
        }
        // 闘牛斜め配置4
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 65)
        {
            Instantiate(enemy_ClamChowder_Group_Four, createPos_FourGroupL.transform.position, transform.rotation);

            nextEnemy = "ビートル5";
            Next_Condition(135);
            nowGroupCnt++;
        }
        // ビートル5
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 66)
        {
            GameObject beetleGroup = Instantiate(enemy_Beetle_Group, createPosRm3.transform.position, transform.rotation);
            beetleGroup.transform.position = new Vector3(15, -8, 0);

            nextEnemy = "闘牛直進12上下";
            Next_Condition(540);
            nowGroupCnt++;
        }
        // 闘牛直進12上下
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 67)
        {
            Instantiate(enemy_Clamchowder_Group_Straight, createPosR3.transform.position, transform.rotation);
            Instantiate(enemy_Clamchowder_Group_Straight, createPosRm3.transform.position, transform.rotation);

            nextEnemy = "闘牛直進12上下";
            Next_Condition(90);
            nowGroupCnt++;
        }
        // 闘牛直進12上下
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 68)
        {
            Instantiate(enemy_Clamchowder_Group_Straight, createPosR3.transform.position, transform.rotation);
            Instantiate(enemy_Clamchowder_Group_Straight, createPosRm3.transform.position, transform.rotation);

            nextEnemy = "闘牛直進12";
            Next_Condition(90);
            nowGroupCnt++;
        }
        // 闘牛直進12
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 69)
        {
            GameObject saveObj = Instantiate(enemy_Clamchowder_Group_Straight, createPosR0.transform.position, transform.rotation);
            group_Script = saveObj.GetComponent<EnemyGroupManage>();
            group_Script.isItemDrop = true;

            nextEnemy = "ヒトデ12";
            Next_Condition(420);
            nowGroupCnt++;
        }
        // ヒトデ12
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 70)
        {
            Instantiate(enemy_Star_Fish_Spowner, transform.position, transform.rotation);

            nextEnemy = "隕石20";
            Next_Condition(240 + 240);
        }
        // 隕石20
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 71)
        {
            Instantiate(Enemy_BoundMeteors, createMeteorPosR0.transform.position, transform.rotation);

            nextEnemy = "隕石20";
            Next_Condition(210);
        }
        // 隕石20
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 72)
        {
            Instantiate(Enemy_BoundMeteors, createMeteorPosR0.transform.position, transform.rotation);

            nextEnemy = "隕石20";
            Next_Condition(210);
        }
        // 隕石20
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 73)
        {
            Instantiate(Enemy_BoundMeteors, createMeteorPosR0.transform.position, transform.rotation);

            nextEnemy = "モアイ";
            Next_Condition(210);
        }
        // 隕石20
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 74)
        {
            Instantiate(Enemy_BoundMeteors, createMeteorPosR0.transform.position, transform.rotation);

            nextEnemy = "モアイ";
            Next_Condition(210);
        }
        // ビッグコアマーク3??????????????????????????????????????????????????????????????????????????????????????????????????????
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 75)
        {
            GameObject Boss_02 = Obj_Storage.Storage_Data.Boss_2.Active_Obj();
            Boss_02.transform.position = new Vector3(13.0f, 0.0f, 0.0f);
            isNowTwoBoss = true;

            GameObject mistSaveObj = Instantiate(mistEffectObj, transform.position, transform.rotation);
            backActive_Script = mistSaveObj.GetComponent<BackgroundActivation>();
            if (isDebug)
            {
                mistEffectObj.transform.position = new Vector3(0, 0, 3);
                mistParticle = mistSaveObj.GetComponent<ParticleSystem>();
                mistParticle.Play();
                backActive_Script.TransparencyChangeTrigger();
            }
            else
            {
                backActive_Script.TransparencyChangeTrigger();
            }

            nextEnemy = "None";
            Next_Condition(120);
        }
        // クリア
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 76)
        {
            Scene_Manager.Manager.Screen_Transition_To_LoadClearScene();
        }
    }

    private void CreateEnemyGroup_01_TypeB()
    {
        bool bigcoreFlag = true;
        bool bigcoreMK2Flag = true;
        bool moaiFlag = true;

		// 円盤上10
		if (Is_A_Specified_Frame(turning_frame) && groupCnt == 1)
		{
			CreateEnemy(EnemyType.UFO_GROUP_NONESHOT, CreatePos.R3);

			nextEnemy = "円盤下10";
			Next_Condition(210);
			nowGroupCnt++;
		}
		// 円盤下10
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 2)
		{
			CreateEnemy(EnemyType.UFO_GROUP_NONESHOT, CreatePos.Rm3);

			nextEnemy = "闘牛斜め配置4中央アイテム2";
			Next_Condition(90);
			nowGroupCnt++;
		}
		// 闘牛斜め配置4中央アイテム2
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 3)
		{
			CreateEnemy(EnemyType.CLAMCHOWDER_GROUP_FOUR, CreatePos.FOURGROUPL);

			nextEnemy = "闘牛斜め配置4";
			Next_Condition(125);
			nowGroupCnt++;
		}
		// 闘牛斜め配置4
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 4)
		{
			CreateEnemy(EnemyType.CLAMCHOWDER_GROUP_FOUR, CreatePos.FOURGROUPL, true);

			nextEnemy = "円盤上10下10";
			Next_Condition(150);
			nowGroupCnt++;
		}
		// 円盤上10下10
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 5)
		{
			CreateEnemy(EnemyType.UFO_GROUP_NONESHOT, CreatePos.R3);
			CreateEnemy(EnemyType.UFO_GROUP_NONESHOT, CreatePos.Rm3);

			nextEnemy = "円盤上10下10狭";
			Next_Condition(150);
			nowGroupCnt++;
		}
		// 円盤上10下10狭
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 6)
		{
			CreateEnemy(EnemyType.UFO_GROUP_NONESHOT, CreatePos.R1);
			CreateEnemy(EnemyType.UFO_GROUP_NONESHOT, CreatePos.Rm1);

			nextEnemy = "闘牛縦3";
			Next_Condition(120);
			nowGroupCnt++;
		}
		// 闘牛突進前2後2前アイテム
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 7)
		{
			CreateEnemy(EnemyType.CLAMCHOWDER_GROUP_FOURTRIANGLE, CreatePos.FOURGROUPL, true);

			nextEnemy = "闘牛斜め配置4";
			Next_Condition(125);
			nowGroupCnt++;
		}
		// 闘牛突進前2後2
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 8)
		{
			CreateEnemy(EnemyType.CLAMCHOWDER_GROUP_FOURTRIANGLE, CreatePos.FOURGROUPL);

			nextEnemy = "円盤上10下10狭";
			Next_Condition(150);
			nowGroupCnt++;
		}
		// 円盤上10下10狭
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 9)
		{
			CreateEnemy(EnemyType.UFO_GROUP_NONESHOT, CreatePos.R1);
			CreateEnemy(EnemyType.UFO_GROUP_NONESHOT, CreatePos.Rm1);

			nextEnemy = "闘牛斜め配置4中央2アイテム";
			Next_Condition(120);
			nowGroupCnt++;
		}
		// 闘牛突進前2後2前アイテム
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 10)
		{
			//GameObject saveObj = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Four.Active_Obj();
			//saveObj.transform.position = createPos_FourGroupL.transform.position;

			CreateEnemy(EnemyType.CLAMCHOWDER_GROUP_FOURTRIANGLE, CreatePos.FOURGROUPL, true);

			nextEnemy = "闘牛斜め配置4";
			Next_Condition(125);
			nowGroupCnt++;
		}
		// 闘牛突進前2後2
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 11)
		{
			CreateEnemy(EnemyType.CLAMCHOWDER_GROUP_FOURTRIANGLE, CreatePos.FOURGROUPL, false);

			nextEnemy = "闘牛縦3中央アイテム";
			Next_Condition(180);
			nowGroupCnt++;
		}
		// 闘牛縦3中央アイテム
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 12)
		{
			CreateEnemy(EnemyType.CLAMCHOWDER_GROUP_THREE, CreatePos.FOURGROUPL, true);

			nextEnemy = "闘牛上2下2";
			Next_Condition(30);
			nowGroupCnt++;
		}
		// 闘牛上2下2
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 13)
		{
			CreateEnemy(EnemyType.CLAMCHOWDER_GROUP_TOPANDUNEDR, CreatePos.FOURGROUPL, false);

			nextEnemy = "闘牛縦3中央アイテム";
			Next_Condition(30);
			nowGroupCnt++;
		}
		// 闘牛縦3中央アイテム
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 14)
		{
			CreateEnemy(EnemyType.CLAMCHOWDER_GROUP_THREE, CreatePos.FOURGROUPL, true);

			nextEnemy = "闘牛上2下2";
			Next_Condition(30);
			nowGroupCnt++;
		}
		// 闘牛上2下2
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 15)
		{
			CreateEnemy(EnemyType.CLAMCHOWDER_GROUP_TOPANDUNEDR, CreatePos.FOURGROUPL, false);

			nextEnemy = "闘牛縦7";
			Next_Condition(30);
			nowGroupCnt++;
		}
		// 闘牛縦7
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 16)
		{
			CreateEnemy(EnemyType.CLAMCHOWDER_GROUP_FIVE, CreatePos.FOURGROUPL, false);

			nextEnemy = "ビッグコア";
			Next_Condition(450);
			nowGroupCnt++;
		}
		// ビッグコア■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 17)
		{
			if (bigcoreFlag)
			{
				GameObject Boss_Middle = Obj_Storage.Storage_Data.Boss_Middle.Active_Obj();
				saveEnemyObj = Boss_Middle;
				Boss_Middle.transform.position = createMiddleBossPos.transform.position;
				Boss_Middle.transform.rotation = transform.rotation;
				Next_Condition(210);
			}
			else
			{
				Next_Condition(1);
			}
			nextEnemy = "円盤5弾";

			nowGroupCnt++;
		}
		// 円盤5弾
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 18)
		{
			//CreateEnemy(EnemyType.UFO_GROUP_FIVE, CreatePos.Rm4, false);

			//nextEnemy = "円盤5弾";
			//Instantiate(enemy_ClamChowder_Group_TwoWaveOnlyDown, createPosR4.transform.position, transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TwoWaveOnlyUp, createPosRm4.transform.position, transform.rotation);

			GameObject saveObj = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_TwoWaveOnlyDown.Active_Obj();
			GameObject saveObj2 = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_TwoWaveOnlyUp.Active_Obj();

			saveObj.transform.position = createPosR4.transform.position;
			saveObj2.transform.position = createPosRm4.transform.position;

			Next_Condition(195);
		}
		// 円盤5弾
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 19)
		{
			//CreateEnemy(EnemyType.UFO_GROUP_FIVE, CreatePos.R4, false);

			//nextEnemy = "円盤5弾";
			GameObject saveObj = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_TwoWaveOnlyDown.Active_Obj();
			GameObject saveObj2 = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_TwoWaveOnlyUp.Active_Obj();

			saveObj.transform.position = createPosR4.transform.position;
			saveObj2.transform.position = createPosRm4.transform.position;
			Next_Condition(195);
		}
		// 円盤5弾
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 20)
		{
			//CreateEnemy(EnemyType.UFO_GROUP_FIVE, CreatePos.Rm4, false);

			//nextEnemy = "円盤5弾";
			GameObject saveObj = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_TwoWaveOnlyDown.Active_Obj();
			GameObject saveObj2 = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_TwoWaveOnlyUp.Active_Obj();

			saveObj.transform.position = createPosR4.transform.position;
			saveObj2.transform.position = createPosRm4.transform.position;
			Next_Condition(195);
		}
		// 円盤5弾
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 21)
		{
			//CreateEnemy(EnemyType.UFO_GROUP_FIVE, CreatePos.R4, false);

			//nextEnemy = "円盤5弾";
			Next_Condition(195);
		}
		// 円盤5弾
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 22)
		{
			//CreateEnemy(EnemyType.UFO_GROUP_FIVE, CreatePos.Rm4, false);

			//nextEnemy = "円盤5弾";
			GameObject saveObj = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_TwoWaveOnlyDown.Active_Obj();
			GameObject saveObj2 = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_TwoWaveOnlyUp.Active_Obj();

			saveObj.transform.position = createPosR4.transform.position;
			saveObj2.transform.position = createPosRm4.transform.position;
			Next_Condition(195);
		}
		// 円盤5弾
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 23)
		{
			//CreateEnemy(EnemyType.UFO_GROUP_FIVE, CreatePos.R4, false);

			//nextEnemy = "円盤5弾";
			Next_Condition(195);
		}
		// 円盤5弾
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 24)
		{
			//CreateEnemy(EnemyType.UFO_GROUP_FIVE, CreatePos.Rm4, false);

			//nextEnemy = "円盤5弾";
			GameObject saveObj = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_TwoWaveOnlyDown.Active_Obj();
			GameObject saveObj2 = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_TwoWaveOnlyUp.Active_Obj();

			saveObj.transform.position = createPosR4.transform.position;
			saveObj2.transform.position = createPosRm4.transform.position;
			Next_Condition(195 + 120);
		}
		// ハエ上2下2広
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 25)
		{
			Instantiate(enemy_Beelzebub_Group_FourWide, createPosR0.transform.position, transform.rotation);

			nextEnemy = "ハエ上2下2広右2アイテム";
			Next_Condition(180);
			nowGroupCnt++;
		}
		// ハエ上2下2広右2アイテム
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 26)
		{
			Instantiate(enemy_Beelzebub_Group_FourWide_Item, createPosR0.transform.position, transform.rotation);

			nextEnemy = "ビートル5";
			Next_Condition(240);
			nowGroupCnt++;
		}
		// ビートル5
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 27)
		{
			CreateEnemy(EnemyType.BEETLE_GROUP_THREE, CreatePos.L0, false);

			nextEnemy = "戦艦";
			Next_Condition(360 + 180);
			nowGroupCnt++;
		}
		// 戦艦
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 28)
		{
			GameObject Battle_Ship1 = Instantiate(enemy_BattleShip, createPosR0.transform.position, enemy_BattleShip.transform.rotation);
			Battle_Ship1.transform.position = createPosR0.transform.position;
			BattleshipType_Enemy b1 = Battle_Ship1.GetComponent<BattleshipType_Enemy>();
			b1.is_sandwich = false;
			b1.Is_up = false;

			nextEnemy = "戦艦上下";
			Next_Condition(240);
			nowGroupCnt++;
		}
		// 戦艦上下
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 29)
		{
			GameObject Battle_Ship1 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
			BattleshipType_Enemy b1 = Battle_Ship1.GetComponent<BattleshipType_Enemy>();
			b1.Is_up = false;

			GameObject Battle_Ship2 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
			BattleshipType_Enemy b2 = Battle_Ship2.GetComponent<BattleshipType_Enemy>();
			b2.Is_up = true;

			nextEnemy = "闘牛直進12";
			Next_Condition(315);
			nowGroupCnt++;
		}
		// 戦艦上下
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 30)
		{
			GameObject Battle_Ship1 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
			BattleshipType_Enemy b1 = Battle_Ship1.GetComponent<BattleshipType_Enemy>();
			b1.Is_up = false;

			GameObject Battle_Ship2 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
			BattleshipType_Enemy b2 = Battle_Ship2.GetComponent<BattleshipType_Enemy>();
			b2.Is_up = true;

			nextEnemy = "闘牛直進12";
			Next_Condition(90);
			nowGroupCnt++;
		}
		// 闘牛直進12
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 31)
		{
			GameObject saveObj = Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position, transform.rotation);
			group_Script = saveObj.GetComponent<EnemyGroupManage>();
			group_Script.isItemDrop = true;

			nextEnemy = "闘牛直進12";
			Next_Condition(150);
			nowGroupCnt++;
		}
		// 闘牛直進12
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 32)
		{
			GameObject saveObj = Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR1.transform.position, transform.rotation);
			GameObject saveObj2 = Instantiate(enemy_ClamChowder_Group_TenStraight, createPosRm1.transform.position, transform.rotation);
			group_Script = saveObj.GetComponent<EnemyGroupManage>();
			group_Script.isItemDrop = true;
			group_Script = saveObj2.GetComponent<EnemyGroupManage>();
			group_Script.isItemDrop = true;

			nextEnemy = "戦艦";
			Next_Condition(150);
			nowGroupCnt++;
		}
		// 闘牛直進12
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 33)
		{
			GameObject saveObj = Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position, transform.rotation);
			group_Script = saveObj.GetComponent<EnemyGroupManage>();
			group_Script.isItemDrop = true;

			nextEnemy = "戦艦";
			Next_Condition(150);
			nowGroupCnt++;
		}
		// 戦艦
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 34)
		{
			GameObject Battle_Ship1 = Instantiate(enemy_BattleShip, createPosR0.transform.position, enemy_BattleShip.transform.rotation);
			Battle_Ship1.transform.position = createPosR0.transform.position;
			BattleshipType_Enemy b1 = Battle_Ship1.GetComponent<BattleshipType_Enemy>();
			b1.is_sandwich = false;
			b1.Is_up = false;

			nextEnemy = "闘牛12直進上下";
			Next_Condition(210);
			nowGroupCnt++;
		}
		// 闘牛12直進上下
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 35)
		{
			//Instantiate(enemy_Clamchowder_Group_Straight, createPosR3.transform.position, transform.rotation);
			//Instantiate(enemy_Clamchowder_Group_Straight, createPosRm3.transform.position, transform.rotation);
			GameObject saveObj = Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(0.0f, 3.5f, 0.0f), transform.rotation);
			GameObject saveObj2 = Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(0.0f, -3.5f, 0.0f), transform.rotation);
			group_Script = saveObj.GetComponent<EnemyGroupManage>();
			group_Script.isItemDrop = true;
			group_Script = saveObj2.GetComponent<EnemyGroupManage>();
			group_Script.isItemDrop = true;

			nextEnemy = "ハエ上2下2広右2アイテム";
			Next_Condition(150);
			nowGroupCnt++;
		}
		// 闘牛12直進上下
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 36)
		{
			//Instantiate(enemy_Clamchowder_Group_Straight, createPosR3.transform.position, transform.rotation);
			//Instantiate(enemy_Clamchowder_Group_Straight, createPosRm3.transform.position, transform.rotation);
			GameObject saveObj = Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(0.0f,4.5f,0.0f), transform.rotation);
			GameObject saveObj2 = Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(0.0f, 2.5f, 0.0f), transform.rotation);
			group_Script = saveObj.GetComponent<EnemyGroupManage>();
			group_Script.isItemDrop = true;
			group_Script = saveObj2.GetComponent<EnemyGroupManage>();
			group_Script.isItemDrop = true;
			GameObject saveObj3 = Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(0.0f, -4.5f, 0.0f), transform.rotation);
			GameObject saveObj4 = Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(0.0f, -2.5f, 0.0f), transform.rotation);
			group_Script = saveObj3.GetComponent<EnemyGroupManage>();
			group_Script.isItemDrop = true;
			group_Script = saveObj4.GetComponent<EnemyGroupManage>();
			group_Script.isItemDrop = true;

			nextEnemy = "ハエ上2下2広右2アイテム";
			Next_Condition(450);
			nowGroupCnt++;
		}
		// ハエ8アイテム4
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 37)
		{
			Instantiate(enemy_Beelzebub_Group_EightNormal_Item, createPosR0.transform.position, transform.rotation);

			nextEnemy = "ハエ上2下2広右2アイテム";
			Next_Condition(180);
			nowGroupCnt++;
		}
		// ビートル7
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 38)
		{
			GameObject beetleGroup = Instantiate(enemy_Beetle_Group_Seven, createPosRm3.transform.position, transform.rotation);
			beetleGroup.transform.position = new Vector3(15, -8, 0);

			nextEnemy = "闘牛縦縞7";
			Next_Condition(360 + 240);
			nowGroupCnt++;
		}
		//------------------------------------------------------------------------------------------------------------------------------------
		// 
		// シマシマ牛
		// 
		//------------------------------------------------------------------------------------------------------------------------------------
		// 闘牛縦縞7
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 39)
		{
			Instantiate(enemy_ClamChowder_Group_SevenStraight, createPosR0.transform.position, transform.rotation);

			nextEnemy = "闘牛縦縞6";
			Next_Condition(60);
		}
		// 闘牛縦縞6
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 40)
		{
			Instantiate(enemy_ClamChowder_Group_SixStraight, createPosR0.transform.position, transform.rotation);

			nextEnemy = "闘牛縦縞7";
			Next_Condition(60);
		}
		// 闘牛縦縞7
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 41)
		{
			Instantiate(enemy_ClamChowder_Group_SevenStraight, createPosR0.transform.position, transform.rotation);

			nextEnemy = "闘牛縦縞6";
			Next_Condition(60);
		}
		// 闘牛縦縞6
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 42)
		{
			Instantiate(enemy_ClamChowder_Group_SixStraight, createPosR0.transform.position, transform.rotation);

			nextEnemy = "闘牛直進12上下";
			Next_Condition(60);
		}
		// 闘牛直進12上下
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 43)
		{
			Instantiate(enemy_Clamchowder_Group_Straight, createPosR3.transform.position, transform.rotation);
			Instantiate(enemy_Clamchowder_Group_Straight, createPosRm3.transform.position, transform.rotation);

			nextEnemy = "闘牛直進12上下";
			Next_Condition(75);
			nowGroupCnt++;
		}
		// 闘牛直進12上下
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 44)
		{
			Instantiate(enemy_Clamchowder_Group_Straight, createPosR3.transform.position, transform.rotation);
			Instantiate(enemy_Clamchowder_Group_Straight, createPosRm3.transform.position, transform.rotation);

			nextEnemy = "闘牛直進12";
			Next_Condition(75);
			nowGroupCnt++;
		}
		// 闘牛直進12
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 45)
		{
			GameObject saveObj = Instantiate(enemy_Clamchowder_Group_Straight, createPosR0.transform.position, transform.rotation);
			group_Script = saveObj.GetComponent<EnemyGroupManage>();
			group_Script.isItemDrop = true;

			nextEnemy = "ビッグコアマーク2";
			Next_Condition(270);
			nowGroupCnt++;
		}
		//------------------------------------------------------------------------------------------------------------------------------------
		// 
		// ビッグコアマーク2■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
		// 
		//------------------------------------------------------------------------------------------------------------------------------------
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 46)
		{
			if (bigcoreMK2Flag)
			{
				GameObject Boss_01 = Obj_Storage.Storage_Data.Boss_1.Active_Obj();
				Boss_01.transform.position = new Vector3(10.0f, 0.0f, 0.0f);

				GameObject mistSaveObj = Instantiate(mistEffectObj, transform.position, transform.rotation);
				mistEffectObj.transform.position = new Vector3(0, 0, 3);
				mistParticle = mistSaveObj.GetComponent<ParticleSystem>();
				backActive_Script = mistSaveObj.GetComponent<BackgroundActivation>();
				mistParticle.Play();
				backActive_Script.TransparencyChangeTrigger();
				isNowOneBoss = true;


				Next_Condition(120);
			}
			else
			{
				Next_Condition(1);
			}

			nextEnemy = "ヒトデ12";
		}
		//------------------------------------------------------------------------------------------------------------------------------------
		// 
		// 宇宙からの贈り物
		// 
		//------------------------------------------------------------------------------------------------------------------------------------
		// ヒトデ12
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 47)
		{
			Instantiate(enemy_Star_Fish_Spowner, transform.position, transform.rotation);

			nextEnemy = "バキュラ4";
			Next_Condition(240 + 360);
		}
		// バキュラ4
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 48)
		{
			Instantiate(enemy_Bacula_FourOnly, createPosR0.transform.position, transform.rotation);

			nextEnemy = "バキュラ4";
			Next_Condition(180);
		}
		// バキュラ4
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 49)
		{
			Instantiate(enemy_Bacula_FourOnly, createPosR0.transform.position, transform.rotation);

			nextEnemy = "バキュラ4";
			Next_Condition(180);
		}
		// バキュラ4
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 50)
		{
			Instantiate(enemy_Bacula_FourOnly, createPosR0.transform.position, transform.rotation);

			nextEnemy = "隕石20";
			Next_Condition(210);
		}
		// から打ち
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 51) Next_Condition(1);
		// から打ち
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 52) Next_Condition(1);
		// 隕石20
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 53)
		{
			Instantiate(Enemy_BoundMeteors, createMeteorPosR0.transform.position, transform.rotation);

			nextEnemy = "隕石20";
			Next_Condition(240);
		}
		// 隕石20
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 54)
		{
			Instantiate(Enemy_BoundMeteors, createMeteorPosR0.transform.position, transform.rotation);

			nextEnemy = "モアイ";
			Next_Condition(360);
		}
		//------------------------------------------------------------------------------------------------------------------------------------
		//
		// モアイ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
		//
		//------------------------------------------------------------------------------------------------------------------------------------
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 55)
		{
			if (moaiFlag)
			{
				GameObject moai = Obj_Storage.Storage_Data.Moai.Active_Obj();
				moai.transform.position = new Vector3(15.44f, -17.0f, 0.0f);
				Wireless_sinario.Is_using_wireless = true;
				isNowMoai = true;
				Next_Condition(620);
			}
			else
			{
				Next_Condition(1);
			}
			nextEnemy = "ヒトデ12";
		}
		// ヒトデ12
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 56)
		{
			Instantiate(enemy_Star_Fish_Spowner, transform.position, transform.rotation);

			nextEnemy = "円盤上10狭下10射撃";
			Next_Condition(240 + 360);
		}
		// 円盤上10狭下10射撃
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 57)
		{
			//Object_Pooling pEnemy_UFO_Group = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group") as GameObject, 1, "enemy_UFO_Group");
			GameObject enemy_UFO_Group = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group.transform.position = createPosR1.transform.position + new Vector3(8.5f, 0, 0);
			enemy_UFO_Group.transform.rotation = transform.rotation;

			//Object_Pooling pEnemy_UFO_Group2 = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group") as GameObject, 1, "enemy_UFO_Group");
			GameObject enemy_UFO_Group2 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group2.transform.position = createPosRm3.transform.position + new Vector3(8.5f, 0, 0);
			enemy_UFO_Group2.transform.rotation = transform.rotation;

			nextEnemy = "円盤上10下10狭射撃";
			Next_Condition(75);
			nowGroupCnt++;
		}
		// 円盤上10下10狭射撃
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 58)
		{
			//Object_Pooling pEnemy_UFO_Group = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group") as GameObject, 1, "enemy_UFO_Group");
			GameObject enemy_UFO_Group = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group.transform.position = createPosR3.transform.position + new Vector3(8.5f, 0, 0);
			enemy_UFO_Group.transform.rotation = transform.rotation;

			//Object_Pooling pEnemy_UFO_Group2 = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group") as GameObject, 1, "enemy_UFO_Group");
			GameObject enemy_UFO_Group2 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group2.transform.position = createPosRm1.transform.position + new Vector3(8.5f, 0, 0);
			enemy_UFO_Group2.transform.rotation = transform.rotation;

			nextEnemy = "円盤上10狭下10射撃";
			Next_Condition(75);
			nowGroupCnt++;
		}
		// 円盤上10狭下10射撃
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 59)
		{
			//Object_Pooling pEnemy_UFO_Group = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group") as GameObject, 1, "enemy_UFO_Group");
			GameObject enemy_UFO_Group = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group.transform.position = createPosR1.transform.position + new Vector3(8.5f, 0, 0);
			enemy_UFO_Group.transform.rotation = transform.rotation;

			//Object_Pooling pEnemy_UFO_Group2 = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group") as GameObject, 1, "enemy_UFO_Group");
			GameObject enemy_UFO_Group2 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group2.transform.position = createPosRm3.transform.position + new Vector3(8.5f, 0, 0);
			enemy_UFO_Group2.transform.rotation = transform.rotation;

			nextEnemy = "円盤上10下10狭射撃";
			Next_Condition(75);
			nowGroupCnt++;
		}
		// 円盤上10下10狭射撃
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 60)
		{
			//Object_Pooling pEnemy_UFO_Group = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group") as GameObject, 1, "enemy_UFO_Group");
			GameObject enemy_UFO_Group = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group.transform.position = createPosR3.transform.position + new Vector3(8.5f, 0, 0);
			enemy_UFO_Group.transform.rotation = transform.rotation;

			//Object_Pooling pEnemy_UFO_Group2 = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group") as GameObject, 1, "enemy_UFO_Group");
			GameObject enemy_UFO_Group2 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group2.transform.position = createPosRm1.transform.position + new Vector3(8.5f, 0, 0);
			enemy_UFO_Group2.transform.rotation = transform.rotation;

			nextEnemy = "円盤上10下10射撃";
			Next_Condition(120);
			nowGroupCnt++;
		}
		//--------------------------------------------------------------
		// 
		// 円盤射撃と戦艦タッグ
		// 
		//--------------------------------------------------------------
		// 戦艦
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 61)
		{
			GameObject Battle_Ship1 = Instantiate(enemy_BattleShip, createPosR0.transform.position, enemy_BattleShip.transform.rotation);
			Battle_Ship1.transform.position = createPosR0.transform.position;
			BattleshipType_Enemy b1 = Battle_Ship1.GetComponent<BattleshipType_Enemy>();
			b1.is_sandwich = false;
			b1.Is_up = false;

			nextEnemy = "戦艦";
			Next_Condition(210);
			nowGroupCnt++;
		}
		// 円盤上10下10射撃
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 62)
		{
			//Object_Pooling pEnemy_UFO_Group = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group") as GameObject, 1, "enemy_UFO_Group");
			GameObject enemy_UFO_Group = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group.transform.position = createPosR4.transform.position + new Vector3(8.5f, 0, 0);
			enemy_UFO_Group.transform.rotation = transform.rotation;

			//Object_Pooling pEnemy_UFO_Group2 = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group") as GameObject, 1, "enemy_UFO_Group");
			GameObject enemy_UFO_Group2 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group2.transform.position = createPosRm4.transform.position + new Vector3(8.5f, 0, 0);
			enemy_UFO_Group2.transform.rotation = transform.rotation;

			nextEnemy = "円盤上10下10射撃";
			Next_Condition(90);
			nowGroupCnt++;
		}
		// 戦艦
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 63)
		{
			GameObject Battle_Ship1 = Instantiate(enemy_BattleShip, createPosR0.transform.position, enemy_BattleShip.transform.rotation);
			Battle_Ship1.transform.position = createPosR0.transform.position;
			BattleshipType_Enemy b1 = Battle_Ship1.GetComponent<BattleshipType_Enemy>();
			b1.is_sandwich = false;
			b1.Is_up = false;

			nextEnemy = "戦艦";
			Next_Condition(30);
			nowGroupCnt++;
		}

		// 円盤上10下10射撃
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 64)
		{
			//Object_Pooling pEnemy_UFO_Group = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group") as GameObject, 1, "enemy_UFO_Group");
			GameObject enemy_UFO_Group = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group.transform.position = createPosR4.transform.position + new Vector3(8.5f, 0, 0);
			enemy_UFO_Group.transform.rotation = transform.rotation;

			//Object_Pooling pEnemy_UFO_Group2 = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group") as GameObject, 1, "enemy_UFO_Group");
			GameObject enemy_UFO_Group2 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group2.transform.position = createPosRm4.transform.position + new Vector3(8.5f, 0, 0);
			enemy_UFO_Group2.transform.rotation = transform.rotation;

			nextEnemy = "円盤上10下10射撃";
			Next_Condition(120);
			nowGroupCnt++;
		}
		// 円盤上10下10射撃
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 65)
		{
			//Object_Pooling pEnemy_UFO_Group = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group") as GameObject, 1, "enemy_UFO_Group");
			GameObject enemy_UFO_Group = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group.transform.position = createPosR4.transform.position + new Vector3(8.5f, 0, 0);
			enemy_UFO_Group.transform.rotation = transform.rotation;

			//Object_Pooling pEnemy_UFO_Group2 = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group") as GameObject, 1, "enemy_UFO_Group");
			GameObject enemy_UFO_Group2 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group2.transform.position = createPosRm4.transform.position + new Vector3(8.5f, 0, 0);
			enemy_UFO_Group2.transform.rotation = transform.rotation;

			nextEnemy = "円盤上10下10射撃";
			Next_Condition(120);
			nowGroupCnt++;
		}
		// 円盤上10下10射撃
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 66)
		{
			//Object_Pooling pEnemy_UFO_Group = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group") as GameObject, 1, "enemy_UFO_Group");
			GameObject enemy_UFO_Group = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group.transform.position = createPosR4.transform.position + new Vector3(8.5f, 0, 0);
			enemy_UFO_Group.transform.rotation = transform.rotation;

			//Object_Pooling pEnemy_UFO_Group2 = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group") as GameObject, 1, "enemy_UFO_Group");
			GameObject enemy_UFO_Group2 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group2.transform.position = createPosRm4.transform.position + new Vector3(8.5f, 0, 0);
			enemy_UFO_Group2.transform.rotation = transform.rotation;

			nextEnemy = "戦艦";
			Next_Condition(120);
			nowGroupCnt++;
		}
		// からうち
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 67) Next_Condition(1);
		// 戦艦上下
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 68)
		{
			GameObject Battle_Ship1 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
			BattleshipType_Enemy b1 = Battle_Ship1.GetComponent<BattleshipType_Enemy>();
			b1.Is_up = false;

			GameObject Battle_Ship2 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
			BattleshipType_Enemy b2 = Battle_Ship2.GetComponent<BattleshipType_Enemy>();
			b2.Is_up = true;

			nextEnemy = "円盤上10下10狭射撃";
			Next_Condition(150);
			nowGroupCnt++;
		}
		// 円盤上10下10狭射撃
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 69)
		{
			//Object_Pooling pEnemy_UFO_Group = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group") as GameObject, 1, "enemy_UFO_Group");
			GameObject enemy_UFO_Group = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group.transform.position = createPosR1.transform.position + new Vector3(8.5f, 0, 0);
			enemy_UFO_Group.transform.rotation = transform.rotation;

			//Object_Pooling pEnemy_UFO_Group2 = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group") as GameObject, 1, "enemy_UFO_Group");
			GameObject enemy_UFO_Group2 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group2.transform.position = createPosRm1.transform.position + new Vector3(8.5f, 0, 0);
			enemy_UFO_Group2.transform.rotation = transform.rotation;

			nextEnemy = "戦艦上下";
			Next_Condition(90);
			nowGroupCnt++;
		}
		// 円盤上10下10狭射撃
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 70)
		{
			//Object_Pooling pEnemy_UFO_Group = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group") as GameObject, 1, "enemy_UFO_Group");
			GameObject enemy_UFO_Group = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group.transform.position = createPosR1.transform.position + new Vector3(8.5f, 0, 0);
			enemy_UFO_Group.transform.rotation = transform.rotation;

			//Object_Pooling pEnemy_UFO_Group2 = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group") as GameObject, 1, "enemy_UFO_Group");
			GameObject enemy_UFO_Group2 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group2.transform.position = createPosRm1.transform.position + new Vector3(8.5f, 0, 0);
			enemy_UFO_Group2.transform.rotation = transform.rotation;

			nextEnemy = "戦艦上下";
			Next_Condition(90);
			nowGroupCnt++;
		}
		// 戦艦上下
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 71)
		{
			GameObject Battle_Ship1 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
			BattleshipType_Enemy b1 = Battle_Ship1.GetComponent<BattleshipType_Enemy>();
			b1.Is_up = false;

			GameObject Battle_Ship2 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
			BattleshipType_Enemy b2 = Battle_Ship2.GetComponent<BattleshipType_Enemy>();
			b2.Is_up = true;

			nextEnemy = "円盤上10下10狭射撃";
			Next_Condition(30);
			nowGroupCnt++;
		}
		// 円盤上10下10狭射撃
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 72)
		{
			//Object_Pooling pEnemy_UFO_Group = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group") as GameObject, 1, "enemy_UFO_Group");
			GameObject enemy_UFO_Group = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group.transform.position = createPosR1.transform.position + new Vector3(8.5f, 0, 0);
			enemy_UFO_Group.transform.rotation = transform.rotation;

			//Object_Pooling pEnemy_UFO_Group2 = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group") as GameObject, 1, "enemy_UFO_Group");
			GameObject enemy_UFO_Group2 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group2.transform.position = createPosRm1.transform.position + new Vector3(8.5f, 0, 0);
			enemy_UFO_Group2.transform.rotation = transform.rotation;

			nextEnemy = "戦艦上下";
			Next_Condition(120);
			nowGroupCnt++;
		}

		// 円盤上10下10狭射撃
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 73)
		{
			//Object_Pooling pEnemy_UFO_Group = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group") as GameObject, 1, "enemy_UFO_Group");
			GameObject enemy_UFO_Group = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group.transform.position = createPosR1.transform.position + new Vector3(8.5f, 0, 0);
			enemy_UFO_Group.transform.rotation = transform.rotation;

			//Object_Pooling pEnemy_UFO_Group2 = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group") as GameObject, 1, "enemy_UFO_Group");
			GameObject enemy_UFO_Group2 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group2.transform.position = createPosRm1.transform.position + new Vector3(8.5f, 0, 0);
			enemy_UFO_Group2.transform.rotation = transform.rotation;

			nextEnemy = "円盤上10下10狭射撃";
			Next_Condition(480);
			nowGroupCnt++;
		}
		// 円盤上10下10狭射撃
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 74)
		{
			//Object_Pooling pEnemy_UFO_Group = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group") as GameObject, 1, "enemy_UFO_Group");
			GameObject enemy_UFO_Group = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group.transform.position = createPosR1.transform.position + new Vector3(8.5f, 0, 0);
			enemy_UFO_Group.transform.rotation = transform.rotation;

			//Object_Pooling pEnemy_UFO_Group2 = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group") as GameObject, 1, "enemy_UFO_Group");
			GameObject enemy_UFO_Group2 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group2.transform.position = createPosRm1.transform.position + new Vector3(8.5f, 0, 0);
			enemy_UFO_Group2.transform.rotation = transform.rotation;

			nextEnemy = "闘牛右上斜め配置7射撃";
			Next_Condition(480);
			nowGroupCnt++;
		}
		//--------------------------------------------------------------
		// 
		// 逆襲の闘牛
		// 
		//--------------------------------------------------------------
		// 闘牛右上斜め配置7射撃
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 75)
		{
			Instantiate(enemy_ClamChowder_Group_UpSevenDiagonal, createPosR0.transform.position, transform.rotation);

			nextEnemy = "闘牛左下斜め配置7射撃";
			Next_Condition(30);
		}
		// 闘牛左下斜め配置7射撃
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 76)
		{
			Instantiate(enemy_ClamChowder_Group_DownSevenDiagonal, createPosR0.transform.position, transform.rotation);

			nextEnemy = "闘牛右上斜め配置7射撃";
			Next_Condition(30);
		}
		// 闘牛右上斜め配置7射撃
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 77)
		{
			Instantiate(enemy_ClamChowder_Group_UpSevenDiagonal, createPosR0.transform.position, transform.rotation);

			nextEnemy = "闘牛左下斜め配置7射撃";
			Next_Condition(30);
		}
		// 闘牛左下斜め配置7射撃
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 78)
		{
			Instantiate(enemy_ClamChowder_Group_DownSevenDiagonal, createPosR0.transform.position, transform.rotation);

			nextEnemy = "闘牛右上斜め配置7射撃";
			Next_Condition(30);
		}
		// 闘牛右上斜め配置7射撃
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 79)
		{
			Instantiate(enemy_ClamChowder_Group_UpSevenDiagonal, createPosR0.transform.position, transform.rotation);

			nextEnemy = "闘牛左下斜め配置7射撃";
			Next_Condition(30);
		}
		// 闘牛左下斜め配置7射撃
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 80)
		{
			Instantiate(enemy_ClamChowder_Group_DownSevenDiagonal, createPosR0.transform.position, transform.rotation);

			nextEnemy = "闘牛右上斜め配置7射撃";
			Next_Condition(30);
		}
		// 闘牛右上斜め配置7射撃
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 81)
		{
			Instantiate(enemy_ClamChowder_Group_UpSevenDiagonal, createPosR0.transform.position, transform.rotation);

			nextEnemy = "闘牛左下斜め配置7射撃";
			Next_Condition(30);
		}
		// 闘牛左下斜め配置7射撃
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 82)
		{
			Instantiate(enemy_ClamChowder_Group_DownSevenDiagonal, createPosR0.transform.position, transform.rotation);

			nextEnemy = "闘牛右上斜め配置7射撃";
			Next_Condition(30);
		}
		// 闘牛右上斜め配置7射撃
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 83)
		{
			Instantiate(enemy_ClamChowder_Group_UpSevenDiagonal, createPosR0.transform.position, transform.rotation);

			nextEnemy = "闘牛左下斜め配置7射撃";
			Next_Condition(30);
		}
		// 闘牛左下斜め配置7射撃
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 84)
		{
			Instantiate(enemy_ClamChowder_Group_DownSevenDiagonal, createPosR0.transform.position, transform.rotation);

			nextEnemy = "闘牛右上斜め配置7射撃";
			Next_Condition(30);
		}

		//戦艦上下
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 85)
		{
			GameObject Battle_Ship1 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
			BattleshipType_Enemy b1 = Battle_Ship1.GetComponent<BattleshipType_Enemy>();
			b1.Is_up = false;

			GameObject Battle_Ship2 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
			BattleshipType_Enemy b2 = Battle_Ship2.GetComponent<BattleshipType_Enemy>();
			b2.Is_up = true;
			nextEnemy = "闘牛130";
			Next_Condition(240);

		}

		// 闘牛130
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 86)
		{
			Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position, transform.rotation);
			Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(2.0f, 0.81f * 1.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(4.0f, 0.81f * 2.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(6.0f, 0.81f * 3.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(8.0f, 0.81f * 4.0f, 0.0f), transform.rotation);
			Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(2.0f, 0.81f * -1.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(4.0f, 0.81f * -2.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(6.0f, 0.81f * -3.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(8.0f, 0.81f * -4.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(10.0f, 0.81f * 5.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(10.0f, 0.81f * -5.0f, 0.0f), transform.rotation);

			nextEnemy = "闘牛130";
			Next_Condition(115);
		}
		// 闘牛130
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 87)
		{
			GameObject Battle_Ship1 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
			BattleshipType_Enemy b1 = Battle_Ship1.GetComponent<BattleshipType_Enemy>();
			b1.Is_up = false;

			GameObject Battle_Ship2 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
			BattleshipType_Enemy b2 = Battle_Ship2.GetComponent<BattleshipType_Enemy>();
			b2.Is_up = true;

			Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position, transform.rotation);
			Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(2.0f, 0.81f * 1.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(4.0f, 0.81f * 2.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(6.0f, 0.81f * 3.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(8.0f, 0.81f * 4.0f, 0.0f), transform.rotation);
			Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(2.0f, 0.81f * -1.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(4.0f, 0.81f * -2.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(6.0f, 0.81f * -3.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(8.0f, 0.81f * -4.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(10.0f, 0.81f * 5.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(10.0f, 0.81f * -5.0f, 0.0f), transform.rotation);

			nextEnemy = "闘牛130";
			Next_Condition(115);
		}
		// 闘牛130
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 88)
		{
			Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position, transform.rotation);
			Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(2.0f, 0.81f * 1.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(4.0f, 0.81f * 2.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(6.0f, 0.81f * 3.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(8.0f, 0.81f * 4.0f, 0.0f), transform.rotation);
			Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(2.0f, 0.81f * -1.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(4.0f, 0.81f * -2.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(6.0f, 0.81f * -3.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(8.0f, 0.81f * -4.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(10.0f, 0.81f * 5.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(10.0f, 0.81f * -5.0f, 0.0f), transform.rotation);

			nextEnemy = "闘牛130";
			Next_Condition(115);
		}
		// 闘牛130
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 89)
		{
			Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position, transform.rotation);
			Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(2.0f, 0.81f * 1.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(4.0f, 0.81f * 2.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(6.0f, 0.81f * 3.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(8.0f, 0.81f * 4.0f, 0.0f), transform.rotation);
			Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(2.0f, 0.81f * -1.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(4.0f, 0.81f * -2.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(6.0f, 0.81f * -3.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(8.0f, 0.81f * -4.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(10.0f, 0.81f * 5.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(10.0f, 0.81f * -5.0f, 0.0f), transform.rotation);

			nextEnemy = "闘牛130";
			Next_Condition(115);
		}
		// 闘牛130
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 90)
		{
			Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position, transform.rotation);
			Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(2.0f, 0.81f * 1.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(4.0f, 0.81f * 2.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(6.0f, 0.81f * 3.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(8.0f, 0.81f * 4.0f, 0.0f), transform.rotation);
			Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(2.0f, 0.81f * -1.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(4.0f, 0.81f * -2.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(6.0f, 0.81f * -3.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(8.0f, 0.81f * -4.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(10.0f, 0.81f * 5.0f, 0.0f), transform.rotation);
			//Instantiate(enemy_ClamChowder_Group_TenStraight, createPosR0.transform.position + new Vector3(10.0f, 0.81f * -5.0f, 0.0f), transform.rotation);

			nextEnemy = "ビッグコアマーク3";
			Next_Condition(300);
		}
		// ビッグコアマーク3??????????????????????????????????????????????????????????????????????????????????????????????????????
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 91)
		{
			GameObject Boss_02 = Obj_Storage.Storage_Data.Boss_2.Active_Obj();
			Boss_02.transform.position = new Vector3(13.0f, 0.0f, 0.0f);
			isNowTwoBoss = true;

			GameObject mistSaveObj = Instantiate(mistEffectObj, transform.position, transform.rotation);
			backActive_Script = mistSaveObj.GetComponent<BackgroundActivation>();
			if (isDebug)
			{
				mistEffectObj.transform.position = new Vector3(0, 0, 3);
				mistParticle = mistSaveObj.GetComponent<ParticleSystem>();
				mistParticle.Play();
				backActive_Script.TransparencyChangeTrigger();
			}
			else
			{
				backActive_Script.TransparencyChangeTrigger();
			}

			nextEnemy = "None";
			Next_Condition(120);
		}
		// クリア
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt >= 92)
		{
			Scene_Manager.Manager.Screen_Transition_To_LoadClearScene();
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
    private bool Is_A_Specified_Frame(int specified_frame)
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
