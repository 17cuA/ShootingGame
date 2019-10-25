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

	public List<GameObject> nodes;

	private int pointMax;
	public int pointCount;
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
		this.nodes        = new List<GameObject>();
		this.emitter      = GameObject.Find("LaserEmitter");
	}

	private void OnEnable()
	{
		ResetGenerator();
		ResetLineRenderer();
		nodes.Clear();
	}


	private void Update()
	{

		//------------------------------------------------------------------------------------------------------------------------------


		//----------------------------------------レーザー　線　レンダリング--------------------------------------------------------------------------

		//if (this.nodes.Count >= 2)
		//{
		//	this.SetLineRenderer(new Vector3(this.nodes[0].transform.position.x, this.nodes[0].transform.position.y, 0)
		//						, new Vector3(this.nodes[nodes.Count - 1].transform.position.x, this.nodes[nodes.Count - 1].transform.position.y, 0));
		//}

        //node　2　以上

        if( Wireless_sinario.Is_using_wireless)
        {
             for (int i = 29; i < Obj_Storage.Storage_Data.Laser_Line.Get_Obj().Count; i++)
             {
                if(!Obj_Storage.Storage_Data.Laser_Line.Get_Obj()[i].gameObject.activeSelf)
                { 
                    if(this.nodes.Contains(Obj_Storage.Storage_Data.Laser_Line.Get_Obj()[i]))
                        this.nodes.Remove(Obj_Storage.Storage_Data.Laser_Line.Get_Obj()[i]);
                    Destroy(Obj_Storage.Storage_Data.Laser_Line.Get_Obj()[i]);
                    Obj_Storage.Storage_Data.Laser_Line.Get_Obj().RemoveAt(i);
                }
             }
        }
        else
        {
			var num = 0;
			num = nodes.Count;
            for (var i = 0; i < num; ++i)
		    {
                if(nodes[i] == null)
                {
                    nodes.Remove(nodes[i]);
                    i--;
					num = nodes.Count;
					continue;
                }


				var currentCheckedLaserNodeGo = nodes[i].gameObject;
			    //現在検索オブジェクトは非アクティブの場合
			    if(!currentCheckedLaserNodeGo.activeSelf)
			    {
				    //管理しないようにする
				   nodes.Remove(currentCheckedLaserNodeGo);
				    //検索位置調整する
				    i = i - 1;
					num = nodes.Count;
					//管理オブジェクト個数を調べ、管理個数は0の場合


					if (nodes.Count == 0)
					{
						ResetGenerator();
						ResetLineRenderer();

						gameObject.SetActive(false);                 //当オブジェクトを非アクティブ状態に
						break;
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
        }


		if (transform.parent.name == "Device_StrightLaserParent")
		{
			if (this.nodes.Count >= 2)
			{
				this.lineRenderer.SetPosition(0, this.nodes[0].transform.position);
				this.lineRenderer.SetPosition(1, this.nodes[nodes.Count - 1].transform.position);

			}
			//node 1
			else if (nodes.Count > 0)
			{

				lineRenderer.SetPosition(0, nodes[0].transform.position);
				lineRenderer.SetPosition(1, nodes[0].transform.position + Vector3.right);

			}
			else if (nodes.Count == 0)
			{
				lineRenderer.SetPosition(0, Vector3.zero);
				lineRenderer.SetPosition(1, Vector3.zero);
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
	public void Setting(float lineWidth,Material material,int pointMax)
	{
		this.lineRenderer.startWidth  = lineWidth;
		this.lineRenderer.endWidth    = lineWidth;
		this.lineRenderer.material    = material;
		this.pointMax                 = pointMax;
	}

	/// <summary>
	/// レーザー連結点発射（生成）する
	/// </summary>
	public void LaunchNode(bool isRotateLaser)
	{
		GameObject node = null;
		node = CreateNode(transform.position, this.emitter.transform.localEulerAngles, isRotateLaser);

		//管理するように
		this.nodes.Add(node);
		this.pointCount++;
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

	private GameObject CreateNode(Vector3 pos, Vector3 rotation, bool isRotateLaser)
	{
		var node = StorageReference.Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePLAYER_LASER, pos, Quaternion.identity);

		if (transform.parent.parent.parent.name == "Player")
		{
			node.name = "Player_Laser";
		}
		else if(transform.parent.parent.parent.name == "Player_2")
		{
			node.name = "Player2_Laser";
		}
		else if(transform.parent.parent.parent.name == "Option")
		{
			if(transform.parent.parent.parent.GetComponent<Bit_Formation_3>().bState == Bit_Formation_3.BitState.Player1)	
				node.name = "Option_Player1_Laser";

			if(transform.parent.parent.parent.GetComponent<Bit_Formation_3>().bState == Bit_Formation_3.BitState.Player2)
				node.name = "Option_Player2_Laser";
		}

		node.transform.localEulerAngles = rotation;
		return node;
	}


}

