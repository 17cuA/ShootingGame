using System;
using System.Collections.Generic;
using UnityEngine;

 [DefaultExecutionOrder(590)]
[RequireComponent(typeof(LineRenderer))]
public class Instance_Laser_Node_Generator : MonoBehaviour
{
	private float shotSpeed;
	private GameObject emitter;

	private bool isFixed;
	public bool IsFixed { get { return isFixed; } set { isFixed = value;} }

	private LineRenderer lineRenderer;

	public List<LaserLine> nodes;

	private int pointMax;
	private int pointCount;
	public bool IsOverLoad
	{
		get
		{
			return this.pointCount > this.pointMax;
		}
	}

	private float s_width;

	private void Awake()
	{
		this.isFixed      = true;
		this.lineRenderer = GetComponent<LineRenderer>();
		this.nodes        = new List<LaserLine>();
		this.emitter      = GameObject.Find("LaserEmitter");
	}

	private void Update()
	{
		//--------------------------------------------------ループチェック-------------------------------------------------------------
		//管理しているレーザー構成オブジェクト個数回実行する
		for(var i = 0; i < this.nodes.Count; ++i)
		{
			//現在検索オブジェクトは非アクティブの場合
			if(!this.nodes[i].gameObject.activeSelf)
			{
				//管理しないようにする
				this.nodes.Remove(nodes[i]);
				//検索位置調整する
				i--;
				//管理オブジェクト個数を調べ、管理個数は0の場合
			}
			//アクティブ状態の場合
			else
			{
				//位置合わせTrue場合、強制的に管理オブジェクトの位置を修正する
				if (this.isFixed)
					this.nodes[i].transform.position = new Vector3(this.nodes[i].transform.position.x, this.transform.position.y, 0);
			}

			if (this.nodes.Count == 0)
			{
				this.nodes.Clear();                               //念のため、管理リストクリアする
				this.ResetGenerator();
				this.ResetLineRenderer();

				this.gameObject.SetActive(false);                 //当オブジェクトを非アクティブ状態に
			}
		}
		//------------------------------------------------------------------------------------------------------------------------------


		//----------------------------------------レーザー　線　レンダリング--------------------------------------------------------------------------

		//if (this.nodes.Count >= 2)
		//{
		//	this.SetLineRenderer(new Vector3(this.nodes[0].transform.position.x, this.nodes[0].transform.position.y, 0)
		//						, new Vector3(this.nodes[nodes.Count - 1].transform.position.x, this.nodes[nodes.Count - 1].transform.position.y, 0));
		//}

		if (this.nodes.Count > 2)
		{

			this.lineRenderer.positionCount = this.nodes.Count;

			for (var i = 0; i < this.nodes.Count; ++i)
			{
				this.lineRenderer.SetPosition(i, this.nodes[i].transform.position);
			}
		}
		else
		{
			ResetLineRenderer();
		}


		//補間




		//var count = nodes.Count - 1;
		////オブジェクト追加
		//for (var i = 0; i < count; ++i)
		//{
		//	if(!nodes[i].isRotateLaser)
		//	{
		//		return;
		//	}
		//	if(Vector3.Distance(nodes[i].transform.position, nodes[i + 1].transform.position) > 1.5f)
		//	{
		//		var pos = (nodes[i].transform.position + nodes[i + 1].transform.position) / 2f;
		//		var direction = ((nodes[i].transform.position - transform.position) + (nodes[i + 1].transform.position - transform.position)) / 2f;
		//		var node = StorageReference.Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePLAYER_LASER, pos, Quaternion.identity);
		//		node.GetComponent<bullet_status>().shot_speed = this.shotSpeed;
		//		node.GetComponent<bullet_status>().Travelling_Direction = direction.normalized;
		//		node.GetComponent<LaserLine>().TrailRenderer.Clear();
		//		node.GetComponent<LaserLine>().TrailRenderer.endWidth = 2;
		//		node.GetComponent<LaserLine>().TrailRenderer.startWidth = 2;
		//		node.GetComponent<LaserLine>().isRotateLaser = true;
		//		this.nodes.Insert(i + 1, node.GetComponent<LaserLine>());
		//		this.pointCount++;
		//		i++;
		//		count++;
		//	}
		//}

		//if (this.lineRenderer.positionCount == 2 && this.lineRenderer.GetPosition(0) == Vector3.zero && this.lineRenderer.GetPosition(1) == Vector3.zero)
		//{
		//	if(this.nodes.Count == 0)
		//		this.gameObject.SetActive(false);
			
		//}


		//---------------------------------------------------------------------------------------------------------------------------------------------	
	}

