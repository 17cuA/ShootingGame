using System;
using System.Collections.Generic;
using UnityEngine;
using Power;

public class Power_Shield : AbstractPower
{
	/// <summary>
	/// 初めて取得時のシールド値（最大値）を保存
	/// </summary>
	private int shield;

	/// <summary>
	/// オーバーライド消滅条件
	/// プレイヤーはシールドを持ってない（破壊された）
	/// </summary>
	public override bool IsLost
	{
		//※※※※※変更必要※※※※※
		get
		{
			return value <= 0;
		}
	}

	/// <summary>
	/// コンストラクタ
	/// 構造方法
	/// </summary>
	/// <param name="type">パワーアップ種類</param>
	/// <param name="value">パワーアップ設定値</param>
	/// <param name="manager">パワーアップマネージャー</param>
	public Power_Shield(PowerType type, int value) : base(type)
	{
		//シールド最大値（初めて取得時の値を保存）
		this.shield = value;
	}

	/// <summary>
	/// シールド取得時の処理
	/// オーバーライド
	/// </summary>
	//※※※※※変更必要※※※※※
	public override void OnPick()
	{
		Debug.Log("シールド" + "(" + Type + ")" + " 取得");

		this.value = shield;
		base.OnPick();
	}

	/// <summary>
	/// シールド消滅時の処理
	/// オーバーライド
	/// </summary>
	//※※※※※変更必要※※※※※
	public override void OnLost()
	{
		Debug.Log("シールド" + "(" + Type + ")" + " 消滅");

		base.OnLost();
	}

	/// <summary>
	/// シールド再取得時の処理
	/// オーバーライド
	/// </summary>
	//※※※※※変更必要※※※※※
	public override void OnPickAgain()
	{
		Debug.Log("シールド" + "(" + Type + ")" + " 再取得");

		//再取得時、実際のシールド値をリセットする
		this.value = shield;
		base.OnPickAgain();
	}
}

