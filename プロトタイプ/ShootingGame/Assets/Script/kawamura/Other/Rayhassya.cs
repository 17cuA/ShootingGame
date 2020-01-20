using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rayhassya : MonoBehaviour
{
	public int aaaaaaaaaaaaaaa = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		Debug.DrawLine(transform.position, transform.position + transform.forward * 3f, Color.red);
		if (Input.GetKeyDown(KeyCode.R))
		{
			RaycastHit hit;
			int layerMask = 1 << 8;

			layerMask = ~layerMask;

			if (Physics.Linecast(transform.position, transform.position + transform.forward * 3f, out hit, LayerMask.GetMask("Wall")))
			{
				//　衝突した面が向いている方向のベクトルをVector3で出力
				Debug.Log(hit.normal);
				//　衝突した面の前方方向と衝突した面の方向から角度を算出し確認
				Debug.Log(Vector3.Angle(hit.transform.forward, hit.normal));
				//　レイを飛ばした方向と衝突した面の間の角度を算出する
				Debug.Log(Quaternion.FromToRotation(transform.forward, hit.normal).eulerAngles);
				aaaaaaaaaaaaaaa++;
			}
		}
	}
}
