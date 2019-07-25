//作成日2019/06/13
// 一面のボスのパーツ管理
// 作成者:諸岡勇樹
/*
 * 2019/07/11　初期位置の確保
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class One_Boss_Parts : character_status
{

	private Vector3 Initial_Position { get; set; }

	private new void Start()
    {
		Initial_Position = transform.position;
    }
    private new void Update()
    {
        
    }
}
