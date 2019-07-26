using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour {


	[SerializeField]
	AnimationCurve positionX;
	[SerializeField]
	AnimationCurve positionY;
	[SerializeField]
	AnimationCurve positionZ;
	[SerializeField, Header("位置の倍率")]
	float positionMagnification = 1f;
	[SerializeField]
	AnimationCurve rotationX;
	[SerializeField]
	AnimationCurve rotationY;
	[SerializeField]
	AnimationCurve rotationZ;
	[SerializeField, Header("回転の倍率")]
	float rotationMagnification = 1f;
	Vector3 startPosition;

	float replayTime = 0f;

	// Use this for initialization
	void Start()
	{
		replayTime = Time.time;
		startPosition = transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		float time = Time.time - replayTime;
		transform.position = new Vector3(startPosition.x + positionX.Evaluate(time) * positionMagnification, startPosition.y + positionY.Evaluate(time) * positionMagnification, startPosition.z + positionZ.Evaluate(time) * positionMagnification);
		transform.eulerAngles = new Vector3(rotationX.Evaluate(time) * rotationMagnification, rotationY.Evaluate(time) * rotationMagnification, rotationZ.Evaluate(time) * rotationMagnification);


	}


}
