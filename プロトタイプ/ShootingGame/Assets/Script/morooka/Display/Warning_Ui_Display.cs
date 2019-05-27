//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2019/05/24
//----------------------------------------------------------------------------------------------
// 「WARNING」の文字の制御
//----------------------------------------------------------------------------------------------
// 2019/05/24：カットイン中の挙動
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
    [Header("フォントのサイズ")]
    private Vector3 font_size;      // フォントサイズ
    [SerializeField]
    [Header("フォントの位置")]
    private Vector3 position;       // フォント位置
    //---------------------------------------------------------------------------

    private int fade_count{set;get;}         // フェード処理時のカウンター
    private Character_Display _Display {set;get;}       // 表示する文字

    void Start()
    {
        Font_Color = new Color();
        fade_speed = (1.0f / 255.0f) / 2.0F;
        fade_count = 0;

        _Display = new Character_Display(7, "morooka/SS", gameObject, position);
        _Display.Character_Preference("WARNING");
        _Display.Size_Change(font_size);
        Font_Color = _Display.Font_Color;
        Font_Color.a = 0.0f;
    }

    public void Update()
    {
        if (fade_count == 12)
        {
            Game_Master.MY.Management_In_Stage = Game_Master.CONFIGURATION_IN_STAGE.eBOSS_BUTLE;
            _Display.AllDestroy();
            Destroy(gameObject);
        }
        else if(fade_count % 4 == 0)
        {
            Font_Color.a -= fade_speed;
            _Display.Color_Change(Font_Color);

            if (Font_Color.a <= 0.3f)
            {
                fade_count+=2;
            }
        }
        else if (fade_count % 2 == 0)
        {
            Font_Color.a += fade_speed;
            _Display.Color_Change(Font_Color);

            if (Font_Color.a >= 1.0f)
            {
                fade_count += 2;
            }
        }

    }
}
