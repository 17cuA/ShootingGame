using UnityEngine;
using System.Collections;

public class MaterialChange : MonoBehaviour
{

	public Material[] materials;
	Renderer rend;
	int cnt = 0;

	void Awake()
	{
		rend = GetComponent<Renderer>();
	}

	void Start()
	{
		rend.material.color = materials[cnt].color;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			cnt++;
			if (cnt > materials.Length - 1) { cnt = 0; }
			rend.material.color = materials[cnt].color;
		}
	}
}