//作成者：川村良太
//敵を出すスクリプト

//2019/08/03改修
using System;
using UnityEngine;

public class EnemyCreate : MonoBehaviour
{
	//━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
	//
	// 生成位置の変数
	//
	//━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
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
	#region createMiddlePos
	public GameObject createMiddleBossPos;
    public GameObject createBattleShipPos;
	#endregion
	//バキュラ位置
	#region CreateBacula
	public GameObject createBaculaGroupPos;
	#endregion
	//隕石生成位置
	#region CreateMeteor
	public GameObject createMeteorPosR0;
	#endregion

	//━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
	//
	// 敵グループプレハブのオブジェクト変数
	//
	//━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
	// UFOオブジェクト
	#region UFO
	public GameObject enemy_UFO_Group_Five;
	#endregion
	// 闘牛オブジェクト
	#region ClamChowder
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
	public GameObject enemy_ClamChowder_Group_ThreeStraight;
	public GameObject enemy_ClamChowder_Group_SevenWave;
    public GameObject enemy_ClamChowder_Group_Straight;
    public GameObject enemy_ClamChowder_Group_StraightBehind;
    public GameObject enemy_ClamChowder_Group_FourTriangle;
    public GameObject enemy_ClamChowder_Group_FourTriangle_B;
    public GameObject enemy_ClamChowder_Group_FourTriangle_C;
    public GameObject enemy_ClamChowder_Group_FourTriangle_NoItem;
    public GameObject enemy_ClamChowder_Group_TwelveStraight;
    public GameObject enemy_ClamChowder_Group_SevenStraight;
    public GameObject enemy_ClamChowder_Group_SixStraight;
    public GameObject enemy_ClamChowder_Group_UpSevenDiagonal;
    public GameObject enemy_ClamChowder_Group_DownSevenDiagonal;
    public GameObject enemy_ClamChowder_Group_TenStraight;
    public GameObject enemy_ClamChowder_Group_FourVerticalAttack;
    public GameObject enemy_ClamChowder_Group_FourVerticalStraight;
    public GameObject enemy_ClamChowder_Group_Seven;
	//public GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp;
	//public GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown;
	//public GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item;
	//public GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item;
	#endregion
	// ハエオブジェクト
	#region Beelzebub
	public GameObject enemy_Beelzebub_Group_FourNomal;
	public GameObject enemy_Beelzebub_Group_FourBack;
	public GameObject enemy_Beelzebub_Group_FourWide;
	public GameObject enemy_Beelzebub_Group_FourWide_Item;
	public GameObject enemy_Beelzebub_Group_EightNormal_Item;
	public GameObject enemy_Beelzebub_Group_TwoWide;
	#endregion
	// ビートルオブジェクト
	#region Beetle
	public GameObject enemy_Beetle_Group;
	public GameObject enemy_Beetle_Group_Three;
	public GameObject enemy_Beetle_Group_Five;
	public GameObject enemy_Beetle_Group_Seven;
	#endregion
	// バキュラオブジェクト
	#region　Bacula
	public GameObject enemy_Bacula_Sixteen;
    public GameObject enemy_Bacula_FourOnly;
    public GameObject enemy_Bacula_Group_Two;
    public GameObject enemy_Bacula_Group_Six;
	#endregion
	// 隕石オブジェクト
	#region Meteor
	public GameObject Enemy_BoundMeteors;
	//public GameObject enemy_Meteor;
	//public GameObject enemy_Meteor_Top;
	//public GameObject enemy_Meteor_Under;
	//public GameObject enemy_Meteors;
	//public GameObject enemy_Meteor_Mini;
	//public GameObject enemy_MeteorWaveGroup;
	#endregion
	// ヒトデオブジェクト
	#region StarFish
	public GameObject enemy_Star_Fish_Spowner;
	#endregion
	// 中ボスオブジェクト
	#region Middle
	public GameObject enemy_MoaiBossGroup;
	private GameObject middleBossOBj;
	private Enemy_MiddleBoss middleBoss_Script;
	//public GameObject enemy_SlowFollow;
	#endregion
	// 保管オブジェクト情報
	#region Temp
	public GameObject saveEnemyObj;
	#endregion

	//━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
	//
	// カウント変数
	//
	//━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
	public int PreviousCount = 0;
    public int frameCnt = 0;    //フレームカウント：これの値で生成のタイミングをはかる
    public int groupCnt = 1;    //画面に出す群れのカウント
    public int nowGroupCnt;
    public int[] groupCntArray;

    public int turning_frame = 180;
    public string nextEnemy;

    public EnemyGroupManage group_Script;

    

    GameObject oneBossOBj;
    One_Boss oneBoss_Script;
    GameObject mistEffectObj;
    ParticleSystem mistParticle;
    public BackgroundActivation backActive_Script;

    GameObject twoBossObj;
    Two_Boss twoBoss_Script;
	//public GameObject enemy_MiddleBoss_Father;
	public GameObject enemy_BattleShip;
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
    public bool isLastBossWireless = false;
	// ビッグコアの出現グループ番号と経過フレーム
    private int bigCoreGroupNum = 17;
    private int bigCoreGroupFrame = 2580;
	// ビッグコア後の敵グループの出現グループ番号と経過フレーム
	private int bigCoreNextGroupNum = 26;
    private int bigCoreNextGroupFrame = 3750;
	// ビッグコア2とビッグコア2後の敵グループの出現グループ番号と経過フレーム
	private int bigCoreMK2GrouNum = 43;
    private int bigCoreMK2GroupFrame = 6345;
    private int bigCoreMK2NextGroupFrame = 6465;
	// モアイとモアイ後の敵グループの出現グループ番号と経過フレーム
	private int moaiGroupNum = 49;
    private int moaiGroupFrame = 8205;
    private int moaiGroupNextGroupFrame = 8325;
	// ビッグコア3の出現グループ番号と経過フレーム
    private int bigCoreMK3GroupNum = 97;
    private int bigCoreMK3GroupFrame = 12185;

	// Debug
	// groupFrameCheckDebugFlagをオンにしている時
	// 中ボス・大ボスの出現を無効にし、出現する瞬間の情報をDebugで表示
	private bool groupFrameCheckDebugFlag = false;
	// 敵グループの種類名・グループ番号・経過フレーム
	void GroupFrameCheckDebug(string groupName, int groupNum, int groupFrame)
    {
        if(groupFrameCheckDebugFlag) Debug.Log("■■■" + groupName + "は番号：" + groupNum + " 総フレーム数：" + groupFrame);
    }

	// 最初のフレーム
    void Start()
    {
		EnemyDebugNumberUpload();

		CreatePosUpload();

		ResourcesUpload();
	}

