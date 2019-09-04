using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_star_Fish : MonoBehaviour
{
	Vector3 playerPos;
	Vector3 firstPos;
	[SerializeField]float speed;
	float arrivalTime;
	// Start is called before the first frame update
	void Start()
    {
		playerPos = ~~;
		firstPos = transform.position;
		arrivalTime = Vector3.Distance(firstPos, playerPos) / speed;
		StartCoroutine(Move());
	}

	// Update is called once per frame
	void Update()
    {
    }

	IEnumerator Move()
	{
		float time = 0;
		while(true)
		{
			time += Time.deltaTime;
			float percentage = time / arrivalTime;
			transform.position = firstPos * (1 - percentage) + playerPos * percentage;
			if ()
				yield break;
			yield return null;
		}
	}
}
