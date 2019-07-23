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

	[SerializeField]
	private List<GameObject> nodes;

	private int pointMax;
	private int pointCount;
	public bool IsOverLoad
	{
		get
		{
			return this.pointCount > this.pointMax;
		}
	}

	private void Awake()
	{
		this.isFixed      = true;
		this.lineRenderer = GetComponent<LineRenderer>();
		this.nodes        = new List<GameObject>();
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
				if(this.nodes.Count == 0)
				{
					this.nodes.Clear();                               //念のため、管理リストクリアする
			
					this.SetLineRenderer(Vector3.zero,Vector3.zero);  //レーザーのレンダリングもリセット
					this.gameObject.SetActive(false);                 //当オブジェクトを非アクティブ状態に
				}
			}
			//アクティブ状態の場合
			else
			{
				//位置合わせTrue場合、強制的に管理オブジェクトの位置を修正する
				if (this.isFixed)
					this.nodes[i].transform.position = new Vector3(this.nodes[i].transform.position.x, this.transform.position.y, 0);
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
	public void LaunchNode(float trailWidth)
	{
		var node = StorageReference.Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePLAYER_LASER, this.transform.position, Quaternion.identity);
		node.GetComponent<bullet_status>().shot_speed = this.shotSpeed;
		node.transform.localRotation = this.emitter.transform.localRotation;
		node.GetComponent<bullet_status>().Travelling_Direction = node.transform.right;
		node.GetComponent<LaserLine>().TrailRenderer.Clear();
		node.GetComponent<LaserLine>().TrailRenderer.endWidth = trailWidth;
		node.GetComponent<LaserLine>().TrailRenderer.startWidth = trailWidth;

		//管理するように
		this.nodes.Add(node);
		this.pointCount++;
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
    }

	public void ResetGenerator()
	{
		pointCount = 0;
	}
}

