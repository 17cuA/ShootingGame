using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Remake_LaserEmitter : MonoBehaviour
{
	[Header("■■■■■　デバッグ設定　■■■■■")]
	[SerializeField] private bool isUseAudio = false;
	[SerializeField] private KeyCode fireKey = KeyCode.Space;
	[SerializeField] private string fireButtonName;

	[Header("■■■■■■■■■　音設定 ■■■■■■■■■")]
	[SerializeField] private AudioClip laserBegin;
	[SerializeField] private AudioClip laserContinuing;
	[SerializeField] private AudioClip laserEnd;
	[SerializeField] [Range(0.3f, 1f)] private float audioCheckLength = 0.6f;
	private AudioSource audioSource;
	
	[Header("■■■■■ Type-1(直線型) ■■■■■")]
	[SerializeField] [Range(10, 70)]    private int s_laserNodeMax = 50;
	[SerializeField] [Range(0, 0.6f)]   private float s_laserOverLoadDuration = 0.4f;
	[SerializeField] [Range(0f, 0.05f)] private float s_laserLaunchInterval = 0.01f;
	private GameObject s_laserGeneratorParent;

	[Header("■■■■■ Type-2(曲線型) ■■■■■")]
	[SerializeField] [Range(10, 100)]  private int r_laserNodeMax = 70;
	[SerializeField] [Range(0, 0.6f)]  private float r_laserOverloadDuration = 0.4f;
	[SerializeField] [Range(0, 0.05f)] private float r_laserLaunchInterval = 0.01f;
	private GameObject r_laserGeneratorParent;

	//■■■■■■■■■ 魔物が潜り込んでいます■■■■■■■■■■
	private GameObject laserEmitterParent;
	private Bit_Formation_3 bitFormation;
	private bool isOption;
	private InputManagerObject inputManager1P;
	private InputManagerObject inputManager2P;
	//■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■


	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
