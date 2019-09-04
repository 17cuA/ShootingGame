//作成日2019/06/13
// 2番目のボスのパーツ管理
// 作成者:諸岡勇樹
/*
 * 2019/07/11　初期位置の確保
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Two_Boss_Parts : character_status
{
	[SerializeField] private GameObject[] supported_objects;
	private Vector3 Initial_Position { get; set; }

	private new void Start()
	{
		base.Start();
		Initial_Position = transform.position;
	}
	private new void Update()
	{
		base.Update();
		if (hp < 1)
		{
			if (supported_objects != null)
			{
				foreach (GameObject obj in supported_objects)
				{
					MeshRenderer ms = obj.GetComponent<MeshRenderer>();
					ms.enabled = false;
				}
			}
			base.Died_Judgment();
			base.Died_Process();
		}
	}
}