	/// <summary>
	/// 設定方法
	/// </summary>
	/// <param name="shotSpeed"> レーザー連結点発射速度  </param>
	/// <param name="lineWidth"> レーザー線太さ          </param>
	/// <param name="nodePrefab">レーザー連結点プレハブ  </param>
	/// <param name="material">  レーザー線マテリアル    </param>
	public void Setting(float shotSpeed, float lineWidth,Material material,int pointMax)
	{
		this.shotSpeed                = shotSpeed;
		this.lineRenderer.startWidth  = lineWidth;
		this.lineRenderer.endWidth    = lineWidth;
		this.lineRenderer.material    = material;
		this.pointMax                 = pointMax;
	}

	/// <summary>
	/// レーザー連結点発射（生成）する
	/// </summary>
	public void LaunchNode(float trailWidth, bool isRotateLaser)
	{
		GameObject node = null;
		node = CreateNode(transform.position, this.emitter.transform.localEulerAngles, trailWidth, isRotateLaser);

		//管理するように
		this.nodes.Add(node.GetComponent<LaserLine>());
		this.pointCount++;

		if (nodes.Count > 1 && isRotateLaser && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftControl)))
		{
			var last = this.nodes[this.nodes.Count - 1];
			var lastlast = this.nodes[this.nodes.Count - 2];
			var pos = (last.transform.position + lastlast.transform.position) / 2;
			var rotation = (last.transform.localEulerAngles + lastlast.transform.localEulerAngles) / 2f;

			node = CreateNode(pos, rotation, trailWidth,isRotateLaser);
			node.GetComponent<LaserLine>().lifeTime = (last.lifeTime + lastlast.lifeTime) / 2f;
			node.GetComponent<LaserLine>().ischangLaserWidth = true;
			//this.nodes.Insert(this.nodes.Count - 1, node.GetComponent<LaserLine>());
			//this.pointCount++;
		}

		//if (nodes.Count != 0)
		//{
		//	var last = this.nodes[this.nodes.Count - 1];
		//	var pos = (last.transform.position + transform.position) / 2;
		//	var rotation = Average(new Quaternion[] { last.transform.localRotation, transform.localRotation });
		//	node = CreateNode(pos, rotation, trailWidth);
		//	this.nodes.Add(node);
		//	this.pointCount++;
		//}
	}

	/// <summary>
	/// レンダリング設定
	/// </summary>
	/// <param name="pos"> 一番目はレーザー頭位置、一番後ろはレーザー尾位置 </param>
	private void SetLineRenderer(params Vector3[] pos)
	{
		for(var i = 0; i < pos.Length; ++i)
		{
			this.lineRenderer.SetPosition(i, pos[i]);
		}
	}

	public void ResetLineRenderer()
    {
        this.lineRenderer.positionCount = 2;
		this.lineRenderer.SetPosition(0, Vector3.zero);
		this.lineRenderer.SetPosition(1, Vector3.zero);
	}

	public void ResetGenerator()
	{
		pointCount = 0;
	}

	private GameObject CreateNode(Vector3 pos, Vector3 rotation, float trailWidth, bool isRotateLaser)
	{
		var node = StorageReference.Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePLAYER_LASER, pos, Quaternion.identity);
		node.GetComponent<bullet_status>().shot_speed = this.shotSpeed;
		node.transform.localEulerAngles = rotation;
		node.GetComponent<bullet_status>().Travelling_Direction = node.transform.right;
		node.GetComponent<LaserLine>().TrailRenderer.Clear();
		node.GetComponent<LaserLine>().TrailRenderer.endWidth = trailWidth;
		node.GetComponent<LaserLine>().TrailRenderer.startWidth = trailWidth;
		node.GetComponent<LaserLine>().isRotateLaser = isRotateLaser;

		if(isRotateLaser)
		{
			node.transform.GetChild(0).gameObject.SetActive(false);
		}
		else
		{
			node.transform.GetChild(0).gameObject.SetActive(true);
		}

		return node;
	}


	public static Quaternion Average(Quaternion[] quatArray)
	{
		var result = new Quaternion();
		var count = quatArray.Length;
		var error = 0;

		while (count > 1)
		{
			if (error >= 10000) break;
			error++;
			var k = 0;
			for (int i = 0; i + 1 < count; i += 2)
			{
				var a = quatArray[i];
				var b = quatArray[i + 1];

				if (Quaternion.Dot(a, a) < Quaternion.kEpsilon)
					a = Quaternion.identity;

				if (Quaternion.Dot(b, b) < Quaternion.kEpsilon)
					b = Quaternion.identity;

				var avgQuat = Quaternion.LerpUnclamped(a, b, 0.5f);

				quatArray[k] = avgQuat;
				k++;
			}

			var lastCount = count;
			count = k;

			if ((lastCount & 1) == 1)
			{
				k++;
				count++;
				quatArray[k] = quatArray[lastCount - 1];
			}
		}

		result = quatArray[0];

		return result;
	}
}

