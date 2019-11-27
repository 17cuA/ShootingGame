using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_Movement : MonoBehaviour
{
	/// <summary>
	/// 移動距離
	/// </summary>
	static public Vector3 MovingDistance { get; private set; } 

	private Vector3 PreviousFramePosition { get; set; }		// 前の位置
	private Vector3 NowFramePosition { get; set; }          // 今の位置

	private void Start()
    {
		PreviousFramePosition = NowFramePosition = transform.position;
    }

	private void LateUpdate()
	{
		MovingDistance = NowFramePosition - PreviousFramePosition;
	}
}