	private void EnemyDebugNumberUpload()
	{
		int allFrame = 0;

		for (int num = 0; num < enemyGroups.Length; num++)
		{
			switch(enemyGroups[num].enemyType)
			{
				case EnemyType.BIGCORE:
					bigCoreGroupNum = num;
					bigCoreGroupFrame = allFrame;
					break;

				case EnemyType.BIGCOREENDGROUP:
					bigCoreNextGroupNum = num;
					bigCoreNextGroupFrame = allFrame;
					break;

				case EnemyType.BIGCOREMK2:
					bigCoreMK2GrouNum = num + 1;
					bigCoreMK2GroupFrame = allFrame;
					bigCoreMK2NextGroupFrame = allFrame + enemyGroups[num].nextGroupFrame;
					break;

				case EnemyType.MOAI:
					moaiGroupNum = num;
					moaiGroupFrame = allFrame;
					moaiGroupNextGroupFrame = allFrame + enemyGroups[num].nextGroupFrame;
					break;

				case EnemyType.BIGCOREMK3:
					bigCoreMK3GroupNum = num;
					bigCoreNextGroupFrame = allFrame;
					break;

				case EnemyType.GAMECLEAR:
					return;
			}

			allFrame += enemyGroups[num].nextGroupFrame;
		}
	}

	private void EnemyDebugNumberUpdate(EnemyType e, bool isNextGroup)
	{
		int allFrame = 0;

		for (int num = 0; num < enemyGroups.Length; num++)
		{
			if (e == enemyGroups[num].enemyType)
			{
				if(!isNextGroup)
				{
					groupCnt = num;
					turning_frame = allFrame;
				}
				else
				{
					groupCnt = num + 1;
					turning_frame = allFrame + enemyGroups[num].nextGroupFrame;
				}
				frameCnt = turning_frame - 60;
				return;
			}
			allFrame += enemyGroups[num].nextGroupFrame;
		}
	}

