/*
 * プレイヤーの挙動作成
 * 久保田 達己
 * 
 * 2019/05/28	カットイン時は操作できないようにした
 * 2019/06/07	陳さんの作ったパワーアップ処理統合
 */
using UnityEngine;
using Power;
using StorageReference;
//using Power;
public class Player2 : character_status
{
	private const float number_Of_Directions = 1.0f;    //方向などを決める時使う定数
	private Vector3 vector3;    //進む方向を決める時に使う
	private float x;    //x座標の移動する時に使う変数
	private float y;    //y座標の移動する時に使う変数
	//グリッド用の変数---------------------------------------
	Vector3 MOVEX = new Vector3(0.166f, 0, 0); // x軸方向に１マス移動するときの距離
	Vector3 MOVEY = new Vector3(0, 0.166f, 0); // y軸方向に１マス移動するときの距離
	public Vector3 target;      // 入力受付時、移動後の位置を算出して保存 
	public float step = 10f;     // 移動速度
	Vector3 prevPos;     // 何らかの理由で移動できなかった場合、元の位置に戻すため移動前の位置を保存
	//----------------------------------------------------

	public Quaternion Direction;   //オブジェクトの向きを変更する時に使う  
	public GameObject shot_Mazle;       //プレイヤーが弾を放つための地点を指定するためのオブジェクト
	private Obj_Storage OS;             //ストレージからバレットの情報取得

	public int invincible_time;              //無敵時間計測用
	public int invincible_Max;          //無敵時間最大時間
	public bool invincible;             //無敵時間帯かどうか
	private Color first_color;          //初期の色を保存しておくようの画像
	public bool activeMissile;        //ミサイルは導入されたかどうか
	public int bitIndex = 0;        //オプションの数


	[SerializeField] private ParticleSystem injection;           //ジェット噴射のエフェクトを入れる
	public ParticleSystem particleSystem;                           //ジェット噴射自体のパーティクルシステム
	private ParticleSystem.MainModule particleSystemMain;   //☝の中のメイン部分（としか言いようがない）
	[SerializeField] private ParticleSystem shield_Effect;       //シールドのエフェクトを入れる
	[SerializeField] private ParticleSystem resporn_Injection;  //復活時のジェット噴射エフェクトを入れる
		//ジェット噴射用の数値-------------------------------
	public const float baseInjectionAmount = 0.2f;          //基本噴射量
	public const float additionalInjectionAmount = 0.1f;    //加算噴射量
	public const float subtractInjectionAmount = 0.1f;      //減算噴射量
    //------------------------------------------------------

	public float swing_facing;              // 旋回向き
	public float facing_cnt;                    // 旋回カウント
	public int shoot_number;                //弾を連続して撃った時の数をカウントするための変数

	public GameObject Laser;                //レーザーのobjectをOnにするために行う処理

	private int missile_dilay_cnt;              // ミサイルの発射間隔カウンター
	public int missile_dilay_max;               // ミサイルの発射間隔

	//public Line_Beam line_beam;

	public enum Bullet_Type  //弾の種類
	{
		Single,
		Double,
		Laser,
	}
	public Bullet_Type bullet_Type; //弾の種類を変更
									//リスポーン時に使用する変数---------------------------------------------------------------------
	private bool Is_Resporn;    //生き返った瞬間かどうか（アニメーションを行うかどうかの判定）
	private float startTime = 0.0f;
	//-----------------------------------------------------------------------
	public ParticleSystem[] effect_mazle_fire = new ParticleSystem[5];  //マズルファイアのエフェクト（unity側の動き）
	private int effect_num = 0; //何番目のマズルフラッシュが稼働するかの
	private float min_speed;        //初期の速度を保存しておくよう変数
									//復活時のエフェクト用変数-------------------------------------
	private int cnt;                        // マテリアルを切り替えるに使用する
	public bool Is_Change;              //マテリアルを切り替える際どちらの色にするかの判定用			
										//--------------------------------------------------------

	public bool Is_Change_Auto;
	public bool IS_Active;

