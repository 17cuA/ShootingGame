using System;
using System.Collections.Generic;
using Power;

public class Power_BulletUpgrade : AbstractPower
{
	private int level = 0;
	public int Level
	{
		get
		{
			return level;
		}
	}
	private int exp = 0;
	public int Exp
	{
		get
		{
			return exp;
		}
	}
	private int getTime = 0;
	public int GetTime
	{
		get
		{
			return getTime;
		}
	}

	/// <summary>
	/// 消える条件
	/// 多分ステージクリア
	/// ※※※※変更必要※※※※
	/// </summary>
	public override bool IsLost
	{
		get
		{
			return false;
		}
	}
	public Power_BulletUpgrade(PowerType type, int levelUpNeedExp) : base(type, levelUpNeedExp)
	{
		this.level = 1;
	}

	public override void OnPick()
	{
		this.exp++;		//取得時経験値アップ
		this.getTime++;//取得回数アップ
	}

	public override void OnUpdate(float deltaTime)
	{
		//現在経験値はレベルアップ必要経験値以上になると
		if(exp >= base.value)
		{
			level++;	//レベルアップ
			exp = 0;	//経験値リセット
		}
	}

	public override void OnPickAgain()
	{
		this.exp++;		 //取得時経験値アップ
		this.getTime++; //取得回数アップ
	}

	public override void OnLost()
	{
		
	}
}

