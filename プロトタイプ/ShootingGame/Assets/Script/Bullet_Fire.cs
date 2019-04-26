using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Fire : MonoBehaviour 
{
    [SerializeField]
    private GameObject Bullet;      //弾のPrefab情報

    private Transform Player_transform;
    // Use this for initialization
    void Start () 
    {
        Bullet = Resources.Load("Player_Bullet") as GameObject;
        Player_transform = transform.parent;
    }

	public void Bullet_Create()
	{
		if (Input.GetButton("Fire2") || Input.GetKeyDown(KeyCode.Space))
		{
			Instantiate
			(
				Bullet,
				gameObject.transform.position,
				Player_transform.rotation
			);
		}
	}
}
