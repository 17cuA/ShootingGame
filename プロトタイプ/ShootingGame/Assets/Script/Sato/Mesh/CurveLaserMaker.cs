using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter), typeof(MeshCollider))]
public class CurveLaserMaker : MonoBehaviour
{
	private List<Vector2> points;
	[SerializeField] float appendDistance = 0.5f;
	private float appendSqrDistance;

	[SerializeField] int maxPointCount = 30;

	private struct section
	{
		public Vector2 direction;   // 方向ベクトル.

		public Vector2 left;        // セクションの左端.
		public Vector2 right;       // セクションの右側.
	}

	private section[] sections;
	[SerializeField] float laserWidth = 1;

	void Start()
	{
		setVectors();
	}
	void Update()
	{
		setPoints();
		setVectors();
		createMesh();
	}

	void Awake()
	{
		appendSqrDistance = Mathf.Pow(appendDistance, 2);
	}

	void OnValidate()
	{
		appendSqrDistance = Mathf.Pow(appendDistance, 2);
	}

	void setVectors()
	{
		// 2つ以上セクションを用意できない状態の場合処理を抜ける.
		if (points == null || points.Count <= 1) return;

		sections = new section[points.Count];

		for (int i = 0; i < points.Count; i++)
		{
			// ----- 方向ベクトルの計算 -----
			if (i == 0)
			{
				// 始点の場合.
				sections[i].direction = points[i + 1] - points[i];
			}
			else if (i == points.Count - 1)
			{
				// 終点の場合.
				sections[i].direction = points[i] - points[i - 1];
			}
			else
			{
				// 途中の場合.
				sections[i].direction = points[i + 1] - points[i - 1];
			}

			sections[i].direction.Normalize();

			// ----- 方向ベクトルに直交するベクトルの計算 -----
			Vector2 side = Quaternion.AngleAxis(90f, -Vector3.forward) * sections[i].direction;
			side.Normalize();

			sections[i].left = points[i] - side * laserWidth / 2f;
			sections[i].right = points[i] + side * laserWidth / 2f;
		}
	}
	void createMesh()
	{
		if (points == null || points.Count <= 1) return;

		MeshFilter mf = GetComponent<MeshFilter>();
		Mesh mesh = mf.mesh = new Mesh();

		mesh.name = "CurveLaserMesh";

		int meshCount = points.Count - 1;                   // 四角メッシュ生成数はセクション - 1.

		Vector3[] vertices = new Vector3[(meshCount) * 4];  // 四角なので頂点数は1つのメッシュに付き4つ.
		int[] triangles = new int[(meshCount) * 2 * 3];     // 1つの四角メッシュには2つ三角メッシュが必要. 三角メッシュには3つの頂点インデックスが必要.

		// ----- 頂点座標の割り当て -----
		for (int i = 0; i < meshCount; i++)
		{
			vertices[i * 4 + 0] = sections[i].left;
			vertices[i * 4 + 1] = sections[i].right;
			vertices[i * 4 + 2] = sections[i + 1].left;
			vertices[i * 4 + 3] = sections[i + 1].right;
		}

		// ----- 頂点インデックスの割り当て -----
		int positionIndex = 0;

		for (int i = 0; i < meshCount; i++)
		{
			triangles[positionIndex++] = (i * 4) + 1;
			triangles[positionIndex++] = (i * 4) + 0;
			triangles[positionIndex++] = (i * 4) + 2;

			triangles[positionIndex++] = (i * 4) + 2;
			triangles[positionIndex++] = (i * 4) + 3;
			triangles[positionIndex++] = (i * 4) + 1;
		}

		mesh.vertices = vertices;
		mesh.triangles = triangles;
	}
	void OnDrawGizmos()
	{
		if (sections == null) return;

		Gizmos.color = Color.black;
		for (int i = 0; i < sections.Length; i++)
		{
			Gizmos.DrawSphere(points[i], 0.1f);
		}

		Gizmos.color = Color.blue;
		for (int i = 0; i < sections.Length; i++)
		{
			Gizmos.DrawSphere(sections[i].left, 0.1f);
			Gizmos.DrawSphere(sections[i].right, 0.1f);
		}
	}
	/// <summary>
	/// マウス入力によってpointsを設定する.
	/// </summary>

	/// <summary>
	/// マウス入力によってpointsを設定する.
	/// </summary>
	void setPoints()
	{
		// マウス押下中のみ処理を行う.
		if (!Input.GetMouseButton(0)) return;

		// マウスの位置をスクリーン座標からワールド座標に変換.
		var screenMousePos = Input.mousePosition;
		screenMousePos.z = -Camera.main.transform.position.z;
		var curPoint = Camera.main.ScreenToWorldPoint(screenMousePos);

		if (points == null)
		{
			points = new List<Vector2>();
			points.Add(curPoint);
		}

		//// 前回のポイントとの比較を行う.
		//var distance = (curPoint - points[points.Count - 1]);
		//if (distance.magnitude >= appendDistance)
		//{
		//	points.Add(curPoint);
		//}
	}
}