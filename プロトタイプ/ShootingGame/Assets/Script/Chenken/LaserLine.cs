using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class LaserLine : Player_Bullet
{
    private new void Update()
    {
        base.Update();

        base.Moving_To_Travelling_Direction();
    }
}
