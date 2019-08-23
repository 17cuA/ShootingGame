using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_Manager : MonoBehaviour
{
	public static SE_Manager SE_Obj;
	[Header("音を鳴らすかどうか")]
	public bool Is_Active;
	[Header("基本的なSEたち")]
	public AudioSource audiosource1;
	[Header("パワーアップ時になるSE")]
	public AudioSource Power_Up;
	[Header("アイテムを取得するときになるSE")]
	public AudioSource Item_Up;
	[Header("爆発のSE")]
	public AudioSource Explosion;
	[Header("爆発のSE小型")]
	public AudioSource Explosion_Small;
	[Header("レーザー用のSE")]
	public AudioSource Laser1;
	private void Awake()
	{
		SE_Obj = GetComponent<SE_Manager>();
	}
	// Start is called before the first frame update
	public void SE_Active(AudioClip se)
	{
		if(Is_Active) audiosource1.PlayOneShot(se);
	}
	//先生からもらったもののSE
	public void SE_Active_2(AudioClip se)
	{
		if (Is_Active) Power_Up.PlayOneShot(se);
	}
	public void SE_Item_Catch(AudioClip se)
	{
		if (Item_Up.isPlaying) Item_Up.Stop();
		if (Is_Active) Item_Up.Play();
	}
	public void SE_Explosion(AudioClip se)
	{
		if (Explosion.isPlaying) Explosion.Stop();
		if (Is_Active) Explosion.PlayOneShot(se);
	}
	public void SE_Explosion_small(AudioClip se)
	{
		if (Explosion_Small.isPlaying) Explosion_Small.Stop();
		if (Is_Active) Explosion_Small.PlayOneShot(se);
	}

}
