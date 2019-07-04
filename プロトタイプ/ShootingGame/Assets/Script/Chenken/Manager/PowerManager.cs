using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 名前空間Power
/// PowerManagerを利用する際にusing powerを書いてください。
/// </summary>
namespace Power
{
	public class PowerManager : Singleton<PowerManager>
	{
		/// <summary>
		/// Powerクラス
		/// Powerの種類、タグ、メイン武器として使われているかどうか、
		/// 強化などの情報を持っている
		/// </summary>
		public class Power
		{
			/// <summary>
			/// UpgradeInfoクラス
			/// 現在レベルと最大アップグレード回数
			/// これから強化できるかどうかの情報を持っている
			/// </summary>
			public class UpgradeInfo
			{
				public int upgradeCount;           //現在強化回数
				public int maxUpgradeTime;      //最大強化回数

				//強化可能かどうか
				public bool canUpgrade
				{
					get
					{
						return upgradeCount < maxUpgradeTime;
					}
				}

				public UpgradeInfo(int maxUpgradeTime)
				{
					this.maxUpgradeTime = maxUpgradeTime;
					this.upgradeCount = 0;
				}

				public void Reset()
				{
					upgradeCount = 0;
				}
			}

			//パワータイプ列挙型
			public enum PowerType { SPEEDUP, MISSILE, DOUBLE, LASER, OPTION, SHIELD, KILLALL, INITSPEED}

			//パワータグ列挙型
			public enum PowerTag { NORMAL, WEAPON, TOENEMY }

			//強化時のイベント（処理、関数群）
			public delegate void OnUpgradeCallBack();

			public PowerType type;								   //パワータイプ
			public PowerTag tag;									   //パワータグ
			public bool isUsing;										　//武器のパワーアップとして使用されている？
			public UpgradeInfo upgradeInfo = null;          //強化情報（未使用時はNull）

			//UIで表示させるか？
			public bool CanShow
			{
				get
				{
					return (tag == PowerTag.WEAPON && !isUsing) || (tag == PowerTag.NORMAL && upgradeInfo.canUpgrade);
				}
			}

			private event OnUpgradeCallBack callBack_void;  //強化イベント

			/// <summary>
			/// コンストラクタ
			/// </summary>
			/// <param name="type">　　　　　　　パワータイプ　</param>
			/// <param name="tag">　　　　　　　パワータグ　　　</param>
			/// <param name="maxUpgradeTime">　最大強化回数　</param>
			public Power(PowerType type, int maxUpgradeTime)
			{
				this.type = type;
				this.tag = PowerTag.NORMAL;

				this.upgradeInfo = new UpgradeInfo(maxUpgradeTime); //強化情報は設定
			}

			/// <summary>
			/// コンストラクタ
			/// </summary>
			/// <param name="type">　パワータイプ</param>
			/// <param name="tag">　 パワータグ</param>
			public Power(PowerType type)
			{
				this.type = type;
				this.tag = PowerTag.WEAPON;
			}

			/// <summary>
			/// コンストラクタ
			/// デフォルトは敵全滅パワー
			/// </summary>
			public Power()
			{
				this.type = PowerType.KILLALL;
				this.tag   = PowerTag.TOENEMY;
			}

			/// <summary>
			/// 強化　具体的な処理
			/// </summary>
			public void Upgrade()
			{
				//イベント発生、関数群実行
				callBack_void?.Invoke();

				//タグが武器の場合
				if (tag == PowerTag.WEAPON)
				{
					//現在武器のパワーアップはこのパワーアップではない場合
					if (Instance.weaponPower != this)
					{
						//現在武器のパワーアップはNullではない場合
						if (Instance.weaponPower != null)
							Instance.weaponPower.isUsing = false;   　//現在武器のパワーアップを　使われていない　に　設定

						Instance.weaponPower = this;                　　//現在武器のパワーアップを　このパワーアップ　に　設定
						Instance.weaponPower.isUsing = true;        　//現在武器のパワーアップを　使われている　に　設定
					}
				}
				//タグが普通の場合
				if (tag == PowerTag.NORMAL)
				{
					//普通の場合は何回もアップグレードできる
					//強化回数　＋1;
					upgradeInfo.upgradeCount++;
				}
			}

			/// <summary>
			/// 関数を　このパワーが強化された時に発生するイベントの関数群　に入れる
			/// </summary>
			/// <param name="callBack">　処理（引数なし）　</param>
			public void AddFunction(OnUpgradeCallBack callBack)
			{
				if (callBack == null)
					return;

				callBack_void += callBack;
			}

			/// <summary>
			/// 関数を　このパワーが強化された時に発生するイベントの関数群　から削除する
			/// </summary>
			/// <param name="callBack">　処理（引数なし）　</param>
			public void RemoveFunction(OnUpgradeCallBack callBack)
			{
				if (callBack == null)
					return;

				callBack_void -= callBack;
			}
		}

		//全部のパワー
		private Dictionary<Power.PowerType, Power> powers = new Dictionary<Power.PowerType, Power>
		{
			{ Power.PowerType.SPEEDUP, new Power(Power.PowerType.SPEEDUP, 5) },
			{ Power.PowerType.MISSILE, new Power(Power.PowerType.MISSILE, 1) },
			{ Power.PowerType.DOUBLE,  new Power(Power.PowerType.DOUBLE) },
			{ Power.PowerType.LASER,   new Power(Power.PowerType.LASER) },
			{ Power.PowerType.OPTION,  new Power(Power.PowerType.OPTION,  4) },
			{ Power.PowerType.SHIELD,  new Power(Power.PowerType.SHIELD, 1) },

			{ Power.PowerType.INITSPEED,new Power(Power.PowerType.INITSPEED, 1) }
		};

