using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//ボタンを使用するためUIとSceneManagerを使用ためSceneManagementを追加
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
	// ボタンをクリックするとBattleSceneに移動します
	public void ButtonClicked_Tittle()
	{
		//SceneManager.LoadScene("TITLE");
	}
	public void ButtonClicked_Stage()
	{
        //FadeManager.Instance.LoadScene ("Stage", 1.0f);
		//SceneManager.LoadScene("Stage");
	}
}