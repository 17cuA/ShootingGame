using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorTest : MonoBehaviour
{
	public float irokawarimasu_H = 0;
	public float irokawarimasu_S = 0;
	public float irokawarimasu_V = 0.4f;

	public Slider H, S, V;
	new Renderer renderer;
	public Color testColor;
    // Start is called before the first frame update
    void Start()
    {
		renderer = gameObject.GetComponent<Renderer>();
		testColor = GetComponent<Renderer>().material.color;
	}

    // Update is called once per frame
    void Update()
    {
		//testColor = Color.red;
		//testColor = new Color32(248, 168, 133, 1);
		testColor = UnityEngine.Color.HSVToRGB(24.0f / 360.0f, 1, irokawarimasu_V);
		renderer.material.color = testColor;
		//gameObject.GetComponent<Renderer>().material.color = testColor;

		irokawarimasu_V += 0.005f;
		if (irokawarimasu_V > 1.0f)
		{
			irokawarimasu_V = 1;
		}
    }
}
