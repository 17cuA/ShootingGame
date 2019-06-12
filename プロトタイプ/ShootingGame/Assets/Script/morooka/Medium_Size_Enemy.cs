//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2019/06/07
//----------------------------------------------------------------------------------------------
//  中型エネミーの挙動
//----------------------------------------------------------------------------------------------
// 2019/06/07：移動、攻撃処理
//----------------------------------------------------------------------------------------------
using UnityEngine;
using StorageReference;

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
	private GameObject[] Shot_Mazle { get; set; }

	private float calculating_number { get; set; }


	void Start()
    {
		Is_Up = true;
		Is_Right = true;
		Shot_Mazle = new GameObject[transform.childCount];
		for(int i = 0; i< transform.childCount;i++)
		{
			Shot_Mazle[i] = transform.GetChild(i).gameObject;
		}

		calculating_number = 2;
	}

    void Update()
    {
		print(Game_Master.MY.Frame_Count);
		float pos_x = Mathf.Cos(calculating_number * Mathf.PI * Time.time);
		float pos_y = Mathf.Sin(calculating_number * calculating_number * Mathf.PI * Time.time);
		transform.position = new Vector2(pos_x, pos_y);
		//// 左上
  //      if(transform.position != corner_position[0] && Is_Up && !Is_Right)
		//{
		//	transform.position = Vector3.Lerp(transform.position, corner_position[0], speed);
		//	if(transform.position == corner_position[0])
		//	{
		//		Is_Right = true;
		//	}
		//}
		//// 右上
  //     else  if(transform.position != corner_position[1] && Is_Up && Is_Right)
		//{
		//	transform.position = Vector3.Lerp(transform.position, corner_position[1], speed);
		//	if (transform.position == corner_position[1])
		//	{
		//		Is_Up = false;
		//	}

		//}
		//// 右下
		//else if(transform.position != corner_position[2] && !Is_Up && Is_Right)
		//{
		//	transform.position = Vector3.Lerp(transform.position, corner_position[2], speed);
		//	if (transform.position == corner_position[2])
		//	{
		//		Is_Right = false;
		//	}

		//}
		//// 左下
		//else if(transform.position != corner_position[3] && !Is_Up && !Is_Right)
		//{
		//	transform.position = Vector3.Lerp(transform.position, corner_position[3], speed);
		//	if (transform.position == corner_position[3])
		//	{
		//		Is_Up = true;
		//	}
		//}

		if (Game_Master.MY.Frame_Count % Shot_DelayMax == 0)
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				Object_Instantiation.Object_Reboot("Enemy_Bullet_01", Shot_Mazle[i].transform.position, Shot_Mazle[i].transform.right);
			}
		}
	}
}
