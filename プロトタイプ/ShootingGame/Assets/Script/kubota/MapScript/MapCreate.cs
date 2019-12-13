using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapCreate : MonoBehaviour
{

	void Start()
    {
		//------------------------------------11.26 陳　追加---------------------------------


		//if(SceneManager.GetActiveScene().name == "Stage_01" 
		//	|| SceneManager.GetActiveScene().name == "Stage_02" 
		//	|| SceneManager.GetActiveScene().name == "Stage_03" 
		//	|| SceneManager.GetActiveScene().name == "Stage_04"
		//	 || SceneManager.GetActiveScene().name == "Stage_05"
		//	  || SceneManager.GetActiveScene().name == "Stage_06"
		//	   || SceneManager.GetActiveScene().name == "Stage_07")
		//{
		//	CreateMap();			//マップの作成（各オブジェクトの移動）
		//}
	}
	public void CreateMap()
	{
		GameObject Player_obj = Obj_Storage.Storage_Data.Player.Active_Obj();	//プレイヤー１をアクティブ状態に
		if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eONE_PLAYER)
		{
			Player_obj.transform.position = new Vector3(-2, 0, 0);				//初期位置へ移動
		}
		else if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
		{
			GameObject Player_obj2 = Obj_Storage.Storage_Data.Player_2.Active_Obj();	//プレイヤー2をアクティブ状態に

			Player_obj.transform.position = new Vector3(-2, 2, 0);			//初期位置へ移動
			Player_obj2.transform.position = new Vector3(-2, -2, 0);		//初期位置へ移動
		}
		else
		{
			Player_obj.transform.position = new Vector3(-2, 0, 0);              //初期位置へ移動
		}
	}
}
