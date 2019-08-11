
//----------------------------------------------------------
//　作成日　2018/11/09
//　作成者　諸岡勇樹
//----------------------------------------------------------
//　ランキング関連
//----------------------------------------------------------
//　作成時　2018/11/09　ランキングの表示
//　　追加　2018/12/11　RankingCarry クラスから情報をもらう
//　　追加　2018/12/11　ディスプレイサイズの設定
//　　変更　2019/01/19　背景の変更
//----------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TextDisplay;

public class RankingDisplay : MonoBehaviour
{
	private Ranking_Strage.RankingInformation[] Rankings_Dis { get; set; }
	public Character_Display[] Object_To_Display { private set; get; }

	private GameObject[] Rank_Parent { get; set; }

	private void Start()
	{
		if (Scene_Manager.Manager.Now_Scene == Scene_Manager.SCENE_NAME.eGAME_CLEAR)
		{
			Rankings_Dis = Ranking_Strage.Strage;

			Rank_Parent = new GameObject[Ranking_Strage.Max_num];
			Object_To_Display = new Character_Display[Ranking_Strage.Max_num];

			for (int i = 0; i < Object_To_Display.Length; i++)
			{
				int ranking_num = i + 1;
				string s_temp = ranking_num.ToString() + "___" + Rankings_Dis[i].name + "__" + Rankings_Dis[i].score.ToString("D10");
				Rank_Parent[i] = new GameObject();
				Rank_Parent[i].name = "Rank" + ranking_num.ToString();
				Object_To_Display[i] = new Character_Display(s_temp.Length, "morooka/SS", Rank_Parent[i], Vector3.zero);
				Object_To_Display[i].Character_Preference(s_temp);
			}
		}
	}

	private void Update()
	{
		
	}
}
