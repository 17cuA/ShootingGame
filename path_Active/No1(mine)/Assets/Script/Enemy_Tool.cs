/*
 * エネミーの挙動作成用ツール
 * クボタタツキ
 *更新履歴
 * 2019/06/14
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//#if UNITY_EDITOR
//SceneViewを取得するために宣言、エディタ外では使えないのでUNITY_EDITORで囲む
//using UnityEditor;
//#endif

//[ExecuteInEditMode]
public class Enemy_Tool : MonoBehaviour
{
	//プロパティ───────────────────────────────────────
	[Header("線の編集許可フラグ")]
	[SerializeField] private bool createLine;
	[SerializeField] private bool clickPut;
	[SerializeField] private GameObject AnkerPrefab;
	[SerializeField] private float interval;        // 配置間隔
	[SerializeField] private Anker[] ankers;
	[SerializeField] public LineRenderer lineRenderer;
	private Camera camera;		//カメラを取得するための変数
	private int prevChildCount;
	private EventType prevEventType;
	private const int debugDivision = 20;           // 分割数
	//────────────────────────────────────────────

	private void Start()
	{
		camera = GameObject.Find("Main Camera").GetComponent<Camera>();
	}


	//更新処理────────────────────────────────────────
	void Update()
	{
		if (!createLine) return;

		if (!CheckError()) return;

		if (Input.GetMouseButtonDown(0))
		{
			Create_beje();
			UpdateAnkerCount();
			lineRenderer.SetPositions(UpdateCurveLine());
		}
	}
	//ベジェ曲線の各ポジションを等間隔に─────────────────────────────────────────
	private void Awake()
	{
		//!< 実行中のみ初期化処理を行う
		//if (EditorApplication.isPlayingOrWillChangePlaymode)
		//{
		Debug.Log(ankers.Length);
		if(ankers.Length != 0)
		{
			int[] adjustDivisions = new int[ankers.Length - 1];
			for (int a = 0; a < ankers.Length - 1; ++a)
			{
				float distance = 0;
				for (int i = 0; i < debugDivision - 1; ++i)
				{
					distance += Vector3.Distance(lineRenderer.GetPosition(i + a * debugDivision),
						lineRenderer.GetPosition(i + 1 + a * debugDivision));
				}
				adjustDivisions[a] = (int)(distance / interval);
			}
			lineRenderer.SetPositions(AjustCurveLine(adjustDivisions));

			Destroy(this);
		}
		//}
	}
	//内部呼び出しメソッド──────────────────────────────────
	/// <summary>
	/// 直線を結ぶ座標を取得する
	/// </summary>
	/// <returns></returns>
	Vector3[] GetLinePositions()
	{
		//!< Center>>Next>>Prev>>Center>>Next>>Prev>>Center
		Vector3[] positions = new Vector3[ankers.Length + 2 * (ankers.Length - 1)];

		//!< 座標の追加
		int i, n;
		for (i = 0, n = 0; n < ankers.Length - 1; i += 3, ++n)
		{
			positions[i] = ankers[n].AnkerPos;
			positions[i + 1] = ankers[n].NextHandlePos;
			positions[i + 2] = ankers[n + 1].PrevHandlePos;
		}

		//!< 最後の座標を追加
		positions[positions.Length - 1] = ankers[ankers.Length - 1].AnkerPos;

		return positions;
	}
	//ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー
	/// <summary>
	/// アンカーの数を更新する
	/// </summary>
	/// <returns></returns>
	void UpdateAnkerCount()
	{
		//!< 前回と変わりなければ終了
		if (transform.childCount == prevChildCount) return;

		//!< すべての子要素を取得
		Transform children = GetComponentInChildren<Transform>();

		//!< 一時的にリスト管理を行い、のちに配列化
		List<Anker> allAnkers = new List<Anker>(0);
		foreach (Transform ob in children)
		{
			allAnkers.Add(ob.gameObject.GetComponent<Anker>());
		}
		ankers = allAnkers.ToArray();

		//!< アンカーの角度をいい感じにする
		//!< 2019/05/13 実行終了後にこの処理が原因で不具合が出るため、一回目はこの処理を行わない。
		if (prevChildCount != 0 && prevChildCount < transform.childCount)
		{
			AdjustAnkersHandleRange();
			AdjustAnkersAngle();
		}
		prevChildCount = transform.childCount;
	}
	//ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー
	/// <summary>
	/// アンカーのハンドルの長さを調整する
	/// </summary>
	/// <returns></returns>
	/// 
	void AdjustAnkersHandleRange()
	{
		if (ankers.Length > 1)
		{
			ankers[ankers.Length - 1].AdjustPrevHandleRange(ankers[ankers.Length - 2]);
			ankers[ankers.Length - 2].AdjustNextHandleRange(ankers[ankers.Length - 1]);
		}
	}

	//ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー
	/// <summary>
	/// アンカーの角度をいい感じにする
	/// </summary>
	/// <returns></returns>
	void AdjustAnkersAngle()
	{
		switch (ankers.Length)
		{
			//!< 直線を描画
			case 2:
				//!< 末端のアンカーの角度をいい感じにする。
				ankers[0].AdjustFirstAnkerAngle(ankers[1]);
				ankers[1].AdjustSecondAnkerAngle(ankers[0]);
				break;
			//!< 曲線を描画
			case 3:
				//!< 終わりから2番目のアンカー角度をいい感じにする
				ankers[ankers.Length - 2].AdjustCenterAnkerAngle(ankers[ankers.Length - 3], ankers[ankers.Length - 1]);

				//!< 終わりから2番目のアンカーの前後のアンカーの角度をいい感じにする
				ankers[ankers.Length - 1].AdjustPrevAnkerAngle(ankers[ankers.Length - 2]);
				ankers[ankers.Length - 3].AdjustNextAnkerAngle(ankers[ankers.Length - 2]);
				break;
			default:
				//!< 終わりから2番目のアンカー角度をいい感じにする
				ankers[ankers.Length - 2].AdjustCenterAnkerAngle(ankers[ankers.Length - 3], ankers[ankers.Length - 1]);
				//!< 終わりから2番目のアンカーの次のアンカーの角度をいい感じにする
				ankers[ankers.Length - 1].AdjustPrevAnkerAngle(ankers[ankers.Length - 2]);
				break;
		}
	}
	//ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー
	/// <summary>
	/// ベジェ曲線を更新する
	/// </summary>
	/// <returns></returns>
	Vector3[] UpdateCurveLine()
	{
		// 直線なら二点を結ぶだけ
		if (ankers.Length < 2)
		{
			lineRenderer.positionCount = 0;
			return new Vector3[0];
		}

		// 直線の座標を取得
		Vector3[] linePositions = GetLinePositions();
		// 曲線の座標数（点の数 + 点と点を分割する点の数）
		Vector3[] positions = new Vector3[0];

		// 点と点の間を曲線にしていく
		for (int i = 0; i < linePositions.Length; i += 3)
		{
			if (i > linePositions.Length - 4) { break; }
			int temp = positions.Length;
			//!< 配列の長さを変更するためのリストを作成
			List<Vector3> list = new List<Vector3>(positions);

			//!< 2線の二次元ベジェ曲線を取得
			Vector3[] beje1 = BezierCurve2(linePositions[i], linePositions[i + 1], linePositions[i + 2], debugDivision);
			Vector3[] beje2 = BezierCurve2(linePositions[i + 1], linePositions[i + 2], linePositions[i + 3], debugDivision);

			//!< 次の座標の情報を挿入
			list.AddRange(BezierCurve3(beje1, beje2, debugDivision));
			lineRenderer.positionCount = list.Count;
			positions = list.ToArray();
		}

		return positions;
	}
	//ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー
	/// <summary>
	/// 3点の座標から二次元ベジェ曲線座標を返す
	/// </summary>
	/// <param name="p1">開始点</param>
	/// <param name="p2">中間点</param>
	/// <param name="p3">終着点</param>
	/// <param name="division">分割数</param>
	/// <returns>二次元ベジェ曲線の座標配列</returns>
	/// <returns></returns>
	Vector3[] BezierCurve2(Vector3 p1, Vector3 p2, Vector3 p3, int division)
	{
		Vector3[] positions = new Vector3[division + 1];
		positions[0] = p1;
		for (int d = 1; d < division; ++d)
		{
			float t = 1.0f / division * d;

			Vector3 v1 = (1 - t) * p1 + t * p2;
			Vector3 v2 = (1 - t) * p2 + t * p3;

			positions[d] =
				t * v2 + (1 - (1.0f / division * d)) * v1;
		}
		positions[positions.Length - 1] = p3;
		return positions;
	}
	//ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー
	/// <summary>
	/// 2線の二次元ベジェ曲線を合成し、三次元ベジェ曲線を作成する
	/// </summary>
	/// <param name="startBeje">開始点を持つベジェ曲線</param>
	/// <param name="endBeje">終着点を持つベジェ曲線</param>
	/// <param name="division">分割数</param>
	/// <returns>三次元ベジェ曲線の座標配列</returns>
	/// <returns></returns>
	Vector3[] BezierCurve3(Vector3[] startBeje, Vector3[] endBeje, int division)
	{
		Vector3[] positions = new Vector3[division + 1];
		positions[0] = startBeje[0];
		for (int d = 1; d < division; ++d)
		{
			float t = 1.0f / division * d;
			//3次元ベジェ曲線の計算↓（t = 時間,division = 分割数,)
			positions[d] =
				t * endBeje[d] + (1 - (1.0f / division * d)) * startBeje[d];
		}
		positions[positions.Length - 1] = endBeje[endBeje.Length - 1];
		return positions;
	}
	//ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー
	/// <summary>
	/// ベジェ曲線を指定された分割数で調整する
	/// </summary>
	/// <param name="divisions">分割数配列</param>
	/// <returns></returns>
	Vector3[] AjustCurveLine(int[] divisions)
	{
		// 直線の座標を取得
		Vector3[] linePositions = GetLinePositions();
		// 曲線の座標数（点の数 + 点と点を分割する点の数）
		Vector3[] positions = new Vector3[0];

		int d = 0;  //分割数の要素数を表す変数
					// 点と点の間を曲線にしていく(三次元ベジェ曲線で作成を行うため点４つでひとつの線)
		for (int i = 0; i < linePositions.Length - 3; i += 3)
		{
			int temp = positions.Length;
			//!< 配列の長さを変更するためのリストを作成
			List<Vector3> list = new List<Vector3>(positions);

			//!< 2線の二次元ベジェ曲線を取得
			Vector3[] beje1 = BezierCurve2(linePositions[i], linePositions[i + 1], linePositions[i + 2], divisions[d]);
			Vector3[] beje2 = BezierCurve2(linePositions[i + 1], linePositions[i + 2], linePositions[i + 3], divisions[d]);

			//!< 次の座標の情報を挿入
			list.AddRange(BezierCurve3(beje1, beje2, divisions[d]));
			//カウントを加算
			lineRenderer.positionCount = list.Count;
			//配列に変換
			positions = list.ToArray();

			++d;
		}
		return positions;
	}
	//ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー
	/// <summary>
	/// 必要なものがアタッチされているか確認し、存在しないのであれば警告を出す
	/// </summary>
	/// <returns></returns>
	bool CheckError()
	{
		if (!AnkerPrefab) { Debug.LogError("アンカープレハブが存在しません"); return false; }
		if (interval <= 0) { Debug.LogError("分割数間隔が設定されていませんせん"); return false; }
		if (!lineRenderer) { Debug.LogError("ラインレンダラーが存在しません"); return false; }
		else { return true; }
	}
	//マウスクリック判定処理─────────────────────────────────
//#if UNITY_EDITOR
	private void Create_beje()
	{
		//Debug.Log(Event.current.keyCode);
		//マウスのクリックがあったら処理
		if (!createLine || !clickPut) return;

		if (Event.current == null || Event.current.type != EventType.MouseUp || Event.current.type == prevEventType)
		{
			prevEventType = Event.current.type;
			return;
		}

		//マウスのクリックした位置情報の取得
		Vector3 mousePos = Input.mousePosition;
		//Y軸方向の補間
		//mousePos.y = SceneView.currentDrawingSceneView.camera.pixelHeight - mousePos.y;
		//Ray..伸びる線のこと
		//シーンビューでマウスをクリックすると伸びる線を作成（画面には見えない）
		Ray ray = camera.ScreenPointToRay(mousePos);
		//当たり判定用の変数作成
		RaycastHit hit = new RaycastHit();
		//当たり判定の処理
		//シーンビューから見てオブジェクトに当たったら処理を開始する
		if (Physics.Raycast(ray, out hit))
		{
			//オブジェクトを作成
			GameObject obj = Instantiate(AnkerPrefab, hit.point, Quaternion.identity);
			//自分の子供にする
			obj.transform.parent = transform;

			Anker temp = obj.GetComponent<Anker>();
			temp.Normal = hit.normal;

			//でばっぐよー
			//Debug.DrawRay(hit.point,hit.normal * 10000.0f, Color.blue, 100.0f, false);

			//オブジェクトの法線の向きをそろえる
			obj.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
		}
		//現在のイベントのタイプの更新
		prevEventType = Event.current.type;
	}
//endif
	//────────────────────────────────────────────

}
