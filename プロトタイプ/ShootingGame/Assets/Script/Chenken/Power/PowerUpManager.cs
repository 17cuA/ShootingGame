using System;
using System.Collections.Generic;

public class PowerUpManager : Singleton<PowerUpManager>
{
	private List<PowerUpBase> powerUps = new List<PowerUpBase>
	{
		{new PowerUpBase(PowerUpType.PowerUp_SpeedUp, "SPEED UP",4) },
		{new PowerUpBase(PowerUpType.PowerUp_Missile,"MISSILE",1) },
		{new PowerUpBase(PowerUpType.PowerUp_Double,"DOUBLE",1) },
		{new PowerUpBase(PowerUpType.PowerUp_Laser,"LASER",1) },
		{new PowerUpBase(PowerUpType.PowerUp_Option,"OPTION",4) },
	};

	private PowerUpBase killAllEnemyPower = new PowerUpBase(PowerUpType.PowerUp_KillAll, "KillAll",1);

	private int currentSlot = -1;
	public int CurrentSlot
	{
		get
		{
			return currentSlot;
		}
	}
	private PowerUpBase currentPowerUp = null;
	public PowerUpBase CurrentPowerUp
	{
		get
		{
			return currentPowerUp;
		}
	}
	private bool isSelect = false;
	public bool IsSelect
	{
		get
		{
			return isSelect;
		}
	}


	public void AddEvent(PowerUpType type, PowerUpBase.OnExcuteCallBack callBack)
	{
		if (callBack == null)
		{
			return;
		}
		if(type == PowerUpType.PowerUp_KillAll)
		{
			killAllEnemyPower.OnExcuteCallBacks.Add(callBack);
			return;
		}
		PowerUpBase temp = null;
		for (var i = 0; i < powerUps.Count; ++i)
		{
			if (powerUps[i].Type == type)
			{
				temp = powerUps[i];
				break;
			}
		}
		if (temp == null)
		{
			return;
		}
		temp.OnExcuteCallBacks.Add(callBack);
	}

	public void RemoveEvent(PowerUpType type, PowerUpBase.OnExcuteCallBack callBack)
	{
		if (callBack == null)
		{
			return;
		}
		if (type == PowerUpType.PowerUp_KillAll)
		{
			if (killAllEnemyPower.OnExcuteCallBacks.Contains(callBack))
			{
				killAllEnemyPower.OnExcuteCallBacks.Remove(callBack);
				return;
			}
		}
		PowerUpBase temp = null;
		for (var i = 0; i < powerUps.Count; ++i)
		{
			if (powerUps[i].Type == type)
			{
				temp = powerUps[i];
				break;
			}
		}
		if (temp == null)
		{
			return;
		}
		if (temp.OnExcuteCallBacks.Contains(callBack))
		{
			temp.OnExcuteCallBacks.Remove(callBack);
		}
	}

	public void Excute()
	{
		if (currentPowerUp != null && !currentPowerUp.CannotUpgrade)
		{
			
			for (var i = 0; i < currentPowerUp.OnExcuteCallBacks.Count; ++i)
			{
				currentPowerUp.OnExcuteCallBacks[i]();
			}
			
			currentSlot = -1;
			currentPowerUp.Count++;
			isSelect = false;		
		}
	}

	public void KillingExcute()
	{
		for (var i = 0; i < currentPowerUp.OnExcuteCallBacks.Count; ++i)
		{
			killAllEnemyPower.OnExcuteCallBacks[i]();
		}	
	}


	public void ApplyPowerUpSelection()
	{
		currentSlot++;
		currentSlot = currentSlot % 5;
		currentPowerUp = powerUps[currentSlot];
		isSelect = true;
	}

	public PowerUpBase GetPowerUp(PowerUpType type)
	{
		PowerUpBase temp = null;
		for (var i = 0; i < powerUps.Count; ++i)
		{
			if (powerUps[i].Type == type)
			{
				temp = powerUps[i];
				break;
			}
		}
		if (temp == null)
		{
			return null;
		}
		return temp;
	}	
}

