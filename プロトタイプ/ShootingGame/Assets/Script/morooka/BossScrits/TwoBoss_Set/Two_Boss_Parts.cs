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
using StorageReference;

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

	private int rism_Cnt
	{
		get; set;
	}
	private int rism_Index
	{

		get;set;
	}
	private int[] rism_Max
	{
		get; set;
	}       // リズム

	private ParticleSystem smoke { get; set; }

	private new void Start()
	{
		Initial_Position = transform.position;
		if (is_mul)
		{
			smoke = transform.GetChild(1).GetComponent<ParticleSystem>();
			smoke.Stop();
			rism_Cnt = 0;
			rism_Index = 0;
			rism_Max = new int[6] { 3, 4, 4, 9, 5, 6 };
		}
		else
		{
			base.Start();
			renderer = GetComponent<MeshRenderer>();
			Damege_Color = new Color(104.0f / 255.0f, 76.0f / 255.0f, 46.0f / 255.0f);
			Is_Bomb = false;
		}
	}
	private new void Update()
	{
		if (!is_mul)
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

	public void Bullet_Shot(Transform tra, int i = 7)
	{
		if (i < 7)
		{
			rism_Index = i;
		}

		Shot_Delay++;
		if (Shot_Delay >= Shot_DelayMax)
		{
			rism_Cnt++;
			Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, tra.position, tra.forward);
			if (rism_Cnt >= rism_Max[rism_Index])
			{
				rism_Index++;
				if (rism_Index == rism_Max.Length)
				{
					rism_Index = 0;
				}
				rism_Cnt = 0;
				Shot_Delay = 0;
			}
		}
	}
}