    public int Bullet_cnt;          //バレットの発射数をかぞえる変数
    private int Bullet_cnt_Max;     //バレットの発射数の最大値を入れる変数
	//プレイヤーがアクティブになった瞬間に呼び出される
	private void OnEnable()
	{
		//プール化したため、ここでイベント発生時の処理を入れとく
		//パワーアップの処理が行われる際に読み込まれる関数
		P2_PowerManager.Instance.AddFunction(P2_PowerManager.Power.PowerType.SPEEDUP, SpeedUp);
		P2_PowerManager.Instance.AddFunction(P2_PowerManager.Power.PowerType.INITSPEED, Init_speed);
		P2_PowerManager.Instance.AddFunction(P2_PowerManager.Power.PowerType.MISSILE, ActiveMissile);
		P2_PowerManager.Instance.AddFunction(P2_PowerManager.Power.PowerType.DOUBLE, ActiveDouble);
		P2_PowerManager.Instance.AddFunction(P2_PowerManager.Power.PowerType.LASER, ActiveLaser);
		P2_PowerManager.Instance.AddFunction(P2_PowerManager.Power.PowerType.OPTION, CreateBit);
		P2_PowerManager.Instance.AddFunction(P2_PowerManager.Power.PowerType.SHIELD, ActiveShield);
		//死んだり、バレットの種類が変わったりする際に呼ばれる関数
		P2_PowerManager.Instance.AddCheckFunction(P2_PowerManager.Power.PowerType.SPEEDUP, () => { return hp < 1; }, () => { Init_speed(); });
		P2_PowerManager.Instance.AddCheckFunction(P2_PowerManager.Power.PowerType.MISSILE, () => { return hp < 1; }, () => { activeMissile = false; });
		P2_PowerManager.Instance.AddCheckFunction(P2_PowerManager.Power.PowerType.DOUBLE, () => { return hp < 1 || bullet_Type == Bullet_Type.Laser; }, () => { Reset_BulletType(); });
		P2_PowerManager.Instance.AddCheckFunction(P2_PowerManager.Power.PowerType.LASER, () => { return hp < 1 || bullet_Type == Bullet_Type.Double; }, () => {
		Reset_BulletType();
		});
		///////////////////////
		P2_PowerManager.Instance.AddCheckFunction(P2_PowerManager.Power.PowerType.SHIELD, () => { return Get_Shield() < 1; }, () => { activeShield = false; });
	}
	//プレイヤーのアクティブが切られたら呼び出される
	private void OnDisable()
	{
		P2_PowerManager.Instance.RemoveFunction(P2_PowerManager.Power.PowerType.SPEEDUP, SpeedUp);
		P2_PowerManager.Instance.RemoveFunction(P2_PowerManager.Power.PowerType.MISSILE, ActiveMissile);
		P2_PowerManager.Instance.RemoveFunction(P2_PowerManager.Power.PowerType.DOUBLE, ActiveDouble);
		P2_PowerManager.Instance.RemoveFunction(P2_PowerManager.Power.PowerType.LASER, ActiveLaser);
		P2_PowerManager.Instance.RemoveFunction(P2_PowerManager.Power.PowerType.OPTION, CreateBit);
		P2_PowerManager.Instance.RemoveFunction(P2_PowerManager.Power.PowerType.SHIELD, ActiveShield);
		P2_PowerManager.Instance.RemoveCheckFunction(P2_PowerManager.Power.PowerType.SPEEDUP, () => { return hp < 1; }, () => { speed = min_speed; });
		P2_PowerManager.Instance.RemoveCheckFunction(P2_PowerManager.Power.PowerType.MISSILE, () => { return hp < 1; }, () => { activeMissile = false; });
		P2_PowerManager.Instance.RemoveCheckFunction(P2_PowerManager.Power.PowerType.DOUBLE, () => { return hp < 1 || bullet_Type == Bullet_Type.Laser; }, () => { Reset_BulletType(); });
		P2_PowerManager.Instance.RemoveCheckFunction(P2_PowerManager.Power.PowerType.LASER, () => { return hp < 1 || bullet_Type == Bullet_Type.Double; }, () => { Reset_BulletType(); /*Laser.SetActive(false);*/ });
		P2_PowerManager.Instance.RemoveCheckFunction(P2_PowerManager.Power.PowerType.SHIELD, () => { return Get_Shield() < 1; }, () => { Set_Shield(3); activeShield = false; });
	}
	new void Start()
	{
		base.Start();
		//各種値の初期化とアタッチされているコンポーネントの情報を取得
		shot_Mazle = gameObject.transform.Find("Bullet_Fire").gameObject;
		vector3 = Vector3.zero;
		Direction = transform.rotation;
		hp = 1;
		HP_Setting();
		Type = Chara_Type.Player;
		//-----------------------------------------------------------------
		bullet_Type = Bullet_Type.Single;   //初期状態をsingleに
		direction = transform.position;
		Set_Shield(3);                                     //シールドに防御可能回数文の値を入れる
		particleSystemMain = particleSystem.main;
		//プレイヤーの各弾や強化のものの判定用変数に初期値の設定
		activeShield = false;
		activeMissile = false;
		injection.Play();   //ジェット噴射のパーティクルを稼働状態に
		shield_Effect.Stop();//シールドのエフェクトを動かさないようにする
		resporn_Injection.Stop();//復活時ジェット噴射を動かさないようにする
		base.Start();
		Is_Resporn = false;
		startTime = 0;
		for (int i = 0; i < effect_mazle_fire.Length; i++) effect_mazle_fire[i].Stop(); //複数設定してある、マズルファイアのエフェクトをそれぞれ停止状態にする
		effect_num = 0;
		min_speed = speed;      //初期の速度を保存しておく
		Laser.SetActive(false); //レーザーの子供が動かないようにするための変数
		P2_PowerManager.Instance.ResetAllPowerUpgradeCount();      //二週目以降からパワーアップしたものをリセットするメソッド
		P2_PowerManager.Instance.ResetSelect();            //プレイヤーのアイテム取得回数をリセットするメソッド
		Is_Change = false;
		Is_Change_Auto = false;
		IS_Active = true;
        Bullet_cnt_Max = 8;
		target = direction;
	}