		//敵全滅パワー
		private Power annihilate = new Power();

		//現在パワー
		public Power CurrentPower
		{
			get
			{
				if (0 <= Position && Position < powers.Count)
					return powers[(Power.PowerType)Position];

				return null;
			}
		}

        public bool CanUpgrade
        {
            get
            {
                return !(CurrentPower == null || (CurrentPower.tag == Power.PowerTag.NORMAL && !CurrentPower.upgradeInfo.canUpgrade)
				|| (CurrentPower.tag == Power.PowerTag.WEAPON && CurrentPower.isUsing));
            }
        }

		//武器のパワーアップとして使われているパワー
		private Power weaponPower = null;

		//位置
		private int position = -1;
		public int Position
		{
			get
			{
				return position;
			}
		}

		/// <summary>
		/// 関数を　このパワーが強化された時に発生するイベントの関数群　に入れる
		/// 具体的な処理はPowerクラス内に移行
		/// </summary>
		/// <param name="callBack">　処理（引数なし）　</param>
		public void AddFunction(Power.PowerType type, Power.OnUpgradeCallBack callBack)
		{
			if (type == Power.PowerType.KILLALL)
			{
				annihilate.AddFunction(callBack);
				return;
			}

			if (!powers.ContainsKey(type))
				return;

			powers[type].AddFunction(callBack);
		}

		/// <summary>
		/// 関数を　このパワーが強化された時に発生するイベントの関数群　から削除する
		/// 具体的な処理はPowerクラス内に移行
		/// </summary>
		/// <param name="callBack">　処理（引数なし）　</param>
		public void RemoveFunction(Power.PowerType type, Power.OnUpgradeCallBack callBack)
		{
			if (type == Power.PowerType.KILLALL)
			{
				annihilate.RemoveFunction(callBack);
				return;
			}

			if (!powers.ContainsKey(type))
				return;

			powers[type].RemoveFunction(callBack);
		}

		/// <summary>
		/// 強化
		/// 具体的な処理はPowerクラス内に移行
		/// </summary>
		public void Upgrade()
		{
			//現在パワーはNull、現在パワーは普通でさらに強化できない、現在パワーは武器のパワーアップ、今使われている　　の場合は　　強化不可能、処理中断
			if (!CanUpgrade)
				return;

			//その以外の場合は強化を行う
			// 具体的な処理はPowerクラス内に移行
			CurrentPower.Upgrade();

			//if (CurrentPower.type == Power.PowerType.SPEEDUP)
			//{
			//	if (!CurrentPower.upgradeInfo.canUpgrade)
			//	{
			//		CurrentPower.upgradeInfo.Reset();
			//		var temp = powers[Power.PowerType.SPEEDUP];
			//		powers[Power.PowerType.SPEEDUP] = powers[Power.PowerType.INITSPEED];
			//		powers[Power.PowerType.INITSPEED] = temp;
			//	}
			//}

			//if (CurrentPower.type == Power.PowerType.INITSPEED)
			//{
			//	if (!CurrentPower.upgradeInfo.canUpgrade)
			//	{
			//		CurrentPower.upgradeInfo.Reset();
			//		var temp = powers[Power.PowerType.INITSPEED];
			//		powers[Power.PowerType.INITSPEED] = powers[Power.PowerType.SPEEDUP];
			//		powers[Power.PowerType.SPEEDUP] = temp;
			//	}
			//}

			//強化成功、位置をリセット
			position = -1;
		}

		public void  Annihilate()
		{
			annihilate.Upgrade();
		}

		/// <summary>
		/// パワー取得時処理
		/// </summary>
		public void Pick()
		{
			//位置移動
			position++;

			position %= powers.Count - 1;
		}


		/// <summary>
		/// パワーを取得
		/// </summary>
		/// <param name="type">　パワータイプ　</param>
		/// <returns></returns>
		public Power GetPower(Power.PowerType type)
		{
			if (type == Power.PowerType.KILLALL)
				return annihilate;

			if (!powers.ContainsKey(type))
				return null;

			return powers[type];
		}

		/// <summary>
		/// シールドリセット
		/// </summary>
		public void ResetShieldPower()
		{
			powers[Power.PowerType.SHIELD].upgradeInfo.Reset();
		}

		/// <summary>
		///　ビットイン以外、プレイヤー死亡時呼び出し
		/// </summary>
		public void ResetAllPower()
		{
			powers[Power.PowerType.SHIELD].upgradeInfo.Reset();
			powers[Power.PowerType.MISSILE].upgradeInfo.Reset();
			powers[Power.PowerType.DOUBLE].isUsing = false;
			powers[Power.PowerType.LASER].isUsing = false;
            weaponPower = null;
			powers[Power.PowerType.INITSPEED].upgradeInfo.Reset();
			powers[Power.PowerType.SHIELD].upgradeInfo.Reset();
			
			if(powers[Power.PowerType.SPEEDUP].type != Power.PowerType.SPEEDUP)
			{
				var temp = powers[Power.PowerType.SPEEDUP];
				powers[Power.PowerType.SPEEDUP] = powers[Power.PowerType.INITSPEED];
				powers[Power.PowerType.INITSPEED] = temp;
			}
		}

		/// <summary>
		/// ビトンリセット
		/// </summary>
		public void ResetOptionPower()
		{
			powers[Power.PowerType.OPTION].upgradeInfo.Reset();
		}

		/// <summary>
		/// ビットン個数を強化回数に設定する
		/// </summary>
		/// <param name="num">　ビットン個数　</param>
		public void SetOptionPower(int num)
		{
			powers[Power.PowerType.OPTION].upgradeInfo.upgradeCount = num;
		}
	}
}

