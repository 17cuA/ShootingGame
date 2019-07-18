using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeSelect : MonoBehaviour
{

	public void SelectSelf()
	{
		//クリックされた時 かつ lockStateがLockedではない時だけ実行
		if (Input.GetMouseButtonDown(0) && Cursor.lockState != CursorLockMode.Locked)
		{
			EventSystem.current.SetSelectedGameObject(gameObject);
		}
	}

	public void NonSelectSelf()
	{
		EventSystem.current.SetSelectedGameObject(null);
	}
}