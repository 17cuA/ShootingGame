using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreate : MonoBehaviour
{
	GameObject createPosR5;
	GameObject createPosR4;
	GameObject createPosR3;
	GameObject createPosR2;
	GameObject createPosR1;
	GameObject createPosR0;
	GameObject createPosRm1;
	GameObject createPosRm2;
	GameObject createPosRm3;
	GameObject createPosRm4;
	GameObject createPosRm5;

	GameObject createPosL5;
	GameObject createPosL4;
	GameObject createPosL3;
	GameObject createPosL2;
	GameObject createPosL1;
	GameObject createPosL0;
	GameObject createPosLm1;
	GameObject createPosLm2;
	GameObject createPosLm3;
	GameObject createPosLm4;
	GameObject createPosLm5;

	GameObject enemy_UFO_Group;
	GameObject clamChowderType_Enemy;
	GameObject clamChowderType_Enemy_Item;
	GameObject enemy_ClamChowder_Group_Two;
	//GameObject 



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
		createPosLm1 = GameObject.Find("CreatePos_Left_-1");
		createPosLm2 = GameObject.Find("CreatePos_Left_-2");
		createPosLm3 = GameObject.Find("CreatePos_Left_-3");
		createPosLm4 = GameObject.Find("CreatePos_Left_-4");
		createPosLm5 = GameObject.Find("CreatePos_Left_-5");


	}

	void Update()
    {
        
    }
}
