/*
 * 20190910 作成
 * author hasegawa yuuta
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// InputManagerをいい感じにできなかったため作成
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
		bool findSame = false;
		InputManagerObject[] temps = FindObjectsOfType<InputManagerObject>();
		for (int i = 0; i < temps.Length; ++i)
		{
			if (temps[i].name == gameObject.name && !findSame)
			{
				findSame = true;
			}
			else if (temps[i].name == gameObject.name && findSame)
			{
				Destroy(gameObject);
				return;
			}
		}
		inputManager.Init();
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
