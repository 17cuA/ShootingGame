﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ランキングの名前入力用
/// </summary>
public class InputRankingName
{
	const int kNameLength = 3;				// 名前の長さ
	char[] name = new char[kNameLength];	// 入力用文字配列
	// 名前をストリングで返すプロパティ
	public string Name
	{
		get
		{
			string result = "";
			for (int i = 0; i < kNameLength; ++i)
			{
				result += name[i];
			}
			return result;
		}
	}
	List<Image> nameImageList = new List<Image>(kNameLength);	// 名前を表示するImageのリスト
	// 名前を表示するImageを格納するプロパティ
	public List<Image> NameImageList
	{
		set
		{
			int i;
			// リストの容量分格納する
			for (i = 0; i < nameImageList.Count; ++i)
			{
				nameImageList[i] = value[i];
			}
			// もし名前の文字数に足りなければ追加する
			while (nameImageList.Count < kNameLength)
			{
				nameImageList.Add(value[i]);
				++i;
			}
		}
	}
	int selectPos = 0;													// 文字の入力位置
	float previousInputY = 0f;											// 前フレームの入力情報
	//const float kSelectScalingValueMax = 0.5f;							// スケール値の最大値
	//const float kSelectScalingReduceValue = 0.2f;						// 1fに減らすスケール値
	//float selectScalingValue = kSelectScalingValueMax;					// 選択している文字の減算するスケール値
	//float selectDefaultScaleValue = 0f;									// スケールの規定値
	const int kBlinkInvisibleFrame = 35;								// 非表示を開始するフレーム数
	const int kBlinkFrameMax = 45;										// 点滅の一回にループするフレーム数
	int blinkFrame = 0;													// 点滅させるためのフレーム数
	public bool IsDecision { get { return selectPos >= kNameLength; } }	// 決定されたかどうか
	/// <summary>
	/// コンストラクタ
	/// </summary>
	/// <param name="defaultName">名前の規定値</param>
	public InputRankingName(string defaultName = "UFO")
	{
		for (int i = 0; i < kNameLength; ++i)
		{
			name[i] = defaultName[i];
		}
	}
	/// <summary>
	/// 呼び出されている間、名前を変更できる
	/// </summary>
	public void SelectingName()
	{
		// 選択位置の移動
		MoveSelectPos();
		// 文字の選択
		SelectChar();
		// 選択されている文字を点滅させる
		BlinkSelectChar();
	}
	/// <summary>
	/// 呼び出している間、選択位置を移動できる
	/// </summary>
	void MoveSelectPos()
	{
		// 決定されていたら処理しない
		if (IsDecision) { return; }
		// 選択されている鵜文字が消えないようにアクティブにする
		nameImageList[selectPos].enabled = true;
		// 文字の選択位置を変える
		if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
		{
			++selectPos;
		}
		if (Input.GetKeyDown(KeyCode.X) || Input.GetButtonDown("Fire2"))
		{
			--selectPos;
		}
		// 要素数が文字数より大きくならないように補正する
		if (selectPos > kNameLength)
		{
			selectPos = kNameLength;
		}
		if (selectPos < 0)
		{
			selectPos = 0;
		}
	}
	/// <summary>
	/// 呼び出している間、選択している文字を変更できる
	/// </summary>
	void SelectChar()
	{
		// 選択範囲外であれば処理しない
		if (selectPos >= kNameLength) { return; }
		float inputY = Input.GetAxisRaw("Vertical");
		// 前フレームも入力していたら移動しない
		if (previousInputY != 0f) { previousInputY = inputY; return; }
		// 元の文字を保存しておく
		char previousChar = name[selectPos];
		// 選択している部分の文字を変える
		if (inputY > 0f)
		{
			++name[selectPos];
		}
		if (inputY < 0f)
		{
			--name[selectPos];
		}
		// 規定文字以内に収める
		if (name[selectPos] > 'Z')
		{
			name[selectPos] = 'A';
		}
		if (name[selectPos] < 'A')
		{
			name[selectPos] = 'Z';
		}
		previousInputY = inputY;
	}
	/// <summary>
	/// 選択されている文字を拡大縮小する
	/// </summary>
	void ScalingSelectChar()
	{
	}
	/// <summary>
	/// 選択している文字を点滅させる
	/// </summary>
	void BlinkSelectChar()
	{
		// 選択範囲外であれば処理しない
		if (selectPos >= kNameLength) { return; }
		nameImageList[selectPos].enabled = blinkFrame < kBlinkInvisibleFrame;
		++blinkFrame;
		if (blinkFrame > kBlinkFrameMax)
		{
			blinkFrame = 0;
		}
	}
}
