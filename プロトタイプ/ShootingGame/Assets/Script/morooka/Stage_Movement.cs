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
	private Vector3 NowFramePosition { get; set; }			// 今の位置

	private Quaternion PreviousFrameRotation { get; set; }			// 前の角度
	private Quaternion NowFrameRotation { get; set; }				// 今の角度

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
		if (Input.GetKey(KeyCode.P) && Input.GetKey(KeyCode.B)) Director.time = 260.0;
		else if (Input.GetKey(KeyCode.P) && Input.GetKey(KeyCode.V)) Director.time = 58.0;
		else if (Input.GetKey(KeyCode.P) && Input.GetKey(KeyCode.C)) Director.time = 75.0;
        else if (Input.GetKey(KeyCode.P) && Input.GetKey(KeyCode.X)) Director.time = 125.0;
        else if (Input.GetKey(KeyCode.P) && Input.GetKey(KeyCode.Z)) Director.time = 178.0;
        else if (Input.GetKey(KeyCode.P) && Input.GetKey(KeyCode.A)) Director.time = 324.0;
        else if (Input.GetKey(KeyCode.P) && Input.GetKey(KeyCode.S)) Director.time = 369.0;
        if (Input.GetKey(KeyCode.Slash)) Director.time += 1.0;
		else if (Input.GetKey(KeyCode.Backslash)) Director.time -= 1.0;
		MovingDistance = transform.position - PreviousFramePosition;
		RotationAmount = Quaternion.Euler(transform.eulerAngles - PreviousFrameRotation.eulerAngles);

		PreviousFramePosition = transform.position;
		PreviousFrameRotation = transform.rotation;
		// タイムラインの停止
		if (Is_TimelinePause)
		{
			Director.Pause();
			Is_TimelinePause = false;
		}
	}
}