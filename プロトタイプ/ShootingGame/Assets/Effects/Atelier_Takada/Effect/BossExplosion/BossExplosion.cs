using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossExplosion : MonoBehaviour
{
	public ParticleStatusChange explosionPrefab;  //爆発のプレハブ

	//爆発パーティクルのデータ
	[System.Serializable]
	public struct ExplosionData
	{
		public Transform position;  //生成場所
		public float time;                  //生成タイミング
		public float size;                  //サイズ
	}

	/*
	[SerializeField]
	public List<ExplosionData> explosionDataList
		= new List<ExplosionData>();    //爆発のタイミング
	*/

	public List<Transform> positionList= new List<Transform>();	//座標
	public List<float> timeList= new List<float>();				//時間
	public List<float> sizeList= new List<float>();				//サイズ



	
	void Start()
	{
		for (int i = 0; i < positionList.Count; i++)
		{
			ParticleStatusChange particle;
			particle = Instantiate(explosionPrefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));  //生成
			particle.transform.parent = positionList[i];												//親変更
			particle.transform.localPosition = new Vector3(0, 0, 0);									//座標変更
			positionList[i].GetComponent<MeshRenderer>().enabled = false;								//座標指標非表示
			particle.SetStatus(timeList[i], sizeList[i]);												//ステータス変更
			particle.GetComponent<ParticleSystem>().Stop();												//Particleシステムの停止

			particle.GetComponent<ParticleSystem>().Play();												//Particleシステムの再生
		}
	}

	void Update()
	{

	}
}
