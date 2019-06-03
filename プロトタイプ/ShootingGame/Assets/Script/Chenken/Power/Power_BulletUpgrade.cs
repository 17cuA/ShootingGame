using System;
using System.Collections.Generic;
using Power;

public class Power_BulletUpgrade : AbstractPower
{

	public override bool IsLost
	{
		get
		{
			return false;
		}
	}
	public Power_BulletUpgrade(PowerType type) : base(type)
	{

	}

	public override void OnPick()
	{
		base.OnPick();
	}

	public override void OnUpdate(float deltaTime)
	{
		base.OnUpdate(deltaTime);
	}

	public override void OnPickAgain()
	{
		base.OnPickAgain();
	}

	public override void OnLost()
	{
		base.OnLost();
	}
}

