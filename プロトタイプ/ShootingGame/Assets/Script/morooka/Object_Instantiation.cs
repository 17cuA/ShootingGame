//作成日2019/06/13
// ストレージから呼び出す関数の管理
// 作成者:諸岡勇樹
/*
 * 2019/06/06	バレットの呼び出し関数の定義
 */
//----------------------------------------------------------------------------------
// using StorageReference;
// をいつもの場所に定義してください。
// 
// 使いたいタイミングで Unity の Instantiate(・・・) のように
// Object_Instantiation.Object_Reboot( 呼び出したいObjectの名前, ポジション, 向き)
// 呼び出してくださいね！！
//----------------------------------------------------------------------------------

using UnityEngine;

namespace StorageReference
{
	public class Object_Instantiation
	{
		/// <summary>
		///  オブジェクトの再起動
		/// </summary>
		/// <param name="name"> 撃ちたいバレットの名前 </param>
		/// <param name="pos"> 打ち出す場所 </param>
		/// <param name="direction"> 向き </param>
		static public GameObject Object_Reboot(string name, Vector3 pos, Vector3 direction)
		{
			GameObject obj = null;
			switch (name)
			{
				#region Player_Bullet
				case "Player_Bullet":
					obj = Obj_Storage.Storage_Data.PlayerBullet.Active_Obj();
					break;
				#endregion
				#region Enemy_Bullet_01
				case "Enemy_Bullet_01":
					obj = Obj_Storage.Storage_Data.EnemyBullet.Active_Obj();
					break;
				#endregion
				#region Enemy
				case "Enemy":
					obj = Obj_Storage.Storage_Data.Enemy1.Active_Obj();
					break;
				#endregion
				#region
				case "Beam_Bullet_Enemy":
					obj = Obj_Storage.Storage_Data.Beam_Bullet_E.Active_Obj();
					break;
				#endregion
				#region
				case "Player_Missile":
					obj = Obj_Storage.Storage_Data.PlayerMissile.Active_Obj();
					break;
				#endregion
				default:
					break;
			}

			if (obj == null) return obj;

			obj.transform.position = pos;
			obj.transform.right = direction;

			return obj;
		}

		/// <summary>
		///  オブジェクトの再起動
		/// </summary>
		/// <param name="name"> 撃ちたいバレットの名前 </param>
		/// <param name="pos"> 打ち出す場所 </param>
		/// <param name="direction"> 向き </param>
		static public GameObject Object_Reboot(string name, Vector3 pos, Quaternion direction)
		{
			GameObject obj = null;
			switch (name)
			{
				#region Player_Bullet
				case "Player_Bullet":
					obj = Obj_Storage.Storage_Data.PlayerBullet.Active_Obj();
					break;
				#endregion
				#region Enemy_Bullet_01
				case "Enemy_Bullet_01":
					obj = Obj_Storage.Storage_Data.EnemyBullet.Active_Obj();
					break;
				#endregion
				#region Enemy
				case "Enemy":
					obj = Obj_Storage.Storage_Data.Enemy1.Active_Obj();
					break;
				#endregion
				default:
					break;
			}

			if (obj == null) return obj;

			obj.transform.position = pos;
			obj.transform.rotation = direction;

			return obj;
		}
	}
}