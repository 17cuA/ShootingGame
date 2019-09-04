﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugManager : MonoBehaviour
{
	[Header("アプリ落とすキー")]
	[SerializeField] private KeyCode shutDownKey = KeyCode.F1;
	[Header("当たり判定を外すキー")]
	[SerializeField] private KeyCode switchCollidersKey = KeyCode.F3;
	[SerializeField] private bool isColliderEnabled = true;
	[Header("入力DEBUGキー")]
	[SerializeField] private KeyCode playersOperationDebugKey = KeyCode.F4;
	[SerializeField] private bool isPlayersOperationDebugging = false;

	private GameObject UIChild;
	private static Text debugText;
	private static ScrollRect scrollRect;

	private void Awake()
	{
		scrollRect = GetComponentInChildren<ScrollRect>();
		debugText = GetComponentInChildren<Text>();
	}

	// Update is called once per frame
	void Update()
	{
		//アプリ落とすショートカット(F1)
		if (Input.GetKeyDown(shutDownKey))
		{
			Application.Quit();
		}
		//コライダー有効化/無効かのショートカット(F3)
		if (Input.GetKeyDown(switchCollidersKey))
		{
			if (isColliderEnabled)
			{
				isColliderEnabled = false;
				Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Default"), LayerMask.NameToLayer("Default"), true);
				Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Default"), LayerMask.NameToLayer("Player"), true);
				Physics.IgnoreLayerCollision(LayerMask.NameToLayer("2D_Bullet"), LayerMask.NameToLayer("Player"), true);
				Physics.IgnoreLayerCollision(LayerMask.NameToLayer("2D_Bullet"), LayerMask.NameToLayer("Default"), true);
				Physics.IgnoreLayerCollision(LayerMask.NameToLayer("2D_Bullet"), LayerMask.NameToLayer("Enemy"), true);
				Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
			}
			else
			{
				isColliderEnabled = true;
				Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Default"), LayerMask.NameToLayer("Default"), false);
				Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Default"), LayerMask.NameToLayer("Player"), false);
				Physics.IgnoreLayerCollision(LayerMask.NameToLayer("2D_Bullet"), LayerMask.NameToLayer("Player"), false);
				Physics.IgnoreLayerCollision(LayerMask.NameToLayer("2D_Bullet"), LayerMask.NameToLayer("Default"), false);
				Physics.IgnoreLayerCollision(LayerMask.NameToLayer("2D_Bullet"), LayerMask.NameToLayer("Enemy"), false);
				Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
			}
		}
		//プレイヤー操作のショートカット(F4)
		if (Input.GetKeyDown(playersOperationDebugKey))
		{
			if (isPlayersOperationDebugging)
			{
				isPlayersOperationDebugging = false;
			}
			else
			{
				isPlayersOperationDebugging = true;
			}
		}
	}

	public static void OperationDebug(string context, string userName)
	{
		string addText = "\n " + userName + ": " + context;
		debugText.text += addText;
		debugText.text = "";
		Canvas.ForceUpdateCanvases();       //关键代码
		scrollRect.verticalNormalizedPosition = 0f;  //关键代码
		Canvas.ForceUpdateCanvases();   //关键代码
	}
}

