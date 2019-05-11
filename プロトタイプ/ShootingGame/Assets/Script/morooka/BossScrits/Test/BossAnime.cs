using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnime : MonoBehaviour
{
	public Animator myAnimator;
	public string[] animeName = new string[3];
	public int i;

    void Start()
    {
		myAnimator = GetComponent<Animator>();
		//animeName[0] = "kurukuru";
		//animeName[1] = "pakapaka";
		//animeName[2] = "yoko";
		i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if((Game_Master.MY.Frame_Count % 270) == 0)
		{
			if(i == 2)
			{
				i = 0;
			}
			else
			{
				i++;
			}
			myAnimator.Play(animeName[i]);
		}
    }
}
