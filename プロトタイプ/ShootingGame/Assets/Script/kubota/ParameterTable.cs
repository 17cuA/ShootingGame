using UnityEngine;

[CreateAssetMenu(menuName = "Paramater/ParameterTable", fileName = "ParameterTable")]
public class ParameterTable : ScriptableObject
{
	[SerializeField, Header("初期体力")]
	private int life;
	public int Get_Life { get { return life; } }

	[SerializeField, Header("初期速度")]
	private int Speed;
	public int Get_Speed { get { return Speed; } }

	[SerializeField, Header("残機数")]
	private int Reaming;
	public int Get_Reaming { get { return Reaming; } }
} // class ParameterTable
