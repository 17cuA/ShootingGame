/*
 * 20190827 作成
 * author hasegawa yuuta
 */
/* トリガーがオンになったときに設定したフレーム数に飛ばす動作の実装 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public class SetTimeOnTriggerBehaviour : PlayableBehaviour
{
	public PlayableDirector Director { get; set; }
	public SetTimeTrigger Trigger { get; set; }
	public int SetFrame { get; set; } = 0;		// とばした先のフレーム
	public int DelayFrame { get; set; } = 0;	// 遅延させるフレーム数
	int frame = 0;								// 現在のフレーム数

	public override void OnBehaviourPlay(Playable playable, FrameData info)
	{
		// ディレクターがなければスキップしない
		if (!Director) { return; }
		// 遅延
		if (frame < DelayFrame) { ++frame; return; }
		// 設定した任意のフレームまでスキップする
		double time = SetFrame / 60.0;
		Director.time = time;
	}
}
