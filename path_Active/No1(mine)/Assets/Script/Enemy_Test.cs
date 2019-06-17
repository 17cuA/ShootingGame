/*
 * 敵キャラのテスト用スクリプト
 * Database_Managerを使って情報を取得
 * 
 * 2019/06/14	csvからのファイル作成ではなく、ボタンを設置し、それが押されたらセーブ
 */
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CSV_Management;

public class Enemy_Test : MonoBehaviour
{
	[SerializeField]public Database_Manager Enemy_Data{private set; get;}       // エネミーのデータベース
	float[,] Float_Data;
	int first = 0;
	LineRenderer LC;			//Linerendererの情報取得
	public string name;		//使用したいcsvデータの名前
	void Start()
	{
		LC = GameObject.Find("Beje").GetComponent<LineRenderer>();
		if (Enemy_Data == null　|| name != null)
		{
			Enemy_Data = new Database_Manager(name);
			Float_Data = new float[Enemy_Data.Database_Array.GetLength(0), Enemy_Data.Database_Array.GetLength(1)];
			for (int i = 0; i < Enemy_Data.Database_Array.GetLength(0); i++)
			{
				for (int j = 0; j < Enemy_Data.Database_Array.GetLength(1); j++)
				{
					Float_Data[i, j] = Enemy_Data.ToFloat(i, j);
				}
			}

		}
	}

    // Update is called once per frame
    void Update()
    {
		if (Enemy_Data == null || name != null)
		{
			transform.position = new Vector3(Float_Data[first, 0], Float_Data[first, 1], Float_Data[first, 2]);

			if (first >= Enemy_Data.Database_Array.GetLength(0) - 1)
			{
				first = 0;
			}
			else
			{
				first++;
			}
		}
    }
}
