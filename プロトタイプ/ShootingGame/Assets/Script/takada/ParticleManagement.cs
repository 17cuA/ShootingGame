using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class ParticleManagement : MonoBehaviour
{
	//Particleprefablist
	public GameObject[] particle = new GameObject[7];
	public GameObject particleGameObject;
	void Start()
	{
		particle[0] = Resources.Load<GameObject>("Effects/Particle_1唐揚げ爆発");
		particle[1] = Resources.Load<GameObject>("Effects/Particle_2黒煙");
		particle[2] = Resources.Load<GameObject>("Effects/Particle_3エネルギー弾");
		particle[3] = Resources.Load<GameObject>("Effects/Particle_4衝撃波");
		particle[4] = Resources.Load<GameObject>("Effects/Particle_5箱爆発");
		particle[5] = Resources.Load<GameObject>("Effects/Particle_6通路");
		particle[6] = Resources.Load<GameObject>("Effects/Particle_7汎用煙");
	}

	void Update()
	{
	}

	//Particleの生成
	//1:自身のオブジェクト
	//2:エフェクトのID
	//3:自身のオブジェクトの座標
	public void ParticleCreation(GameObject gameObject,int particleID, Vector3 objectPosition)
	{

		//呼び出し元オブジェクトの座標で指定IDのパーティクルを生成
		particleGameObject = Instantiate(particle[particleID], objectPosition, particle[particleID].transform.rotation);
        //呼び出し元をパーティクルの親に設定
		//particleGameObject.transform.parent = gameObject.transform;
		Debug.Log("hollo");

	}
}
