using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_last : MonoBehaviour
{

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.L)|| Input.GetKeyDown("joystick button 0"))
		{
			SceneManager.LoadScene("TITLE");
		}
	}
}