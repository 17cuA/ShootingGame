using System;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : Singleton<PowerUpManager>
{
	//デフォルトパワーアップリスト
	private List<PowerUpBase> powerUps = new List<PowerUpBase>
	{
		//初期化方法　->　パワーアップタイプ、パワーアップ名、アップグレード可能回数
		{ new PowerUpBase(PowerUpType.PowerUp_SpeedUp,"SPEED UP", 4) },
		{ new PowerUpBase(PowerUpType.PowerUp_Missile,"MISSILE" , 1) },
		{ new PowerUpBase(PowerUpType.PowerUp_Double, "DOUBLE"  , true) },
		{ new PowerUpBase(PowerUpType.PowerUp_Laser,  "LASER"   , true) },
		{ new PowerUpBase(PowerUpType.PowerUp_Option, "OPTION"  , 4) },
		{ new PowerUpBase(PowerUpType.PowerUp_Undecided,  "?"   , 4) },
	};

	//別種パワーアップ　->　敵全滅させる
	private PowerUpBase killAllEnemyPower = new PowerUpBase(PowerUpType.PowerUp_KillAll, "KillAll",1);

	private int currentSlot = -1;
	//現在位置
	public int CurrentSlot
	{
		get
		{
			return currentSlot;
		}
	}
	//現在選択パワーアップ
	public PowerUpBase CurrentPowerUp
	{
		get
		{
			if(currentSlot < 0)
				return null;

			return powerUps[currentSlot];
		}
	}
	private bool isSelect = false;
	//選択中なのかどうか
	public bool IsSelect
	{
		get
		{
			return isSelect;
		}
	}

	/// <summary>
	/// アイテム所得時の処理を入れる関数
	/// </summary>
	/// <param name="type">　　　パワーアップタイプ　　　　　　　　</param>
	/// <param name="callBack">　パワーアップ所得時処理（引数なし）</param>
	public void AddEvent(PowerUpType type, PowerUpBase.OnExcuteCallBack callBack)
	{
		//Null　処理は認めない
		if (callBack == null)
			return;

		//特別処理（敵全滅させる）
		if(type == PowerUpType.PowerUp_KillAll)
		{
			killAllEnemyPower.OnExcuteCallBacks.Add(callBack);
			return;
		}

		//一時的なパワーアップ
		PowerUpBase temp = null;
		
		//パワーアップリストチェック
		for (var i = 0; i < powerUps.Count; ++i)
		{
			//検索成功
			if (powerUps[i].Type == type)
			{
				temp = powerUps[i];
				break;
			}
		}

		//検索失敗
		if (temp == null)
		{
			Debug.LogError("存在しないパワーアップ、処理監視失敗しました。");
			return;
		}

		//監視処理を追加
		temp.OnExcuteCallBacks.Add(callBack);
	}

	/// <summary>
	/// アイテム所得時の処理を消す関数
	/// </summary>
	/// <param name="type">　　　パワーアップタイプ　</param>
	/// <param name="callBack">　パワーアップ所得時処理　</param>
	public void RemoveEvent(PowerUpType type, PowerUpBase.OnExcuteCallBack callBack)
	{
		//Null　処理は認めない
		if (callBack == null)
			return;

		//特別処理（敵全滅させる）
		if (type == PowerUpType.PowerUp_KillAll)
		{
			//消そうとする処理は含める場合
			if (killAllEnemyPower.OnExcuteCallBacks.Contains(callBack))
			{
				//処理を消す
				killAllEnemyPower.OnExcuteCallBacks.Remove(callBack);
				return;
			}
		}
		//一時的なパワーアップ
		PowerUpBase temp = null;

		//パワーアップリストチェック
		for (var i = 0; i < powerUps.Count; ++i)
		{
			//検索成功
			if (powerUps[i].Type == type)
			{
				temp = powerUps[i];
				break;
			}
		}

		//検索失敗
		if (temp == null)
		{
			Debug.LogError("存在しないパワーアップ、処理削除失敗しました。");
			return;
		}

		//監視処理を削除
		if (temp.OnExcuteCallBacks.Contains(callBack))		
			temp.OnExcuteCallBacks.Remove(callBack);
	}

	/// <summary>
	/// パワーアップ決定
	/// 確認ボタン　-> パワーアップ決定　->　何かの処理をする
	/// </summary>
	public void Excute()
	{
		//現在パワーアップはアップグレード出来ない、そしてNull　ではない場合
		if (CurrentPowerUp != null && isSelect)
		{
			if (!CurrentPowerUp.CannotUpgrade)
			{
				CurrentPowerUp.Excute();

				isSelect = false;
			}	

			if(CurrentPowerUp.IsMainWeaponUpgrade)
			{
				if (CurrentPowerUp.IsWeaponUsing)
				{
					for (var i = 0; i < powerUps.Count; ++i)
					{
						if (powerUps[i].Type != CurrentPowerUp.Type && powerUps[i].IsMainWeaponUpgrade && powerUps[i].IsWeaponUsing)
						{
							powerUps[i].IsWeaponUsing = false;
						}
					}
				}
				else
				{
					CurrentPowerUp.Excute();
					isSelect = false;
				}
			}
		}
	}

	/// <summary>
	/// 敵全滅させるアイテム取得時呼び出す
	/// 使うタイミングがプレイヤーが決められない
	/// 取得瞬間実行する
	/// </summary>
	public void KillingExcute()
	{
		for (var i = 0; i < CurrentPowerUp.OnExcuteCallBacks.Count; ++i)
			killAllEnemyPower.OnExcuteCallBacks[i]();
	}

	/// <summary>
	/// アイテムを拾い、選択状態にする
	/// 同時にUIに反映
	/// </summary>
	public void ApplyPowerUpSelection()
	{
		if (IsSelect)
		{
			currentSlot++;
			currentSlot = currentSlot % powerUps.Count;
		}
		else
		{
			isSelect = true;
			currentSlot = 0;
		}
	}

	/// <summary>
	/// パワーアップリストからパワーアップ取得
	/// </summary>
	/// <param name="type">　</param>
	/// <returns></returns>
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

