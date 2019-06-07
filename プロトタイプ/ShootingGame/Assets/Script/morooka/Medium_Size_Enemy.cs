//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2019/06/07
//----------------------------------------------------------------------------------------------
//  中型エネミーの挙動
//----------------------------------------------------------------------------------------------
// 2019/06/07：移動、攻撃処理
//----------------------------------------------------------------------------------------------
using UnityEngine;

public class Medium_Size_Enemy : character_status
{
	[SerializeField]
	[Header("コーナーの数")]
	private int corner_number;
	[SerializeField]
	[Header("コーナーの位置")]
	private Vector3[] corner_position;
	private bool Is_Up { get; set; }
	private bool Is_Right { get; set; }

	void Start()
    {
		Is_Up = true;
		Is_Right = true;
    }

    void Update()
    {
		// 左上
        if(transform.position != corner_position[0] && Is_Up && !Is_Right)
		{
			transform.position = Vector3.Lerp(transform.position, corner_position[0], speed);
			if(transform.position == corner_position[0])
			{
				Is_Right = true;
			}
		}
		// 右上
       else  if(transform.position != corner_position[1] && Is_Up && Is_Right)
		{
			transform.position = Vector3.Lerp(transform.position, corner_position[1], speed);
			if (transform.position == corner_position[1])
			{
				Is_Up = false;
			}

		}
		// 右下
		else if(transform.position != corner_position[2] && !Is_Up && Is_Right)
		{
			transform.position = Vector3.Lerp(transform.position, corner_position[2], speed);
			if (transform.position == corner_position[2])
			{
				Is_Right = false;
			}

		}
		// 左下
		else if(transform.position != corner_position[3] && !Is_Up && !Is_Right)
		{
			transform.position = Vector3.Lerp(transform.position, corner_position[3], speed);
			if (transform.position == corner_position[3])
			{
				Is_Up = true;
			}
		}
	}
}