	new void Update()
	{
		//デバックキー
		if (Input.GetKeyDown(KeyCode.Alpha6)) IS_Active = !IS_Active;

		//稼働状態なら動かす
		if (IS_Active)
		{
			//復活時のアニメーション
			if (Is_Resporn)
			{
				resporn_Injection.Play();
				//Debug.Log("hei");
				capsuleCollider.enabled = false;
				startTime += Time.deltaTime;
				transform.position = Vector3.Slerp(new Vector3(-9, 0, -30), direction, startTime);

				if (transform.position == direction)
				{
					resporn_Injection.Stop();
					startTime = 0;
					Is_Resporn = false;
				}
			}
			else
			{
				//-------------------------------
				//デバックの工程
				if (Input.GetKeyDown(KeyCode.Alpha1)) Damege_Process(1);
				if (Input.GetKeyDown(KeyCode.Alpha2)) PowerManager.Instance.Pick();
				if (Input.GetKeyDown(KeyCode.Alpha3)) hp = 1000;
				if (Input.GetKeyDown(KeyCode.Alpha4))
				{
					hp = 0;
					Remaining--;
					Debug.Log("hei");
					ParticleCreation(0);        //爆発のエフェクト発動
					Reset_Status();             //体力の修正
					invincible = false;         //無敵状態にするかどうかの処理
					invincible_time = 0;        //無敵時間のカウントする用の変数の初期化
					bullet_Type = Bullet_Type.Single;
					Is_Resporn = true;
					Laser.SetActive(false);
					return;
				}
				if (Input.GetKeyDown(KeyCode.Alpha5)) Remaining++;
				//---------------------------

				P2_PowerManager.Instance.Update();
				//ビットン数をパワーマネージャーに更新する
				P2_PowerManager.Instance.UpdateBit(bitIndex);

				//if(shield < 1)
				//{
				//	PowerManager.Instance.ResetShieldPower();
				//	shield_Effect.Play(false);
				//}
				if (hp < 1)
				{
					if (Laser.activeSelf) { Laser.SetActive(false); }   //もし、レーザーが稼働状態であるならば、非アクティブにする
					P2_PowerManager.Instance.ResetSelect();                //アイテム取得回数をリセットする
					Remaining--;                                        //残機を1つ減らす
																		//残機が残っていなければ
					if (Remaining < 1)
					{
						//残機がない場合死亡
						Died_Process();

					}
					//残機が残っていたら
					else
					{
						ParticleCreation(0);        //爆発のエフェクト発動
						Reset_Status();             //体力の修正
													//gameObject.transform.position = direction;      //初期位置に戻す
													//if (laser.isPlaying) laser.Stop();               //レーザーを稼働状態の時、停止状態にする
						invincible = true;         //無敵状態にするかどうかの処理
						invincible_time = 0;        //無敵時間のカウントする用の変数の初期化
						bullet_Type = Bullet_Type.Single;       //撃つ弾の種類を変更する
						Is_Resporn = true;                      //復活用の処理を行う
					}
				}
				//無敵時間の開始
				Invincible();

				//プレイヤーの移動処理
				if (transform.position == target)
				{
					//MoveX();
					SetTargetPosition();
				}

				Move();

				//プレイヤーの移動処理
				//Player_Move();

				//弾の発射（Fire2かSpaceキーで撃てる）
				if (Shot_Delay > Shot_DelayMax)
				{
					//弾を射出
					Bullet_Create();
				}
				//パワーアップ処理
				if (Input.GetKeyDown(KeyCode.X) || Input.GetButton("P2_Fire2"))
				{
					//アイテムを規定数所持していたらその値と同じものの効果を得る
					P2_PowerManager.Instance.Upgrade();
				}
				// 通常のバレットのディレイ計算
				Shot_Delay++;
				// ミサイルのディレイ計算
				missile_dilay_cnt++;
			}
		}
		else
		{
			capsuleCollider.enabled = false;
		}
	}
	void SetTargetPosition()
	{
		x = Input.GetAxis("Horizontal");            //x軸の入力
		y = Input.GetAxis("Vertical");              //y軸の入力

		//プレイヤーの移動に上下左右制限を設ける
		if (transform.position.y >= 4.5f && y > 0) y = 0;
		if (transform.position.y <= -4.5f && y < 0) y = 0;
		if (transform.position.x >= 17.0f && x > 0) x = 0;
		if (transform.position.x <= -17.0f && x < 0) x = 0;

		prevPos = target;

		// プレイヤー機体の旋回
		// プレイヤーの向き(Y軸の正負)で角度算出
		if (transform.eulerAngles.x != (swing_facing * y))
		{
			// 参考にしたURL↓
			// https://tama-lab.net/2017/06/unity%E3%81%A7%E3%82%AA%E3%83%96%E3%82%B8%E3%82%A7%E3%82%AF%E3%83%88%E3%82%92%E5%9B%9E%E8%BB%A2%E3%81%95%E3%81%9B%E3%82%8B%E6%96%B9%E6%B3%95%E3%81%BE%E3%81%A8%E3%82%81/
			// Unity にある Mathf.LerpAngle 関数を使用
			float angle = Mathf.LerpAngle(0.0f, (swing_facing * y), facing_cnt / 10.0f);
			transform.eulerAngles = new Vector3(angle, 0, 0);
			facing_cnt++;
		}
		else
		{
			facing_cnt = 0;
		}

		//右上
		if (x > 0 && y > 0)
		{
			target = transform.position + MOVEX + MOVEY;

		}
		//右下
		else if (x > 0 && y < 0)
		{
			target = transform.position + MOVEX - MOVEY;

		}
		//左下
		else if (x < 0 && y < 0)
		{
			target = transform.position - MOVEX - MOVEY;

		}
		//左上
		else if (x < 0 && y > 0)
		{
			target = transform.position - MOVEX + MOVEY;

		}
		//上
		else if (y > 0)
		{
			target = transform.position + MOVEY;

		}
		//右
		else if (x > 0)
		{
			target = transform.position + MOVEX;

		}
		//下
		else if (y < 0)
		{
			target = transform.position - MOVEY;

		}
		//左
		else if (x < 0)
		{
			target = transform.position - MOVEX;

		}

	}
	void Move()
	{
		transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
	}

