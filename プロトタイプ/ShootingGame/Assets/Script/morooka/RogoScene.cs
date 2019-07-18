using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RogoScene : MonoBehaviour
{
	//private Color alfa;
	//private SpriteRenderer renderer;
	//private float upSpeed;
	//bool ok;

	int Diray;

    void Start()
    {
		//alfa = new Color(1.0f,1.0f,1.0f,1.0f);
		//upSpeed = 2.0f / 255.0f;
		//renderer = GetComponent<SpriteRenderer>();
		//renderer.color = alfa;
		//ok = true;
		Diray = 0;
    }

    // Update is called once per frame
    void Update()
    {
		if (Scene_Manager.Manager.Is_Fade_Finished)
		{
			Diray++;
			if (Diray == 60)
			{
				Scene_Manager.Manager.Screen_Transition_To_Title();
			}
		}

		//if(ok)
		//{
		//	alfa.a -= upSpeed;
		//	renderer.color = alfa;
		//	if (alfa.a <= 0.0f)
		//	{
		//		ok = false;
		//	}
		//}
		//else
		//{
		//	alfa.a += upSpeed;
		//	renderer.color = alfa;
		//}

		//if (renderer.color.a >= 1.0f)
		//{
		//	SceneManager.LoadScene("Title");
		//}
	}
}
