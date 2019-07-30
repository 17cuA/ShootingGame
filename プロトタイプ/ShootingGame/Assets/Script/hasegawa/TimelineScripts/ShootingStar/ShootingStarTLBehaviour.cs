using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class ShootingStarTLBehaviour : PlayableBehaviour
{
	public ShootingStarAnimator shootingStarAnimator { get; set; }
	public override void OnBehaviourPlay(Playable playable, FrameData info)
	{
		shootingStarAnimator.AnimationRandomShootingStar();
	}
}