	//コントローラーの操作
	private void Player_Move()
	{
		x = Input.GetAxis("P2_Horizontal");            //x軸の入力
		y = Input.GetAxis("P2_Vertical");              //y軸の入力

		//プレイヤーの移動に上下左右制限を設ける
		if (transform.position.y >= 4.5f && y > 0) y = 0;
		if (transform.position.y <= -4.5f && y < 0) y = 0;
		if (transform.position.x >= 17.0f && x > 0) x = 0;
		if (transform.position.x <= -17.0f && x < 0) x = 0;

		vector3 = new Vector3(x, y, 0);     //移動のベクトルをvector3に入れる
											// プレイヤー機体の旋回
											// プレイヤーの向き(Y軸の正負)で角度算出
		if (transform.eulerAngles.x != (swing_facing * y))
		{
			// 参考にしたURL↓
			// https://tama-lab.net/2017/06/unity%E3%81%A7%E3%82%AA%E3%83%96%E3%82%B8%E3%82%A7%E3%82%AF%E3%83%88%E3%82%92%E5%9B%9E%E8%BB%A2%E3%81%95%E3%81%9B%E3%82%8B%E6%96%B9%E6%B3%95%E3%81%BE%E3%81%A8%E3%82%81/
			// Unity にある Mathf.LerpAngle 関数を使用
			float angle = Mathf.LerpAngle(0.0f, (swing_facing * y), facing_cnt / 10.0f);
			transform.eulerAngles = new Vector3(angle, 0, 0);
			facing_cnt++;
		}
		else
		{
			facing_cnt = 0;
		}
		//右入力
		if (0 < x)
		{
			//噴射量の変更(基本噴射量 + 加算用噴射量 * 入力割合)
			particleSystemMain.startLifetime = baseInjectionAmount + additionalInjectionAmount * x;
		}
		//左入力
		else if (x < 0)
		{
			//噴射量の変更(基本噴射量 + 減算用噴射量 * 入力割合)
			particleSystemMain.startLifetime = baseInjectionAmount + subtractInjectionAmount * x;
		}
		else if (x == 0)
		{
			//噴射量を規定の値に戻す
			particleSystemMain.startLifetime = baseInjectionAmount;
		}
		//位置情報の更新
		transform.position = transform.position + vector3 * Time.deltaTime * speed;
	}
	//無敵時間（色の点滅も含め）
	private void Invincible()
	{
		if (invincible)
		{
			if (invincible_time > invincible_Max)
			{
				invincible = false;
			}
			else
			{
				Change_Material(2);
			}
			invincible_time++;          //フレーム管理
			if (capsuleCollider.enabled == true) capsuleCollider.enabled = false;    //規定のコライダーをオフに変更
		}
		else
		{
			for (int i = 0; i < object_material.Length; i++)
			{

				object_material[i].material = Get_self_material(i);
			}

			if (capsuleCollider.enabled == false) capsuleCollider.enabled = true;   //カプセルコライダーをオンにする
		}
	}

