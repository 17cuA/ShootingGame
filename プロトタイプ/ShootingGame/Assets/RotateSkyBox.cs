using UnityEngine;
using System.Collections;

public class RotateSkyBox : MonoBehaviour
{
	public double _anglePerFrame = 0.1f;    // 1フレームに何度回すか[unit : deg]
	double _rot = 0.0f;

	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		_rot += _anglePerFrame;
		if (_rot >= 360.0f)
		{    // 0～360°の範囲におさめたい
			_rot -= 360.0f;
		}
		RenderSettings.skybox.SetFloat("_Rotation", (float)_rot);    // 回す
	}
}
