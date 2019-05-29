//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2019/05/24
//----------------------------------------------------------------------------------------------
// 「WARNING」の文字の制御
//----------------------------------------------------------------------------------------------
// 2019/05/24：カットイン中の挙動
// 2019/05/27：Unity でフェード回数を決められるようにする
// 2019/05/29：レターボックスの動きの制御
//----------------------------------------------------------------------------------------------
using UnityEngine;
using TextDisplay;

public class Warning_Ui_Display : MonoBehaviour
{
    //---------------------------------------------------------------------------
    // Unity 側調節よう変数
    //---------------------------------------------------------------------------
    [SerializeField]
    [Header("フォントの色")]
    private Color Font_Color;       // フォントカラー
    [SerializeField]
    [Header("フェードスピード")]
    private float fade_speed;       // フェードスピード
    [SerializeField]
    [Header("フェード回数")]
    private float fade_times;       // フェード回数
    [SerializeField]
    [Header("フォントのサイズ")]
    private Vector3 font_size;      // フォントサイズ
    [SerializeField]
    [Header("フォントの位置")]
    private Vector3 position;       // フォント位置
    [SerializeField]
    [Header("レターボックスのサイズ")]
    private Vector3 letter_size;       // レターボックスのサイズ
	[SerializeField]
	[Header("レターボックスの移動スピード")]
	private float letter_speed;		// レターボックスの移動スピード
	[SerializeField]
	[Header("カットイン時のレターボックスの位置(上の数値のみ、下は上に自動的に合わせる)")]
	private float letter_cut_in_pos;        // カットイン時のレターボックスの位置
	//---------------------------------------------------------------------------

	private int fade_count{set;get;}						// フェード処理時のカウンター
    public Character_Display Display {private set;get;}		// 表示する文字
    private bool Additional_Permit {set;get;}				// 加算許可
	private GameObject[] Letter_Box{set; get;}				// レターボックス
	private Vector3[] Default_Position { set;get;}			// デフォルトの位置
	private Vector3[] Cut_In_Position {set; get;}			// カットイン時の位置

    private void Start()
    {
        Font_Color = new Color();
        fade_speed = (1.0f / 255.0f) / 2.0f;
        fade_count = 0;

        Display = new Character_Display(7, "morooka/SS", gameObject, position);
        Display.Character_Preference("WARNING");
        Display.Size_Change(font_size);
        Font_Color = Display.Font_Color;
        Font_Color.a = 0.0f;
        Display.Set_Enable(false);
        Additional_Permit = true;
		Letter_Box = new GameObject[2];

		for(int i = 0; i < Letter_Box.Length; i ++)
		{
			Letter_Box[i] = transform.GetChild(i).gameObject;
			Default_Position[i] = Letter_Box[i].transform.position;
		}

		Cut_In_Position[0].y = letter_cut_in_pos;
		Cut_In_Position[1].y = letter_cut_in_pos * -1;
    }

    private void Update()
    {
        switch (Game_Master.MY.Management_In_Stage)
        {
            case Game_Master.CONFIGURATION_IN_STAGE.eNORMAL:
                if(Display.Enable)
                {
                    Display.Set_Enable(false);
                }
                break;
            case Game_Master.CONFIGURATION_IN_STAGE.eBOSS_CUT_IN:
                if(fade_count == 0)
                {
                    Display.Set_Enable(true);
                    fade_count++;
                }
                else if(fade_count >= 1 && fade_count < 1 + fade_times * 2)
                {
                    if(Additional_Permit)
                    {
                        Font_Color.a += fade_speed;
                        Display.Color_Change(Font_Color);

                        if (Font_Color.a >= 1.0f)
                        {
                            fade_count++;
                            Additional_Permit = false;
                        }
                    }
                    else if(!Additional_Permit)
                    {
                        Font_Color.a -= fade_speed;
                        Display.Color_Change(Font_Color);

                        if (Font_Color.a <= 0.3f)
                        {
                            fade_count++;
                            Additional_Permit = true;
                        }
                    }
                }
                else if(fade_count == 1 + fade_times * 2)
                {
                    Font_Color.a = 0.0f;
                    Display.Set_Enable(false);
                    Game_Master.MY.Management_In_Stage = Game_Master.CONFIGURATION_IN_STAGE.eBOSS_BUTLE;
                }

                break;
            case Game_Master.CONFIGURATION_IN_STAGE.eBOSS_BUTLE:
                break;
            case Game_Master.CONFIGURATION_IN_STAGE.eCLEAR:
                break;
            default:
                break;
        }
    }

	private void letter_moove()
	{
		
	}
}
