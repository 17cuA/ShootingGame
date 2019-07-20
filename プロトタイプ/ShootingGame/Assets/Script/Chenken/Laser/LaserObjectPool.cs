using System;
using System.Collections.Generic;
using UnityEngine;

public class LaserObjectPool
{
	private GameObject laserPrefab;
	private List<GameObject> laserPool;
	private int max;
	private int objectCount;

	private LaserObjectPool(GameObject laserPrefab, int max)
	{
		this.laserPrefab = laserPrefab;
		this.max = max;
		this.laserPool = new List<GameObject>();
		this.objectCount = 0;
	}

}

