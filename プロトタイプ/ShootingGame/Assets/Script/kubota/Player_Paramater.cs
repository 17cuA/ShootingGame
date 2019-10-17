using UnityEngine;

[CreateAssetMenu(menuName = "Paramater/ParameterTable", fileName = "ParameterTable")]
public class Player_Paramater : ScriptableObject
{
	[SerializeField, Header("初期体力")]
	private int life;
	public int Get_Life { get { return life; } }

	[SerializeField, Header("初期速度")]
	private int Speed;
	public int Get_Speed { get { return Speed; } }

	[SerializeField, Header("スコア設定")]
	private uint Score;
	public uint Get_Score { get { return Score; } }

	[SerializeField, Header("残機数")]
	private int Reaming;
	public int Get_Reaming { get { return Reaming; } }

	[SerializeField, Header("弾発射感覚")]
	private int Bullet_Delay;
	public int Get_BulletDelay { get { return Bullet_Delay; } }
}
