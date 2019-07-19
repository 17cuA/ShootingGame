using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class LaserLine : Player_Bullet
{
    private void Awake()
    {
        base.shot_speed = 0.8f;
        base.attack_damage = 1;
        base.Travelling_Direction = Vector3.right;
    }

    private new void Update()
    {
        base.Update();

        base.Moving_To_Travelling_Direction();
    }
}
