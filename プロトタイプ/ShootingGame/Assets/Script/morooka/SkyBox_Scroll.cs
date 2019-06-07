﻿//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2019/06/06
//----------------------------------------------------------------------------------------------
// スカイボックスの回転処理
//----------------------------------------------------------------------------------------------
// 2019/06/06：スカイボックスの回転
//----------------------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;

public class SkyBox_Scroll : MonoBehaviour
{
	[SerializeField]
	[Header("スカイボックスの移動速度")]
	private float _anglePerFrame;    // 1フレームに何度回すか[unit : deg]
	private float _rot = 0.0f;

	void Update()
	{
		_rot += _anglePerFrame;
		if (_rot >= 360.0f)
		{    // 0～360°の範囲におさめたい
			_rot -= 360.0f;
		}
		RenderSettings.skybox.SetFloat("_Rotation", _rot);    // 回す
	}
}
