using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final_Boss : MonoBehaviour
{
	// "final"
	private Queue<Vector3> focusOnPosQueue;
	public float focusRadius = 3f;
	public float focusUpdateTime = 0.5f;
	private float timer;
	public GameObject foucusTarget;
	public float speed;
	private float moveTime;
	private float moveTimer;
	private Vector3 startMovePos;
	[SerializeField] private bool isMove = false;


	private void Awake()
	{
		focusOnPosQueue = new Queue<Vector3>();
	}

	private void Update()
	{
		if (timer >= focusUpdateTime)
		{
			var cols = Physics.OverlapSphere(this.transform.position, focusRadius, 1 << LayerMask.NameToLayer("Player"));
			if (cols != null)
			{
				for (var i = 0; i < cols.Length; ++i)
				{
					if (cols[i].name == "Player")
					{
						focusOnPosQueue.Enqueue(cols[i].transform.position);
						Debug.Log(cols[i].transform.position);
					}
				}
			}
			else
			{
				var target = GameObject.Find("Player");
				if(target != null)
				{
					var angle = Vector3.Angle(Vector3.right, target.transform.position - this.transform.position);
					Vector3 newFocusOnPos = Vector3.zero;
					newFocusOnPos.x = transform.position.x + focusRadius * Mathf.Cos(angle * Mathf.Deg2Rad);
					newFocusOnPos.y = transform.position.y + focusRadius * Mathf.Sin(angle * Mathf.Deg2Rad);
					focusOnPosQueue.Enqueue(newFocusOnPos);
					Debug.Log(newFocusOnPos);
				}
			}
			timer = 0f;
		}
		timer += Time.deltaTime;

		if (!isMove)
		{
			if (focusOnPosQueue.Count > 0)
			{
				var pos = focusOnPosQueue.Peek();
				moveTime = Vector3.Distance(pos, foucusTarget.transform.position) / speed;
				startMovePos = foucusTarget.transform.position;
				isMove = true;
			}
		}
		else
		{
			if(moveTimer <= moveTime)
			{
				foucusTarget.transform.position = Vector3.Lerp(startMovePos, focusOnPosQueue.Peek(), moveTimer / moveTime);

				moveTimer += Time.deltaTime;
			}
			else
			{
				foucusTarget.transform.position = focusOnPosQueue.Dequeue();
				moveTimer = 0;
				isMove = false;
			}
		}
	}
}
