//作成日2019/07/08
// シーンマネージャー
// 作成者:諸岡勇樹
/*
 * 2019/07/17 フェードイン、フェードアウトする　シーン遷移
 * 2019/07/18 フェードアウト後、ディレイを入れる
 * 2019/07/19 フェードイン中にボタンが押されるとゲームが止まるバグの修正
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
	/// <summary>
	/// シーンの名前管理イナム
	/// </summary>
	public enum SCENE_NAME
	{
		eROGO,
		eTITLE,
		eMENU,

         //------------------7/21追加-----------------
         //勝手にいじっちゃて申し訳ない
         //説明シーン列挙型追加
         //-------------------------------------------
        //eINSTRUCTION, 


		eSTAGE,
		eGAME_OVER,
		eGAME_CLEAR,
	}

	static public Scene_Manager Manager { get; private set; }		// シーンマネージャー自体の保存

	[SerializeField, Header("フェードインスピード")]		private float fade_in_speed;
	[SerializeField, Header("フェードアウトスピード")]		private float fade_out_speed;
	[SerializeField, Header("シーン遷移の遅延時間")]		private int transition_deferred;

	private Image Renderer_For_Fade { get; set; }					// フェード用SpriteRenderer
	private float Fade_In_Quantity { get; set; }					// 1フレームのフェードイン量
	private float Fade_Out_Quantity { get; set; }					// 1フレームのフェードアウト量
	private int Transition_Deferred_Cnt{ get; set; }				// シーン遷移の遅延時間カウント
	public SCENE_NAME Now_Scene{ get; private set; }				// 今のシーン保存用
	public SCENE_NAME Next_Scene { get; private set; }				// 次のシーン保存用
	public bool Is_Fade_Finished { get; private set; }				// フェードが終わっているかどうか
	public bool Is_Fade_In_Intermediate { get; private set; }		// フェードイン中かどうか
	public bool Is_Fade_Out_Intermediate { get; private set; }		// フェードアウト中かどうか

	void Start()
    {
		Manager = GetComponent<Scene_Manager>();

		Renderer_For_Fade = transform.GetChild(0).GetChild(0).GetComponent<Image>();
		//for (int i = 0;i<Renderer_For_Fade.Length;i++)
		//{
		//	Renderer_For_Fade[i] = transform.GetChild(i).GetChild(0).GetComponent<Image>();
		//}

		Next_Scene = Now_Scene = (SCENE_NAME)SceneManager.GetActiveScene().buildIndex;
		Fade_In_Quantity = fade_in_speed / 255.0f;
		Fade_Out_Quantity = fade_out_speed / 255.0f;
		Is_Fade_Finished = false;
		Is_Fade_In_Intermediate = true;
		Is_Fade_Out_Intermediate = false;

		Transition_Deferred_Cnt = 0;
	}

    // Update is called once per frame
    void Update()
    {
		if(Is_Fade_In_Intermediate && !Is_Fade_Out_Intermediate)
		{
			Fade_In();
		}
		else if(!Is_Fade_In_Intermediate && Is_Fade_Out_Intermediate)
		{
			if (Fade_Out())
			{
				Transition_Deferred_Cnt++;
				if (Transition_Deferred_Cnt > transition_deferred)
				{
					SceneManager.LoadScene((int)Next_Scene);
				}
			}
		}
	}

	/// <summary>
	/// フェードイン
	/// </summary>
	/// <returns> フェードインが終わっているかどうか </returns>
	public bool Fade_In()
	{
		Is_Fade_Finished = true;
		Is_Fade_In_Intermediate = false;

		//foreach (Image image in Renderer_For_Fade)
		//{
		if (Renderer_For_Fade.color.a > 0.0f)
		{
			Color color_for_fade = Renderer_For_Fade.color;
				color_for_fade.a -= Fade_In_Quantity;

		Renderer_For_Fade.color = color_for_fade;

				Is_Fade_Finished = false;
				Is_Fade_In_Intermediate = true;
		}
		//}
		return Is_Fade_Finished;
	}

	/// <summary>
	/// フェードアウト
	/// </summary>
	/// <returns> フェードアウトが終わっているかどうか </returns>
	public bool Fade_Out()
	{
		Is_Fade_Finished = true;

		//foreach (Image image in Renderer_For_Fade)
		//{
		if (Renderer_For_Fade.color.a < 1.0f)
		{
			Color color_for_fade = Renderer_For_Fade.color;
				color_for_fade.a += Fade_Out_Quantity;

		Renderer_For_Fade.color = color_for_fade;

				Is_Fade_Finished = false;
		}
		//}
		return Is_Fade_Finished;
	}

	/// <summary>
	/// ロゴシーンに移動
	/// </summary>
	public void Screen_Transition_To_ROGO()
	{
		if(!Is_Fade_Out_Intermediate && Is_Fade_Finished)
		{
			Is_Fade_Out_Intermediate = false;
		}
		Next_Scene = SCENE_NAME.eROGO;
	}

	/// <summary>
	/// タイトルに移動
	/// </summary>
	public void Screen_Transition_To_Title()
	{
		if(!Is_Fade_Out_Intermediate && Is_Fade_Finished)
		{
			Is_Fade_Out_Intermediate = true;
		}
		Next_Scene = SCENE_NAME.eTITLE;
	}

	/// <summary>
	/// メニューに移動
	/// </summary>
	public void Screen_Transition_To_Menu()
	{
		if(!Is_Fade_Out_Intermediate && Is_Fade_Finished)
		{
			Is_Fade_Out_Intermediate = true;
		}

		Next_Scene = SCENE_NAME.eMENU;
	}

	/// <summary>
	/// ステージに移動
	/// </summary>
	public void Screen_Transition_To_Stage()
	{
		if(!Is_Fade_Out_Intermediate && Is_Fade_Finished)
		{
			Is_Fade_Out_Intermediate = true;
		}
		Next_Scene = SCENE_NAME.eSTAGE;
	}

	/// <summary>
	/// ゲームオーバーに移動
	/// </summary>
	public void Screen_Transition_To_Over()
	{
		if(!Is_Fade_Out_Intermediate && Is_Fade_Finished)
		{
			Is_Fade_Out_Intermediate = true;
		}

		Next_Scene = SCENE_NAME.eGAME_OVER;
	}

	/// <summary>
	/// ゲームクリアーに移動
	/// </summary>
	public void Screen_Transition_To_Clear()
	{
		if(!Is_Fade_Out_Intermediate && Is_Fade_Finished)
		{
			Is_Fade_Out_Intermediate = true;
		}

		Next_Scene = SCENE_NAME.eGAME_CLEAR;
	}

    /// <summary>
    /// ----------------7/21追加------------------
    /// 作成者：陳
    /// ゲーム説明に移動
    /// ------------------------------------------
    /// </summary>
 //   public void Screen_Transition_To_Instruction()
	//{
	//	if(!Is_Fade_Out_Intermediate && Is_Fade_Finished)
	//	{
	//		Is_Fade_Out_Intermediate = true;
	//	}

	//	Next_Scene = SCENE_NAME.eINSTRUCTION;
	//}

	/// <summary>
	/// 任意のシーンに移動
	/// </summary>
	/// <param name="name"> シーンの名前 </param>
	public void Scene_Transition(SCENE_NAME name)
	{
		if (!Is_Fade_Out_Intermediate && Is_Fade_Finished)
		{
			Is_Fade_Out_Intermediate = true;
		}
		Next_Scene = name;
	}
}
