using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_anime : MonoBehaviour
{
	public Animation animator;
	public AnimationClip yoko;
	public AnimationClip tate;
	public AnimationClip paka;

    void Start()
    {
		animator = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if((Game_Master.MY.Frame_Count % 150) == 0)
		{
			animator.Play();
		}
    }
}
