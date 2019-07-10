//作成日2019/07/09
// UFO母艦型エネミーの挙動
// 作成者:諸岡勇樹
/*
 * 2019/07/09　UFO型エネミーの放出
 * 2019/07/10　敵の各門6体ずつ放出にする
 * 2019/07/10　1/24でアイテムの敵生成（アイテムの敵がプーリングになし、現状空き）
 */

using UnityEngine;
using StorageReference;

public class UfoMotherType_Enemy : character_status
{
	[SerializeField]
	[Header("回転速度")]
	private float rotating_velocity;        // 回転速度
	[SerializeField]
	private Transform[] shot_mazle;
	[SerializeField, Header("攻撃のインターバル")]
	private int attack_interval;

	private int Sortie_Number { get; set; }
	private int Shot_Cnt { get; set; }
	private int Interval_Cnt { get; set; }

	void Start()
	{
		Sortie_Number = 6;
		Shot_Cnt = 0;
		Interval_Cnt = 0;
	}

	void Update()
	{
		transform.Rotate(new Vector3(0.0f, 0.0f, rotating_velocity));

		if (Shot_Cnt < Sortie_Number)
		{
			Shot_Delay++;
			if (Shot_Delay > Shot_DelayMax)
			{
				for (int i = 0; i < shot_mazle.Length; i++)
				{
					if (Random.Range(0, Sortie_Number * 4) == 0)
					{ }
					else
					{
						Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eUFOTYPE_ENEMY, shot_mazle[i].position, shot_mazle[i].right);
					}
				}

				Shot_Delay = 0;
				Shot_Cnt++;
			}
		}
		else if (Shot_Cnt == Sortie_Number)
		{
			Interval_Cnt++;

			if (Interval_Cnt > attack_interval)
			{
				Interval_Cnt = 0;
				Shot_Cnt = 0;
			}
		}
	}
}
