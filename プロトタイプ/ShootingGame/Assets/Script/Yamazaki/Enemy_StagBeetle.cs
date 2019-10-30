// 作成者：山嵜ジョニー
// クワガタの挙動
// 2019/10/24
using UnityEngine;

public class Enemy_StagBeetle : character_status
{
	public enum State
	{
		NONE,
		STRAIGHT,
		SINMOVE,
		SINMOVETARGET,
		ATTACK,
	}
	
	public State eState = State.NONE;
	public float speedMax = 0.0f;
	public int speedUpframeMax = 60;
	public float addSpeed = 0.0f;
	public bool isSpeedUp = false;
	public int sinAngleFrame;
	public int sinAngleFrameMax = 180;
	public float yRange = 8.0f;
	public character_status playerObj; // プレイヤー

	// Start is called before the first frame update
	private new void Start()
    {
		speedMax = speed;
		eState = State.STRAIGHT;
		sinAngleFrame = 0;
		base.Start();
	}

    // Update is called once per frame
    private new void Update()
    {
		// プレイヤー格納
		TargetPlayer();

		switch (eState)
		{
			case State.STRAIGHT:
				transform.position += new Vector3(-speed / 60.0f, 0.0f, 0.0f);
				if (Vector3.Distance(playerObj.transform.position, transform.position) <= 16.5f)
				{
					eState = State.SINMOVE;
				}
				break;

			case State.SINMOVE:
				transform.position = new Vector3(transform.position.x + -speed / 120.0f, Mathf.Sin(((float)sinAngleFrame / (float)sinAngleFrameMax) * Mathf.PI * 2.0f) * yRange / 2.0f, 0.0f);
				sinAngleFrame++;
				if (sinAngleFrame / sinAngleFrameMax >= 1.0f)
				{
					eState = State.SINMOVETARGET;
				}
				break;

			case State.SINMOVETARGET:
				transform.position = new Vector3(transform.position.x + -speed / 120.0f, Mathf.Sin(((float)sinAngleFrame / (float)sinAngleFrameMax) * Mathf.PI * 2.0f) * yRange / 2.0f, 0.0f);
				sinAngleFrame++;
				if (Mathf.Abs(playerObj.transform.position.y - transform.position.y) <= 1.0f)
				{
					eState = State.ATTACK;
					speed = -speed / 2.0f;
					addSpeed = (speedMax - speed) / speedUpframeMax;
					isSpeedUp = true;
				}
				break;

			case State.ATTACK:
				transform.position += new Vector3(-speed / 12.0f, 0.0f, 0.0f);
				if (isSpeedUp)
				{
					speed += addSpeed;
					if (speed >= speedMax)
					{
						speed = speedMax;
						isSpeedUp = false;
					}
				}
				break;

			default:
				break;
		}

		HSV_Change();
		if (hp < 1)
		{
			Died_Process();
		}
		if (transform.localPosition.x < -35)
		{
			Destroy(this.gameObject);
		}
		base.Update();
	}

	private void TargetPlayer()
	{
		if (playerObj == null)
		{
			if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eONE_PLAYER)
			{
				playerObj = Obj_Storage.Storage_Data.GetPlayer().GetComponent<character_status>();
			}
			else if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
			{
				if (Vector3.Distance(Obj_Storage.Storage_Data.GetPlayer().transform.position, transform.position) < Vector3.Distance(Obj_Storage.Storage_Data.GetPlayer2().transform.position, transform.position))
				{
					playerObj = Obj_Storage.Storage_Data.GetPlayer().GetComponent<character_status>();
				}
				else
				{
					playerObj = Obj_Storage.Storage_Data.GetPlayer2().GetComponent<character_status>();
				}
			}
		}
		else if (playerObj != null)
		{
			// 片方のオブジェクトが消えてるとき
			if (playerObj.Remaining == 0)
			{
				// 1P のとき
				if (Obj_Storage.Storage_Data.GetPlayer().GetComponent<character_status>() == playerObj)
				{
					playerObj = playerObj = Obj_Storage.Storage_Data.GetPlayer2().GetComponent<character_status>();
				}
				// 2Pのとき
				if (Obj_Storage.Storage_Data.GetPlayer2().GetComponent<character_status>() == playerObj)
				{
					playerObj = playerObj = Obj_Storage.Storage_Data.GetPlayer().GetComponent<character_status>();
				}
			}
		}
	}
}