	//プレイヤーの方向転換
	private void Change_In_Direction()
	{
		//方向に−１をかけて反転した物を入れる
		Direction *= new Quaternion(0, -1, 0, 0);
		transform.rotation = Direction;
	}
	//弾の発射
	public void Bullet_Create()
	{
		if (Input.GetButtonDown("P2_Fire3"))
		{
			Debug.Log("モードチェンジ");
			Is_Change_Auto = !Is_Change_Auto;
		}

		if (!Is_Change_Auto)
		{
			//マニュアル操作
			if (Input.GetButtonDown("P2_Fire1") || Input.GetKeyDown(KeyCode.Space))
			{
				Shot_Delay = 0;

				switch (bullet_Type)
				{
					case Bullet_Type.Single:
						Single_Fire();
						effect_mazle_fire[effect_num].Play();
						effect_num++;
						break;
					case Bullet_Type.Double:
						Double_Fire();
						effect_mazle_fire[effect_num].Play();
						effect_num++;
						break;
					default:
						break;
				}
				if (effect_num > 4)
				{
					effect_num = 0;
				}
				if (activeMissile && missile_dilay_cnt > missile_dilay_max)
				{
					Missile_Fire();
					missile_dilay_cnt = 0;
				}

			}
		}
		else
		{
			//オート操作
			if (Input.GetButton("P2_Fire1") || Input.GetKey(KeyCode.Space))
			{
				// 連続で4発まで撃てるようにした
				if (shoot_number < 5)
				{
					switch (bullet_Type)
					{
						case Bullet_Type.Single:
							Single_Fire();
							effect_mazle_fire[effect_num].Play();
							effect_num++;
							shoot_number++;

							break;
						case Bullet_Type.Double:
							Double_Fire();
							effect_mazle_fire[effect_num].Play();
							effect_num++;
							shoot_number++;
							break;
						default:
							break;
					}
					if (activeMissile && missile_dilay_cnt > missile_dilay_max)
					{
						Missile_Fire();
						missile_dilay_cnt = 0;
					}
					Shot_Delay = 0;

				}
				// 4発撃った後、10フレーム程置く
				else if (shoot_number == 15)
				{
					shoot_number = 0;
					effect_num = 0;
				}
				else
				{
					shoot_number++;

				}
			}
			if (Input.GetButtonUp("P2_Fire1") || Input.GetKey(KeyCode.Space))
			{
				shoot_number = 0;
			}
			if (effect_num > 4)
			{
				effect_num = 0;
			}

		}
	}

