/*
 * 20190904 作成
 * author hasegawa yuuta
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

/// <summary>
/// ムービーの自動再生の手助けをする
/// </summary>
public class DemoMovieControl : MonoBehaviour
{
	[SerializeField] VideoPlayer videoPlayer;
	int frame = 0;
	[SerializeField, Tooltip("ムービーに遷移するまでの秒数")] float transitionTime = 0;
	[SerializeField, Tooltip("画面を覆うImage")] Image displayPlane;

	void Start()
	{
		// ムービーが設定されていなかったらエラーを表示して処理をやめる
		if (!videoPlayer) { Debug.LogError("Not set to a video of my field!"); return; }
		// Imageが設定されていなかったら生成する
		if (!displayPlane)
		{
			GameObject plane = new GameObject("DisplayPlane");
			displayPlane = plane.AddComponent<Image>();
			Canvas anyCanvas = FindObjectOfType<Canvas>();
			displayPlane.transform.parent = anyCanvas.transform;
			displayPlane.rectTransform.position = Vector2.zero;
			displayPlane.rectTransform.sizeDelta = new Vector2(3840f, 1080f);
			displayPlane.color = Color.black;
		}
	}

	void Update()
	{
		if (!videoPlayer) { return; }
	}
}
