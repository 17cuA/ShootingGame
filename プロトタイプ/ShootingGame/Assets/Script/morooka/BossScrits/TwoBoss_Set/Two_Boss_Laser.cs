//作成日2019/09/05
// 一面のボス本番_2匹目_レーザー挙動
// 作成者:諸岡勇樹
/*
 * 2019/09/05　作成
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Two_Boss_Laser : MonoBehaviour
{
	public float shot_speed;//弾の速度
	public float attack_damage;//ダメージの変数
	private string Parent_ID { get; set; }
	private string Partner_ID { get; set; }

	void Update()
	{
		// 画面外判定
		if (transform.position.x >= 25.0f || transform.position.x <= -25.0f
			|| transform.position.y >= 10.5f || transform.position.y <= -10.5f)
		{
			Delete_processing();
		}
	}

	private void LateUpdate()
	{
		Vector3 temp = transform.localPosition;
		temp.z += shot_speed;
		transform.localPosition = temp;
	}

	/// <summary>
	/// 回転等の軸になる親の設定
	/// </summary>
	/// <param name="parent"> 親になるトランスフォーム </param>
	public void Manual_Start(Transform parent)
	{
		transform.parent = parent;
		//transform.localScale = new Vector3(12.0f, 12.0f, 12.0f);

		Two_Boss_Parts parts = parent.GetComponent<Two_Boss_Parts>();
		if(parts != null)
		{
			Parent_ID = parts.ID;
			Partner_ID = parts.Partner_ID;
		}
	}

	// 当たり判定
	protected void OnTriggerEnter(Collider col)
	{
		if (gameObject.tag == "Enemy_Bullet" && col.gameObject.tag == "Player")
		{
			GameObject effect = Obj_Storage.Storage_Data.Effects[11].Active_Obj();
			ParticleSystem particle = effect.GetComponent<ParticleSystem>();
			effect.transform.position = gameObject.transform.position;
			particle.Play();
		}
		else	if (col.gameObject != transform.parent)
			{
				Debug.Log(col.name);
				Delete_processing();
			}
		
	}

	public void Delete_processing()
	{
		GameObject obj = gameObject;
		Obj_Storage.Storage_Data.Two_Boss_Laser.Set_Parent_Obj(ref obj);
		gameObject.SetActive(false);
	}
}
