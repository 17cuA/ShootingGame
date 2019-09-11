using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Moai : character_status
{
	public enum AttackState
	{
		RingShot,
		MiniMoai,
		Laser,
		Stay,
	}

	public AttackState attackState;

	GameObject faceObj;
	GameObject mouthObj;
	GameObject moaiAttackObj;
	GameObject saveObj;



	public Rigidbody moai_rigidbody;

	MoaiAnimation moaiAnime_Script;
	Enemy_Moai_Attack moaiAttack_Script;

	Vector3 velocity;
	public Vector4 moaiColor;

	public Renderer[] moai_material;                                  // オブジェクトのマテリアル情報
	public Material[] moai_material_save;

	public float wireles_DelayCnt;
	public float wireles_DelayMax;

	public float speedX;
	public float speedX_Value;
	public float speedY;
	[Header("入力用 Y速度値")]
	public float speedY_Value;
	public float rotaY;
	[Header("入力用 回転変化値")]
	public float rotaY_Value;
	public float defaultRotaY_Value;
	[Header("入力用 回転最大値")]
	public float rotationYMax;

	public float rotaX;
	public float rotaX_Value;
	public float rotaZ;
	public float rotaZ_Value;

	public int attackLoopCnt;
	public float aliveCnt;

	public float color_Value;
	public float HpMax;
	public float bulletRota_Value;  //発射する弾の角度範囲用

	//攻撃関係の管理で使うよ！--------------------------------------------
	public int ringShotCnt;         //リング攻撃した数
	[Header("入力用 リング攻撃回数")]
	public int ringShotMax;     //リング攻撃回数
	public float ringShotDelay;

	public int miniMoaisCnt;
	[Header("入力用 ミニモアイを出す回数")]
	public int miniMoaisMax;
	public float attackDelay;
	//攻撃関係の管理で使うやつの終わりだよ！-----------------------------

	public ParticleSystem explosionEffect;



	public bool isAppearance = true;        //最初の登場用
	public bool isExit = false;                 //退場用
	public bool isMove = false;
	public bool isWireles = true;
	public bool isMouthOpen = false;
	public bool isRingShot = true;
	public bool isMiniMoai = false;
	public bool isLaserEmd = false;
	public bool isDead = false;
	new void Start()
	{
		faceObj = transform.GetChild(0).gameObject;
		mouthObj = transform.GetChild(1).gameObject;
		moaiAnime_Script = mouthObj.GetComponent<MoaiAnimation>();

		moaiAttackObj = transform.GetChild(2).gameObject;
		moaiAttack_Script = mouthObj.GetComponent<Enemy_Moai_Attack>();
		speedY = speedY_Value;
		defaultRotaY_Value = rotaY_Value;
		rotaY = 90;
		HpMax = hp;
		isDead = false;

		HP_Setting();
		base.Start();
	}


	new void Update()
	{
		Physics.gravity = new Vector3(0, -0.3f, 0);

        if (!isAppearance && !isExit && Game_Master.Management_In_Stage == Game_Master.CONFIGURATION_IN_STAGE.WIRELESS)
        {
            hp = 1800;
            for (int i = 0; i < object_material.Length; i++)
            {
                object_material[i].material = self_material[i];
            }

            return;
        }

		if (isAppearance)
		{
			hp = 1800;
			velocity = gameObject.transform.rotation * new Vector3(0, speedY, 0);
			gameObject.transform.position += velocity * Time.deltaTime;

			if (transform.position.y > 0)
			{

				transform.position = new Vector3(transform.position.x, 0, transform.position.z);
			}

			if (transform.position.y > -6.5f)
			{
				rotaY += rotaY_Value;
				rotationYMax -= rotaY_Value;
				if (rotaY > 270f)
				{
					rotaY = 270f;
				}

				//if (rotationYMax < 0)
				//{
				//	rotaY_Value = 0;
				//}
				if (rotationYMax < 180f)
				{
					rotaY_Value = defaultRotaY_Value * (rotationYMax / 180f) + 0.35f;

				}
				if (transform.position.y > -6.5f)
				{
					speedY = speedY_Value * (transform.position.y / -6.5f) + 0.5f;
				}
			}

			//if (rotaY < -90)
			//{

			//	rotaY = -90;
			//}

			transform.rotation = Quaternion.Euler(0, rotaY, 0);

			if (transform.position.y >= 0 && rotaY >= 270)
			{
				transform.position = new Vector3(transform.position.x, 0, transform.position.z);
				isAppearance = false;
				moaiAnime_Script.isOpen = true;
			}
		}
		else if (isDead)
		{
			rotaX_Value = 0.1f;
			rotaX -= rotaX_Value;
			rotaY_Value = 0.1f;
			rotaY -= 0.1f;
			rotaZ_Value = 0.5f;
			rotaZ -= rotaZ_Value;

			transform.rotation = Quaternion.Euler(rotaX, rotaY, 0);

			if (transform.position.y < -10f)
			{
				gameObject.SetActive(false);
			}
		}
		else if (isExit)
		{
			speedY = 3;
			rotaY_Value = -1f;
			rotaY += rotaY_Value;
			velocity = gameObject.transform.rotation * new Vector3(0, speedY, 0);
			gameObject.transform.position += velocity * Time.deltaTime;
			transform.rotation = Quaternion.Euler(0, rotaY, 0);

			if (transform.position.y > 12)
			{

				gameObject.SetActive(false);
			}
		}
		else
		{
            aliveCnt += Time.deltaTime;
            //if(isWireles)
            //{
            //	wireles_DelayCnt += Time.deltaTime;
            //	if (wireles_DelayCnt > wireles_DelayMax)
            //	{
            //		isWireles = false;
            //		moaiAnime_Script.isOpen = true;
            //	}

            //}
            //else
            //{
            //	aliveCnt += Time.deltaTime;

            //}
        }


        if (attackLoopCnt >= 3)
		{
			moaiAnime_Script.isOpen = false;
		}

		if (isMouthOpen)
		{
			switch (attackState)
			{
				case AttackState.RingShot:
					if (ringShotCnt > ringShotMax)
					{
						isMouthOpen = false;
						moaiAnime_Script.isClose = true;
						attackState = AttackState.MiniMoai;
						ringShotCnt = 0;
						//moaiAttack_Script.ringShot_DelayCnt = 0;
					}

					break;

				case AttackState.MiniMoai:
					if (isMiniMoai)
					{
						isMouthOpen = false;
						moaiAnime_Script.isClose = true;
					}
					if (miniMoaisCnt > miniMoaisMax)
					{
						isMouthOpen = false;
						moaiAnime_Script.isClose = true;
						attackState = AttackState.Laser;
						miniMoaisCnt = 0;
					}

					break;

				case AttackState.Laser:
					if (isLaserEmd)
					{
						isMouthOpen = false;
						moaiAnime_Script.isClose = true;
						attackState = AttackState.RingShot;
						isLaserEmd = false;
						attackLoopCnt++;
					}
					break;

			}
		}

		if (aliveCnt > 135)
		{
			isExit = true;
		}

		if (hp < 1&& !isDead)
		{
			isDead = true;
			moai_rigidbody.useGravity = true;
			moaiAnime_Script.isOpen = false;
			moaiAnime_Script.isClose = false;
			rotaY = -90f;
			
			faceObj.layer= LayerMask.NameToLayer("Explosion"); 
			mouthObj.layer= LayerMask.NameToLayer("Explosion");
			MoaiDead();
			//for (int i = 0; i < object_material.Length; i++)
			//{
			//	object_material[i].material = self_material[i];
			//}

			//Died_Process();
		}

        //if (isDead)
        //{
        //	for (int i = 0; i < object_material.Length; i++)
        //	{
        //		object_material[i].material = self_material[i];
        //	}

        //}
        if (!isDead) 
		{
             base.Update();


        }
		HpColorChange();

		//for (int i = 0; i < self_material.Length; i++) self_material[i] = moai_material[i].material;


		void HpColorChange()
		{
			v_Value = 1.0f - transform.position.z * 0.015f;

			if (v_Value > 1.0f)
			{
				v_Value = 1.0f;
			}

			//test = 1 - hp / HpMax;
			color_Value = (float)hp / HpMax;

			//setColor = new Vector4(1 * v_Value, 1 * v_Value, 1 * v_Value, 1 * v_Value);
			//moaiColor = new Vector4(1 * v_Value, 1 * v_Value, 1 * v_Value, 1 * v_Value);

			for (int i = 0; i < moai_material.Length; i++)
			{
				moaiColor = new Vector4(1, color_Value, color_Value, 1);
				//moai_material[i].material = moai_material_save[i];
				moai_material[i].material.SetVector("_BaseColor", moaiColor);
				//moai_material_save[i].material.SetVector("_BaseColor", moaiColor);

			}

			//      foreach (Renderer renderer in object_material)
			//{
			//          renderer.material.SetVector("_BaseColor", setColor);
			//	//renderer.material.color = UnityEngine.Color.HSVToRGB(0, 0, v_Value);
			//}

		}
	}
	void MoaiDead()
	{
		//Game_Master.MY.Score_Addition(score, Opponent);
		SE_Manager.SE_Obj.SE_Explosion(Obj_Storage.Storage_Data.audio_se[22]);

		explosionEffect.gameObject.SetActive(true);

	}
}