	private void CreatePosUpload()
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
	}

	private void ResourcesUpload()
	{
		#region リソース取得
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
		enemy_ClamChowder_Group_ThreeStraight = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeStraight") as GameObject;
		enemy_ClamChowder_Group_SevenWave = Resources.Load("Enemy/Enemy_ClamChowder_Group_Seven") as GameObject;
        enemy_ClamChowder_Group_Straight = Resources.Load("Enemy/Enemy_ClamChowder_Group_Straight") as GameObject;
        enemy_ClamChowder_Group_StraightBehind = Resources.Load("Enemy/Enemy_ClamChowder_Group_StraightBehind") as GameObject;
        enemy_ClamChowder_Group_FourTriangle = Resources.Load("Enemy/Enemy_ClamChowder_Group_FourTriangle") as GameObject;
        enemy_ClamChowder_Group_FourTriangle_B = Resources.Load("Enemy/Enemy_ClamChowder_Group_FourTriangle_B") as GameObject;
        enemy_ClamChowder_Group_FourTriangle_C = Resources.Load("Enemy/Enemy_ClamChowder_Group_FourTriangle_C") as GameObject;
        enemy_ClamChowder_Group_FourTriangle_NoItem = Resources.Load("Enemy/Enemy_ClamChowder_Group_FourTriangle_NoItem") as GameObject;
        enemy_ClamChowder_Group_TwelveStraight = Resources.Load("Enemy/Enemy_ClamChowder_Group_TwelveStraight") as GameObject;
        enemy_ClamChowder_Group_SevenStraight = Resources.Load("Enemy/Enemy_ClamChowder_Group_SevenStraight") as GameObject;
        enemy_ClamChowder_Group_SixStraight = Resources.Load("Enemy/Enemy_ClamChowder_Group_SixStraight") as GameObject;
        enemy_ClamChowder_Group_UpSevenDiagonal = Resources.Load("Enemy/Enemy_ClamChowder_Group_UpSevenDiagonal") as GameObject;
        enemy_ClamChowder_Group_DownSevenDiagonal = Resources.Load("Enemy/Enemy_ClamChowder_Group_DownSevenDiagonal") as GameObject;
        enemy_ClamChowder_Group_TenStraight = Resources.Load("Enemy/Enemy_ClamChowder_Group_TenStraight") as GameObject;
        enemy_ClamChowder_Group_FourVerticalStraight = Resources.Load("Enemy/Enemy_ClamChowder_Group_FourVerticalStraight") as GameObject;
        enemy_ClamChowder_Group_FourVerticalAttack = Resources.Load("Enemy/Enemy_ClamChowder_Group_FourVerticalAttack") as GameObject;
        enemy_ClamChowder_Group_Seven = Resources.Load("Enemy/Enemy_ClamChowder_Group_Seven") as GameObject;
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
        enemy_Beelzebub_Group_TwoWide = Resources.Load("Enemy/Enemy_Beelzebub_Group_TwoWide_Item") as GameObject;
        enemy_Bacula_Sixteen = Resources.Load("Enemy/Enemy_Bacula_Sixteen") as GameObject;
        enemy_Bacula_FourOnly = Resources.Load("Enemy/Enemy_Bacula_FourOnly") as GameObject;
        enemy_Bacula_Group_Two = Resources.Load("Enemy/Enemy_Bacula_Group_Two") as GameObject;
        enemy_Bacula_Group_Six = Resources.Load("Enemy/Enemy_Bacula_Group_Six") as GameObject;
        //enemy_Meteor = Resources.Load("Enemy/Enemy_Meteor") as GameObject;
        //enemy_Meteor_Mini = Resources.Load("Enemy/Enemy_Meteor_Mini") as GameObject;
        //enemy_Meteor_Top = Resources.Load("Enemy/Enemy_Meteor_Top") as GameObject;
        //enemy_Meteor_Under = Resources.Load("Enemy/Enemy_Meteor_Under") as GameObject;
        //enemy_Meteors = Resources.Load("Enemy/Meteors") as GameObject;
        //enemy_MeteorWaveGroup = Resources.Load("Enemy/Enemy_MeteorWaveGroup") as GameObject;
        //enemy_SlowFollow = Resources.Load("Enemy/Enemy_SlowFollow") as GameObject;
        Enemy_BoundMeteors = Resources.Load("Enemy/BoundMeteors") as GameObject;
        enemy_Star_Fish_Spowner = Resources.Load("Enemy/Enemy_Star_Fish_Spowner") as GameObject;
        enemy_Beetle_Group = Resources.Load("Enemy/Enemy_Beetle_Group") as GameObject;
        enemy_Beetle_Group_Three = Resources.Load("Enemy/Enemy_Beetle_Group_Three") as GameObject;
        enemy_Beetle_Group_Five = Resources.Load("Enemy/Enemy_Beetle_Group") as GameObject;
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
		#endregion
	}

	// 毎フレーム更新
	void Update()
    {
        if (Game_Master.Management_In_Stage == Game_Master.CONFIGURATION_IN_STAGE.WIRELESS)
        {
            return;
        }

        if (!isNowOneBoss && !isNowTwoBoss && !isNowMoai)
        {
            frameCnt++;
        }

		DebugKeyUpdate();

		//中ボス撃破
		if (middleBoss_Script != null && isMiddleBossSkip)
        {
            if (middleBoss_Script.Is_Dead)
            {
                turning_frame = bigCoreNextGroupFrame; //←今爆発がでかいのでちょっと間を空けます
                frameCnt = bigCoreNextGroupFrame - 60;
                groupCnt = bigCoreNextGroupNum;
                isMiddleBossSkip = false;
            }
        }

        //第一ボス出現時に無線をONにする🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲
        if (groupCnt == bigCoreMK2GrouNum && frameCnt == turning_frame - 60f)
        {
            Wireless_sinario.Is_using_wireless = true;
        }

        //第二ボス出現時に無線をONにする🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲
        if (groupCnt == bigCoreMK3GroupNum && frameCnt == turning_frame - 60f)
        {
            isLastBossWireless = true;
            Wireless_sinario.Is_using_wireless = true;
        }

        //第一ボスを撃破したら間隔を詰める🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲
        if (oneBoss_Script != null)
        {
            if (oneBoss_Script.Is_Dead)
            {
                if (isOneBossAlive)
                {
                    if (backActive_Script)
                    {
                        backActive_Script.TransparencyChangeTrigger();
                        Wireless_sinario.Is_using_wireless = true;
                    }
                    isNowOneBoss = false;
                    isOneBossAlive = false;
                }
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
		
        CreateEnemyGroup_01();
    }

	// 指定の敵グループを出す
	private void DebugKeyUpdate()
	{
		// 次の敵グループ
		if (Input.GetKeyDown(KeyCode.N)) { if (groupCnt < enemyGroups.Length - 1) frameCnt = turning_frame; }
		// ビッグコア
		if (Input.GetKeyDown(KeyCode.J)) { EnemyDebugNumberUpdate(EnemyType.BIGCORE, false); }
		// ビッグコア後
		if (Input.GetKeyDown(KeyCode.K)) { EnemyDebugNumberUpdate(EnemyType.BIGCOREENDGROUP, false); }
		// ビッグコアMK2
		if (Input.GetKeyDown(KeyCode.M)) { EnemyDebugNumberUpdate(EnemyType.BIGCOREMK2, false); }
		// ビッグコアMK2後
		if (Input.GetKeyDown(KeyCode.B)) { EnemyDebugNumberUpdate(EnemyType.BIGCOREMK2, true); }
		// モアイ
		if (Input.GetKeyDown(KeyCode.B) & Input.GetKey(KeyCode.H)) { EnemyDebugNumberUpdate(EnemyType.MOAI, false); }
		// モアイ後
		if (Input.GetKeyDown(KeyCode.B) & Input.GetKey(KeyCode.U)) { EnemyDebugNumberUpdate(EnemyType.MOAI, true); }
		// ビッグコアMK3
		if (Input.GetKeyDown(KeyCode.L)) { EnemyDebugNumberUpdate(EnemyType.BIGCOREMK3, false); }
	}

	//--------------------------------------------------------------------
	// 敵グループの種類の情報
	public enum EnemyType
	{
		NONE,
		UFO_GROUP,
		UFO_GROUP_NONESHOT,
		UFO_GROUP_FIVE,
		CLAMCHOWDER_GROUP_STRAIGHT,
		CLAMCHOWDER_GROUP_FOUR,
		CLAMCHOWDER_GROUP_FOURTRIANGLE,
		CLAMCHOWDER_GROUP_FOURTRIANGLE_B,
		CLAMCHOWDER_GROUP_FOURTRIANGLE_C,
		CLAMCHOWDER_GROUP_THREE,
		CLAMCHOWDER_GROUP_THREESTRAIGHT,
		CLAMCHOWDER_GROUP_TOPANDUNEDR,
		CLAMCHOWDER_GROUP_FIVE,
		CLAMCHOWDER_GROUP_TWOWAVEONLYDOWN,
		CLAMCHOWDER_GROUP_TWOWAVEONLYUP,
		CLAMCHOWDER_GROUP_TENSTRAIGHT,
		CLAMCHOWDER_GROUP_SEVEN,
		CLAMCHOWDER_GROUP_UPSEVENDIAGONAL,
		CLAMCHOWDER_GROUP_DOWNSEVENDIAGONAL,
		BEETLE_GROUP_THREE,
		BEETLE_GROUP_SEVEN,
		BEETLE_GROUP_FIVE,
		BIGCORE,
		BIGCOREMK2,
		BIGCOREMK3,
		BIGCOREENDGROUP,
		BEELZEBUB_GROUP_FOUR,
		BEELZEBUB_GROUP_FOURWIDE,
		BATTLESHIP,
		BATTLESHIP_TOPANDUNDER,
		BATTLESHIP_TOP,
		BATTLESHIP_UNDER,
		BACULA_GROUP_TWO,
		BACULA_GROUP_SIX,
		CLAMCHOWDER_GROUP_FOURVERTICALATTACK,
		CLAMCHOWDER_GROUP_FOURSTRAIGHT,
		BEELZEBUB_GROUP_TWOWIDE,
		BEELZEBUB_GROUP_EIGHTNORMAL,
		STARFISH,
		BOUNDMETEORS,
		MOAI,
		GAMECLEAR,
	}

	// 敵グループを出現させる位置情報
	public enum CreatePos
	{
		L4, L3, L2, L1, L0, Lm1, Lm2, Lm3, Lm4,
		FOURGROUPL,
		R4, R3, R2, R1, R0, Rm1, Rm2, Rm3, Rm4,
		R0PX2Y081,
		R0PX2MY081,
	}

	// 敵グループの情報
	public struct EnemyGroup
	{
		public string enemyGroupName;
		public EnemyType enemyType;
		public CreatePos createPos;
		public bool isItem;
		public int nextGroupFrame;

		public EnemyGroup(string enemyGroupName, EnemyType enemyType, CreatePos createPos, bool isItem, int nextGroupFrame) : this()
		{
			this.enemyGroupName = enemyGroupName;
			this.enemyType = enemyType;
			this.createPos = createPos;
			this.isItem = isItem;
			this.nextGroupFrame = nextGroupFrame;
		}
	}

	// 敵グループの種類名を受け取り生成
	private GameObject CreateEnemy(EnemyType e, CreatePos p, bool isItem = false)
    {
        Vector3 pos = Vector3.zero;

        switch (p)
        {
            case CreatePos.FOURGROUPL:pos = createPos_FourGroupL.transform.position; break;
			case CreatePos.R4: pos = createPosR4.transform.position; break;
			case CreatePos.R3: pos = createPosR3.transform.position; break;
			case CreatePos.R2: pos = createPosR2.transform.position; break;
			case CreatePos.R1: pos = createPosR1.transform.position; break;
			case CreatePos.R0: pos = createPosR0.transform.position; break;
			case CreatePos.Rm1: pos = createPosRm1.transform.position; break;
			case CreatePos.Rm2: pos = createPosRm2.transform.position; break;
			case CreatePos.Rm3: pos = createPosRm3.transform.position; break;
			case CreatePos.Rm4: pos = createPosRm4.transform.position; break;
			case CreatePos.R0PX2Y081: pos = createPosR0.transform.position + new Vector3(2.0f, 0.81f * 1.0f, 0.0f); break;
			case CreatePos.R0PX2MY081: pos = createPosR0.transform.position + new Vector3(2.0f, 0.81f * -1.0f, 0.0f); break;
			default: pos = Vector3.zero; break;
        }

        switch (e)
        {
			case EnemyType.BIGCOREENDGROUP:
				if(groupFrameCheckDebugFlag) GroupFrameCheckDebug("中ボス後", groupCnt, turning_frame);
				break;

			case EnemyType.GAMECLEAR:
				Scene_Manager.Manager.Screen_Transition_To_Clear();
				break;

			case EnemyType.CLAMCHOWDER_GROUP_UPSEVENDIAGONAL:
				Instantiate(enemy_ClamChowder_Group_UpSevenDiagonal, createPosR0.transform.position, transform.rotation);
				break;

			case EnemyType.CLAMCHOWDER_GROUP_DOWNSEVENDIAGONAL:
				Instantiate(enemy_ClamChowder_Group_DownSevenDiagonal, createPosR0.transform.position, transform.rotation);
				break;

			case EnemyType.MOAI:
				if (groupFrameCheckDebugFlag)
				{
					GroupFrameCheckDebug("モアイ", groupCnt, turning_frame);
					GroupFrameCheckDebug("モアイ後", groupCnt + 1, turning_frame + enemyGroups[groupCnt].nextGroupFrame);
				}
				else
				{
					GameObject moai = Obj_Storage.Storage_Data.Moai.Active_Obj();
					moai.transform.position = new Vector3(15.44f, -17.0f, 0.0f);
					Wireless_sinario.Is_using_wireless = true;
					isNowMoai = true;
				}
				break;

			case EnemyType.BOUNDMETEORS:
				saveEnemyObj = Obj_Storage.Storage_Data.boundMeteors.Active_Obj();
				saveEnemyObj.transform.position = createMeteorPosR0.transform.position;
				//Instantiate(Enemy_BoundMeteors, createMeteorPosR0.transform.position, transform.rotation);
				break;

			case EnemyType.STARFISH:
				Instantiate(enemy_Star_Fish_Spowner, pos, transform.rotation);
				break;

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

            case EnemyType.UFO_GROUP:
                //Object_Pooling pEnemy_UFO_Group = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group_NoneShot") as GameObject, 1, "enemy_UFO_Group");
                //GameObject enemy_UFO_Group = pEnemy_UFO_Group.Active_Obj();
                GameObject enemy_UFO_Group2 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
                enemy_UFO_Group2.transform.position = pos + new Vector3(8.5f, 0, 0);
                enemy_UFO_Group2.transform.rotation = transform.rotation;
                //Object_Pooling pEnemy_UFO_Group = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group_NoneShot") as GameObject, 1, "enemy_UFO_Group");
                //GameObject enemy_UFO_Group = Obj_Storage.Storage_Data.enemy_UFO_Group_NoneShot.Active_Obj();
                //enemy_UFO_Group.transform.position = createPosR3.transform.position + new Vector3(8.5f, 0, 0);
                //enemy_UFO_Group.transform.rotation = transform.rotation;
                break;

            case EnemyType.CLAMCHOWDER_GROUP_STRAIGHT:
                saveEnemyObj = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Straight.Active_Obj();
                saveEnemyObj.transform.position = pos;
                //Instantiate(enemy_Clamchowder_Group_Straight, pos, transform.rotation);
                break;

            case EnemyType.CLAMCHOWDER_GROUP_FOUR:
                if (!isItem)
                {
                    saveEnemyObj = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Four.Active_Obj();
                    saveEnemyObj.transform.position = createPos_FourGroupL.transform.position;
                    //Instantiate(enemy_ClamChowder_Group_Four_NoItem, pos, transform.rotation);
                }
                else
                {
                    saveEnemyObj = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Four_NoItem.Active_Obj();
                    saveEnemyObj.transform.position = createPos_FourGroupL.transform.position;
                    //GameObject saveObj = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Four_NoItem.Active_Obj();
                    //GameObject saveObjB = Instantiate(enemy_ClamChowder_Group_Four, createPos_FourGroupL.transform.position, transform.rotation);
                    //saveObjB.transform.position = createPos_FourGroupL.transform.position;
                }
                break;

            case EnemyType.CLAMCHOWDER_GROUP_FIVE:
                saveEnemyObj = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Five_NoItem.Active_Obj();
                saveEnemyObj.transform.position= createPos_FourGroupL.transform.position;
                //Instantiate(enemy_ClamChowder_Group_Five_NoItem, createPos_FourGroupL.transform.position, transform.rotation);
                break;

            case EnemyType.CLAMCHOWDER_GROUP_FOURTRIANGLE:
                if (!isItem)
                {
                    saveEnemyObj = Obj_Storage.Storage_Data.enemy_ClamChowder_FourTriangle_NoItem.Active_Obj();
                    saveEnemyObj.transform.position = createPos_FourGroupL.transform.position;
                    //Instantiate(enemy_ClamChowder_Group_FourTriangle_NoItem, createPos_FourGroupL.transform.position, transform.rotation);
                }
                else
                {
                    saveEnemyObj = Obj_Storage.Storage_Data.enemy_ClamChowder_FourTriangle.Active_Obj();
                    saveEnemyObj.transform.position = createPos_FourGroupL.transform.position;
                    //Instantiate(enemy_ClamChowder_Group_FourTriangle, createPos_FourGroupL.transform.position, transform.rotation);
                }
                break;

            case EnemyType.CLAMCHOWDER_GROUP_FOURTRIANGLE_B:
                Instantiate(enemy_ClamChowder_Group_FourTriangle_B, createPos_FourGroupL.transform.position, transform.rotation);
                break;

            case EnemyType.CLAMCHOWDER_GROUP_FOURTRIANGLE_C:
                Instantiate(enemy_ClamChowder_Group_FourTriangle_C, createPos_FourGroupL.transform.position, transform.rotation);
                break;

            case EnemyType.CLAMCHOWDER_GROUP_THREE:
                if (!isItem)
                {
                    saveEnemyObj = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Three.Active_Obj();
                    saveEnemyObj.transform.position = createPos_FourGroupL.transform.position+pos;
                    //Instantiate(enemy_ClamChowder_Group_FourTriangle_NoItem, pos, transform.rotation);
                }
                else
                {
                    saveEnemyObj = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Three_Item.Active_Obj();
                    saveEnemyObj.transform.position = createPos_FourGroupL.transform.position + pos;
                    //Instantiate(enemy_ClamChowder_Group_Three_Item, createPos_FourGroupL.transform.position, transform.rotation);
                }
                break;

			case EnemyType.CLAMCHOWDER_GROUP_THREESTRAIGHT:
                
                Instantiate(enemy_ClamChowder_Group_ThreeStraight, pos, transform.rotation);
                break;

            case EnemyType.CLAMCHOWDER_GROUP_FOURSTRAIGHT:
                Instantiate(enemy_ClamChowder_Group_FourVerticalStraight, pos, transform.rotation);
                break;

            case EnemyType.CLAMCHOWDER_GROUP_TOPANDUNEDR:
                saveEnemyObj = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Two_Top.Active_Obj();
                saveEnemyObj.transform.position = createPos_FourGroupL.transform.position;

                saveEnemyObj = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Two_Under.Active_Obj();
                saveEnemyObj.transform.position = createPos_FourGroupL.transform.position;
                //Instantiate(enemy_ClamChowder_Group_Two_Top, createPos_FourGroupL.transform.position, transform.rotation);
                //Instantiate(enemy_ClamChowder_Group_Two_Under, createPos_FourGroupL.transform.position, transform.rotation);
                break;

            case EnemyType.CLAMCHOWDER_GROUP_TWOWAVEONLYUP:
                saveEnemyObj = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_TwoWaveOnlyUp.Active_Obj();
                saveEnemyObj.transform.position = pos;
				//Instantiate(enemy_ClamChowder_Group_TwoWaveOnlyUp, pos, transform.rotation);
				break;

            case EnemyType.CLAMCHOWDER_GROUP_TWOWAVEONLYDOWN:
                saveEnemyObj = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_TwoWaveOnlyDown.Active_Obj();
                saveEnemyObj.transform.position = pos;
				//Instantiate(enemy_ClamChowder_Group_TwoWaveOnlyDown, pos, transform.rotation);
				break;

			case EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT:
				//saveEnemyObj = Instantiate(enemy_ClamChowder_Group_TenStraight, pos, transform.rotation);
				saveEnemyObj = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_TenStraight.Active_Obj();
				saveEnemyObj.transform.position = pos;
				group_Script = saveEnemyObj.GetComponent<EnemyGroupManage>();
				group_Script.isItemDrop = isItem;
				break;

            case EnemyType.CLAMCHOWDER_GROUP_FOURVERTICALATTACK:
                Instantiate(enemy_ClamChowder_Group_FourVerticalAttack, createPos_FourGroupL.transform.position, transform.rotation);
                break;

            case EnemyType.CLAMCHOWDER_GROUP_SEVEN:
                saveEnemyObj = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Seven.Active_Obj();
                saveEnemyObj.transform.position = pos;
                //Instantiate(enemy_ClamChowder_Group_Seven, pos, transform.rotation);
                return saveEnemyObj;

            case EnemyType.BEETLE_GROUP_THREE:
                saveEnemyObj = Obj_Storage.Storage_Data.enemy_BeetleGroup_Three.Active_Obj();
                saveEnemyObj.transform.position = new Vector3(15, -8, 0);
                //GameObject beetleGroup_Three = Instantiate(enemy_Beetle_Group_Three, createPosRm3.transform.position, transform.rotation);
                //beetleGroup_Three.transform.position = new Vector3(15, -8, 0);
                break;

            case EnemyType.BEETLE_GROUP_FIVE:
                GameObject beetleGroup_Five = Instantiate(enemy_Beetle_Group_Five, createPosRm3.transform.position, transform.rotation);
                beetleGroup_Five.transform.position = new Vector3(15, -8, 0);
                break;

            case EnemyType.BEETLE_GROUP_SEVEN:
                saveEnemyObj = Obj_Storage.Storage_Data.enemy_Beetle_Group_Seven.Active_Obj();
                saveEnemyObj.transform.position = new Vector3(13, -8, 0);
                //GameObject beetleGroup_Seven = Instantiate(enemy_Beetle_Group_Seven, createPosRm3.transform.position, transform.rotation);
                //beetleGroup_Seven.transform.position = new Vector3(15, -8, 0);
                break;

            case EnemyType.BEELZEBUB_GROUP_FOURWIDE:
				if (!isItem)
				{
                    //saveEnemyObj = Obj_Storage.Storage_Data.enemy_Beelzebub_Group_FourWide.Active_Obj();
                    //saveEnemyObj.transform.position = pos;
                    Instantiate(enemy_Beelzebub_Group_FourWide, pos, transform.rotation);
                }
				else
				{
                    //saveEnemyObj = Obj_Storage.Storage_Data.enemy_Beelzebub_Group_FourWide_Item.Active_Obj();
                    //saveEnemyObj.transform.position = pos;
                    Instantiate(enemy_Beelzebub_Group_FourWide_Item, pos, transform.rotation);
                }
                break;

            case EnemyType.BEELZEBUB_GROUP_TWOWIDE:
                Instantiate(enemy_Beelzebub_Group_TwoWide, pos, transform.rotation);
                break;


            case EnemyType.BACULA_GROUP_TWO:
                Instantiate(enemy_Bacula_Group_Two, createBaculaGroupPos.transform.position, transform.rotation);
                break;

            case EnemyType.BACULA_GROUP_SIX:
                Instantiate(enemy_Bacula_Group_Six, createBaculaGroupPos.transform.position, transform.rotation);
                break;

            case EnemyType.BATTLESHIP:
                GameObject Battle_Ship1 = Instantiate(enemy_BattleShip, createPosR0.transform.position, enemy_BattleShip.transform.rotation);
                Battle_Ship1.transform.position = createPosR0.transform.position;
                BattleshipType_Enemy b1 = Battle_Ship1.GetComponent<BattleshipType_Enemy>();
                b1.is_sandwich = false;
                b1.Is_up = false;
                //            saveEnemyObj = Obj_Storage.Storage_Data.BattleShipType_Enemy.Active_Obj();
                //            saveEnemyObj.transform.position = pos;
                ////GameObject Battle_Ship1 = Instantiate(enemy_BattleShip, pos, enemy_BattleShip.transform.rotation);
                //BattleshipType_Enemy b1 = saveEnemyObj.GetComponent<BattleshipType_Enemy>();
                //b1.is_sandwich = false;
                //b1.Is_up = false;
                break;

			case EnemyType.BATTLESHIP_TOP:
                GameObject Battle_Ship6 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
                BattleshipType_Enemy b6 = Battle_Ship6.GetComponent<BattleshipType_Enemy>();
                b6.Is_up = false;
                //            saveEnemyObj = Obj_Storage.Storage_Data.BattleShipType_Enemy.Active_Obj();
                //            saveEnemyObj.transform.position = createBattleShipPos.transform.position;
                //            //GameObject Battle_Ship4 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
                //BattleshipType_Enemy b4 = saveEnemyObj.GetComponent<BattleshipType_Enemy>();
                //            b4.is_sandwich = true;
                //            b4.Is_up = false;
                break;

            case EnemyType.BATTLESHIP_UNDER:
                GameObject Battle_Ship2 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
                BattleshipType_Enemy b2 = Battle_Ship2.GetComponent<BattleshipType_Enemy>();
                b2.Is_up = true;
                //saveEnemyObj = Obj_Storage.Storage_Data.BattleShipType_Enemy.Active_Obj();
                //saveEnemyObj.transform.position = createBattleShipPos.transform.position;
                ////GameObject Battle_Ship5 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
                //BattleshipType_Enemy b5 = saveEnemyObj.GetComponent<BattleshipType_Enemy>();
                //b5.is_sandwich = true;
                //b5.Is_up = true;
                break;

            case EnemyType.BATTLESHIP_TOPANDUNDER:
                CreateEnemy(EnemyType.BATTLESHIP_TOP, CreatePos.L0, false);
                CreateEnemy(EnemyType.BATTLESHIP_UNDER, CreatePos.L0, false);
                //saveEnemyObj = Obj_Storage.Storage_Data.BattleShipType_Enemy.Active_Obj();
                //BattleshipType_Enemy b3 = saveEnemyObj.GetComponent<BattleshipType_Enemy>();
                //b3.is_sandwich = true;
                //b3.Is_up = false;
                //saveEnemyObj.transform.position = new Vector3(25, -5, 0);

                //saveEnemyObj.transform.position = createBattleShipPos.transform.position;
                //saveEnemyObj.transform.position = b3.defautpos;

                //GameObject Battle_Ship3 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);

                //saveEnemyObj = Obj_Storage.Storage_Data.BattleShipType_Enemy.Active_Obj();
                //BattleshipType_Enemy b2 = saveEnemyObj.GetComponent<BattleshipType_Enemy>();
                //b2.is_sandwich = true;
                //saveEnemyObj.transform.position = b2.defautpos;
                //GameObject Battle_Ship2 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
                //b2.Is_up = true;
                //            saveEnemyObj.transform.position = new Vector3(25, 5, 0);
                break;


			case EnemyType.BIGCORE:
				if (groupFrameCheckDebugFlag)
				{
					GroupFrameCheckDebug("中ボス", groupCnt, turning_frame);
				}
				else
				{
					GameObject Boss_Middle = Obj_Storage.Storage_Data.Boss_Middle.Active_Obj();
					saveEnemyObj = Boss_Middle;
					Boss_Middle.transform.position = createMiddleBossPos.transform.position;
					Boss_Middle.transform.rotation = transform.rotation;
				}
				break;

            case EnemyType.BIGCOREMK2:
				if (groupFrameCheckDebugFlag)
				{
					GroupFrameCheckDebug("ボス1", groupCnt, turning_frame);
					GroupFrameCheckDebug("ボス1後", groupCnt + 1, turning_frame + enemyGroups[groupCnt].nextGroupFrame);
				}
				else
				{
					GameObject Boss_01 = Obj_Storage.Storage_Data.Boss_1.Active_Obj();
					Boss_01.transform.position = new Vector3(10.0f, 0.0f, 0.0f);
					GameObject mistSaveObj = Instantiate(mistEffectObj, transform.position, transform.rotation);
					mistSaveObj.transform.position = new Vector3(0, 0, 3);
					mistParticle = mistSaveObj.GetComponent<ParticleSystem>();
					backActive_Script = mistSaveObj.GetComponent<BackgroundActivation>();
					mistParticle.Play();
					backActive_Script.TransparencyChangeTrigger();
					isNowOneBoss = true;
				}
                break;

			case EnemyType.BIGCOREMK3:
				if (groupFrameCheckDebugFlag)
				{
					GroupFrameCheckDebug("ボス2", groupCnt, turning_frame);
				}
				else
				{
					GameObject Boss_02 = Obj_Storage.Storage_Data.Boss_2.Active_Obj();
					Boss_02.transform.position = new Vector3(13.0f, 0.0f, 0.0f);
					isNowTwoBoss = true;

					//GameObject mistSaveObj = Instantiate(mistEffectObj, transform.position, transform.rotation);
					//backActive_Script.TransparencyChangeTrigger();
					if (isDebug)
					{
						GameObject mistSaveObj = Instantiate(mistEffectObj, transform.position, transform.rotation);
						backActive_Script = mistSaveObj.GetComponent<BackgroundActivation>();

						mistEffectObj.transform.position = new Vector3(0, 0, 3);
						mistParticle = mistSaveObj.GetComponent<ParticleSystem>();
						mistParticle.Play();
					}
					backActive_Script.TransparencyChangeTrigger();
				}
				break;


			case EnemyType.BEELZEBUB_GROUP_FOUR:
                //Instantiate(enemy_Beelzebub_Group_EightNormal_Item, pos, transform.rotation);
                break;

            case EnemyType.BEELZEBUB_GROUP_EIGHTNORMAL:
                Instantiate(enemy_Beelzebub_Group_EightNormal_Item, pos, transform.rotation);
                break;

			default:
                break;
        }

        return null;
    }

	// 出現する敵グループ全体の情報
	public EnemyGroup[] enemyGroups = new EnemyGroup[150]
	{
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 120),
		new EnemyGroup("円盤上10", EnemyType.UFO_GROUP_NONESHOT, CreatePos.R3, true, 240),
		new EnemyGroup("円盤下10", EnemyType.UFO_GROUP_NONESHOT, CreatePos.Rm3, true, 300),
		new EnemyGroup("闘牛斜め配置中央アイテム", EnemyType.CLAMCHOWDER_GROUP_FOUR, CreatePos.L0, true, 390),
		new EnemyGroup("円盤上", EnemyType.UFO_GROUP_NONESHOT, CreatePos.R3, true, 0),
		new EnemyGroup("円盤下", EnemyType.UFO_GROUP_NONESHOT, CreatePos.Rm3, true, 240),
		new EnemyGroup("円盤上狭", EnemyType.UFO_GROUP_NONESHOT, CreatePos.R1, true, 0),
		new EnemyGroup("円盤下狭", EnemyType.UFO_GROUP_NONESHOT, CreatePos.Rm1, true, 240),
		new EnemyGroup("闘牛突進三角B", EnemyType.CLAMCHOWDER_GROUP_FOURTRIANGLE_B, CreatePos.L0, true, 240),
		new EnemyGroup("闘牛突進三角C", EnemyType.CLAMCHOWDER_GROUP_FOURTRIANGLE_C, CreatePos.L0, false, 240),
		new EnemyGroup("闘牛縦3アイテム", EnemyType.CLAMCHOWDER_GROUP_THREE, CreatePos.L0, true, 45),
		new EnemyGroup("闘牛上2下2", EnemyType.CLAMCHOWDER_GROUP_TOPANDUNEDR, CreatePos.FOURGROUPL, false, 45),
		new EnemyGroup("闘牛縦3アイテム", EnemyType.CLAMCHOWDER_GROUP_THREE, CreatePos.L0, true, 45),
		new EnemyGroup("闘牛上2下2", EnemyType.CLAMCHOWDER_GROUP_TOPANDUNEDR, CreatePos.FOURGROUPL, false, 45),
		new EnemyGroup("闘牛縦7", EnemyType.CLAMCHOWDER_GROUP_FIVE, CreatePos.FOURGROUPL, false, 45),
		new EnemyGroup("闘牛縦7", EnemyType.CLAMCHOWDER_GROUP_FIVE, CreatePos.FOURGROUPL, false, 45),
		new EnemyGroup("闘牛縦7", EnemyType.CLAMCHOWDER_GROUP_FIVE, CreatePos.FOURGROUPL, false, 360),
		new EnemyGroup("🔲🔲🔲ビッグコア🔲🔲🔲", EnemyType.BIGCORE, CreatePos.L0, false, 180),
		new EnemyGroup("闘牛上2", EnemyType.CLAMCHOWDER_GROUP_TWOWAVEONLYDOWN, CreatePos.R4, false, 0),
		new EnemyGroup("闘牛下2", EnemyType.CLAMCHOWDER_GROUP_TWOWAVEONLYUP, CreatePos.Rm4, false, 180),
		new EnemyGroup("闘牛上2", EnemyType.CLAMCHOWDER_GROUP_TWOWAVEONLYDOWN, CreatePos.R4, false, 0),
		new EnemyGroup("闘牛下2", EnemyType.CLAMCHOWDER_GROUP_TWOWAVEONLYUP, CreatePos.Rm4, false, 180),
		new EnemyGroup("闘牛上2", EnemyType.CLAMCHOWDER_GROUP_TWOWAVEONLYDOWN, CreatePos.R4, false, 0),
		new EnemyGroup("闘牛下2", EnemyType.CLAMCHOWDER_GROUP_TWOWAVEONLYUP, CreatePos.Rm4, false, 240),
		new EnemyGroup("闘牛上2", EnemyType.CLAMCHOWDER_GROUP_TWOWAVEONLYDOWN, CreatePos.R4, false, 0),
		new EnemyGroup("闘牛下2", EnemyType.CLAMCHOWDER_GROUP_TWOWAVEONLYUP, CreatePos.Rm4, false, 180),
		new EnemyGroup("闘牛上2", EnemyType.CLAMCHOWDER_GROUP_TWOWAVEONLYDOWN, CreatePos.R4, false, 0),
		new EnemyGroup("闘牛下2", EnemyType.CLAMCHOWDER_GROUP_TWOWAVEONLYUP, CreatePos.Rm4, false, 510),
		new EnemyGroup("ビッグコア後2", EnemyType.BIGCOREENDGROUP, CreatePos.L0, false, 0),
		new EnemyGroup("ハエ2", EnemyType.BEELZEBUB_GROUP_TWOWIDE, CreatePos.R0, true, 270),
		new EnemyGroup("ビートル3", EnemyType.BEETLE_GROUP_THREE, CreatePos.L0, false, 300),
		new EnemyGroup("円盤上射撃", EnemyType.UFO_GROUP, CreatePos.R3, true, 0),
		new EnemyGroup("円盤下射撃", EnemyType.UFO_GROUP, CreatePos.Rm3, true, 360),
		new EnemyGroup("戦艦上下(現在停止中)", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("戦艦", EnemyType.BATTLESHIP, CreatePos.R0, false, 0),
		new EnemyGroup("闘牛10直進上", EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, CreatePos.R3, true, 0),
		new EnemyGroup("闘牛10直進下", EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, CreatePos.Rm3, true, 180),
		new EnemyGroup("闘牛10直進上", EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, CreatePos.R3, true, 0),
		new EnemyGroup("闘牛10直進下", EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, CreatePos.Rm3, true, 90),
		new EnemyGroup("戦艦上下", EnemyType.BATTLESHIP_TOPANDUNDER, CreatePos.R0, false, 150),
		new EnemyGroup("闘牛縦3直進", EnemyType.CLAMCHOWDER_GROUP_THREESTRAIGHT, CreatePos.R0, false, 120),
		new EnemyGroup("闘牛縦3直進", EnemyType.CLAMCHOWDER_GROUP_THREESTRAIGHT, CreatePos.R0, false, 120),
		new EnemyGroup("闘牛縦3直進", EnemyType.CLAMCHOWDER_GROUP_THREESTRAIGHT, CreatePos.R0, false, 210),
		new EnemyGroup("闘牛縦7", EnemyType.CLAMCHOWDER_GROUP_SEVEN, CreatePos.R0, true, 45),
		new EnemyGroup("闘牛縦7", EnemyType.CLAMCHOWDER_GROUP_SEVEN, CreatePos.R0, true, 180),
		new EnemyGroup("🔲🔲🔲🔲🔲ビッグコアマーク2🔲🔲🔲🔲🔲", EnemyType.BIGCOREMK2, CreatePos.L0, false, 120),
		new EnemyGroup("ヒトデ12", EnemyType.STARFISH, CreatePos.L0, true, 600),
		new EnemyGroup("隕石20", EnemyType.BOUNDMETEORS, CreatePos.L0, false, 210),
		new EnemyGroup("バキュラ6", EnemyType.BACULA_GROUP_SIX, CreatePos.R0, false, 360),
		new EnemyGroup("隕石20", EnemyType.BOUNDMETEORS, CreatePos.L0, false, 300),
		new EnemyGroup("隕石20", EnemyType.BOUNDMETEORS, CreatePos.L0, false, 270),
		new EnemyGroup("🔲🔲🔲モアイ🔲🔲🔲", EnemyType.MOAI, CreatePos.L0, false, 120),
		new EnemyGroup("ヒトデ12", EnemyType.STARFISH, CreatePos.L0, true, 600),
		new EnemyGroup("円盤上10狭射撃", EnemyType.UFO_GROUP, CreatePos.R1, true, 0),
		new EnemyGroup("円盤下10射撃", EnemyType.UFO_GROUP, CreatePos.Rm3, true, 75),
		new EnemyGroup("円盤上10射撃", EnemyType.UFO_GROUP, CreatePos.R3, true, 0),
		new EnemyGroup("円盤下10狭射撃", EnemyType.UFO_GROUP, CreatePos.Rm1, true, 75),
		new EnemyGroup("円盤上10狭射撃", EnemyType.UFO_GROUP, CreatePos.R1, true, 0),
		new EnemyGroup("円盤下10射撃", EnemyType.UFO_GROUP, CreatePos.Rm3, true, 75),
		new EnemyGroup("円盤上10射撃", EnemyType.UFO_GROUP, CreatePos.R3, true, 0),
		new EnemyGroup("円盤下10狭射撃", EnemyType.UFO_GROUP, CreatePos.Rm1, true, 120),
		new EnemyGroup("戦艦", EnemyType.BATTLESHIP, CreatePos.R0, false, 210),
		new EnemyGroup("円盤上10射撃", EnemyType.UFO_GROUP, CreatePos.R4, true, 0),
		new EnemyGroup("円盤下10射撃", EnemyType.UFO_GROUP, CreatePos.Rm4, true, 120),
		new EnemyGroup("戦艦", EnemyType.BATTLESHIP, CreatePos.R0, false, 0),
		new EnemyGroup("円盤上10射撃", EnemyType.UFO_GROUP, CreatePos.R4, true, 0),
		new EnemyGroup("円盤下10射撃", EnemyType.UFO_GROUP, CreatePos.Rm4, true, 120),
		new EnemyGroup("円盤上10射撃", EnemyType.UFO_GROUP, CreatePos.R4, true, 0),
		new EnemyGroup("円盤下10射撃", EnemyType.UFO_GROUP, CreatePos.Rm4, true, 120),
		new EnemyGroup("円盤上10射撃", EnemyType.UFO_GROUP, CreatePos.R4, true, 0),
		new EnemyGroup("円盤下10射撃", EnemyType.UFO_GROUP, CreatePos.Rm4, true, 420),
		new EnemyGroup("バキュラ2", EnemyType.BACULA_GROUP_TWO, CreatePos.R0, false, 120),
		new EnemyGroup("ハエ8", EnemyType.BEELZEBUB_GROUP_EIGHTNORMAL, CreatePos.R0, false, 180),
		new EnemyGroup("バキュラ2", EnemyType.BACULA_GROUP_TWO, CreatePos.R0, false, 270),
		new EnemyGroup("闘牛左上斜め配置7射撃", EnemyType.CLAMCHOWDER_GROUP_UPSEVENDIAGONAL, CreatePos.L0, false, 40),
		new EnemyGroup("闘牛左下斜め配置7射撃", EnemyType.CLAMCHOWDER_GROUP_DOWNSEVENDIAGONAL, CreatePos.L0, false, 40),
		new EnemyGroup("闘牛左上斜め配置7射撃", EnemyType.CLAMCHOWDER_GROUP_UPSEVENDIAGONAL, CreatePos.L0, false, 40),
		new EnemyGroup("闘牛左下斜め配置7射撃", EnemyType.CLAMCHOWDER_GROUP_DOWNSEVENDIAGONAL, CreatePos.L0, false, 40),
		new EnemyGroup("闘牛左上斜め配置7射撃", EnemyType.CLAMCHOWDER_GROUP_UPSEVENDIAGONAL, CreatePos.L0, false, 40),
		new EnemyGroup("闘牛左下斜め配置7射撃", EnemyType.CLAMCHOWDER_GROUP_DOWNSEVENDIAGONAL, CreatePos.L0, false, 15),
		new EnemyGroup("戦艦上下", EnemyType.BATTLESHIP_TOPANDUNDER, CreatePos.L0, false, 270),
		new EnemyGroup("闘牛10", EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, CreatePos.R0, false, 0),
		new EnemyGroup("闘牛10", EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, CreatePos.R0PX2Y081, false, 0),
		new EnemyGroup("闘牛10", EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, CreatePos.R0PX2MY081, false, 115),
		new EnemyGroup("闘牛10", EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, CreatePos.R0, false, 0),
		new EnemyGroup("闘牛10", EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, CreatePos.R0PX2Y081, false, 0),
		new EnemyGroup("闘牛10", EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, CreatePos.R0PX2MY081, false, 115),
		new EnemyGroup("闘牛10", EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, CreatePos.R0, false, 0),
		new EnemyGroup("闘牛10", EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, CreatePos.R0PX2Y081, false, 0),
		new EnemyGroup("闘牛10", EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, CreatePos.R0PX2MY081, false, 115),
		new EnemyGroup("円盤上10狭射撃", EnemyType.UFO_GROUP, CreatePos.R1, true, 0),
		new EnemyGroup("円盤下10狭射撃", EnemyType.UFO_GROUP, CreatePos.Rm1, true, 90),
		new EnemyGroup("ビートル7", EnemyType.BEETLE_GROUP_SEVEN, CreatePos.R0, false, 15),
		new EnemyGroup("闘牛縦4突進", EnemyType.CLAMCHOWDER_GROUP_FOURVERTICALATTACK, CreatePos.R0, false, 45),
		new EnemyGroup("円盤上10射撃", EnemyType.UFO_GROUP, CreatePos.R4, true, 0),
		new EnemyGroup("円盤下10射撃", EnemyType.UFO_GROUP, CreatePos.Rm4, true, 60),
		new EnemyGroup("闘牛縦4突進", EnemyType.CLAMCHOWDER_GROUP_FOURVERTICALATTACK, CreatePos.R0, false, 60),
		new EnemyGroup("円盤上10射撃", EnemyType.UFO_GROUP, CreatePos.R3, true, 0),
		new EnemyGroup("円盤下10射撃", EnemyType.UFO_GROUP, CreatePos.Rm3, true, 360),
		new EnemyGroup("🔲🔲🔲🔲🔲ビッグコアマーク3🔲🔲🔲🔲🔲", EnemyType.BIGCOREMK3, CreatePos.L0, false, 120),
		new EnemyGroup("ゲームクリア", EnemyType.GAMECLEAR, CreatePos.L0, false, 10000),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
	};

	//敵を出す関数
	private void CreateEnemyGroup_01()
    {
		if (Is_A_Specified_Frame(turning_frame))
		{
			do
			{
				PreviousCount = frameCnt;
				CreateEnemy(enemyGroups[groupCnt].enemyType, enemyGroups[groupCnt].createPos, enemyGroups[groupCnt].isItem);
				Next_Condition(enemyGroups[groupCnt].nextGroupFrame);
				nextEnemy = enemyGroups[groupCnt].enemyGroupName;
			}
			// 次のフレーム経過が0以下の時繰り返し
			while (enemyGroups[groupCnt-1].nextGroupFrame <= 0);
		}
	}
	
    /// <summary>
    /// 出現フレームと経過フレームが一致またはそれ以上の時有効
    /// </summary>
    /// <param name="specified_frame"> 指定フレーム </param>
    /// <returns > あっているか </returns>
    private bool Is_A_Specified_Frame(int specified_frame)
    {
        return frameCnt >= specified_frame && specified_frame >= PreviousCount;
    }

	// 次の敵グループが出現するフレームに変更
    private void Next_Condition(int add_frame)
    {
        groupCnt++;
        //nowGroupCnt++;
        turning_frame += add_frame;
    }
}
