using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option_Scale : MonoBehaviour
{
	int scaleDelay;
	float scale_value;

	bool isScaleInc = false;
	bool isScaleDec=false;
    // Start is called before the first frame update
    void Start()
    {
		scale_value = 1.5f;
		isScaleInc = true;
		//isScaleInc = true;
    }

    // Update is called once per frame
    void Update()
    {
		//オプションの縮小試し
		scaleDelay++;
		if (scaleDelay > 5)
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

	}
}
