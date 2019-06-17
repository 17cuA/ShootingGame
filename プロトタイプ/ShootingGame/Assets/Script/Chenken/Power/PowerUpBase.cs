using System;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType
{
	PowerUp_SpeedUp = 0,
	PowerUp_Missile,
	PowerUp_Double,
	PowerUp_Laser,
	PowerUp_Option,
	PowerUp_Undecided,

	PowerUp_KillAll,

}
public class PowerUpBase 
{
	public delegate void OnExcuteCallBack();

	public PowerUpType Type { get; private set; }

	public string Name { get; private set; }

	public int Max { get; private set; }

	public int Count { get; set; }

	public bool CannotUpgrade
	{
		get
		{
			return Count == Max;
		}
	}

	private List<OnExcuteCallBack> onExcuteCallBacks;
	public List<OnExcuteCallBack> OnExcuteCallBacks
	{
		get
		{
			return onExcuteCallBacks;
		}
	}

	public PowerUpBase(PowerUpType type,string name,int max)
	{
		onExcuteCallBacks = new List<OnExcuteCallBack>();
		this.Type = type;
		this.Name = name;
		this.Max = max;
	}
}

