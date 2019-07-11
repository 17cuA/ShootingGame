using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurController : MonoBehaviour
{
	private Material material = null;

	[Range(0.01f, 8f)]
	public float sigma = 4f;

	void Start()
    {
		this.material = gameObject.GetComponent<Renderer>().material;

	}

	void Update()
    {
		this.material.SetFloat("_Sigma", sigma);
    }
}
