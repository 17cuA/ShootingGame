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
	[Header("回転速度")]
	private float rotating_velocity;		// 回転速度
	private GameObject[] Shot_Mazle { get; set; }

	void Start()
    {
		Shot_Mazle = new GameObject[transform.childCount];
		for(int i = 0; i< transform.childCount;i++)
		{
			Shot_Mazle[i] = transform.GetChild(i).gameObject;
		}
	}

    void Update()
    {
		transform.Rotate(new Vector3(0.0f, 0.0f, rotating_velocity));

		if (Game_Master.MY.Frame_Count % Shot_DelayMax == 0)
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, Shot_Mazle[i].transform.position, Shot_Mazle[i].transform.right);
			}
		}
	}
}
