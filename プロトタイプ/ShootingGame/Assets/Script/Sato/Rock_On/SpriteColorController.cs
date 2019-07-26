﻿/*====================================================*/
// 内容        ：画像の色を徐々に変えるスクリプト
// ファイル    ：SpriteColorController.cs
//
// Copyright (C) 齋藤　優剛 All Rights Reserved.
/*----------------------------------------------------*/
//〔更新履歴〕
// 2018/10/18 〔齋藤　優剛〕新規作成
/*----------------------------------------------------*/
//        デバッグ地蔵様を安置致します。
//        バグやエラーが起こりませんように……
//              ,＿＿＿,
//            ／     ／＼
//          ／     ／_＿ ＼
//        ／_____／（´ｰ｀）＼
//        ￣||||||  |っ￠||￣
//          ||||||  /,,|ゝii
//         |￣￣￣|  ~~凸（￣）凸
/*====================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*-----------*/
//　色の情報　//
/*-----------*/
[Serializable]
public struct ColorState
{
	public float chengeTime;	//!< 次の色に変わるまでにかかる時間[s]
	public float waitTime;		//!< 色が変わった後に待つ時間[s]
	public Color chengeColor;	//!< 次に変わる色
}

public class SpriteColorController : MonoBehaviour
{
	/*-----------*/
	//　変数宣言　//
	/*-----------*/
	SpriteRenderer sprite = null;
	[SerializeField] bool isRoop = false;		//!< ループさせるか
	[SerializeField] List<ColorState> colorState;

	/*-----------*/
	//　関数宣言　//
	/*-----------*/
	void Start()
	{
		sprite = GetComponent<SpriteRenderer>();
		StartCoroutine("ChangeCameraColor");
	}


	/*----------次の色や時間を指定------------*/
	IEnumerator ChangeCameraColor()
	{
		while (isRoop)
		{
			for (int i = 0; i < colorState.Count; ++i)
			{
				yield return StartCoroutine(ChangeColor(colorState[i].chengeColor, colorState[i].chengeTime));
				yield return new WaitForSeconds(colorState[i].waitTime);
			}
		}
	}
	/*--------------------------------------*/

	/*----------色を変えるメソッド------------*/
	IEnumerator ChangeColor(Color toColor, float duration)
	{
		Color fromColor = sprite.color;
		Color nextColor = toColor - fromColor;
		float startTime = Time.time;
		float endTime = Time.time + duration;

		while (Time.time < endTime)
		{
			fromColor.r = fromColor.r + (Time.deltaTime / duration) * nextColor.r;
			fromColor.g = fromColor.g + (Time.deltaTime / duration) * nextColor.g;
			fromColor.b = fromColor.b + (Time.deltaTime / duration) * nextColor.b;
			fromColor.a = fromColor.a + (Time.deltaTime / duration) * nextColor.a;
			sprite.color = fromColor;
			yield return 0;
		}

		sprite.color = toColor;

		yield break;
	}
	/*--------------------------------------*/
}