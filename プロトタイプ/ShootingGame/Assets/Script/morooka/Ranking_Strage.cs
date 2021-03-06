﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranking_Strage : MonoBehaviour
{
	//　ランキングに必要な情報のまとめ
	public struct RankingInformation
	{
		public string name;     //　ランキングを獲得した人の名前
		public uint score;       //　スコア

		public RankingInformation(string s = "", uint n = 0) : this()
		{
			name = s;
			score = n;
		}
	}

	public const int Max_num = 30;
	public const int Reserve_Number = 31;
	public const string kDefaultName = "YOU";
	public const string kEmptyName = "XXX";
	static public Ranking_Strage Strage_Data { get; private set; }
	static public RankingInformation[] Strage { get; private set; }		// 倉庫

	private RankingDisplay _Display { get; set; }

	void Start()
    {
		if (Scene_Manager.Manager.Now_Scene == Scene_Manager.SCENE_NAME.eTITLE)
		{
			Strage_Data = GetComponent<Ranking_Strage>();
			Ranking_Lode();
		}
		else if(Scene_Manager.Manager.Now_Scene == Scene_Manager.SCENE_NAME.eGAME_CLEAR)
		{
			Set_Score(kDefaultName, Game_Master.display_score);
			_Display = GetComponent<RankingDisplay>();
			_Display.Init();
		}
	}

	public void Update()
	{
		if (Scene_Manager.Manager.Now_Scene == Scene_Manager.SCENE_NAME.eTITLE)
		{
			if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.Alpha0))
			{
				PlayerPrefs.DeleteAll();
				Ranking_Lode();
			}
		}
	}

	//　配列の降順ソート（ランキングの設定）
	public RankingInformation[] Strage_Sort(RankingInformation[] rankingArray)
	{
		RankingInformation temp;

		for (int i = 0; i < Max_num; i++)
		{
			for (int j = Max_num - i; j > i; j--)
			{
				if (rankingArray[j].score >= rankingArray[j - 1].score)
				{
					temp = rankingArray[j];
					rankingArray[j] = rankingArray[j - 1];
					rankingArray[j - 1] = temp;
				}
			}
		}
		return rankingArray;
	}

	public void Set_Score(string name, uint score)
	{
		Strage[Max_num].name = name;
		Strage[Max_num].score = score;
		Strage = Strage_Sort(Strage);
		Ranking_Save();
	}

	public void Ranking_Save()
	{
		for(int i = 0; i < Max_num; i++)
		{
			PlayerPrefs.SetString(i.ToString() + "_Name", Strage[i].name);
			PlayerPrefs.SetInt(i.ToString() + "_Score", (int)Strage[i].score);

			Debug.Log("Save_Set:key:" + i.ToString() + " name:" + Strage[i].name + " Score:" + Strage[i].score);
		}

		PlayerPrefs.Save();
	}

	private void Ranking_Lode()
	{
		Strage = new RankingInformation[Reserve_Number];
		for (int i = 0; i < Max_num; i++)
		{
			Strage[i].name = PlayerPrefs.GetString(i.ToString() + "_Name", kEmptyName);
			Strage[i].score = (uint)PlayerPrefs.GetInt(i.ToString() + "_Score", 0);
			Debug.Log("Lode_kekka:key:" + i.ToString() + " name:" + Strage[i].name + " Score:" + Strage[i].score);
		}
	}


}
