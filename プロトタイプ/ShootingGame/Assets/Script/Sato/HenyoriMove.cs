using UnityEngine;
using System.Collections;

public class HenyoriMove : MonoBehaviour, IPointGetter
{

	[SerializeField] float moveSpeed = 4;
	private const float waitTime = 0.5f;
	private WaitForSeconds waitOnStart = new WaitForSeconds(waitTime);

	private Transform cachedTransform
	{
		get
		{
			return _cachedTransform ? _cachedTransform : _cachedTransform = transform;
		}
	}

	private Transform _cachedTransform;

	Vector3? IPointGetter.GetPoint()
	{
		return cachedTransform.position;
	}

	IEnumerator Start()
	{
		yield return waitOnStart;
		yield return new WaitForSeconds(0.2f);
		changeSpeedCor(-0.5f, 0.8f, true);
		yield return changeDirectionCor(-180, 1.5f, true);
		yield return changeDirectionCor(-120, 1, true);
		changeSpeedCor(2f, 1f, true);
		yield return new WaitForSeconds(0.28f);
		yield return changeDirectionCor(80, 0.8f, true);
		yield return changeDirectionCor(-80, 0.8f, true);
	}

	void Update()
	{
		if (Time.time < waitTime) return;
		cachedTransform.position += cachedTransform.up * moveSpeed * Time.deltaTime;
	}
	IEnumerator changeSpeedCor(float dest, float term, bool relative)
	{
		if (relative)
		{
			dest += moveSpeed;
		}

		if (term <= 0)
		{
			moveSpeed = dest;
			yield break;
		}

		float startSpeed = moveSpeed;
		float startTime = Time.time;
		var waitForEndOfFrame = new WaitForEndOfFrame();

		while (startTime + term > Time.time)
		{
			float rate = (Time.time - startTime) / term;
			moveSpeed = startSpeed * (1 - rate) + dest * rate;
			yield return waitForEndOfFrame;
		}

		moveSpeed = dest;
	}
	IEnumerator changeDirectionCor(float dest, float term, bool relative)
	{
		if (relative)
		{
			dest += cachedTransform.eulerAngles.z;
		}

		if (term <= 0)
		{
			setAngleZ(dest);
			yield break;
		}

		float startDir = cachedTransform.eulerAngles.z;
		float startTime = Time.time;
		var waitForEndOfFrame = new WaitForEndOfFrame();

		while (startTime + term > Time.time)
		{
			float rate = (Time.time - startTime) / term;
			float curDir = startDir * (1 - rate) + dest * rate;
			setAngleZ(curDir);
			yield return waitForEndOfFrame;
		}

		setAngleZ(dest);
	}
	void setAngleZ(float newAngle)
	{
		cachedTransform.eulerAngles = new Vector3(cachedTransform.eulerAngles.x, cachedTransform.eulerAngles.y, newAngle);
	}
}