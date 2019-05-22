//──────────────────────────────────────────────────────────────────────────
// ファイル名	：Anker.cs
// 概要			：ベジェ曲線のアンカーの役割をするオブジェクトのクラス
// 作成者		：杉山 雅哉
// 作成日		：2018/04/16
// 
//──────────────────────────────────────────────────────────────────────────
// 更新履歴：
// 2019/04/16 [杉山 雅哉] 新たに点が置かれたときに、ハンドルを生成させる
// 2019/04/17 [杉山 雅哉] 一方のハンドルの座標が変わったとき、角度だけ対角線上に合わせるようにする。
// 2019/04/19 [杉山 雅哉] 使用を変更し、座標も対角線上に置く。
// 2019/05/09 [杉山 雅哉] 次のアンカーが生成されたとき、自動的にいい感じの角度に変える
//
// TODO：
//
//──────────────────────────────────────────────────────────────────────────

using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
//SceneViewを取得するために宣言、エディタ外では使えないのでUNITY_EDITORで囲む
using UnityEditor;
#endif
using UnityEngine;

[ExecuteInEditMode]
public class Anker : MonoBehaviour
{
	//変数宣言──────────────────────────────────────────────────────────────────────
	Anker nextAnker;							//!< 次のアンカー情報
	[SerializeField] GameObject prevHandle;		//!< 前の曲線に影響するハンドル
	[SerializeField] GameObject nextHandle;		//!< 次の曲線に影響するハンドル
	[SerializeField] LineRenderer lineRenderer;	//!< ラインレンダラー情報

	Vector3 prevPrevPosition;
	Vector3 prevNextPosition;
	//──────────────────────────────────────────────────────────────────────────
	public Vector3 PrevHandlePos
	{
		get { return prevHandle.transform.position; }
		set { prevHandle.transform.position = value; }
	}
	public Vector3 NextHandlePos
	{
		get { return nextHandle.transform.position; }
		set { nextHandle.transform.position = value; }
	}
	public Vector3 AnkerPos
	{
		get { return transform.position; }
		set { transform.position = value; }
	}
	//外部呼出しメソッド─────────────────────────────────────────────────────────────────
	/// <summary>
	/// 一つ前のアンカーとの角度と位置をいい感じにする
	/// </summary>
	/// <param name="prev">一つ手前のアンカー</param>
	/// <returns></returns>
	public void AdjustPrevAnkerAngle(Anker prev)
	{
		transform.LookAt(prev.AnkerPos);
	}
	/// <summary>
	/// 前後のアンカーとの角度と位置をいい感じにする（オーバーロード）
	/// </summary>
	/// <param name="prev">一つ手前のアンカー</param>
	/// <param name="next">一つ先のアンカー</param>
	/// <returns></returns>
	public void AdjustAnkerAngle(Anker prev, Anker next)
	{
		transform.eulerAngles -= new Vector3(0.0f, 90.0f, 0.0f);
	}
	/// <summary>
	/// 一つ後のアンカーとの角度と位置をいい感じにする
	/// </summary>
	/// <param name="prev">一つ手前のアンカー</param>
	/// <returns></returns>
	public void AdjustNextAnkerAngle(Anker next)
	{
		transform.LookAt(next.AnkerPos);
		transform.eulerAngles *= -1.0f;
	}
	//──────────────────────────────────────────────────────────────────────────

	//内部メソッド────────────────────────────────────────────────────────────────────
	void Update()
	{
		gameObject.hideFlags = HideFlags.None;
		if(PrevHandlePos != prevPrevPosition)
		{
			nextHandle.transform.localPosition = -prevHandle.transform.localPosition;
		}
		else if(NextHandlePos != prevNextPosition)
		{
			prevHandle.transform.localPosition = -nextHandle.transform.localPosition;
		}

		//!< LineRendererのポジションを更新
		UpdateLinePosition();

		prevPrevPosition = PrevHandlePos;
		prevNextPosition = NextHandlePos;
	}
	//ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー
	/// <summary>
	/// LineRendererのポジションを更新する
	/// </summary>
	/// <returns></returns>
	void UpdateLinePosition()
	{
		lineRenderer.SetPosition(0, PrevHandlePos);
		lineRenderer.SetPosition(1, transform.position);
		lineRenderer.SetPosition(2, NextHandlePos);
	}
	//──────────────────────────────────────────────────────────────────────────
}
