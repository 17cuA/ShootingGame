using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PowerUpComponent : MonoBehaviour
{
	[SerializeField] private float targetScale;
	[SerializeField] private float currentScale;

	public float changeTime = 1f;
	[SerializeField]private float changeTimer;
	private bool isResize = false;

	private void Awake()
	{
		currentScale = targetScale = 1f;
	}



	// Update is called once per frame
    void Update()
    {
        if(currentScale != targetScale)
		{
			var scale = Mathf.Lerp(currentScale, targetScale, changeTimer / changeTime);
			currentScale = scale;
			transform.localScale = new Vector3(currentScale, currentScale, currentScale);
			changeTimer += Time.deltaTime;
			if(changeTimer > changeTime)
			{
				changeTimer = changeTime;
				currentScale = targetScale;
				transform.localScale = new Vector3(currentScale, currentScale, currentScale);
			}
		}
		else
		{
			changeTimer = 0f;
			isResize = false;
		}
    }

	public void Enlarge(float enlargeIndex)
	{
		targetScale = enlargeIndex;
		changeTimer = 0f;
	}

	public void Resize()
	{
		if(isResize)
		{
			return;
		}

		isResize = true;
		targetScale = 1f;
		changeTimer = 0f;
	}
}
