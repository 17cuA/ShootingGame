﻿using System.Collections;
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
	[Header("登場用のSE")]
	public AudioSource Entry;
	[Header("バキュラヒットSE")]
	public AudioSource Baculor;
	[Header("無線開始時などに使う用")]
	public AudioSource Wireless;
	private void Awake()
	{
		SE_Obj = GetComponent<SE_Manager>();
	}
	// Start is called before the first frame update
	public void SE_Active(AudioClip se)
	{
		if (audiosource1.isPlaying) audiosource1.Stop();
		if (Is_Active) audiosource1.PlayOneShot(se);
	}
	//先生からもらったもののSE
	public void SE_Active_2(AudioClip se)
	{
		if (Is_Active) Power_Up.PlayOneShot(se);
	}
	//アイテム取得用
	public void SE_Item_Catch()
	{
		if (Item_Up.isPlaying) Item_Up.Stop();
		if (Is_Active) Item_Up.Play();
	}
	//爆発用
	public void SE_Explosion(AudioClip se)
	{
		if (Explosion.isPlaying) Explosion.Stop();
		if (Is_Active) Explosion.PlayOneShot(se);
	}
	//雑魚敵用爆発
	public void SE_Explosion_small(AudioClip se)
	{
		if (Explosion_Small.isPlaying) Explosion_Small.Stop();
		if (Is_Active) Explosion_Small.PlayOneShot(se);
	}
	//登場＆リスポーン用
	public void SE_Entry(AudioClip se)
	{
		if (Entry.isPlaying) Entry.Stop();
		if (Is_Active) Entry.PlayOneShot(se);
	}
	//バキュラヒット用
	public void SE_Baculor(AudioClip se)
	{
		if (Baculor.isPlaying) Baculor.Stop();
		if (Is_Active) Baculor.PlayOneShot(se);
	}

	public void Wirelss_SE(AudioClip se)
	{
		if (Wireless.isPlaying) Baculor.Stop();
		if (Is_Active) Wireless.PlayOneShot(se);
	}
}
