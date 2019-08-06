using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_One_A111_Individual : MonoBehaviour
{
	public bool Completion { get; set; }

	/// <summary>
	/// パーティクルの再生が終わった時に実行される
	/// </summary>
	private void OnParticleSystemStopped()
	{
		Completion = true;
	}

	private void OnEnable()
	{
		Completion = false;
	}
}
