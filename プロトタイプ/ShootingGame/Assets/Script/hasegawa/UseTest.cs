﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseTest : MonoBehaviour
{
	[SerializeField] InputManager inputManager = new InputManager();
	[SerializeField] Vector2 debugAreaPosition = Vector2.zero;
	bool isInputSetting = false;
	void Start()
	{
		inputManager.Init();
	}
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.F5))
		{
			isInputSetting = true;
		}
		if (isInputSetting)
		{
			isInputSetting = !inputManager.SettingButton();
		}
	}
	void OnGUI()
	{
		if (isInputSetting) { return; }
		string displayText = "";
		Rect displayAreaSize = new Rect(debugAreaPosition.x, debugAreaPosition.y, 350f, 0f);
		if (ControlerDevice.GetButton(inputManager.Button["Shot"], inputManager.PadNumber))
		{
			displayText += "Input Button 1\n";
			displayAreaSize.height += 60f;
		}
		if (ControlerDevice.GetButton(inputManager.Button["Multiple"], inputManager.PadNumber))
		{
			displayText += "Input Button 2\n";
			displayAreaSize.height += 60f;
		}
		if (ControlerDevice.GetButton(inputManager.Button["Item"], inputManager.PadNumber))
		{
			displayText += "Input Button 3";
			displayAreaSize.height += 60f;
		}
		if (displayText == "") { return; }
		GUI.TextField(displayAreaSize, displayText);
		GUI.skin.textField.fontSize = 50;
	}
}
