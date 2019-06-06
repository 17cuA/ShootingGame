using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapCreate : MonoBehaviour
{
	//ワールド座標でのマップの左下の部分
	//ここから敵の配置などをしていく
	private const int up_left_pos_y = 5;
	private const int up_left_pos_x = -8;

	//シーンを切り替えるときにプレイヤーの死亡情報などを取得するための変数
	private SceneChanger SC;

	void Start()
    {
		if(SceneManager.GetActiveScene().name == "Stage")
		{
			SC = GetComponent<SceneChanger>();
			//OC = GetComponent<Object_Creation>();
			//csvフォルダからマップ情報を取得
			//１列ごとに取得
			CreateMap();			//マップの作成（各オブジェクトの移動）
			SC.Chara_Get();
		}
	}
	void CreateMap()
	{
		for (int y = 0; y < Obj_Storage.Storage_Data.CsvData.Count; y++)
		{
			for (int x = 0; x < Obj_Storage.Storage_Data.CsvData[y].Length; x++)
			{
				Obj_Storage.Storage_Data.pos = new Vector3(up_left_pos_x + x * 2, up_left_pos_y - y, 0);
				switch(Obj_Storage.Storage_Data.CsvData[y][x])
				{
					case "0":
						break;
					case "1":
						GameObject Player_obj = Obj_Storage.Storage_Data.Player.Active_Obj();
						Player_obj.transform.position = Obj_Storage.Storage_Data.pos;
						break;
					case "2":
						GameObject Enemy_obj = Obj_Storage.Storage_Data.Enemy1.Active_Obj();
						Enemy_obj.transform.position = Obj_Storage.Storage_Data.pos;
						break;
					case "3":
						GameObject Boss_obj = Obj_Storage.Storage_Data.Boss.Active_Obj();
						Boss_obj.transform.position = Obj_Storage.Storage_Data.pos;
						break;
					default:
						break;
				}
			}
		}
	}
}
