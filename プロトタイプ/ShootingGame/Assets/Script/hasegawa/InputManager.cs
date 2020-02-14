﻿/*
 * 20190909 作成
 * author hasegawa yuuta
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 入力するボタンの名前を保持しておく
/// </summary>
[System.Serializable]
public class InputManager
{
	[SerializeField, Tooltip("コントローラの番号")] ePadNumber padNumber = ePadNumber.eNone;
	public ePadNumber PadNumber { get { return padNumber; } }
	[SerializeField, Tooltip("Unity側で設定するボタン名のリスト")] List<eCode> defaultButtonNameList = new List<eCode>();	// uinty側で設定するボタンの名前のリスト
	[SerializeField, Tooltip("実際に使用する名前のリスト")] List<string> useButtonNameList = new List<string>();				// スクリプト側で使用するボタン名のリスト
	[SerializeField, Tooltip("確定させるまでの時間")] float decisionTime = 5f;												// 決定するまでの時間
	float inputTime;																									// 入力を受けている時間
	Dictionary<string, eCode> reflectButtonNameMap = new Dictionary<string, eCode>();                                   // スクリプト側に渡すボタンの名前
	Dictionary<string, string> old_reflectButtonNameMap = new Dictionary<string, string>();                                   // スクリプト側に渡すボタンの名前
	Dictionary<string, eCode> settingButtonNameMap = new Dictionary<string, eCode>();									// ボタンの再設定をするときの一時変数
	int settingButtonNum = 0;                                                                                           // 設定しているボタンの要素番号
	eCode previousInputButtonName = eCode.ePad_None;																				// 前フレームに入力を受けていたボタンの名前
	public Dictionary<string, string> oldButton { get { return old_reflectButtonNameMap; } }
	public Dictionary<string, eCode> Button { get { return reflectButtonNameMap; } }
	[SerializeField, Tooltip("設定時に表示するフォント")] Font textFont;													// 設定時に表示するテキストのフォント
	[SerializeField, Tooltip("表示するフォントのX座標")] float textPositionX = 0;											// 表示するテキストのx座標
	Text inputInfoText;
	/// <summary>
	/// コンストラクタ
	/// </summary>
	public InputManager()
	{
		if (defaultButtonNameList.Count < useButtonNameList.Count) { Debug.LogWarning("UseButtonListCount is more than DefaultButtonNameList.Count!"); }
		for (int i = 0; i < useButtonNameList.Count && i < defaultButtonNameList.Count; ++i)
		{
			reflectButtonNameMap.Add(useButtonNameList[i], defaultButtonNameList[i]);
			settingButtonNameMap.Add(useButtonNameList[i], eCode.ePad_None);
		}
	}
	/// <summary>
	/// 初期化
	/// </summary>
	public void Init()
	{
		if (defaultButtonNameList.Count < useButtonNameList.Count) { Debug.LogWarning("UseButtonListCount is more than DefaultButtonNameList.Count!"); }
		for (int i = 0; i < useButtonNameList.Count && i < defaultButtonNameList.Count; ++i)
		{
			reflectButtonNameMap.Add(useButtonNameList[i], defaultButtonNameList[i]);
			settingButtonNameMap.Add(useButtonNameList[i], eCode.ePad_None);
		}
		if (padNumber == ePadNumber.eNone) { padNumber = ePadNumber.ePlayer1; }
	}
	/// <summary>
	/// 呼び出されている間、ボタンの再設定を行う
	/// </summary>
	/// <returns>再設定がすべて終わったかどうか</returns>
	public bool SettingButton()
	{
		// テキスト生成
		if (!inputInfoText)
		{
			Canvas anyCanvas = GameObject.FindObjectOfType<Canvas>();
			inputInfoText = new GameObject("InputInfo").AddComponent<Text>();
			inputInfoText.rectTransform.SetParent(anyCanvas.transform);
			inputInfoText.rectTransform.localPosition = new Vector2(textPositionX, 0f);
			inputInfoText.font = textFont;
			inputInfoText.fontSize = 50 / (int)(3840f / Screen.width);
			inputInfoText.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
		}
		bool isComplete = false;
		bool isInput = false;
		eCode inputButtonName = eCode.ePad_None;
		// 設定されたボタンをそれぞれ確認していく
		for (int i = 0; i < defaultButtonNameList.Count; ++i)
		{
			// すでに入力を受け付けているものはスキップする
			if (settingButtonNameMap.ContainsValue(defaultButtonNameList[i]))
			{
				continue;
			}
			// 入力を受けていたら名前を一時保存する
			if (ControllerDevice.GetButton(defaultButtonNameList[i], padNumber) && !isInput)
			{
				inputButtonName = defaultButtonNameList[i];
				isInput = true;
			}
			// 同時押しされていたら解除
			else if (ControllerDevice.GetButton(defaultButtonNameList[i], padNumber) && isInput)
			{
				inputButtonName = eCode.ePad_None;
				break;
			}
			// ボタンが上げられたらスキップする
			else if (ControllerDevice.GetButtonUp(defaultButtonNameList[i], padNumber))
			{
				inputButtonName = eCode.ePad_None;
				continue;
			}
		}
		// 一つのボタンが押されていたら入力時間をカウントする
		if (previousInputButtonName == inputButtonName && inputButtonName != eCode.ePad_None)
		{
			inputTime += Time.deltaTime;
		}
		// そうでない時は時間リセット
		else
		{
			inputTime = 0f;
		}
		// 決定される時間になったら決定して次のボタンに移行する
		if (inputTime >= decisionTime)
		{
			settingButtonNameMap[useButtonNameList[settingButtonNum]] = inputButtonName;
			++settingButtonNum;
		}
		previousInputButtonName = inputButtonName;
		// 全て設定されたら反映と初期化をしてメソッドをtrueで返す
		if (settingButtonNum >= useButtonNameList.Count)
		{
			// ボタンの名前を設定
			foreach (string buttonName in useButtonNameList)
			{
				reflectButtonNameMap[buttonName] = settingButtonNameMap[buttonName];
			}
			// 設定用のマップを初期化
			for (int i = 0; i < useButtonNameList.Count; ++i)
			{
				settingButtonNameMap[useButtonNameList[i]] = eCode.ePad_None;
			}
			isComplete = true;
			previousInputButtonName = eCode.ePad_None;
			settingButtonNum = 0;
			GameObject.Destroy(inputInfoText);
		}
		// テキスト表示
		inputInfoText.text = "Setting Now";
		for (int i = 0; i < useButtonNameList.Count; ++i)
		{
			// ボタン名の情報
			inputInfoText.text += "\n" + useButtonNameList[i] + " : ";
			// 入力状態の情報
			if (settingButtonNum == i)
			{
				inputInfoText.text += "Setting";
			}
			else if (settingButtonNameMap[useButtonNameList[i]] == eCode.ePad_None)
			{
				inputInfoText.text += "Not set";
			}
			else if (settingButtonNameMap[useButtonNameList[i]] != eCode.ePad_None)
			{
				inputInfoText.text += "Complete";
			}
		}
		return isComplete;
	}
}
