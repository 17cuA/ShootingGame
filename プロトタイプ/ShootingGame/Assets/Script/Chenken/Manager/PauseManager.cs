using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
	[Header("Debug用")]
	public bool isUseKey = true;

	[Header("-----キーボード設定-----")]
	[Header("Pause制御キー")]
	public KeyCode controlKey;

	//[Header("-----コントローラ設定-----")]
	//[Header("Pause制御ボタン")]
	//public string controlName;

	private static bool isPause;
	public static bool IsPause
	{
		get
		{
			return isPause;
		}
	}
    // Start is called before the first frame update

    void Update()
    {
		if (!isPause)
		{
			if ((Input.GetKeyDown(controlKey) && isUseKey) /*|| (Input.GetButtonDown(controlName) && !isUseKey)*/)
			{
				Time.timeScale = 0f;
				isPause = !isPause;
				PauseComponent.Pause();
			}
			
		}
		else
		{
			if((Input.GetKeyDown(controlKey) && isUseKey) /*|| (Input.GetButtonDown(controlName) && !isUseKey)*/)
			{
				Time.timeScale = 1f;
				isPause = !isPause;
				PauseComponent.Resume();
			}
		}
		
    }
}
