using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TextDisplay;
public class EndRollScript : MonoBehaviour
{
	//　テキストのスクロールスピード
	[SerializeField]
	private float textScrollSpeed = 30;
	//　テキストの制限位置
	[SerializeField]
	private float limitPosition = 730f;
	//　エンドロールが終了したかどうか
	private bool isStopEndRoll;
	//　シーン移動用コルーチン
	private Coroutine endRollCoroutine;

	//private Character_Display 

	// Update is called once per frame
	void Update()
	{
		//　エンドロールが終了した時
		if (isStopEndRoll)
		{
			endRollCoroutine = StartCoroutine(GoToNextScene());
		}
		else
		{
			//　エンドロール用テキストがリミットを越えるまで動かす
			if (transform.position.y <= limitPosition)
			{
				transform.position = new Vector2(transform.position.x , transform.position.y + textScrollSpeed * Time.deltaTime);
			}
			else
			{
				isStopEndRoll = true;
			}
		}
	}
	IEnumerator GoToNextScene()
	{
		//　5秒間待つ
		yield return new WaitForSeconds(5f);
		if (Input.GetKeyDown("space"))
		{
			StopCoroutine(endRollCoroutine);
			SceneManager.LoadScene("EndRollStartScene");
		}
		yield return null;
	}
}
