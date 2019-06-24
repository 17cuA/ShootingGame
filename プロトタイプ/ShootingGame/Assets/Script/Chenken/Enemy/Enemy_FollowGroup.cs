using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_FollowGroup : character_status
{
	public float preMoveDuration;
	public List<GameObject> childs = new List<GameObject>();
	public Item dropItem;
	public int index;

	private bool only = true;
	private float preMoveTime;
	private Vector3 target;
	private List<Vector3> childTarget = new List<Vector3>();
	private void Awake()
	{
		base.Type = Chara_Type.Enemy;
		base.HP_Setting();

		target = Vector3.up ;
	}

	private void Start()
	{
		preMoveTime = Time.time + preMoveDuration;
		for (var i = 0; i < childs.Count; ++i)
		{
			childTarget.Add(transform.position);
		}
	}

	private void Update()
	{
		//円形
		if (Time.time > preMoveTime)
		{
			if (only)
			{
				only = false;
				direction *= -1;
			}
			else
			{
				direction = (direction + target * index * Time.deltaTime).normalized;

				if (Vector3.Distance(direction, target) < 0.2f)
				{
					direction = target;
					target = Normal(target);
				}
			}
		}
		else
		{
			for(var i = 0; i < childs.Count; ++i)
			{
				childTarget[i] = transform.position;
			}
		}


		for (var i = 0; i < childs.Count; ++i)
		{
			if (i == 0)
			{
				if (Vector3.Distance(childs[i].transform.position, childTarget[i]) >= 0.5f)
				{	
					childs[i].transform.position = Vector3.MoveTowards(childs[i].transform.position, childTarget[i], speed * Time.deltaTime);
				}
				else
				{
					childTarget[i] = transform.position;
				}
			}
			else
			{
				if (Vector3.Distance(childs[i].transform.position, childTarget[i]) >= 0.5f)
				{
					childs[i].transform.position = Vector3.MoveTowards(childs[i].transform.position, childTarget[i], speed * Time.deltaTime);
				}
				else
				{
					childTarget[i] = childs[i - 1].transform.position;
				}
			}
		}

		transform.position += direction * speed * Time.deltaTime;
	}

	private Vector3 Normal(Vector3 vec)
	{
		return new Vector3(-vec.y, vec.x, 0);
	}
}

