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
		static public void Object_Reboot(string name, Vector3 pos, Vector3 direction)
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

			if (obj == null) return;

			obj.transform.position = pos;
			obj.transform.right = direction;
		}
	}
}