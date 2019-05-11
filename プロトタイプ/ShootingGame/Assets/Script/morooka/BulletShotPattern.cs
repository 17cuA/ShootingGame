using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 3つの引数を持つShot型という関数を参照できる型を用意
// 関数が参照できればいいので名前は何でも問題ないが、今はわかりやすくShot型
delegate void Shot(Vector3 parentPosition, int num, float dispalaceAngle);

public class BulletShotPattern : MonoBehaviour
{
	// バレットのプレハブ、もしくはオブジェクトプール(バレットオブジェクトのclass名は仮なので元の型で合わせてください)
	//public Bullet bulletPrefab;
	////List<Bullet> bulletPool = new List<Bullet>();

	//// インスタンスを生成しておかないといけない(それしかやり方がわからない)のでHierarchyにオブジェクトとして置いておく
	//public static BulletShotPattern instance;

	//void Start()
	//{
	//	instance = FindObjectOfType<BulletShotPattern>();
	//}

	////--------------------------------------------------------------------
	////<! 下記に生成パターンを実装していく
	////<! 引数に必ずVector3型、int型、float型の引数を持たせること
	////<! 引数の順番は上記の順番に合わせること
	////<! 戻り値の型はvoid型にすること
	////<! というか定義したShot型に合わせること
	////--------------------------------------------------------------------

	///// <summary>
	///// 扇形に生成する
	///// </summary>
	///// <param name="parentPosition">生成するオブジェクトの位置</param>
	///// <param name="num">扇状に生成する数</param>
	///// <param name="direction">ずらす角度の度数</param>
	//public void ShotSector(Vector3 parentPosition, int num, float displaceAngle)
	//{
	//	// 実際の扇形に生成する処理
	//	for (int i = 0; i < num; ++i)
	//	{
	//		// 生成する数で180°を分割したラジアンを取得する
	//		float rad = (i / (float)num) * Mathf.PI + (displaceAngle * Mathf.Deg2Rad);
	//		// ラジアンをもとにx軸とy軸に振り分ける
	//		float x = Mathf.Cos(rad);
	//		float y = Mathf.Sin(rad);
	//		// 生成
	//		Bullet temp = Instantiate(bulletPrefab, parentPosition, bulletPrefab.transform.rotation);
	//		// 方角を設定する
	//		temp.Init(new Vector3(x, y, 0f));
	//		// ここで方角にオブジェクトの角度を合わせる(その都度調整が必要)
	//		temp.transform.eulerAngles = new Vector3(0f, 0f, (rad - 1f / 2f * Mathf.PI) * Mathf.Rad2Deg);
	//	}
	//}

	///// <summary>
	///// 一つ生成する
	///// </summary>
	///// <param name="parentPosition">生成するオブジェクトの位置</param>
	///// <param name="num">生成する数</param>
	///// <param name="direction">ずらす角度の度数</param>
	//public void ShotOnce(Vector3 parentPosition, int num, float displaceAngle)
	//{
	//	Bullet temp = Instantiate(bulletPrefab, parentPosition, bulletPrefab.transform.rotation);
	//	// ラジアンをもとにx軸とy軸に振り分ける
	//	float x = Mathf.Cos(displaceAngle * Mathf.Deg2Rad);
	//	float y = Mathf.Sin(displaceAngle* Mathf.Deg2Rad);
	//	// 方角を設定する
	//	temp.Init(new Vector3(x, y, 0f));
	//	// ここで方角にオブジェクトの角度を合わせる(その都度調整が必要)
	//	temp.transform.eulerAngles = new Vector3(0f, 0f, -displaceAngle);
	//}
}
