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
}
