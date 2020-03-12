using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_ReflectedBullet : bullet_status
{
	public float speed;
	public Vector3 direction;
	public bool isTouch = false;
	public float touchCoolDownTime = 0.1f;
	private float touchCoolDownTimer = 0f;
	[Range(3, 10)]
	public int reflectCount = 5;
	private int reflectCounter = 0;
	public bool isCloseReflect = false;


	public void Update()
	{
		//
		if (transform.position.x >= 20f || transform.position.x <= -20f
				   || transform.position.y >= 8 || transform.position.y <= -8f)
		{
			gameObject.SetActive(false);
		}

		if (!isTouch)
		{
			if (!isCloseReflect)
			{
				if (transform.position.x >= 18.5f || transform.position.x <= -18.5f
				|| transform.position.y >= 6f || transform.position.y <= -6f)
				{
					isTouch = true;
					reflectCounter++;

					if (reflectCounter == reflectCount)
						isCloseReflect = true;

					var angle = Mathf.Atan(direction.y / direction.x) * Mathf.Rad2Deg;
					var realAngle = 180 - angle;
					transform.localEulerAngles = new Vector3(0, 0, realAngle);
					Debug.Log(angle);

					Vector3 newDir = Vector3.zero;
					if (transform.position.y >= 6f)
						newDir = Vector3.Reflect(direction, Vector3.up);
					else if (transform.position.y <= -6f)
						newDir = Vector3.Reflect(direction, Vector3.down);
					else if (transform.position.x >= 18.5f)
						newDir = Vector3.Reflect(direction, Vector3.right);
					else if (transform.position.x <= -18.5f)
						newDir = Vector3.Reflect(direction, Vector3.left);

					direction = newDir;
				}
			}
		}
		else
		{
				touchCoolDownTimer += Time.deltaTime;
			if(touchCoolDownTimer >= touchCoolDownTime)
			{
				isTouch = false;
				touchCoolDownTimer = 0f;
			}
		}
	

		transform.position += direction * speed * Time.deltaTime;
	}

	public void Set(Vector3 direction, float angle, bool canReflect = false)
	{
		this.direction = direction;
		this.transform.localEulerAngles = new Vector3(0, 0, angle);
		isCloseReflect = !canReflect;
	}
}
