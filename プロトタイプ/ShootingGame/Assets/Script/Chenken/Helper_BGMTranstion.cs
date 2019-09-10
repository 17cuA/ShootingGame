using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper_BGMTranstion : MonoBehaviour
{
	[SerializeField] private AudioClip startBGMClip;
	[SerializeField] private AudioClip oneBossBGMClip;
	[SerializeField] private AudioClip oneBossOverBGMClip;
	[SerializeField] private AudioClip twoBossBGMClip;

	[SerializeField] private float fadeInStartVolume;
	[SerializeField] private float fadeInTime;
	private float fadeInTimer;
	[SerializeField] private bool isFadeIn = false;

	[SerializeField] private float fadeOutOverVolume;
	[SerializeField] private float fadeOutTime;
	private float fadeOutTimer;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
