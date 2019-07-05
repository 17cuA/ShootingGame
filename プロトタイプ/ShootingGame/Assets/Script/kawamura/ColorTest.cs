using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorTest : MonoBehaviour
{
	public float irokawarimasu_H = 0;
	public float irokawarimasu_S = 0;
	public float irokawarimasu_V = 0;

	public Slider H, S, V;
	public Color testColor;
    // Start is called before the first frame update
    void Start()
    {
		testColor = GetComponent<Renderer>().material.color;
	}

    // Update is called once per frame
    void Update()
    {
		testColor = UnityEngine.Color.HSVToRGB(H.value, S.value, V.value);
		//irokawarimasu += 0.005f;
    }
}
