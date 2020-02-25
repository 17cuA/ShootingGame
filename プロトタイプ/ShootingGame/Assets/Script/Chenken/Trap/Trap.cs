using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
	[System.Serializable]
	public struct HitAngleRange
	{
		public float min;
		public float max;
	}

	[Header("仕掛け設定、該当項目を選んでね★")]
	public bool isTop = false;
	public bool isBottom = false;

	[Header("移動範囲(角度)")]
	public HitAngleRange hitAngleRange;

	[Range(2,4)] public int hitCountMax;
	private int hitCounter = 0;

	public float decountTime = 0.5f;
	private float decountTimer;

	private float targetAngle;
	private bool isInvaild = false;
	[Range(0.05f,0.95f)] public float invaildTime = 0.5f;
	private float invaildTimer;

	public float returnAngle;
	public float returnTime = 0.5f;
	public int returnCountMax = 4;
	private int returnCount;
	private float returnTimer;

	private bool isToTarget = false;
	private bool isReturning = false;

	private void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			if (hitCounter < hitCountMax && !isInvaild)
			{
				hitCounter++;

				targetAngle = Mathf.Lerp(hitAngleRange.min, hitAngleRange.max, (float)(hitCounter) / (float)(hitCountMax));
				isInvaild = true;
				isToTarget = true;
			}
		}

		if(isInvaild)
		{
			invaildTimer += Time.deltaTime;
			if(invaildTimer >= invaildTime)
			{
				isInvaild = false;
				invaildTime = 0f;
			}
		}

		if(hitCounter > 0)
		{
			decountTimer += Time.deltaTime;
			if(decountTimer > decountTime)
			{
				hitCounter--;
				decountTimer = 0;
			}
		}

		if(isToTarget)
		{
			var x = Mathf.Lerp(transform.localEulerAngles.x, targetAngle, returnTimer / returnTime);
			transform.localEulerAngles = new Vector3(x, 90, 90);

			returnTimer += Time.deltaTime;

			if(returnTimer >= returnTime)
			{
				isToTarget = false;
				isReturning = true;
				returnTimer = 0;
			}
		}
		else
		{
			if(isReturning)
			{
				var x = Mathf.Lerp(transform.localEulerAngles.x, returnAngle, returnTimer / returnTime);
				transform.localEulerAngles = new Vector3(x, 90, 90);

				returnTimer += Time.deltaTime;

				if (returnTimer >= returnTime)
				{
					returnCount++;

					var delta = Mathf.Abs(Mathf.Abs(transform.localEulerAngles.x) - 90);
					var randomAngle = UnityEngine.Random.Range(delta / 2, delta);
					returnAngle = -90 + randomAngle * (-1) * returnCount;

					returnTimer = 0;
					if (returnCount == returnCountMax)
					{
						isReturning = false;
						return;
					}
				}
			}
		}
	}
}
