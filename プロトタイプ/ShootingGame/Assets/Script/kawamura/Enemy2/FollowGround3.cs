using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class FollowGround3 : MonoBehaviour
{
	//自分の状態
	public enum DirectionState
	{
		Left,           //左向き
		Right,      //右向き
		Roll,           //回転中
		Stop,           //停止
	}

	public DirectionState direcState;       //状態変数
	DirectionState saveDirection;           //状態を一時保存する変数

	public GameObject angleObj;
	GameObject childObj;
	Vector3 velocity;

	CharacterController characterController;

	[Header("入力用　歩くスピード")]
	public float speedXMax;
	public float speedX;
	public float speedYMax;
	public float speedY;
	public float rotaY;                 //角度
	[Header("入力用　回転スピード")]
	public float rotaSpeed;
	[Header("入力用　歩く最大時間（秒）")]
	public float walkTimeMax;
	public float walkTimeCnt;
	[Header("入力用　止まっている最大時間（秒）")]
	public float stopTimeMax;
	public float stopTimeCnt;           //止まっている時間カウント
	[Header("入力用　攻撃間隔")]
	public float attackTimeMax;
	public float attackTimeCnt;
	float rollDelayCnt;                 //回転した後のカウント（回転直後に当たり判定をしないようにするため）

	//
	public Vector3 groundNormal = Vector3.zero;

	private Vector3 lastGroundNormal = Vector3.zero;
	public Vector3 lastHitPoint = new Vector3(Mathf.Infinity, 0, 0);

	public float groundAngle = 0;
	//

	//
	Vector3 normalVector = Vector3.zero;
	public Vector3 onPlane;
	//

	public bool isRoll;         //回転中かどうか
	bool isRollEnd = false;     //回転が終わったかどうか
	bool isAttack = true;
	public bool cccc = false;
	public bool isHit = false;
	public bool isHitP = false;
	void Start()
	{
		characterController = GetComponent<CharacterController>();
		childObj = transform.GetChild(0).gameObject;
		walkTimeCnt = 0;
		stopTimeCnt = 0;
		rollDelayCnt = 0;
		isRoll = false;
		isAttack = true;
	}

	void Update()
	{
		//this.controller.Move(Vector3.MoveTowards(this.transform.position, cameraPosition, delta) - this.transform.position + Physics.gravity);
		//characterController.Move(velocity * Time.deltaTime);
		//とりあえずすり抜けをなくす処理
		if (transform.position.y < -4.15f)
		{
			transform.position = new Vector3(transform.position.x, -4.15f, 0);
		}
		//回転が終わった後当たり判定に間を空けるためカウント
		if (isRollEnd)
		{
			rollDelayCnt++;
			if (rollDelayCnt > 5)
			{
				isRollEnd = false;
				rollDelayCnt = 0;
			}
		}

		//////
		// 平面に投影したいベクトルを作成
		Vector3 inputVector = Vector3.zero;
		inputVector.x = Input.GetAxis("Horizontal");
		inputVector.z = Input.GetAxis("Vertical");

		// 平面に沿ったベクトルを計算
		onPlane = Vector3.ProjectOnPlane(inputVector, normalVector);
		//////

		//if (direcState == DirectionState.Left || direcState == DirectionState.Right)
		//{
		//	walkTimeCnt += Time.deltaTime;
		//	if (walkTimeCnt > walkTimeMax)
		//	{
		//		walkTimeCnt = 0;
		//		direcState = DirectionState.Stop;
		//	}
		//}
		//動く関数
		Move();
	}

	//----------------ここから関数----------------
	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		cccc = true;
		if (hit.normal.y > 0 && hit.moveDirection.y < 0)
		{
			if ((hit.point - lastHitPoint).sqrMagnitude > 0.001f || lastGroundNormal == Vector3.zero)
			{
				groundNormal = hit.normal;
			}
			else
			{
				groundNormal = lastGroundNormal;
			}

			lastHitPoint = hit.point;
		}

		// 現在の接地面の角度を取得
		groundAngle = Vector3.Angle(hit.normal, Vector3.up);
		groundAngle = Mathf.Round(groundAngle * 10);
		groundAngle /= 10;
	}

	//動く関数
	void Move()
	{
		switch (direcState)
		{
			//左向きの時移動する
			case DirectionState.Left:
				velocity = angleObj.transform.rotation * new Vector3(-speedX, -speedY, 0);
				gameObject.transform.position += velocity * Time.deltaTime;
				//坂を上り下りできる移動
				characterController.Move(velocity * Time.deltaTime);

				if (characterController.collisionFlags != CollisionFlags.None)
				{
					//speedY = 0;
					isHit = true;
				}
				else
				{
					//speedY = 3f;
					isHit = false;
				}
				walkTimeCnt += Time.deltaTime;
				if (walkTimeCnt > walkTimeMax)
				{
					walkTimeCnt = 0;
					saveDirection = direcState;
					direcState = DirectionState.Stop;
				}
				break;

			//右向きの時移動する
			case DirectionState.Right:
				velocity = angleObj.transform.rotation * new Vector3(speedX, -speedY, 0);
				//gameObject.transform.position += velocity * Time.deltaTime;
				characterController.Move(velocity * Time.deltaTime);
				if (characterController.collisionFlags != CollisionFlags.None)
				{
					//speedY = 0;
					isHit = true;
				}
				else
				{
					//speedY = 3f;
					isHit = false;
				}

				walkTimeCnt += Time.deltaTime;
				if (walkTimeCnt > walkTimeMax)
				{
					walkTimeCnt = 0;
					saveDirection = direcState;
					direcState = DirectionState.Stop;
				}
				break;

			//回転する
			case DirectionState.Roll:
				//直前の状態が左向きだったら
				if (saveDirection == DirectionState.Left)
				{
					//向きをマイナス
					rotaY -= rotaSpeed;
					if (rotaY < -180f)
					{
						rotaY = -180f;
						direcState = DirectionState.Right;
						isRoll = false;
						isRollEnd = true;
					}
					transform.rotation = Quaternion.Euler(0, rotaY, 0);
				}
				//直前の状態が右向きだったら
				else if (saveDirection == DirectionState.Right)
				{
					//向きをプラス
					rotaY += rotaSpeed;
					if (rotaY > 0)
					{
						rotaY = 0;
						direcState = DirectionState.Left;
						isRoll = false;
						isRollEnd = true;
					}
					transform.rotation = Quaternion.Euler(0, rotaY, 0);
				}
				break;

			case DirectionState.Stop:
				stopTimeCnt += Time.deltaTime;
				attackTimeCnt += Time.deltaTime;

				if (stopTimeCnt > stopTimeMax)
				{
					direcState = saveDirection;
					stopTimeCnt = 0;
					attackTimeCnt = 0;
					isAttack = true;
				}
				if (isAttack && attackTimeCnt > attackTimeMax)
				{
					Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, childObj.transform.position, childObj.transform.rotation);
					isAttack = false;
				}
				break;
		}
	}

	private void OnTriggerEnter(Collider col)
	{

		//当たったら
		if (col.gameObject.tag == "Wall")
		{
			isHitP = true;
			//if (!isRollEnd && !isRoll)
			//{
			//	saveDirection = direcState;
			//	direcState = DirectionState.Roll;
			//	isRoll = true;
			//}
		}
		else
		{
			isHitP = false;
		}
	}

	private void OnCollisionStay(Collision collision)
	{
		if(collision.gameObject.tag=="Wall")
		{

		}
	}
	private void OnCollision(Collision collision)
	{
		// 衝突した面の、接触した点における法線を取得
		normalVector = collision.contacts[0].normal;
	}
}
