using System;
using System.Collections.Generic;
using UnityEngine;

namespace Power
{
	public delegate bool CheckPowerResetCallBack();
	public delegate void ExcutePowerUpgradeCallBack();
	public delegate void ResetPowerCallBack();

	public enum PowerType { SPEEDUP, MISSILE, DOUBLE, LASER, OPTION, SHIELD, INITSPEED, KILLALL }

	public class PowerManager : Singleton<PowerManager>
	{
		public class Power
		{
			public enum PowerType
			{
				SPEEDUP,
				MISSILE,
				DOUBLE,
				LASER,
				OPTION,
				SHIELD,
				INITSPEED,
				KILLALL
			}

			private int upgradeCount;
			private int maxUpgradeCount;
			private PowerType type;
			private event ExcutePowerUpgradeCallBack onUpgradeCallBack;
			private event ResetPowerCallBack onResetCallBack;
			private CheckPowerResetCallBack onCheckResetCallBack;
			private bool resetCheck;

			public PowerType Type { get { return type; } }
			public bool CanUpgrade
			{
				get
				{
					return upgradeCount < maxUpgradeCount;
				}
			}

			public Power(PowerType type, int maxUpgradeCount)
			{
				this.upgradeCount = 0;
				this.maxUpgradeCount = maxUpgradeCount;
				this.type = type;
				this.resetCheck = false;
			}

			public Power(PowerType type)
			{
				this.type = type;
				this.resetCheck = false;
				this.upgradeCount = 0;
				this.maxUpgradeCount = 999999999;
			}


			public void Upgrade()
			{
				onUpgradeCallBack?.Invoke();

				upgradeCount++;
				Debug.Log("強化成功、パワーアップ名：" + type.ToString());

			}

			public void Update()
			{

				if (onCheckResetCallBack != null && resetCheck != onCheckResetCallBack() && onCheckResetCallBack() == true)
				{
					SetUpgradeCount(0);
					onResetCallBack?.Invoke();
					resetCheck = onCheckResetCallBack();
					Debug.Log(type.ToString() + "リセット");
				}
				resetCheck = false;
			}


			public void SetUpgradeCount(int count)
			{
				if (upgradeCount == count)
					return;

				if (count > maxUpgradeCount)
					return;

				upgradeCount = count;
			}

			public void AddUpgradeFunction(ExcutePowerUpgradeCallBack callBack)
			{
				if (callBack == null)
					return;

				onUpgradeCallBack += callBack;
			}

			public void RemoveUpgradeFunction(ExcutePowerUpgradeCallBack callBack)
			{
				if (callBack == null)
					return;

				onUpgradeCallBack -= callBack;
			}

			public void AddCheckFunction(CheckPowerResetCallBack callBack,ResetPowerCallBack resetCallBack)
			{
				if (callBack == null)
					return;

				onCheckResetCallBack = callBack;
				onResetCallBack += resetCallBack;
			}

			public void RemoveCheckFunction(ResetPowerCallBack resetCallBack)
			{
				onCheckResetCallBack = null;

				if (onCheckResetCallBack != null)
					onResetCallBack -= resetCallBack;
			}
		}
	

		private Dictionary<Power.PowerType, Power> powers = new Dictionary<Power.PowerType, Power>
		{
			{ Power.PowerType.SPEEDUP, new Power(Power.PowerType.SPEEDUP, 5) },
			{ Power.PowerType.MISSILE, new Power(Power.PowerType.MISSILE, 1) },
			{ Power.PowerType.DOUBLE,  new Power(Power.PowerType.DOUBLE,  1) },
			{ Power.PowerType.LASER,   new Power(Power.PowerType.LASER,   1) },
			{ Power.PowerType.OPTION,  new Power(Power.PowerType.OPTION,  4) },
			{ Power.PowerType.SHIELD,  new Power(Power.PowerType.SHIELD,  1) }
		};

		private Power annihilate = new Power(Power.PowerType.KILLALL);

		private int position = -1;
		public int Position
		{
			get
			{
				return position;
			}
		}

		public Power CurrentPower
		{
			get
			{
				if (0 <= position && position < powers.Count)
					return powers[(Power.PowerType)position];

				return null;
			}
		}

		public bool CurrentPowerCanUpgrade
		{
			get
			{
				return CurrentPower != null && CurrentPower.CanUpgrade;
			}
		}

		public void AddFunction(Power.PowerType type, ExcutePowerUpgradeCallBack callBack)
		{
			if (callBack == null)
				return;

			if(type == Power.PowerType.KILLALL)
			{
				annihilate.AddUpgradeFunction(callBack);
				return;
			}

			if (!powers.ContainsKey(type))
				return;

			powers[type].AddUpgradeFunction(callBack);
		}

		public void RemoveFunction(Power.PowerType type, ExcutePowerUpgradeCallBack callBack)
		{
			if (callBack == null)
				return;

			if(type == Power.PowerType.KILLALL)
			{
				annihilate.RemoveUpgradeFunction(callBack);
				return;
			}

			if (!powers.ContainsKey(type))
				return;

			powers[type].RemoveUpgradeFunction(callBack);
		}

		public void AddCheckFunction(Power.PowerType type, CheckPowerResetCallBack callBack,ResetPowerCallBack resetCallBack)
		{
			if (callBack == null)
				return;

			if (!powers.ContainsKey(type))
				return;

			powers[type].AddCheckFunction(callBack,resetCallBack);
		}

		public void RemoveCheckFunction(Power.PowerType type, CheckPowerResetCallBack callBack,ResetPowerCallBack resetCallBack)
		{
			if (callBack == null)
				return;

			if (!powers.ContainsKey(type))
				return;

			powers[type].RemoveCheckFunction(resetCallBack);
		}

		public void Upgrade()
		{
			if (!CurrentPowerCanUpgrade)
				return;

			CurrentPower.Upgrade();
			position = -1;
		}

		public void Annihilate()
		{
			annihilate.Upgrade();
		}

		public void Pick()
		{
			position++;

			position %= powers.Count;
		}

		public Power GetPower(Power.PowerType type)
		{
			if (type == Power.PowerType.KILLALL)
				return annihilate;

			if (!powers.ContainsKey(type))
				return null;

			return powers[type];
		}

		public void UpdateBit(int count)
		{
			powers[Power.PowerType.OPTION].SetUpgradeCount(count);
		}

		public void Update()
		{
			for(var i = 0; i < powers.Count - 1; ++i)
			{
				var power = powers[(Power.PowerType)i];
				power.Update();
			}
		}

		//後消す
		public void ResetShieldPower() { }
		public void ResetAllPower() { }
	}	
}
