using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Moai : character_status
{
    GameObject saveObj;
	public Vector4 moaiColor;

	public Renderer[] moai_material;                                  // オブジェクトのマテリアル情報
	public Material[] moai_material_save;
	 
	public float speedX;
	public float speedX_Value;
	public float speedY;
	public float speedY_Value;

	public float color_Value;
	public float HpMax;
	public float bulletRota_Value;  //発射する弾の角度範囲用

	public bool isAppearance = true;		//最初の登場用
	public bool isMove = false;
	public bool isMouthOpen = false;
	public bool isRingShot = true;
	public bool isMiniMoai = false;
	public bool isLaser = false;
	new void Start()
    {
		HpMax = hp;

		HP_Setting();
		base.Start();
    }


	new void Update()
    {
		if (hp < 1)
		{
			Died_Process();
		}
		base.Update();

		HpColorChange();
		//for (int i = 0; i < self_material.Length; i++) self_material[i] = moai_material[i].material;
    }


	void HpColorChange()
	{
		v_Value = 1.0f - transform.position.z * 0.015f;

		if (v_Value > 1.0f)
		{
			v_Value = 1.0f;
		}

		//test = 1 - hp / HpMax;
		color_Value = hp / HpMax;

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
