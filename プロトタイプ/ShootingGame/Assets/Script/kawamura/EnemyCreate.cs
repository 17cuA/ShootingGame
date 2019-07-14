using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreate : MonoBehaviour
{
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

    public GameObject createMiddleBossPos;

	public GameObject enemy_UFO_Group;
	public GameObject enemy_ClamChowder_Group_Four;
	public GameObject enemy_ClamChowder_Group_Two_Top;
	public GameObject enemy_ClamChowder_Group_Two_Under;
	public GameObject enemy_ClamChowder_Group_Three_Item;
	public GameObject enemy_ClamChowder_Group_Seven;
	public GameObject enemy_MiddleBoss_Father;
	public GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp;
	public GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown;
	public GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item;
	public GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item;

	public int frameCnt = 0;
	public int groupCnt = 1;
	float plusNum=60;
	float plusNum2 = 60;
	float plusNum3 = 60;

	public bool isCreate;
	
	void Start()
    {
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

		enemy_UFO_Group = Resources.Load("Enemy/Enemy_UFO_Group") as GameObject;
		enemy_ClamChowder_Group_Four = Resources.Load("Enemy/Enemy_ClamChowder_Group_Four") as GameObject;
		enemy_ClamChowder_Group_Two_Top = Resources.Load("Enemy/Enemy_ClamChowder_Group_Two_Top") as GameObject;
		enemy_ClamChowder_Group_Two_Under = Resources.Load("Enemy/Enemy_ClamChowder_Group_Two_Under") as GameObject;
		enemy_ClamChowder_Group_Three_Item = Resources.Load("Enemy/Enemy_ClamChowder_Group_Three_Item") as GameObject;
		enemy_ClamChowder_Group_Seven = Resources.Load("Enemy/Enemy_ClamChowder_Group_Seven") as GameObject;
		enemy_MiddleBoss_Father = Resources.Load("Enemy/Enemy_MiddleBoss_Father") as GameObject;
		enemy_ClamChowder_Group_ThreeWaveOnlyUp = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyUp") as GameObject;
		enemy_ClamChowder_Group_ThreeWaveOnlyDown = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyDown") as GameObject;
		enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item") as GameObject;
		enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item") as GameObject;

		groupCnt = 1;
	}

	void Update()
    {
		CreateCheck();

		CreateEnemyGroup();


		frameCnt++;
    }

	//--------------------------------------------------------------------
	void CreateEnemyGroup()
	{
		if (isCreate)
		{
			switch (groupCnt)
			{
				case 1:
					isCreate = false;
					groupCnt++;
					Instantiate(enemy_UFO_Group, createPosR4.transform.position, transform.rotation);
					break;

				case 2:
					isCreate = false;
					groupCnt++;
					Instantiate(enemy_UFO_Group, createPosRm4.transform.position, transform.rotation);
					break;

				case 3:
					isCreate = false;
					groupCnt++;
					Instantiate(enemy_UFO_Group, createPosR4.transform.position, transform.rotation);

					break;

				case 4:
					isCreate = false;
					groupCnt++;
					Instantiate(enemy_UFO_Group, createPosRm4.transform.position, transform.rotation);

					break;

				case 5:
					isCreate = false;
					groupCnt++;
					Instantiate(enemy_ClamChowder_Group_Four, createPos_FourGroup.transform.position, transform.rotation);

					break;

				case 6:
					isCreate = false;
					groupCnt++;
					Instantiate(enemy_UFO_Group, createPosR4.transform.position, transform.rotation);
					Instantiate(enemy_UFO_Group, createPosRm4.transform.position, transform.rotation);

					break;

				case 7:
					isCreate = false;
					groupCnt++;
					Instantiate(enemy_UFO_Group, createPosR1.transform.position, transform.rotation);
					Instantiate(enemy_UFO_Group, createPosRm1.transform.position, transform.rotation);

					break;

				case 8:
					isCreate = false;
					groupCnt++;
					Instantiate(enemy_UFO_Group, createPosR4.transform.position, transform.rotation);
					Instantiate(enemy_UFO_Group, createPosRm4.transform.position, transform.rotation);

					break;

				case 9:
					isCreate = false;
					groupCnt++;
					Instantiate(enemy_ClamChowder_Group_Two_Top, createPos_FourGroup.transform.position, transform.rotation);
					Instantiate(enemy_ClamChowder_Group_Two_Under, createPos_FourGroup.transform.position, transform.rotation);

					break;

				case 10:
					isCreate = false;
					groupCnt++;
					Instantiate(enemy_ClamChowder_Group_Three_Item, createPos_FourGroup.transform.position, transform.rotation);

					break;

				case 11:
					isCreate = false;
					groupCnt++;
					Instantiate(enemy_ClamChowder_Group_Seven, createPos_FourGroup.transform.position, transform.rotation);

					break;

				case 12:
					isCreate = false;
					groupCnt++;
                    Instantiate(enemy_MiddleBoss_Father, createMiddleBossPos.transform.position, transform.rotation);

					break;

				case 13:
					isCreate = false;
					groupCnt++;
					Instantiate(enemy_ClamChowder_Group_ThreeWaveOnlyUp, createPosR3.transform.position, transform.rotation);
					Instantiate(enemy_ClamChowder_Group_ThreeWaveOnlyDown, createPosRm3.transform.position, transform.rotation);

					break;

				case 14:
					isCreate = false;
					groupCnt++;
					Instantiate(enemy_ClamChowder_Group_ThreeWaveOnlyUp, createPosR3.transform.position, transform.rotation);
					Instantiate(enemy_ClamChowder_Group_ThreeWaveOnlyDown, createPosRm3.transform.position, transform.rotation);

					break;

				case 15:
					isCreate = false;
					groupCnt++;
					Instantiate(enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item, createPosR3.transform.position, transform.rotation);
					Instantiate(enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item, createPosRm3.transform.position, transform.rotation);

					break;

				case 16:
					isCreate = false;
					groupCnt++;
					Instantiate(enemy_ClamChowder_Group_ThreeWaveOnlyUp, createPosR3.transform.position, transform.rotation);
					Instantiate(enemy_ClamChowder_Group_ThreeWaveOnlyDown, createPosRm3.transform.position, transform.rotation);

					break;

				case 17:
					isCreate = false;
					groupCnt++;
					Instantiate(enemy_ClamChowder_Group_ThreeWaveOnlyUp, createPosR3.transform.position, transform.rotation);
					Instantiate(enemy_ClamChowder_Group_ThreeWaveOnlyDown, createPosRm3.transform.position, transform.rotation);

					break;

			}
		}
	}
	//---------------------------------------------------------------------

	//---------------------------------------------------------------------
	void CreateCheck()
	{
		if (frameCnt == 3520.0f + plusNum + plusNum2 + plusNum3)
		{
			isCreate = true;
		}
		else if (frameCnt == 3340.0f + plusNum + plusNum2 + plusNum3)
		{
			isCreate = true;
		}
		else if (frameCnt == 3160.0f + plusNum + plusNum2 + plusNum3)
		{
			isCreate = true;
		}
		else if (frameCnt == 2980.0f + plusNum + plusNum2 + plusNum3)
		{
			isCreate = true;
		}
		else if (frameCnt == 2800.0f + plusNum + plusNum2 + plusNum3)
		{
			isCreate = true;

		}
		else if (frameCnt == 2560.0f + plusNum + plusNum2 + plusNum3)
		{
			isCreate = true;

		}
		else if (frameCnt == 2350.0f + plusNum + plusNum2 + plusNum3)
		{
			isCreate = true;

		}
		else if (frameCnt == 1560.0f + plusNum + plusNum2 + plusNum3)
		{
			isCreate = true;

		}
		else if (frameCnt == 1470.0f + plusNum + plusNum2 + plusNum3)
		{
			isCreate = true;

		}
		else if (frameCnt == 1380.0f + plusNum + plusNum2 + plusNum3)
		{
            isCreate = true;
		}
		else if (frameCnt == 1200.0f + plusNum + plusNum2 + plusNum3)
		{
			isCreate = true;

		}
		else if (frameCnt == 1140.0f + plusNum + plusNum2)
		{
			isCreate = true;

		}
		else if (frameCnt == 1080.0f + plusNum)
		{
			isCreate = true;

		}
		else if (frameCnt == 780.0f)
		{
			isCreate = true;

		}
		else if (frameCnt == 660.0f)
		{
			isCreate = true;

		}
		else if (frameCnt == 480.0f)
		{
			isCreate = true;

		}
		else if (frameCnt == 300.0f)
		{
			isCreate = true;

		}
		else if (frameCnt == 120.0f)
		{
			isCreate = true;

		}
	}
	//---------------------------------------------------------------------
}
