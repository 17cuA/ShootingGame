//作成日2019/07/09
// UFO母艦型エネミーの挙動
// 作成者:諸岡勇樹
/*
 * 2019/07/09　UFO型エネミーの放出
 */

using UnityEngine;
using StorageReference;

public class UfoMotherType_Enemy : character_status
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
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eUFOTYPE_ENEMY, Shot_Mazle[i].transform.position, Shot_Mazle[i].transform.right);
			}
		}
	}
}
