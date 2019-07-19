//作成者：川村良太
//エネミーのバーストショットのスクリプト
//バースト出発射する数、バーストの間隔、バースト中の弾の間隔を指定可能

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class Enemy_BurstShot : MonoBehaviour
{
	float Shot_Delay;                       //バーストとバーストの間隔時間を計る
	public int Shot_Delay_Max;                     //１つのバーストの間隔
	float burst_delay;                      //バーストの1発1発の間隔時間を計る
	public float burst_Delay_Max;           //バーストの1発1発の間隔
	public int burst_Num;                   //撃つバースト数
	int burst_Shotshot_Cnt;                 //何発撃ったかのカウント
	bool isBurst = false;                   //バーストを撃つかどうか
	private Transform Enemy_transform;  //自身のtransform
	private GameObject Bullet;  //弾のプレハブ、リソースフォルダに入っている物を名前から取得。
	void Start()
	{
		Enemy_transform = transform.parent;
		Bullet = Resources.Load("Bullet/Enemy_Bullet") as GameObject;
		burst_delay = 0;
		Shot_Delay = 0;
		burst_Shotshot_Cnt = 0;
	}

	void Update()
	{
        //親のtransformを代入
        Enemy_transform = transform.parent;

		if (isBurst)
		{
			//バーストショット関数呼び出し
			BurstShot();
		}

		else if (Shot_Delay > Shot_Delay_Max && transform.position.x < 9)
		{
			isBurst = true;
			Shot_Delay = 0;
		}
		else
		{
			Shot_Delay += Time.deltaTime;
		}
		void BurstShot()
		{
			if (burst_delay > burst_Delay_Max)
			{
                //弾生成
                Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, transform.position, transform.rotation);

                //Instantiate(Bullet, gameObject.transform.position, transform.rotation);

                //発射数カウントプラス
                ++burst_Shotshot_Cnt;
				//バースト計測リセット
				burst_delay = 0;
			}
			//バースト計測プラス
			burst_delay += Time.deltaTime;
			//バーストを指定の数撃ち切ったら
			if (burst_Shotshot_Cnt == burst_Num)
			{
				//バーストをfalse、発射数リセット
				isBurst = false;
				burst_Shotshot_Cnt = 0;
			}
		}

	}
}
