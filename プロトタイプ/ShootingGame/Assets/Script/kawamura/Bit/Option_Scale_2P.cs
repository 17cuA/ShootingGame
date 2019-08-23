using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option_Scale_2P : MonoBehaviour
{
	Bit_Formation_2P bf_2P;

	int scaleDelay;
	float scale_value;
	public float scale_Collect;

	bool isScaleInc = false;
	bool isScaleDec = false;
	public bool isCollectInc = true;
	// Start is called before the first frame update

	private void Awake()
	{
		bf_2P = transform.parent.gameObject.GetComponent<Bit_Formation_2P>();
	}
	void Start()
	{
		scale_value = 1.5f;
		scale_Collect = 0;
		isScaleInc = true;
		//isScaleInc = true;
	}

	// Update is called once per frame
	void Update()
	{
		//オプションの縮小試し
		scaleDelay++;
		if (isCollectInc)
		{
			scale_Collect += 0.1f;
			if (scale_Collect > 1.5f)
			{
				scale_Collect = 1.5f;
				isScaleDec = true;
				isScaleInc = false;
				isCollectInc = false;
			}
			transform.localScale = new Vector3(scale_Collect, scale_Collect, 0);

		}
		else if (scaleDelay > 5)
		{
			if (isScaleInc)
			{
				scale_value += 0.2f;
				if (scale_value > 1.5f)
				{
					scale_value = 1.5f;
					isScaleInc = false;
					isScaleDec = true;
				}
			}
			else if (isScaleDec)
			{
				scale_value -= 0.2f;
				if (scale_value < 1.1f)
				{
					scale_value = 1.1f;
					isScaleDec = false;
					isScaleInc = true;
				}
			}

			//scale_value = Mathf.Sin(Time.frameCount) / 12.5f + 0.42f;
			transform.localScale = new Vector3(scale_value, scale_value, 0);
			scaleDelay = 0;
		}
		if (bf_2P.isCollection)
		{
			scale_Collect = 0;
			transform.localScale = new Vector3(scale_Collect, scale_Collect, 0);
			isCollectInc = true;
		}
	}
}
