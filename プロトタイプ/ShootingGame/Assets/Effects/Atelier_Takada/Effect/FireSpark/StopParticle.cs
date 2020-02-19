using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopParticle : MonoBehaviour
{
	private ParticleSystem particleSystem;

	void Start()
	{
		particleSystem = GetComponent<ParticleSystem>();

		//すぐ止めるバージョン
		particleSystem.Simulate(10f);
	}

	void Update()
	{
		//slow再生バージョン
		/*
		particleSystem.Simulate(
			t: 1f/300f,					//パーティクルシステムを早送りする時間
			withChildren: true,			//子のパーティクルシステムもすべて早送りするかどうか
			restart: false				//再起動し最初から再生するかどうか
			);
		*/
	}
}
