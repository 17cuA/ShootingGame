using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KUSODASATITLE : MonoBehaviour
{
	float time = 0f;
	float sceneChangeTime = 10f;


    // Start is called before the first frame update
    void Start()
    {
		time = 0f;

	}

    // Update is called once per frame
    void Update()
    {
		time += Time.deltaTime;
		if (time > sceneChangeTime)
		{
			SceneManager.LoadScene("KUSODASATITLE");
		}


    }
}
