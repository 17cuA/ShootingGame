using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleChange2 : MonoBehaviour
{
	public GameObject parentObj;

	public FollowGround3 followGround_Script;
	public float angleZ;
	public float angleZ_ChangeValue;


	//
	RaycastHit hit; //ヒットしたオブジェクト情報
	public Vector3 angleTest;
	public Vector3 rayDirection;
	//

	//
	public int Act_Step { get; set; }                  // 行動の変更用
	private RaycastHit hit_mesh;            // 衝突したオブジェクトのメッシュ(コライダーの一部)の情報
	public float Length_On_Landing { get; set; }        // 落下時のレイの長さ
	public float Length_On_Gliding { get; set; }            // 滑空時のレイの長さ
	private float ray_length;       // レイの長さ
	public float speed;

	private float constant_velocity_line_speed;     // 等速直線速度
	public Vector3 Ray_Direction { get; set; }          // レイの向き
	private int Running_Flame { get; set; }             // 起動している間のフレーム
	public float inner_product;
	//

	void Start()
    {
		hit_mesh = new RaycastHit();
		Length_On_Landing = ray_length;
		Length_On_Gliding = ray_length * 3.0f;

	}

	// Update is called once per frame
	void Update()
    {
		if (followGround_Script.direcState == FollowGround3.DirectionState.Left)
		{
			if (followGround_Script.isHitP)
			{
				angleZ = -followGround_Script.groundAngle;
			}
			else
			{
				angleZ += angleZ_ChangeValue;
				followGround_Script.groundAngle = angleZ;
			}
		}
		else if (followGround_Script.direcState == FollowGround3.DirectionState.Right)
		{
			if (followGround_Script.isHitP)
			{
				angleZ = followGround_Script.groundAngle;
			}
			else
			{
				angleZ -= angleZ_ChangeValue;
				followGround_Script.groundAngle = angleZ;
			}
		}
		transform.rotation = Quaternion.Euler(0, 0, angleZ);

		//----------------------------------------------------
		// 障害物に当たる前の行動
		if (Act_Step == 0)
		{
			HorizontalProjection();
		}
		// 障害物に当たった後の行動
		else if (Act_Step == 2)
		{
			// 自身の向いている方向に移動
			//transform.position += transform.right.normalized * speed;
			//	自身の下方向にコライダーがあるとき
			Vector3 vector = transform.position;
			vector.x += 0.5f;
		}
		// 先端に触れたメッシュ(コライダーの一部)があるとき
		if (Physics.Raycast(transform.position, Ray_Direction, out hit_mesh, Length_On_Landing))
		{
			// コライダーの持ち主がWAllのとき
			if (hit_mesh.transform.gameObject.tag == "Wall")
			{
				Moving_Facing_Confirmation(hit_mesh.normal);
				// 移動の種類を変える
				Act_Step = 2;
				Length_On_Landing = Length_On_Gliding;
			}
		}
		else
		{
			Debug.DrawRay(transform.position, Ray_Direction * 0.5F, Color.white);
		}

		//----------------------------------------------------

		RayTest();
	}

	void RayTest()
	{
		int layerMask = 1 << 8;

		layerMask = ~layerMask;

		if (Physics.Raycast(transform.position, -transform.up, out hit,0.5f, layerMask))
		{
			//rayDelayCnt++;
			if (hit.collider.tag == "Wall")
			{
				//if (rayDelayCnt > rayDelayMax)
					angleTest = Quaternion.FromToRotation(-transform.up, hit.normal).eulerAngles;

					//angleChange_Script.angleZ = angleZ;


					//if (raytopParent_Script.angleZ != angleZ + 90)
					//{
					//	raytopParent_Script.angleZ = angleZ + 90;
					//}

				Debug.DrawRay(transform.position, rayDirection * hit.distance, Color.red);
			}
		}
		else
		{
			Debug.DrawRay(transform.position, -transform.up * 0.5F, Color.white);
		}
	}
	/// <summary>
	/// 水平投射
	/// </summary>
	private void HorizontalProjection()
	{
		Vector3 vector = new Vector3(constant_velocity_line_speed, Ray_Direction.y * (Running_Flame * speed));
		transform.right = vector;
		//transform.position += vector.normalized * speed;
		Running_Flame++;
	}

	/// <summary>
	/// 移動向き確認
	/// </summary>
	private void Moving_Facing_Confirmation(Vector3 mesh_normal)
	{
		// 内閣の計算---------------------------------------------------------------------------------------------
		mesh_normal.y *= (Ray_Direction.y * -1.0f);
		mesh_normal.x *= Ray_Direction.y;
		transform.right = new Vector2(mesh_normal.y, mesh_normal.x);
		inner_product = transform.right.x * Ray_Direction.x + transform.right.y * Ray_Direction.y;
		// 内閣の計算---------------------------------------------------------------------------------------------

		// 内角が0以下のとき
		//if (inner_product < 0)
		//{
		//	// 自信を消す
		//	AddExplosionProcess();
		//	gameObject.SetActive(false);
		//}
	}
}
