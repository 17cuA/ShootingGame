using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossExplosion : MonoBehaviour
{
	public ParticleStatusChange explosionPrefab;	//爆発のプレハブ
	public GameObject bigExplosionPrefab;			//大爆発のプレハブ

	public List<Transform> positionList= new List<Transform>();	//座標
	public List<float> timeList= new List<float>();				//時間
	public List<float> sizeList= new List<float>();             //サイズ


	void Start()
	{
		foreach (Transform childTransform in positionList)
		{
			//最後の爆発の1秒後に再生
			Destroy(childTransform.gameObject.GetComponent<MeshRenderer>());
		}

		//小爆発
		for (int i = 0; i < positionList.Count; i++)
		{
			ParticleStatusChange particle;
			particle = Instantiate(explosionPrefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));  //生成
			particle.transform.parent = positionList[i];                                                //親変更
			particle.transform.localPosition = new Vector3(0, 0, 0);                                    //座標変更
			positionList[i].GetComponent<MeshRenderer>().enabled = false;                               //座標指標非表示
			particle.SetStatus(timeList[i]+Random.Range(-0.1f, 0.1f), sizeList[i]);                       //ステータス変更
			particle.GetComponent<ParticleSystem>().Stop();                                             //Particleシステムの停止

			particle.GetComponent<ParticleSystem>().Play();                                             //Particleシステムの再生
		}

		//大爆発
		if (bigExplosionPrefab != null)
		{
			bigExplosionPrefab.GetComponent<ParticleSystem>().Stop();                                               //Particleシステムの停止
			foreach (Transform childTransform in bigExplosionPrefab.transform)
			{
				//最後の爆発の1秒後に再生
				childTransform.gameObject.GetComponent<ParticleSystem>().startDelay += timeList[timeList.Count - 1] + 1f;
			}
			bigExplosionPrefab.GetComponent<ParticleSystem>().Play();
		}
	}
}
