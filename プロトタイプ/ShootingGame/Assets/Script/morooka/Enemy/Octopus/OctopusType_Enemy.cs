//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2019/11/15
//----------------------------------------------------------------------------------------------
// タコ型エネミーの挙動
//----------------------------------------------------------------------------------------------
// 2019/11/15　移動の挙動
// 2019/11/19　ジャンプ一回に一回攻撃
// 2019/11/25　攻撃アニメーション設定
//----------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class OctopusType_Enemy : character_status
{
	// 移動の方向
	public enum DIRECTION
	{
		eUP　= 1,				// 上はね
		eDOWN = -1,			// 下はね
	}

	//左右どちらに動くか
	public enum DIRECTION_HORIZONTAL
	{
		eLEFT,
		eRIGHT,

	}
	// アニメーションの種類管理
	public enum OCTOPUS_ANIMATION
	{
		eDEFA,			// デフォルト
		eOPEN,			// オープン
		eCLOSE,		// クローズ
	}

	[Header("コライダー関係")]
	[SerializeField,Tooltip ("前の判定")] private Parts_Collider flomtCollieder;
	[SerializeField,Tooltip ("下のコライダー")] private Parts_Collider downCollieder;

	[Header("移動関係")]
	[SerializeField, Tooltip("ジャンプ力")] private float jumpPower;
	[SerializeField, Tooltip("横移動速度")] private float horizontalMovementSpeed;
	[SerializeField, Tooltip("落下速度")] private float fallSpeed;
	[SerializeField, Tooltip("落下向き")] public DIRECTION bottomDirection;
	[SerializeField, Tooltip("左右移動向き")] public DIRECTION_HORIZONTAL direc_Horizon;
	[SerializeField, Tooltip("回転速度")] private float rotationalSpeed;

	[Header("攻撃関係")]
	[SerializeField, Tooltip("弾数")] private int numberBullets;
	[SerializeField, Tooltip("攻撃頻度")] private int attackFrequency;
	[SerializeField, Tooltip("アニメーション")] private Animation animationAssets;

	private Rigidbody rigidbody;								// リジッドボディ
	public float horizontalMovementDirection;			// 横移動の向き(1で右、-1で左)
	private Vector3 FallingDirection;							// 落下向き
	private bool Is_Turn;											// 回転するか
	private bool Is_EndAttackMotion;							// 攻撃モーションが終わったか
	private float TotalRotation;									// 回転した総量
	private Vector3 StockVelocity;								// ベロシティの一時保存
	private Vector3[] BulletDirection;							// 弾出る方向
	private int NumberJumps;									// ジャンプ回数
	private OCTOPUS_ANIMATION AnimationType;        // 再生中のアニメーション保存

	private string[] AnimationName = new string[3]
	{
		"defo",
		"Open",
		"Close"
	};

	new private void Start()
    {
		rigidbody = GetComponent<Rigidbody>();

		switch (bottomDirection)
		{
			case DIRECTION.eUP:
				transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
				break;
			case DIRECTION.eDOWN:
				transform.rotation = Quaternion.identity;
				break;
			default:
				break;
		}


		FallingDirection.y = (float)bottomDirection;

		Vector3 temp = rigidbody.velocity;
		temp.x += horizontalMovementDirection * horizontalMovementSpeed;
		rigidbody.velocity = temp;

		float temp_2 = 360.0f / (float)numberBullets;
		BulletDirection = new Vector3[numberBullets];
		for (int i = 0; i < numberBullets; i++)
		{
			BulletDirection[i].z = i * temp_2;
		}

		AnimationType = OCTOPUS_ANIMATION.eDEFA;

		base.Start();
	}

	new private void Update()
    {
		base.Update();
		// 左右当たり
		if (flomtCollieder.Is_HitRayCast)
		{
			if (flomtCollieder.HitObject.transform.tag == "Wall")
			{
				// 移動向き変更
				horizontalMovementDirection *= -1.0f;

				Vector3 temp = rigidbody.velocity;
				temp.x += (horizontalMovementDirection * horizontalMovementSpeed) * 2.0f;
				rigidbody.velocity = temp;

				// 当たり判定の向き変更
				Vector3 temp_2 = flomtCollieder.gameObject.transform.eulerAngles;
				temp_2.z += 180.0f;
				flomtCollieder.gameObject.transform.eulerAngles = temp_2;
			}
		}
		// 底面当たり
		if (downCollieder.Is_HitRayCast)
		{
			if (downCollieder.HitObject.transform.tag == "Wall")
			{
				// 底面の向きに合わせて飛び跳ね
				transform.up = downCollieder.HitObject.normal;
				rigidbody.velocity = downCollieder.HitObject.normal * jumpPower;

				Vector3 temp = rigidbody.velocity;
				temp.x += horizontalMovementDirection * horizontalMovementSpeed;
				rigidbody.velocity = temp;

				// オブジェクトの向きを合わせる
				FallingDirection = downCollieder.HitObject.normal * -1.0f;

				// 攻撃リセット
				Is_EndAttackMotion = false;

				NumberJumps++;
			}
		}

		// ターンしないとき
		if (!Is_Turn)
		{
			// 底面方向に落下
			rigidbody.velocity += FallingDirection * fallSpeed;

			// 縦の移動速度が0に近づいたとき、まだ攻撃していないとき
			if (Mathf.Abs(rigidbody.velocity.y) < 0.1f && !Is_EndAttackMotion && NumberJumps >= attackFrequency)
			{
				// ベロシティ一時保存、ベロシティ0に
				StockVelocity = rigidbody.velocity;
				rigidbody.velocity = Vector3.zero;
				// ターン開始
				Is_Turn = true;

				// オープンアニメーション再生
				animationAssets.Play(AnimationName[(int)OCTOPUS_ANIMATION.eOPEN]);
			}
		}
		// ターンするとき
		else if (Is_Turn)
		{
			// 別途総回転量の保存
			TotalRotation += rotationalSpeed;
			// 回転
			transform.Rotate(new Vector3(0.0f, rotationalSpeed, 0.0f));

			// 総回転量180以上のとき、攻撃していないとき
			if (TotalRotation >= 180.0f && !Is_EndAttackMotion)
			{
				// 撃ちだし
				foreach (var dir in BulletDirection)
				{
					GameObject obj = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, transform.position, Quaternion.Euler(dir));
				}
				Is_EndAttackMotion = true;
			}
			// 総回転量360以上のとき
			else if (TotalRotation >= 360.0f)
			{
				// ベロシティを戻して回転終了
				rigidbody.velocity = StockVelocity;
				TotalRotation = 0.0f;
				NumberJumps = 0;

			// クローズアニメーション再生
			animationAssets.Play(AnimationName[(int)OCTOPUS_ANIMATION.eCLOSE]);

			Is_Turn = false;
			}
		}
	}
}
