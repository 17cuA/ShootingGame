//作成者：川村良太
//敵を出すスクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreate : MonoBehaviour
{
	//生成位置右側（Rなので右側、mRはマイナスなのでmがついてる、5が上下の最大）
    public GameObject createPosR5;				
    public GameObject createPosR4;
    public GameObject createPosR3;
    public GameObject createPosR2;
    public GameObject createPosR1;
    public GameObject createPosR0;
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
    public GameObject createPos_FourGroup;
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
    //public GameObject enemy_ClamChowder_Group_Two_Top;
    //public GameObject enemy_ClamChowder_Group_Two_Under;
    //public GameObject enemy_ClamChowder_Group_Three_Item;
    //public GameObject enemy_ClamChowder_Group_Seven;
    //public GameObject enemy_MiddleBoss_Father;
    //public GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp;
    //public GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown;
    //public GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item;
    //public GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item;
    public GameObject enemy_BattleShip;
    public GameObject enemy_BattleShip2;

    public int frameCnt = 0;	//フレームカウント：これの値で生成のタイミングをはかる
    public int groupCnt = 1;	//画面に出す群れのカウント
    float plusNum = 60;			//
    float plusNum2 = 60;		//この三つは強引に表示のフレームをずらすために使ったので消した方がいいけど面倒
    float plusNum3 = 60;		//

    public bool isCreate;		//表示するときにtrueにする

    void Start()
    {
		//位置オブジェクト取得
        createPosR5 = GameObject.Find("CreatePos_Right_5");
        createPosR4 = GameObject.Find("CreatePos_Right_4");
        createPosR3 = GameObject.Find("CreatePos_Right_3");
        createPosR2 = GameObject.Find("CreatePos_Right_2");
        createPosR1 = GameObject.Find("CreatePos_Right_1");
        createPosR0 = GameObject.Find("CreatePos_Right_0");
        createPosRm1 = GameObject.Find("CreatePos_Right_-1");
        createPosRm2 = GameObject.Find("CreatePos_Right_-2");
        createPosRm3 = GameObject.Find("CreatePos_Right_-3");
        createPosRm4 = GameObject.Find("CreatePos_Right_-4");
        createPosRm5 = GameObject.Find("CreatePos_Right_-5");

        createPosL5 = GameObject.Find("CreatePos_Left_5");
        createPosL4 = GameObject.Find("CreatePos_Left_4");
        createPosL3 = GameObject.Find("CreatePos_Left_3");
        createPosL2 = GameObject.Find("CreatePos_Left_2");
        createPosL1 = GameObject.Find("CreatePos_Left_1");
        createPosL0 = GameObject.Find("CreatePos_Left_0");
        createPos_FourGroup = GameObject.Find("CreatePos_FourGroup");
        createPosLm1 = GameObject.Find("CreatePos_Left_-1");
        createPosLm2 = GameObject.Find("CreatePos_Left_-2");
        createPosLm3 = GameObject.Find("CreatePos_Left_-3");
        createPosLm4 = GameObject.Find("CreatePos_Left_-4");
        createPosLm5 = GameObject.Find("CreatePos_Left_-5");

        createMiddleBossPos = GameObject.Find("CreateMiddleBossPos");
        createBattleShipPos = GameObject.Find("CreateBattleshipPos");

        //enemy_UFO_Group = Resources.Load("Enemy/Enemy_UFO_Group") as GameObject;
        //enemy_ClamChowder_Group_Four = Resources.Load("Enemy/Enemy_ClamChowder_Group_Four") as GameObject;
        //enemy_ClamChowder_Group_Two_Top = Resources.Load("Enemy/Enemy_ClamChowder_Group_Two_Top") as GameObject;
        //enemy_ClamChowder_Group_Two_Under = Resources.Load("Enemy/Enemy_ClamChowder_Group_Two_Under") as GameObject;
        //enemy_ClamChowder_Group_Three_Item = Resources.Load("Enemy/Enemy_ClamChowder_Group_Three_Item") as GameObject;
        //enemy_ClamChowder_Group_Seven = Resources.Load("Enemy/Enemy_ClamChowder_Group_Seven") as GameObject;
        //enemy_MiddleBoss_Father = Resources.Load("Enemy/Enemy_MiddleBoss_Father") as GameObject;
        //enemy_ClamChowder_Group_ThreeWaveOnlyUp = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyUp") as GameObject;
        //enemy_ClamChowder_Group_ThreeWaveOnlyDown = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyDown") as GameObject;
        //enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item") as GameObject;
        //enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item") as GameObject;
        enemy_BattleShip = Resources.Load("Enemy/BattleshipType_Enemy") as GameObject;
        enemy_BattleShip2 = Resources.Load("Enemy/BattleshipType_Enemy (1)") as GameObject;

		//群れカウント初期化
        groupCnt = 1;
    }

    void Update()
    {
        CreateCheck();

        CreateEnemyGroup();

        frameCnt++;
    }

	//--------------------------------------------------------------------

	//敵を出す関数
	void CreateEnemyGroup()
    {
        if (isCreate)
        {
            switch (groupCnt)
            {
                case 1:						//円盤の群れを１つ右上から出す
                    isCreate = false;
                    groupCnt++;
                    //Instantiate(Obj_Storage.Storage_Data.enemy_UFO_Group_prefab, createPosR4.transform.position, transform.rotation);
                    GameObject enemy_UFO_Group = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
                    enemy_UFO_Group.transform.position = createPosR3.transform.position;
                    enemy_UFO_Group.transform.rotation = transform.rotation;
                    break;
					
                case 2:						//円盤の群れを１つ右下から出す
                    isCreate = false;
                    groupCnt++;
                    //Instantiate(enemy_UFO_Group, createPosRm4.transform.position, transform.rotation);
                    GameObject enemy_UFO_Group2 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
                    enemy_UFO_Group2.transform.position = createPosRm3.transform.position;
                    enemy_UFO_Group2.transform.rotation = transform.rotation;
                    break;

                case 3:						//円盤の群れを１つ右上から出す
					isCreate = false;
                    groupCnt++;
                    //Instantiate(enemy_UFO_Group, createPosR4.transform.position, transform.rotation);
                    GameObject enemy_UFO_Group3 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
                    enemy_UFO_Group3.transform.position = createPosR3.transform.position;
                    enemy_UFO_Group3.transform.rotation = transform.rotation;
                    break;

                case 4:						//円盤の群れを１つ右下から出す
					isCreate = false;
                    groupCnt++;
                    //Instantiate(enemy_UFO_Group, createPosRm4.transform.position, transform.rotation);
                    GameObject enemy_UFO_Group4 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
                    enemy_UFO_Group4.transform.position = createPosRm3.transform.position;
                    enemy_UFO_Group4.transform.rotation = transform.rotation;
                    break;

                case 5:						//奥からくる斜めに並んだ闘牛型の群れを出す
                    isCreate = false;
                    groupCnt++;
                    //Instantiate(enemy_ClamChowder_Group_Four, createPos_FourGroup.transform.position, transform.rotation);
                    GameObject enemy_ClamChowder_Group_Four = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Four.Active_Obj();
                    enemy_ClamChowder_Group_Four.transform.position = createPos_FourGroup.transform.position;
                    enemy_ClamChowder_Group_Four.transform.rotation = transform.rotation;
                    break;

                case 6:						//円盤の群れを右上と右下から１つずつ出す
                    isCreate = false;
                    groupCnt++;
                    //Instantiate(enemy_UFO_Group, createPosR4.transform.position, transform.rotation);
                    GameObject enemy_UFO_Group5 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
                    enemy_UFO_Group5.transform.position = createPosR3.transform.position;
                    enemy_UFO_Group5.transform.rotation = transform.rotation;

                    //Instantiate(enemy_UFO_Group, createPosRm4.transform.position, transform.rotation);
                    GameObject enemy_UFO_Group6 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
                    enemy_UFO_Group6.transform.position = createPosRm3.transform.position;
                    enemy_UFO_Group6.transform.rotation = transform.rotation;

                    break;

                case 7:						//円盤の群れを右側から中央寄りで2つ出す
                    isCreate = false;
                    groupCnt++;
                    //Instantiate(enemy_UFO_Group, createPosR1.transform.position, transform.rotation);
                    GameObject enemy_UFO_Group7 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
                    enemy_UFO_Group7.transform.position = createPosR1.transform.position;
                    enemy_UFO_Group7.transform.rotation = transform.rotation;

                    //Instantiate(enemy_UFO_Group, createPosRm1.transform.position, transform.rotation);
                    GameObject enemy_UFO_Group8 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
                    enemy_UFO_Group8.transform.position = createPosRm1.transform.position;
                    enemy_UFO_Group8.transform.rotation = transform.rotation;

                    break;

                case 8:						//円盤の群れを右上と右下から１つずつ出す
					isCreate = false;
                    groupCnt++;
                    //Instantiate(enemy_UFO_Group, createPosR4.transform.position, transform.rotation);
                    GameObject enemy_UFO_Group9 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
                    enemy_UFO_Group9.transform.position = createPosR3.transform.position;
                    enemy_UFO_Group9.transform.rotation = transform.rotation;

                    //Instantiate(enemy_UFO_Group, createPosRm4.transform.position, transform.rotation);
                    GameObject enemy_UFO_Group10 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
                    enemy_UFO_Group10.transform.position = createPosRm3.transform.position;
                    enemy_UFO_Group10.transform.rotation = transform.rotation;

                    break;

                case 9:						//奥からくる闘牛型が縦に2つ並んだ敵の群れを２つ出す
                    isCreate = false;
                    groupCnt++;
                    //Instantiate(enemy_ClamChowder_Group_Two_Top, createPos_FourGroup.transform.position, transform.rotation);
                    GameObject enemy_ClamChowder_Group_Two_Top = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Two_Top.Active_Obj();
                    enemy_ClamChowder_Group_Two_Top.transform.position = createPos_FourGroup.transform.position;
                    enemy_ClamChowder_Group_Two_Top.transform.rotation = transform.rotation;

                    //Instantiate(enemy_ClamChowder_Group_Two_Under, createPos_FourGroup.transform.position, transform.rotation);
                    GameObject enemy_ClamChowder_Group_Two_Under = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Two_Under.Active_Obj();
                    enemy_ClamChowder_Group_Two_Under.transform.position = createPos_FourGroup.transform.position;
                    enemy_ClamChowder_Group_Two_Under.transform.rotation = transform.rotation;

                    break;

                case 10:					//奥からくる闘牛型が縦に3つ並んだ敵の群れを１つ出す（真ん中がアイテムを落とす敵）
					isCreate = false;
                    groupCnt++;
                    //GameObject Battle_Ship2 = enemy_BattleShip2;
                    //Instantiate(enemy_BattleShip2, createBattleShipPos.transform.position, enemy_BattleShip2.transform.rotation);

                    //GameObject Battle_Ship1 = enemy_BattleShip;
                    //Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
					//Instantiate(enemy_ClamChowder_Group_Three_Item, createPos_FourGroup.transform.position, transform.rotation);
					GameObject enemy_ClamChowder_Group_Three_Item = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Three_Item.Active_Obj();
					enemy_ClamChowder_Group_Three_Item.transform.position = createPos_FourGroup.transform.position;
					enemy_ClamChowder_Group_Three_Item.transform.rotation = transform.rotation;

					break;

				case 11:					//戦艦を2体出す
					isCreate = false;
					groupCnt++;
					GameObject Battle_Ship2 = enemy_BattleShip2;
					Instantiate(enemy_BattleShip2, createBattleShipPos.transform.position, enemy_BattleShip2.transform.rotation);

					GameObject Battle_Ship1 = enemy_BattleShip;
					Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);

					break;

                case 12:					//奥からくる闘牛型が縦7つに並んだ群れを一つ出す
                    isCreate = false;
                    groupCnt++;
                    //Instantiate(enemy_ClamChowder_Group_Seven, createPos_FourGroup.transform.position, transform.rotation);
                    GameObject enemy_ClamChowder_Group_Seven = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Seven.Active_Obj();
                    enemy_ClamChowder_Group_Seven.transform.position = createPos_FourGroup.transform.position;
                    enemy_ClamChowder_Group_Seven.transform.rotation = transform.rotation;

                    break;

                case 13:					//中ボス出す
                    isCreate = false;
                    groupCnt++;
                    //Instantiate(enemy_MiddleBoss_Father, createMiddleBossPos.transform.position, transform.rotation);
                    GameObject Boss_Middle = Obj_Storage.Storage_Data.Boss_Middle.Active_Obj();
                    Boss_Middle.transform.position = createMiddleBossPos.transform.position;
                    Boss_Middle.transform.rotation = transform.rotation;

                    break;

                case 14:					//右上と右下に闘牛型が3つ縦に並んだ群れを出す
                    isCreate = false;
                    groupCnt++;
                    //Instantiate(enemy_ClamChowder_Group_ThreeWaveOnlyUp, createPosR3.transform.position, transform.rotation);
                    GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyUp.Active_Obj();
                    enemy_ClamChowder_Group_ThreeWaveOnlyUp.transform.position = createPosR3.transform.position;
                    enemy_ClamChowder_Group_ThreeWaveOnlyUp.transform.rotation = transform.rotation;

                    //Instantiate(enemy_ClamChowder_Group_ThreeWaveOnlyDown, createPosRm3.transform.position, transform.rotation);
                    GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyDown.Active_Obj();
                    enemy_ClamChowder_Group_ThreeWaveOnlyDown.transform.position = createPosRm3.transform.position;
                    enemy_ClamChowder_Group_ThreeWaveOnlyDown.transform.rotation = transform.rotation;


                    break;

                case 15:						//右上と右下に闘牛型が3つ縦に並んだ群れを出す
					isCreate = false;
                    groupCnt++;
                    //Instantiate(enemy_ClamChowder_Group_ThreeWaveOnlyUp, createPosR3.transform.position, transform.rotation);
                    GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp2 = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyUp.Active_Obj();
                    enemy_ClamChowder_Group_ThreeWaveOnlyUp2.transform.position = createPosR3.transform.position;
                    enemy_ClamChowder_Group_ThreeWaveOnlyUp2.transform.rotation = transform.rotation;

                    //Instantiate(enemy_ClamChowder_Group_ThreeWaveOnlyDown, createPosRm3.transform.position, transform.rotation);
                    GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown2 = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyDown.Active_Obj();
                    enemy_ClamChowder_Group_ThreeWaveOnlyDown2.transform.position = createPosRm3.transform.position;
                    enemy_ClamChowder_Group_ThreeWaveOnlyDown2.transform.rotation = transform.rotation;

                    break;

                case 16:						//右上と右下に闘牛型が3つ縦に並んだ群れを出す（アイテム落とす敵入り）
					isCreate = false;
                    groupCnt++;
                    //Instantiate(enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item, createPosR3.transform.position, transform.rotation);
                    GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item.Active_Obj();
                    enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item.transform.position = createPosR3.transform.position;
                    enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item.transform.rotation = transform.rotation;

                    //Instantiate(enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item, createPosRm3.transform.position, transform.rotation);
                    GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item.Active_Obj();
                    enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item.transform.position = createPosRm3.transform.position;
                    enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item.transform.rotation = transform.rotation;

                    break;

                case 17:						//右上と右下に闘牛型が3つ縦に並んだ群れを出す
					isCreate = false;
                    groupCnt++;
                    //Instantiate(enemy_ClamChowder_Group_ThreeWaveOnlyUp, createPosR3.transform.position, transform.rotation);
                    GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp3 = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyUp.Active_Obj();
                    enemy_ClamChowder_Group_ThreeWaveOnlyUp3.transform.position = createPosR3.transform.position;
                    enemy_ClamChowder_Group_ThreeWaveOnlyUp3.transform.rotation = transform.rotation;

                    //Instantiate(enemy_ClamChowder_Group_ThreeWaveOnlyDown, createPosRm3.transform.position, transform.rotation);
                    GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown3 = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyDown.Active_Obj();
                    enemy_ClamChowder_Group_ThreeWaveOnlyDown3.transform.position = createPosRm3.transform.position;
                    enemy_ClamChowder_Group_ThreeWaveOnlyDown3.transform.rotation = transform.rotation;

                    break;

                case 18:						//右上と右下に闘牛型が3つ縦に並んだ群れを出す
					isCreate = false;
                    groupCnt++;
                    //Instantiate(enemy_ClamChowder_Group_ThreeWaveOnlyUp, createPosR3.transform.position, transform.rotation);
                    GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp4 = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyUp.Active_Obj();
                    enemy_ClamChowder_Group_ThreeWaveOnlyUp4.transform.position = createPosR3.transform.position;
                    enemy_ClamChowder_Group_ThreeWaveOnlyUp4.transform.rotation = transform.rotation;

                    //Instantiate(enemy_ClamChowder_Group_ThreeWaveOnlyDown, createPosRm3.transform.position, transform.rotation);
                    GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown4 = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyDown.Active_Obj();
                    enemy_ClamChowder_Group_ThreeWaveOnlyDown4.transform.position = createPosRm3.transform.position;
                    enemy_ClamChowder_Group_ThreeWaveOnlyDown4.transform.rotation = transform.rotation;

                    break;

            }
        }
    }
    //---------------------------------------------------------------------

    //---------------------------------------------------------------------
    void CreateCheck()
    {
		//groupCntが18の時のを出すタイミング
		if (frameCnt == 3760.0f + plusNum + plusNum2 + plusNum3)
        {
            isCreate = true;
        }
		//groupCntが17の時のを出すタイミング
		else if (frameCnt == 3580.0f + plusNum + plusNum2 + plusNum3)
        {
            isCreate = true;
        }
		//groupCntが16の時のを出すタイミング
		else if (frameCnt == 3400.0f + plusNum + plusNum2 + plusNum3)
        {
            isCreate = true;
        }
		//groupCntが15の時のを出すタイミング
		else if (frameCnt == 3220.0f + plusNum + plusNum2 + plusNum3)
        {
            isCreate = true;
        }
		//groupCntが14の時のを出すタイミング
		else if (frameCnt == 3040.0f + plusNum + plusNum2 + plusNum3)
        {
            isCreate = true;

        }
		//groupCntが13の時のを出すタイミング
		else if (frameCnt == 2800.0f + plusNum + plusNum2 + plusNum3)
        {
            isCreate = true;

        }
		//groupCntが12の時のを出すタイミング
		else if (frameCnt == 2350.0f + plusNum + plusNum2 + plusNum3)
        {
            isCreate = true;

        }
		//groupCntが11の時のを出すタイミング
		else if (frameCnt == 1560.0f + plusNum + plusNum2 + plusNum3)
        {
            isCreate = true;

        }
		//groupCntが10の時のを出すタイミング
		else if (frameCnt == 1470.0f + plusNum + plusNum2 + plusNum3)
        {
            isCreate = true;

        }
		//groupCntが9の時のを出すタイミング
		else if (frameCnt == 1380.0f + plusNum + plusNum2 + plusNum3)
        {
            isCreate = true;
        }
		//groupCntが8の時のを出すタイミング
		else if (frameCnt == 1200.0f + plusNum + plusNum2 + plusNum3)
        {
            isCreate = true;

        }
		//groupCntが7の時のを出すタイミング
		else if (frameCnt == 1140.0f + plusNum + plusNum2)
        {
            isCreate = true;

        }
		//groupCntが6の時のを出すタイミング
		else if (frameCnt == 1080.0f + plusNum)
        {
            isCreate = true;

        }
		//groupCntが5の時のを出すタイミング
		else if (frameCnt == 780.0f)
        {
            isCreate = true;

        }
		//groupCntが4の時のを出すタイミング
		else if (frameCnt == 660.0f)
        {
            isCreate = true;

        }
		//groupCntが3の時のを出すタイミング
		else if (frameCnt == 480.0f)
        {
            isCreate = true;

        }
		//groupCntが2の時のを出すタイミング
		else if (frameCnt == 300.0f)
        {
            isCreate = true;

        }
		//groupCntが1の時のを出すタイミング
        else if (frameCnt == 120.0f)
        {
            isCreate = true;

        }
    }
    //---------------------------------------------------------------------
}