	private void Single_Fire()
	{
		Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePLAYER2_BULLET, shot_Mazle.transform.position, Direction);
		SE_Manager.SE_Obj.SE_Active(Obj_Storage.Storage_Data.audio_se[4]);
	}
	private void Double_Fire()
	{
		Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePLAYER2_BULLET, shot_Mazle.transform.position, Direction);
		Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePLAYER2_BULLET, shot_Mazle.transform.position, Quaternion.Euler(0, 0, 45));
		SE_Manager.SE_Obj.SE_Active(Obj_Storage.Storage_Data.audio_se[4]);
	}
	//	ミサイルの発射
	private void Missile_Fire()
	{
		GameObject obj = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePLAYER_MISSILE, shot_Mazle.transform.position, Direction);
		obj.GetComponent<Missile>().Setting_On_Reboot(1);
	}
	//プレイヤーの速度上昇
	private void SpeedUp()
	{
		speed *= 1.1f;
		Debug.Log("スピードUP");
		GameObject effect = Obj_Storage.Storage_Data.Effects[6].Active_Obj();
		ParticleSystem particle = effect.GetComponent<ParticleSystem>();
		effect.transform.position = gameObject.transform.position;
		particle.Play();
		Voice_Manager.VOICE_Obj.Voice_Active(Obj_Storage.Storage_Data.audio_voice[12]);
		SE_Manager.SE_Obj.SE_Active_2(Obj_Storage.Storage_Data.audio_se[16]);

	}
	//ミサイルをアクティブに
	private void ActiveMissile()
	{
		activeMissile = true;
		Debug.Log("ミサイル導入");
		GameObject effect = Obj_Storage.Storage_Data.Effects[6].Active_Obj();
		ParticleSystem particle = effect.GetComponent<ParticleSystem>();
		effect.transform.position = gameObject.transform.position;
		particle.Play();
		Voice_Manager.VOICE_Obj.Voice_Active(Obj_Storage.Storage_Data.audio_voice[13]);
		SE_Manager.SE_Obj.SE_Active_2(Obj_Storage.Storage_Data.audio_se[16]);

	}
	private void ActiveDouble()
	{
		Debug.Log("ダブル導入");
		if (Laser.activeSelf) { Laser.SetActive(false); }   //もし、レーザーが稼働状態であるならば、非アクティブにする
		bullet_Type = Bullet_Type.Double;
		GameObject effect = Obj_Storage.Storage_Data.Effects[6].Active_Obj();
		ParticleSystem particle = effect.GetComponent<ParticleSystem>();
		effect.transform.position = gameObject.transform.position;
		particle.Play();
		Voice_Manager.VOICE_Obj.Voice_Active(Obj_Storage.Storage_Data.audio_voice[14]);
		SE_Manager.SE_Obj.SE_Active_2(Obj_Storage.Storage_Data.audio_se[16]);

	}
	//レーザーを打てるように
	private void ActiveLaser()
	{
		Debug.Log("レーザーに変更");
		bullet_Type = Bullet_Type.Laser;
		//プレイヤーパワーアップ時のエフェクト発動処理----------------------------------------------------------------------
		GameObject effect = Obj_Storage.Storage_Data.Effects[6].Active_Obj();
		ParticleSystem particle = effect.GetComponent<ParticleSystem>();
		effect.transform.position = gameObject.transform.position;
		particle.Play();
		//----------------------------------------------------------------------
		Voice_Manager.VOICE_Obj.Voice_Active(Obj_Storage.Storage_Data.audio_voice[15]);
		SE_Manager.SE_Obj.SE_Active_2(Obj_Storage.Storage_Data.audio_se[16]);

		Laser.SetActive(true);
	}
	//シールドの発動
	private void ActiveShield()
	{
		activeShield = true;            //シールドが発動するかどうかの判定
		Set_Shield(3);
		shield_Effect.Play();               //パーティクルの稼働
		Debug.Log("シールド発動");
		//------------------------------------------------------------------------
		GameObject effect = Obj_Storage.Storage_Data.Effects[6].Active_Obj();
		ParticleSystem powerup = effect.GetComponent<ParticleSystem>();
		effect.transform.position = gameObject.transform.position;
		powerup.Play();
		shield_Effect.Play();
		//------------------------------------------------------------------------
		Voice_Manager.VOICE_Obj.Voice_Active(Obj_Storage.Storage_Data.audio_voice[25]);
		SE_Manager.SE_Obj.SE_Active_2(Obj_Storage.Storage_Data.audio_se[16]);

	}
	//オプションをアクティブに
	private void CreateBit()
	{
		//オプションをそれぞれアクティブに
		switch (bitIndex)
		{
			case 0:
				Obj_Storage.Storage_Data.Option.Active_Obj();
				bitIndex++;
				break;
			case 1:
				Obj_Storage.Storage_Data.Option.Active_Obj();
				bitIndex++;
				break;
			case 2:
				Obj_Storage.Storage_Data.Option.Active_Obj();
				bitIndex++;
				break;
			case 3:
				Obj_Storage.Storage_Data.Option.Active_Obj();
				bitIndex++;
				break;
			default:
				break;
		}
		Voice_Manager.VOICE_Obj.Voice_Active(Obj_Storage.Storage_Data.audio_voice[16]);
		Debug.Log("ビットン生成");
	}
	private void Init_speed()
	{
		speed = min_speed;
		SE_Manager.SE_Obj.SE_Active_2(Obj_Storage.Storage_Data.audio_se[16]);

	}
	//レーザーの攻撃を初期バレットまたはダブルに変更
	private void Reset_BulletType()
	{
		if (hp < 1) bullet_Type = Bullet_Type.Single;
	}
	//----------------------------------------------------------
	//プレイヤーの無敵表現（チカチカ光る感じ）
	private void Change_Material(int j)
	{
		//規定フレームを越したら動くように
		if (cnt > j)
		{
			//元のmaterialか白に変更するかどうかの判定
			if (Is_Change == false)
			{
				//主人公機のmaterialを白に変更
				for (int i = 0; i < object_material.Length; i++)
				{
					object_material[i].material = white_material;
				}
				Is_Change = true;
			}
			else
			{
				//主人公機のmaterialを元の色に変更
				for (int i = 0; i < object_material.Length; i++)
				{

					object_material[i].material = Get_self_material(i);
				}
				Is_Change = false;
			}
			//countをもとに戻す
			cnt = 0;
		}
		//フレーム加算
		cnt++;
	}
}
