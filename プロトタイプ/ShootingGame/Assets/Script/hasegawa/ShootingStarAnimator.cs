/*
 * 20190725 作成
 * author hasegawa yuuta
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStarAnimator : MonoBehaviour
{
	[SerializeField] List<ShootingStarBehaviour> animationObjectList = new List<ShootingStarBehaviour>();
	GameObject starParentObject = null;

	void Start()
	{
		starParentObject = new GameObject("StarObjects");
		Object[] array = FindObjectsOfType<ShootingStarBehaviour>();
		foreach(Object a in array)
		{
			if (animationObjectList.Contains(a as ShootingStarBehaviour)) { continue; }
			animationObjectList.Add(a as ShootingStarBehaviour);
			(a as ShootingStarBehaviour).transform.parent = starParentObject.transform;
		}
	}
	public void AnimationRandomShootingStar()
	{
		if (animationObjectList.Count <= 0) { return; }
		int randomNum = Random.Range(0, animationObjectList.Count);
		animationObjectList[randomNum].Activate();
	}
}
