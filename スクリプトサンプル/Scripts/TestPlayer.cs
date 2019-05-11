using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
	// 変数宣言---------------------------------------------------
	Shot shot;		// BulletShotPatternで定義されている関数(メソッド)を参照するための変数のようなもの
	//-----------------------------------------------------------

	void Start()
	{
		shot = BulletShotPattern.instance.ShotSector;
	}

	void Update()
	{
		// 生成する
		if (Input.GetKeyDown(KeyCode.Space))
		{
			shot(transform.position, 5, 45f);
		}
		// 生成パターンを切り替える
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			shot = BulletShotPattern.instance.ShotSector;
		}
		if (Input.GetKeyDown(KeyCode.RightShift))
		{
			shot = BulletShotPattern.instance.ShotOnce;
		}
	}
}
