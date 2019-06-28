using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter), typeof(MeshCollider))]
public class CurveLaserMaker : MonoBehaviour
{

	[SerializeField] List<Vector3> points;

	private struct section
	{
		public Vector3 direction;   // 方向ベクトル.

		public Vector3 left;        // セクションの左端.
		public Vector3 right;       // セクションの右側.
	}

	private section[] sections;
	[SerializeField] float laserWidth = 1;

	[SerializeField] Material laserMat;

	IPointGetter pointGetter;

	void Start()
	{
		pointGetter = transform.GetComponentInChildren<IPointGetter>();
		GetComponent<MeshRenderer>().sortingOrder = GetInstanceID();
	}
	void Update()
	{
			// 取得した座標が無効の場合、すべての処理は行う必要がない.
			if (!pointGetter.GetPoint().HasValue) return;

			setPoints();
			setVectors();
			createMesh();
	}
	[SerializeField] float appendDistance = 5.0f;
	private float appendSqrDistance;

	[SerializeField] int maxPointCount = 100;
	[SerializeField] bool keepPointLength;

	/// <summary>
	/// マウス入力によってpointsを設定する.
	/// </summary>
	void Awake()
	{
		appendSqrDistance = Mathf.Pow(appendDistance, 2);
	}

	void setPoints()
	{

		// 値の取得.
		var curPoint = pointGetter.GetPoint().Value;

		//// 値の取得.
		//var curPoint = pointGetter.GetPoint();

		//// マウス押下中のみ処理を行う.
		//if (!Input.GetMouseButton(0)) return;

		// マウスの位置をスクリーン座標からワールド座標に変換.
		var screenMousePos = Input.mousePosition;
		screenMousePos.z = -Camera.main.transform.position.z;
		//var curPoint = Camera.main.ScreenToWorldPoint(screenMousePos);

		if (points == null)
		{
			points = new List<Vector3>();
			points.Add(curPoint);
		}

		//ポイント追加
		addPoint(curPoint);
		// 前回のポイントとの比較を行う.
		var distance = (curPoint - points[points.Count -1]);
		if (distance.sqrMagnitude >= appendSqrDistance)
		{
			points.Add(curPoint);
		}
		// 最大数を超えた場合ポイントを削除.
		while (points.Count > maxPointCount)
		{
			points.RemoveAt(0);
		}
	}
	void addPoint(Vector3 curPoint)
	{
		if (keepPointLength)
		{
			while (true)
			{
				var distance = (curPoint - points[points.Count - 1]);
				if (distance.sqrMagnitude < appendSqrDistance) break;

				distance *= appendDistance / distance.magnitude;
				points.Add(points[points.Count - 1] + distance);
			}
		}
		else
		{
			var distance = (curPoint - points[points.Count - 1]);
			if (distance.sqrMagnitude >= appendSqrDistance)
			{
				points.Add(curPoint);
			}
		}
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
			Vector3 side = Quaternion.AngleAxis(90f, -Vector3.forward) * sections[i].direction;
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
		MeshCollider mc = GetComponent<MeshCollider>();
		MeshRenderer mr = GetComponent<MeshRenderer>();


		mesh.name = "CurveLaserMesh";

		int meshCount = points.Count - 1;                   // 四角メッシュ生成数はセクション - 1.

		Vector3[] vertices = new Vector3[(meshCount) * 4];  // 四角なので頂点数は1つのメッシュに付き4つ.
		Vector2[] uvs = new Vector2[vertices.Length];
		int[] triangles = new int[(meshCount) * 2 * 3];     // 1つの四角メッシュには2つ三角メッシュが必要. 三角メッシュには3つの頂点インデックスが必要.

		// ----- 頂点座標の割り当て -----
		for (int i = 0; i < meshCount; i++)
		{
			vertices[i * 4 + 0] = sections[i].left;
			vertices[i * 4 + 1] = sections[i].right;
			vertices[i * 4 + 2] = sections[i + 1].left;
			vertices[i * 4 + 3] = sections[i + 1].right;

			var step = (float)1 / meshCount;

			uvs[i * 4 + 0] = new Vector2(0f, i * step);
			uvs[i * 4 + 1] = new Vector2(1f, i * step);
			uvs[i * 4 + 2] = new Vector2(0f, (i + 1) * step);
			uvs[i * 4 + 3] = new Vector2(1f, (i + 1) * step);
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
		mesh.uv = uvs;
		mesh.triangles = triangles;
		mc.sharedMesh = mesh;
		mr.material = laserMat;
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
}