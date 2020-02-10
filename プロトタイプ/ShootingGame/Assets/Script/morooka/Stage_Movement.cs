using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Stage_Movement : MonoBehaviour
{
	/// <summary>
	/// 移動距離
	/// </summary>
	static public Vector3 MovingDistance { get; private set; }

	/// <summary>
	/// 回転量
	/// </summary>
	static public Quaternion RotationAmount { get; private set; }

	private Vector3 PreviousFramePosition { get; set; }		// 前の位置
	private Vector3 NowFramePosition { get; set; }          // 今の位置

	private Quaternion PreviousFrameRotation { get; set; }			// 前の角度
	private Quaternion NowFrameRotation { get; set; }					// 今の角度

	private PlayableDirector Director { get; set; }             // デバッグ用プレイアブルディレクター

	[Tooltip("タイムラインの停止指示")]public bool Is_TimelinePause;

	private void Start()
    {
		PreviousFramePosition = NowFramePosition = transform.position;
		Director = GetComponent<PlayableDirector>();
		Is_TimelinePause = false;
	}

	private void LateUpdate()
	{
		if (Input.GetKey(KeyCode.P) && Input.GetKey(KeyCode.B)) Director.time = 253.06;

		MovingDistance = NowFramePosition - PreviousFramePosition;
		RotationAmount = Quaternion.Euler(NowFrameRotation.eulerAngles - PreviousFrameRotation.eulerAngles);

		// タイムラインの停止
		if(Is_TimelinePause)
		{
			Director.Pause();
			Is_TimelinePause = false;
		}
	}
}