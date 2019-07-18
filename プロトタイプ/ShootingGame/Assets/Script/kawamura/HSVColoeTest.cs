using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSVColoeTest : MonoBehaviour
{
	Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
		renderer = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
		renderer.material.color = UnityEngine.Color.HSVToRGB(0, 0, 0);
    }
}
