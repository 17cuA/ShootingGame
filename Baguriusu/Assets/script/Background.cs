using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]
public class Background : MonoBehaviour
{

	public float ScrollSpeed = 2f;
	private MeshRenderer mr;

	float offset = 0f;

	// Use this for initialization
	void Start()
	{
		mr = GetComponent<MeshRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		offset = Mathf.Repeat(Time.time * ScrollSpeed, 1f);
		// offset = 1f - offset; //逆向きに動かしたい場合
		mr.material.SetTextureOffset("_MainTex", new Vector2(0f, offset));
	}
}