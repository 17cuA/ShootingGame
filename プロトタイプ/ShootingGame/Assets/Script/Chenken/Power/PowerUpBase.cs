using System;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType
{
	PowerUp_SpeedUp = 0,	//加速
	PowerUp_Missile,		//ミサイル導入
	PowerUp_Double,			//
	PowerUp_Laser,			//レーサー導入
	PowerUp_Option,			//ビット作成
	PowerUp_Undecided,		//未知（実シールド？）

	PowerUp_KillAll,		//敵全滅

}
public class PowerUpBase
{
	public delegate void OnExcuteCallBack();        //パワーアップの処理タイプ（引数なし）
	public PowerUpType Type { get; private set; }   //パワーアップタイプ（Get用）
	public string Name { get; private set; }    //パワーアップ名（Get用）
	public int Max { get; private set; }    //パワーアップレベル最大数（Get用）
	public int Count { get; private set; }  //現在パワーアップレベル（Get、Set）
	public bool CannotUpgrade
	{
		get
		{
			return Count == Max;
		}
	}

	public bool IsMainWeaponUpgrade { get; private set; }
	public bool IsWeaponUsing { get; set; }



	//パワーアップ時の処理集合
	private List<OnExcuteCallBack> onExcuteCallBacks;
	public List<OnExcuteCallBack> OnExcuteCallBacks
	{
		get
		{
			return onExcuteCallBacks;
		}
	}

	public void Excute()
	{
		if (!CannotUpgrade)
		{
			for (var i = 0; i < onExcuteCallBacks.Count; ++i)
			{
				onExcuteCallBacks[i]();
			}

			if (IsMainWeaponUpgrade)
				IsWeaponUsing = true;
			else
				Count++;

		}
	}


	/// <summary>
	/// コンストラクタで初期化させる
	/// </summary>
	/// <param name="type">　　パワーアップタイプ　　　　</param>
	/// <param name="name">　　パワーアップ名　　　　　　</param>
	/// <param name="max">　　パワーアップ可能最大レベル　</param>
	public PowerUpBase(PowerUpType type,string name,int max)
	{
		onExcuteCallBacks = new List<OnExcuteCallBack>();
		this.Type = type;
		this.Name = name;
		this.Max = max;
	}

	public PowerUpBase(PowerUpType type, string name, bool isMainWeaponUpgrade)
	{
		onExcuteCallBacks = new List<OnExcuteCallBack>();
		this.Type = type;
		this.Name = name;
		this.IsMainWeaponUpgrade = isMainWeaponUpgrade;
		this.IsWeaponUsing = false;
	}
}

