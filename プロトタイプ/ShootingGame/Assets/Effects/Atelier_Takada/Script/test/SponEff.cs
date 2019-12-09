using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SponEff : MonoBehaviour
{
	public GameObject obj;		//生成するオブジェクト
	[SerializeField]
	private float elapsedTime;	//経過時間
	private float interval;     //周期
	public float intervalMax;   //周期の最大値
	public float intervalMin;   //周期の最低値

	public float generationRange;   //生成範囲

	void Start()
	{
		interval = Random.Range(intervalMin, intervalMax);
		elapsedTime = 0.0f;

		Mathf.LerpUnclamped(-generationRange, generationRange, 0.8f);
	}

	// Update is called once per frame
	void Update()
	{
		elapsedTime += Time.deltaTime;


		if (elapsedTime > interval)
		{
			Vector3 vec3 = new Vector3(
					Random.Range(-generationRange, generationRange),
					0.0f,
					0.0f);

			Instantiate(obj, transform.position + vec3, transform.rotation);
			interval = Random.Range(intervalMin, intervalMax);
			elapsedTime = 0.0f;
		}

	}
}
