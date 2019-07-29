﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

[TrackColor(0.5f,0.5f,0.5f)]
[TrackClipType(typeof(ShootingStarTLClip))]
[TrackBindingType(typeof(ShootingStarAnimator))]
public class ShootingStarTLTrack : TrackAsset
{
}
