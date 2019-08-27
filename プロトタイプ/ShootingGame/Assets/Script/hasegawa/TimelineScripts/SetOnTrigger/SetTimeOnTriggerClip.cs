using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public class SetTimeOnTriggerClip : PlayableAsset, ITimelineClipAsset
{
	public ClipCaps clipCaps
	{
		get { return ClipCaps.None; }
	}

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		// 生成
		ScriptPlayable<SetTimeOnTriggerBehaviour> playable = ScriptPlayable<SetTimeOnTriggerBehaviour>.Create(graph);
		SetTimeOnTriggerBehaviour behaviour = playable.GetBehaviour();
		// 情報の設定
		return playable;
	}
}