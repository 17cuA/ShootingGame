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
	enum MoviePlayState
	{
		ePlaying,
		eStop,
		eNone,
	}

	[SerializeField] VideoPlayer videoPlayer;
	[SerializeField] RawImage videoRawImage;
	MoviePlayState playState = MoviePlayState.eStop;
	int frame = 0;
	[SerializeField, Tooltip("ムービーに遷移するまでの秒数")] float transitionTime = 0;
	int TransitionFrame { get { return (int)(transitionTime * 60f); } }
	[SerializeField, Tooltip("画面を覆うImage")] Image displayPlane;
	bool IsPlayingMovie { get { return playState == MoviePlayState.ePlaying; } }
	bool isInput = false;

	void Start()
	{
		// ムービーが設定されていなかったらエラー出力して処理をやめる
		if (!videoPlayer) { Debug.LogError("Not set to a video of my field!"); return; }
		// ムービーを停止状態にする
		videoPlayer.Stop();
		videoPlayer.gameObject.SetActive(false);
		// RawImageがセットされていなかったらvideoPlayerと同じRenderTextureがセットされているのを取得する
		if (!videoRawImage)
		{
			bool isFind = false;
			RenderTexture setRenderTexture = videoPlayer.targetTexture;
			RawImage[] getRawImage = FindObjectsOfType<RawImage>();
			foreach (RawImage rawImage in getRawImage)
			{
				// 見つけたらキャッシュしてループを終わらせる
				if (rawImage.texture == setRenderTexture)
				{
					videoRawImage = rawImage;
					isFind = true;
					break;
				}
			}
			// RawImageが見つからなかったらエラー出力して処理をやめる
			if (!isFind)
			{
				Debug.LogError("Not set to a RawImage of my field and not found RawImage!");
				return;
			}
		}
		videoRawImage.gameObject.SetActive(false);
		// Imageが設定されていなかったら生成する
		if (!displayPlane)
		{
			GameObject plane = new GameObject("DisplayPlane");
			displayPlane = plane.AddComponent<Image>();
			Canvas anyCanvas = FindObjectOfType<Canvas>();
			displayPlane.transform.parent = anyCanvas.transform;
			displayPlane.rectTransform.localPosition = Vector2.zero;
			displayPlane.rectTransform.sizeDelta = new Vector2(3840f, 1080f);
			displayPlane.color = Color.black;
		}
		displayPlane.color = new Color(displayPlane.color.r, displayPlane.color.g, displayPlane.color.b, 0f);
	}

	void Update()
	{
		if (!videoPlayer || !videoRawImage) { return; }
		if (playState == MoviePlayState.ePlaying)
		{
			PlayBehaviour();
		}
		else if (playState == MoviePlayState.eStop)
		{
			StopBehaviour();
		}
		++frame;
	}

	/// <summary>
	/// ムービー再生中の動作
	/// </summary>
	void PlayBehaviour()
	{
		if (Input.anyKeyDown)
		{
			isInput = true;
		}
		// ムービーが終わりに近づくにつれて暗転させる
		if ((ulong)videoPlayer.frame >= videoPlayer.frameCount - 10 || isInput)
		{
			displayPlane.color += new Color(0f, 0f, 0f, 5f / 60f);
		}
		else if (frame < 12)
		{
			displayPlane.color -= new Color(0f, 0f, 0f, 5f / 60f);
			if (displayPlane.color.a < 0f)
			{
				displayPlane.color = new Color(displayPlane.color.r, displayPlane.color.g, displayPlane.color.b, 0f);
			}
		}
		// 完全に暗転したらムービーを停止させる
		if (displayPlane.color.a >= 1f)
		{
			playState = MoviePlayState.eStop;
			videoPlayer.Stop();
			videoPlayer.gameObject.SetActive(false);
			videoRawImage.gameObject.SetActive(false);
			isInput = false;
			frame = 0;
		}
	}

	/// <summary>
	/// ムービーが流れていないときの動作
	/// </summary>
	void StopBehaviour()
	{
		// 設定したフレーム数を過ぎたら暗転させる
		if (frame > TransitionFrame)
		{
			displayPlane.color += new Color(0f, 0f, 0f, 5f / 60f);
		}
		else if (frame < 12)
		{
			displayPlane.color -= new Color(0f, 0f, 0f, 5f / 60f);
			if (displayPlane.color.a < 0f)
			{
				displayPlane.color = new Color(displayPlane.color.r, displayPlane.color.g, displayPlane.color.b, 0f);
			}
		}
		// 暗転したらムービーを流す
		if (displayPlane.color.a >= 1f)
		{
			playState = MoviePlayState.ePlaying;
			videoPlayer.gameObject.SetActive(true);
			videoRawImage.gameObject.SetActive(true);
			videoPlayer.Play();
			frame = 0;
		}
	}
}
