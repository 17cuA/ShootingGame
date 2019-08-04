//作成者：川村良太
//敵を出すスクリプト

//2019/08/03改修

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class EnemyCreate : MonoBehaviour
{
	//生成位置右側（Rなので右側、mRはマイナスなのでmがついてる、5が上下の最大）
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

	//生成位置左側
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

	//中ボス位置と戦艦位置
    public GameObject createMiddleBossPos;
    public GameObject createBattleShipPos;

    //public GameObject enemy_UFO_Group;
    //public GameObject enemy_ClamChowder_Group_Four;
	public GameObject enemy_ClamChowder_Group_FourBehind;
	//public GameObject enemy_ClamChowder_Group_Two_Top;
	//public GameObject enemy_ClamChowder_Group_Two_Under;
	//public GameObject enemy_ClamChowder_Group_Three_Item;
	//public GameObject enemy_ClamChowder_Group_Seven;
	public GameObject enemy_Clamchowder_Group_Straight;
	public GameObject enemy_Clamchowder_Group_StraightBehind;
    //public GameObject enemy_MiddleBoss_Father;
    //public GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp;
    //public GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown;
    //public GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item;
    //public GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item;
    public GameObject enemy_BattleShip;
	public GameObject enemy_Beelzebub_Group_Five;

	public GameObject saveEnemyObj;

	public int PreviousCount = 0;

	public int frameCnt = 0;	//フレームカウント：これの値で生成のタイミングをはかる
    public int groupCnt = 1;	//画面に出す群れのカウント
    float plusNum = 60;			//
    float plusNum2 = 60;		//この三つは強引に表示のフレームをずらすために使ったので消した方がいいけど面倒
    float plusNum3 = 60;        //

	public int turning_frame = 180;
	public string nextEnemy;

	public bool isCreate;		//表示するときにtrueにする

    void Start()
    {
		//位置オブジェクト取得
		//右側
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
		//左側
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

        createMiddleBossPos = GameObject.Find("CreateMiddleBossPos");
        createBattleShipPos = GameObject.Find("CreateBattleshipPos");
		//enemy_UFO_Group = Resources.Load("Enemy/Enemy_UFO_Group") as GameObject;
		//enemy_ClamChowder_Group_Four = Resources.Load("Enemy/Enemy_ClamChowder_Group_Four") as GameObject;
		enemy_ClamChowder_Group_FourBehind = Resources.Load("Enemy/Enemy_ClamChowder_Group_FourBehind") as GameObject;

		//enemy_ClamChowder_Group_Two_Top = Resources.Load("Enemy/Enemy_ClamChowder_Group_Two_Top") as GameObject;
		//enemy_ClamChowder_Group_Two_Under = Resources.Load("Enemy/Enemy_ClamChowder_Group_Two_Under") as GameObject;
		//enemy_ClamChowder_Group_Three_Item = Resources.Load("Enemy/Enemy_ClamChowder_Group_Three_Item") as GameObject;
		//enemy_ClamChowder_Group_Seven = Resources.Load("Enemy/Enemy_ClamChowder_Group_Seven") as GameObject;
		enemy_Clamchowder_Group_Straight = Resources.Load("Enemy/Enemy_ClamChowder_Group_Straight") as GameObject;
		enemy_Clamchowder_Group_StraightBehind = Resources.Load("Enemy/Enemy_ClamChowder_Group_StraightBehind") as GameObject;
		//enemy_MiddleBoss_Father = Resources.Load("Enemy/Enemy_MiddleBoss_Father") as GameObject;
		//enemy_ClamChowder_Group_ThreeWaveOnlyUp = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyUp") as GameObject;
		//enemy_ClamChowder_Group_ThreeWaveOnlyDown = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyDown") as GameObject;
		//enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item") as GameObject;
		//enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item") as GameObject;
		enemy_BattleShip = Resources.Load("Enemy/BattleshipType_Enemy") as GameObject;
		enemy_Beelzebub_Group_Five = Resources.Load("Enemy/Enemy_Beelzebub_Group_five") as GameObject;

		//群れカウント初期化
        groupCnt = 1;
    }

    void Update()
    {
		PreviousCount = frameCnt;
		frameCnt++;
		if(Input.GetKeyDown(KeyCode.N))
		{
			frameCnt = turning_frame;
		}
		if (saveEnemyObj != null)
		{

		}
		//CreateCheck();
		switch(Scene_Manager.Manager.Now_Scene)
		{
			case Scene_Manager.SCENE_NAME.eSTAGE_01:
				CreateEnemyGroup_01();
				break;
			case Scene_Manager.SCENE_NAME.eSTAGE_02:
				CreateEnemyGroup_02();
				break;
			default:
				break;
		}
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
			enemy_UFO_Group.transform.position = createPosR3.transform.position;
			enemy_UFO_Group.transform.rotation = transform.rotation;

			nextEnemy = "右下円盤";
			Next_Condition(250);
		}
		//円盤の群れを１つ右下から出す(430)
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 2)
		{
			GameObject enemy_UFO_Group2 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group2.transform.position = createPosRm3.transform.position;
			enemy_UFO_Group2.transform.rotation = transform.rotation;

			nextEnemy = "右上円盤";

			Next_Condition(250);
		}
		//円盤の群れを１つ右上から出す(680)
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 3)
		{
			GameObject enemy_UFO_Group3 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group3.transform.position = createPosR3.transform.position;
			enemy_UFO_Group3.transform.rotation = transform.rotation;

			nextEnemy = "右下円盤";
			Next_Condition(250);
		}
		//円盤の群れを１つ右下から出す(930)
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 4)
		{
			GameObject enemy_UFO_Group4 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group4.transform.position = createPosRm3.transform.position;
			enemy_UFO_Group4.transform.rotation = transform.rotation;

			nextEnemy = "突進闘牛";
			Next_Condition(120);
		}
		//奥からくる斜めに並んだ闘牛型の群れを出す 1050
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 5)
		{
			GameObject enemy_ClamChowder_Group_Four = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Four.Active_Obj();
			enemy_ClamChowder_Group_Four.transform.position = createPos_FourGroupL.transform.position;
			enemy_ClamChowder_Group_Four.transform.rotation = transform.rotation;

			nextEnemy = "真ん中アイテムの闘牛3体群れ";
			Next_Condition(460);
		}
		//奥からくる闘牛型が縦に3つ並んだ敵の群れを１つ出す（真ん中がアイテムを落とす敵） 1510
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 6)
		{
			GameObject enemy_ClamChowder_Group_Three_Item = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Three_Item.Active_Obj();
			enemy_ClamChowder_Group_Three_Item.transform.position = createPos_FourGroupL.transform.position;
			enemy_ClamChowder_Group_Three_Item.transform.rotation = transform.rotation;

			nextEnemy = "縦2体の闘牛上下で";
			Next_Condition(40);
		}
		//奥からくる闘牛型が縦に2つ並んだ敵の群れを２つ出す 1550
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 7)
		{
			GameObject enemy_ClamChowder_Group_Two_Top = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Two_Top.Active_Obj();
			enemy_ClamChowder_Group_Two_Top.transform.position = createPos_FourGroupL.transform.position;
			enemy_ClamChowder_Group_Two_Top.transform.rotation = transform.rotation;

			GameObject enemy_ClamChowder_Group_Two_Under = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Two_Under.Active_Obj();
			enemy_ClamChowder_Group_Two_Under.transform.position = createPos_FourGroupL.transform.position;
			enemy_ClamChowder_Group_Two_Under.transform.rotation = transform.rotation;

			nextEnemy = "突進闘牛後ろから";
			Next_Condition(480);
		}

		//奥からくる突進の闘牛型を画面右から左へ出す 2030
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 8)
		{
			Instantiate(enemy_ClamChowder_Group_FourBehind, createPos_FourGroupR.transform.position, transform.rotation);

			nextEnemy = "右上＆右下円盤間隔広め";
			Next_Condition(460);
		}
		//円盤の群れを右上と右下から１つずつ出す 2490
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 9)
		{
			GameObject enemy_UFO_Group5 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group5.transform.position = createPosR3.transform.position;
			enemy_UFO_Group5.transform.rotation = transform.rotation;

			GameObject enemy_UFO_Group6 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group6.transform.position = createPosRm3.transform.position;
			enemy_UFO_Group6.transform.rotation = transform.rotation;

			nextEnemy = "右上＆右下円盤間隔狭め";
			Next_Condition(120);
		}
		//円盤の群れを右側から中央寄りで2つ出す 2610
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 10)
		{
			GameObject enemy_UFO_Group7 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group7.transform.position = createPosR1.transform.position;
			enemy_UFO_Group7.transform.rotation = transform.rotation;

			GameObject enemy_UFO_Group8 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group8.transform.position = createPosRm1.transform.position;
			enemy_UFO_Group8.transform.rotation = transform.rotation;

			nextEnemy = "右上＆右下円盤間隔広め";
			Next_Condition(120);
		}
		//円盤の群れを右上と右下から１つずつ出す 2730
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 11)
		{
			GameObject enemy_UFO_Group9 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group9.transform.position = createPosR3.transform.position;
			enemy_UFO_Group9.transform.rotation = transform.rotation;

			GameObject enemy_UFO_Group10 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
			enemy_UFO_Group10.transform.position = createPosRm3.transform.position;
			enemy_UFO_Group10.transform.rotation = transform.rotation;

			nextEnemy = "戦艦2体";
			Next_Condition(200);
		}
		//戦艦を2体出す 2930
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 12)
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
		//戦艦を2体出す2回目 3480
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 13)
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
		//奥からくる闘牛型が縦7つに並んだ群れを一つ出す 4130
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 14)
		{
			GameObject enemy_ClamChowder_Group_Seven = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Seven.Active_Obj();
			enemy_ClamChowder_Group_Seven.transform.position = createPos_FourGroupL.transform.position;
			enemy_ClamChowder_Group_Seven.transform.rotation = transform.rotation;

			nextEnemy = "縦7体の闘牛2回目";
			Next_Condition(40);
		}
		//奥からくる闘牛型が縦7つに並んだ群れを一つ出す 4170
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 15)
		{
			GameObject enemy_ClamChowder_Group_Seven = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Seven.Active_Obj();
			enemy_ClamChowder_Group_Seven.transform.position = createPos_FourGroupL.transform.position;
			enemy_ClamChowder_Group_Seven.transform.rotation = transform.rotation;

			nextEnemy = "縦7体の闘牛3回目";
			Next_Condition(40);
		}
		//奥からくる闘牛型が縦7つに並んだ群れを一つ出す 4210
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 16)
		{
			GameObject enemy_ClamChowder_Group_Seven = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Seven.Active_Obj();
			enemy_ClamChowder_Group_Seven.transform.position = createPos_FourGroupL.transform.position;
			enemy_ClamChowder_Group_Seven.transform.rotation = transform.rotation;

			nextEnemy = "中ボス！！！";
			Next_Condition(820);
		}
		//中ボス出す 5030
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 17)
		{
			GameObject Boss_Middle = Obj_Storage.Storage_Data.Boss_Middle.Active_Obj();
			saveEnemyObj = Boss_Middle;
			Boss_Middle.transform.position = createMiddleBossPos.transform.position;
			Boss_Middle.transform.rotation = transform.rotation;

			nextEnemy = "右上と右下に縦3の闘牛";
			Next_Condition(180);
		}
		//右上と右下に闘牛型が3つ縦に並んだ群れを出す 5210
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 18)
		{
			GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyUp.Active_Obj();
			enemy_ClamChowder_Group_ThreeWaveOnlyUp.transform.position = createPosR3.transform.position;
			enemy_ClamChowder_Group_ThreeWaveOnlyUp.transform.rotation = transform.rotation;

			GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyDown.Active_Obj();
			enemy_ClamChowder_Group_ThreeWaveOnlyDown.transform.position = createPosRm3.transform.position;
			enemy_ClamChowder_Group_ThreeWaveOnlyDown.transform.rotation = transform.rotation;

			nextEnemy = "右上と右下に縦3の闘牛2回目";
			Next_Condition(180);
		}
		//右上と右下に闘牛型が3つ縦に並んだ群れを出す 5390
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 19)
		{
			GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp2 = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyUp.Active_Obj();
			enemy_ClamChowder_Group_ThreeWaveOnlyUp2.transform.position = createPosR3.transform.position;
			enemy_ClamChowder_Group_ThreeWaveOnlyUp2.transform.rotation = transform.rotation;

			GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown2 = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyDown.Active_Obj();
			enemy_ClamChowder_Group_ThreeWaveOnlyDown2.transform.position = createPosRm3.transform.position;
			enemy_ClamChowder_Group_ThreeWaveOnlyDown2.transform.rotation = transform.rotation;

			nextEnemy = "右上と右下に真ん中がアイテムの縦3の闘牛";
			Next_Condition(180);
		}
		//右上と右下に闘牛型が3つ縦に並んだ群れを出す（アイテム落とす敵入り） 5570

		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 20)
		{
			GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item.Active_Obj();
			enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item.transform.position = createPosR3.transform.position;
			enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item.transform.rotation = transform.rotation;

			GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item.Active_Obj();
			enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item.transform.position = createPosRm3.transform.position;
			enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item.transform.rotation = transform.rotation;

			nextEnemy = "右上と右下に縦3の闘牛3回目";
			Next_Condition(240);
		}
		//右上と右下に闘牛型が3つ縦に並んだ群れを出す 5810
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 21)
		{
			GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp3 = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyUp.Active_Obj();
			enemy_ClamChowder_Group_ThreeWaveOnlyUp3.transform.position = createPosR3.transform.position;
			enemy_ClamChowder_Group_ThreeWaveOnlyUp3.transform.rotation = transform.rotation;

			GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown3 = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyDown.Active_Obj();
			enemy_ClamChowder_Group_ThreeWaveOnlyDown3.transform.position = createPosRm3.transform.position;
			enemy_ClamChowder_Group_ThreeWaveOnlyDown3.transform.rotation = transform.rotation;

			nextEnemy = "右上と右下に縦3の闘牛4回目";
			Next_Condition(180);
		}
		// 右上と右下に闘牛型が3つ縦に並んだ群れを出す 5990
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 22)
		{
			GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp4 = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyUp.Active_Obj();
			enemy_ClamChowder_Group_ThreeWaveOnlyUp4.transform.position = createPosR3.transform.position;
			enemy_ClamChowder_Group_ThreeWaveOnlyUp4.transform.rotation = transform.rotation;

			GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown4 = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyDown.Active_Obj();
			enemy_ClamChowder_Group_ThreeWaveOnlyDown4.transform.position = createPosRm3.transform.position;
			enemy_ClamChowder_Group_ThreeWaveOnlyDown4.transform.rotation = transform.rotation;

			nextEnemy = "直進の闘牛を左上と左下から（後ろからくる）";
			Next_Condition(800);
		}
		//直進の闘牛を左上と左下から（後ろからくる）6790
		else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 23)
		{
			Instantiate(enemy_Clamchowder_Group_StraightBehind, createPosL3.transform.position, transform.rotation);
			Instantiate(enemy_Clamchowder_Group_StraightBehind, createPosLm3.transform.position, transform.rotation);

			nextEnemy = "直進の闘牛を右中央から（前からくる）";
			Next_Condition(360);
		}
		//直進の闘牛を右中央から（前からくる） 7150
		else if(Is_A_Specified_Frame(turning_frame) && groupCnt == 24)
		{
			Instantiate(enemy_Clamchowder_Group_Straight, createPosR0.transform.position, transform.rotation);
			//Instantiate(enemy_Clamchowder_Group_Straight, createPosRm2.transform.position, transform.rotation);
			nextEnemy = "右からハエ型4体の群れ出す";
			Next_Condition(500);

		}
		//右からハエ型5体の群れ出す 7510
		else if(Is_A_Specified_Frame(turning_frame) && groupCnt == 25)
		{
			Instantiate(enemy_Beelzebub_Group_Five, createPosR0.transform.position, transform.rotation);
			//Instantiate(enemy_Clamchowder_Group_Straight, createPosRm2.transform.position, transform.rotation);
			nextEnemy = "戦艦二体";
			Next_Condition(540);

		}
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 26)
        {
            GameObject Battle_Ship1 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
            BattleshipType_Enemy b1 = Battle_Ship1.GetComponent<BattleshipType_Enemy>();
            b1.Is_up = false;

            GameObject Battle_Ship2 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
            BattleshipType_Enemy b2 = Battle_Ship2.GetComponent<BattleshipType_Enemy>();
            b2.Is_up = true;

            nextEnemy = "直線闘牛を右真ん中から";
            Next_Condition(200);

        }
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 27)
        {
            Instantiate(enemy_Clamchowder_Group_Straight, createPosR0.transform.position, transform.rotation);

            nextEnemy = "直線闘牛を右真ん中から";
            Next_Condition(200);

        }

        // 23 ラスボス(6590)
        else if (Is_A_Specified_Frame(turning_frame) && groupCnt == 30)
		{
			GameObject Boss_01 = Obj_Storage.Storage_Data.Boss_1.Active_Obj();
			Boss_01.transform.position = Vector3.zero;

			Next_Condition(340);
		}
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
		turning_frame += add_frame;
	}
}
