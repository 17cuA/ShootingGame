//作成日2019/07/08
// シーンマネージャー
// 作成者:諸岡勇樹
/*
 * 2019/07/17 フェードイン、フェードアウトする　シーン遷移
 * 2019/07/18 フェードアウト後、ディレイを入れる
 * 2019/07/19 フェードイン中にボタンが押されるとゲームが止まるバグの修正
 * 2019/07/21 説明シーン追加
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
		eCAUTION,
		eROGO,
		eTITLE,
		eMENU,
        eINSTRUCTION,
        eSTAGE_01,
		eSTAGE_02,
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
		Next_Scene = Now_Scene = (SCENE_NAME)SceneManager.GetActiveScene().buildIndex;
		Fade_In_Quantity = (255.0f / fade_in_speed) / 255.0f;
		Fade_Out_Quantity = (255.0f / fade_out_speed) / 255.0f;

		if (Now_Scene != SCENE_NAME.eTITLE)
		{
			Is_Fade_Finished = false;
			Is_Fade_In_Intermediate = true;
			Is_Fade_Out_Intermediate = false;
		}
		else if(Now_Scene == SCENE_NAME.eTITLE)
		{
			Renderer_For_Fade.color = Color.clear;
			Is_Fade_Finished = true;
			Is_Fade_In_Intermediate = false;
			Is_Fade_Out_Intermediate = false;
		}

		Transition_Deferred_Cnt = 0;
	}

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

		if (Renderer_For_Fade.color.a > 0.0f)
		{
			Color color_for_fade = Renderer_For_Fade.color;
				color_for_fade.a -= Fade_In_Quantity;

		Renderer_For_Fade.color = color_for_fade;

				Is_Fade_Finished = false;
				Is_Fade_In_Intermediate = true;
		}
		return Is_Fade_Finished;
	}

	/// <summary>
	/// フェードアウト
	/// </summary>
	/// <returns> フェードアウトが終わっているかどうか </returns>
	public bool Fade_Out()
	{
		Is_Fade_Finished = true;

		if (Renderer_For_Fade.color.a < 1.0f)
		{
			Color color_for_fade = Renderer_For_Fade.color;
				color_for_fade.a += Fade_Out_Quantity;

		Renderer_For_Fade.color = color_for_fade;

				Is_Fade_Finished = false;
		}
		return Is_Fade_Finished;
	}

	/// <summary>
	/// 注意書きシーンへ移動
	/// </summary>
	public void Screen_Transition_To_Caution()
	{
		if (!Is_Fade_Out_Intermediate && Is_Fade_Finished)
		{
			Is_Fade_Out_Intermediate = true;
		}
		Next_Scene = SCENE_NAME.eCAUTION;
	}

	/// <summary>
	/// ロゴシーンに移動
	/// </summary>
	public void Screen_Transition_To_ROGO()
	{
		if(!Is_Fade_Out_Intermediate && Is_Fade_Finished)
		{
			Is_Fade_Out_Intermediate = true;
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
	/// ステージ_01 に移動
	/// </summary>
	public void Screen_Transition_To_Stage_01()
	{
		if(!Is_Fade_Out_Intermediate && Is_Fade_Finished)
		{
			Is_Fade_Out_Intermediate = true;
		}
		Next_Scene = SCENE_NAME.eSTAGE_01;
	}

	/// <summary>
	/// ステージ_02 に移動
	/// </summary>
	public void Screen_Transition_To_Stage_02()
	{
		if(!Is_Fade_Out_Intermediate && Is_Fade_Finished)
		{
			Is_Fade_Out_Intermediate = true;
		}
		Next_Scene = SCENE_NAME.eSTAGE_02;
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
    /// ゲーム説明に移動
    /// </summary>
    public void Screen_Transition_To_Instruction()
    {
        if (!Is_Fade_Out_Intermediate && Is_Fade_Finished)
        {
            Is_Fade_Out_Intermediate = true;
        }

        Next_Scene = SCENE_NAME.eINSTRUCTION;
    }

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
