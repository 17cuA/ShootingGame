//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2019/05/24
//----------------------------------------------------------------------------------------------
// ゲーム画面中の黒枠制御
//----------------------------------------------------------------------------------------------
// 2019/05/24：シーン中の移動
//----------------------------------------------------------------------------------------------
using UnityEngine;

public class BlackFlame : MonoBehaviour
{
    [SerializeField]
    [Header("レターボックスの移動速度")]
    private float move_speed;       // 移動速度

    private Vector3[] cut_in_position;      // カットイン中のポジション
    private Vector3[] default_position;     // デフォルトのポジション

    public RectTransform[] Framese{private set; get;}       // 黒枠のトランスフォーム

    void Start()
    {
        Framese = new RectTransform[transform.childCount];
        cut_in_position = new Vector3[Framese.Length];
        default_position = new Vector3[Framese.Length];

        for (int i = 0; i < Framese.Length; i++)
        {
            Framese[i] = transform.GetChild(i).transform as RectTransform;
            default_position[i] = Framese[i].localPosition;
        }

        cut_in_position[0] = new Vector3(0.0f, 480.0f, 0.0f);
        cut_in_position[1] = new Vector3(0.0f, -480.0f, 0.0f);
    }

    void Update()
    {
        switch (Game_Master.MY.Management_In_Stage)
        {
            case Game_Master.CONFIGURATION_IN_STAGE.eNORMAL:
                Moving_Position_Default();
                break;
            case Game_Master.CONFIGURATION_IN_STAGE.eBOSS_CUT_IN:
                Moving_Position_Cut_In();
                break;
            case Game_Master.CONFIGURATION_IN_STAGE.eBOSS_BUTLE:
                Moving_Position_Default();
                break;
            case Game_Master.CONFIGURATION_IN_STAGE.eCLEAR:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// デフォルトの位置に移動させる
    /// </summary>
    private void Moving_Position_Default()
    {
        for(int i = 0; i < Framese.Length; i++)
        {
            if(Framese[i].localPosition != default_position[i])
            {
                Framese[i].localPosition = Vector3.MoveTowards(Framese[i].localPosition, default_position[i], move_speed);
            }
        }
    }

    /// <summary>
    /// カットイン中の位置に移動させる
    /// </summary>
    private void Moving_Position_Cut_In()
    {
        for(int i = 0; i < Framese.Length; i++)
        {
            if(Framese[i].localPosition != cut_in_position[i])
            {
                Framese[i].localPosition = Vector3.MoveTowards(Framese[i].transform.localPosition, cut_in_position[i], move_speed);
            }
        }
    }
}
