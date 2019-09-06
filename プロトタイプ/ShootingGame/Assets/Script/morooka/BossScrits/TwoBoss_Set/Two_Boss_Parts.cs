//作成日2019/06/13
// 2番目のボスのパーツ管理
// 作成者:諸岡勇樹
/*
 * 2019/07/11　初期位置の確保
 * 2019/09/05　ID追加
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Two_Boss_Parts : character_status
{
	[SerializeField] private GameObject[] supported_objects;
	[SerializeField, Tooltip("ID")] private string id;
	[SerializeField, Tooltip("マルチプル")] private bool is_mul;
	public string ID
	{
		get { return id; }
	}
	[SerializeField, Tooltip("相手のID")] private string partner_id;
	public string Partner_ID
	{
		get { return partner_id; }
	}

	private Vector3 Initial_Position { get; set; }
	private MeshRenderer renderer { get; set; }
	private Color Damege_Color { get; set; }
	private bool Is_Bomb { get; set; }

	private new void Start()
	{
		base.Start();
		Initial_Position = transform.position;
		renderer = GetComponent<MeshRenderer>();
		Damege_Color = new Color(104.0f / 255.0f, 76.0f / 255.0f, 46.0f / 255.0f);
		Is_Bomb = false;
	}
	private new void Update()
	{
		if (is_mul)
		{
			if (hp < 1)
			{
				if (!Is_Bomb)
				{
					material_Reset();
					ParticleCreation(0);
					renderer.material.SetVector("_BaseColor", Damege_Color); 
					Is_Bomb = true;
					GetComponent<Collider>().enabled = false;
				}

			}
			else
			{
  				base.Update();
			}
		}
		else
		{
			base.Update();
			if (hp < 1)
			{
				if (supported_objects != null)
				{
					foreach (GameObject obj in supported_objects)
					{
						renderer.enabled = false;
					}
				}
				base.Died_Judgment();
				base.Died_Process();
			}
		}
	}
}
