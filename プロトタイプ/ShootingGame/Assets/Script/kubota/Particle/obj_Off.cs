//自動でparticleをオフにする機能

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obj_Off : MonoBehaviour
{
	ParticleSystem particle;    //particleの情報を取得
	int frame;                //生成されてからの時間をカウント
	public int frame_Max;
	private void Start()
	{
		particle = GetComponent<ParticleSystem>();
	}
	void Update()
    {
		frame++;
		if(frame > frame_Max)
		{
			particle.Stop();
		}
    }
}
