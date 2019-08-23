using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMove : MonoBehaviour
{
	RectTransform rect;
	public float x, y, z;
	// Start is called before the first frame update
	void Start()
    {
		rect = GetComponent<RectTransform>();
	}

    // Update is called once per frame
    void Update()
    {
		rect.localPosition += new Vector3(x, y, z);
	}
}
