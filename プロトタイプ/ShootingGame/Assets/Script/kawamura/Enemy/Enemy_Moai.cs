using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Moai : character_status
{
    GameObject saveObj;
	public Vector4 moaiColor;

	public Renderer[] moai_material;                                  // オブジェクトのマテリアル情報

	public float test;
	public float HpMax;
	public float bulletRota_Value;  //発射する弾の角度範囲用

	public bool isMouthOpen = false;
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
		HpColorChange();
		for (int i = 0; i < self_material.Length; i++) self_material[i] = moai_material[i].material;
		base.Update();
    }


	void HpColorChange()
	{
		v_Value = 1.0f - transform.position.z * 0.015f;

		if (v_Value > 1.0f)
		{
			v_Value = 1.0f;
		}

		//test = 1 - hp / HpMax;
		test = hp / HpMax;

		//setColor = new Vector4(1 * v_Value, 1 * v_Value, 1 * v_Value, 1 * v_Value);
		//moaiColor = new Vector4(1 * v_Value, 1 * v_Value, 1 * v_Value, 1 * v_Value);

		for (int i = 0; i < moai_material.Length; i++)
		{
			moaiColor = new Vector4(1, test,test, 1);
			moai_material[i].material.SetVector("_BaseColor", moaiColor);
		}

		//      foreach (Renderer renderer in object_material)
		//{
		//          renderer.material.SetVector("_BaseColor", setColor);
		//	//renderer.material.color = UnityEngine.Color.HSVToRGB(0, 0, v_Value);
		//}

	}
}
