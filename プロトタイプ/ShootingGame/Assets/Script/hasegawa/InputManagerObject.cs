/*
 * 20190910 作成
 * author hasegawa yuuta
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// InputManagerをスクリプタブルオブジェクトにできなかったため作成
/// </summary>
public class InputManagerObject : MonoBehaviour
{
	[SerializeField, Tooltip("入力情報")] InputManager inputManager = new InputManager();
	public InputManager Manager { get { return inputManager; } }
	/// <summary>
	/// 入力の設定を行っている最中かどうか
	/// </summary>
	public bool IsInputSetting { get; private set; }

	void Start()
	{
		inputManager.Init();
		print("init input");
		DontDestroyOnLoad(gameObject);
	}

	void Update()
	{
		if (SceneManager.GetActiveScene().name != "Title") { return; }
		if (Input.GetKeyDown(KeyCode.F5))
		{
			IsInputSetting = true;
		}
		if (IsInputSetting)
		{
			IsInputSetting = !inputManager.SettingButton();
		}
	}
}
