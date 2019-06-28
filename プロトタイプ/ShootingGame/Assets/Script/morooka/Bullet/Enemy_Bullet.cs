using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : bullet_status
{
	private new void Start()
	{
		base.Start();
		Tag_Change("Enemy_Bullet");
	}
	private new void Update()
    {
		Moving_To_Facing();
    }
}
