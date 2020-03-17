using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class Enemy_star_Fish : character_status
{

	public int TargetNumber = 0;		//１Pか２Pを狙うかのチェック用	1が１P、2が２P

    GameObject item;
	public GameObject playerPos;
	public Vector3 firstPos;
	public int num = 0;
	private Vector3 vector;		//単位ベクトルを入れる
    public bool haveItem = false;
	//死亡時の弾発射に使用
	Quaternion deadAttackRotation;
	float rotaZ;

	private void OnEnable()
	{

	}
	// Start is called before the first frame update
	new void Start()
	{
        if (gameObject.GetComponent<DropItem>())
        {
            DropItem dItem = gameObject.GetComponent<DropItem>();
            haveItem = true;
        }
        item = Resources.Load("Item/Item_Test") as GameObject;
        base.Start();
	}
	// Update is called once per frame
	new void Update()
	{
		//おそらく単位ベクトルの計算
		if (vector == new Vector3(0, 0, 0) || playerPos != null) vector = calcPos();     //単位ベクトルの取得 

		transform.position -= vector * speed;
		if(hp < 1)
		{
            if (haveItem)
            {
                Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePOWERUP_ITEM, this.transform.position, Quaternion.identity);
            }
			if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().ToString() == "Stage_02")
			{
				DeadAttack();

			}
			Reset_Value();
			base.Died_Process();
		}
		base.Update();
	}

	private void OnTriggerExit(Collider col)
	{
		if (col.gameObject.name == "WallLeft" || col.gameObject.name == "WallTop" || col.gameObject.name == "WallUnder" || col.gameObject.name == "WallRight")
		{
			gameObject.SetActive(false);
		}
	}
	//単位ベクトル計算用
	Vector3 calcPos()
	{
		if (TargetNumber == 1)

		{
			playerPos = Obj_Storage.Storage_Data.GetPlayer();
		}
		else if (TargetNumber == 2)
		{
			playerPos = Obj_Storage.Storage_Data.GetPlayer2();
		}

		Vector3 PlayerpositionData = playerPos.transform.position;
		PlayerpositionData.z = 0;
		Vector3 pos = PlayerpositionData - firstPos;
		Debug.Log("単位ベクトル：" + pos);

		//pos.z = 0;
		return pos.normalized;
	}
	/// <summary>
	/// 死んだときの反撃
	/// </summary>
	void DeadAttack()
	{
		for (int i = 0; i < 6; i++)
		{
			deadAttackRotation = Quaternion.Euler(0, 0, rotaZ);
			Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, transform.position, deadAttackRotation);
			rotaZ += 60;
		}
		rotaZ = 0;
	}

	public void Attack_Target_Decision(int number)
	{
		num = number;
	}

	/// <summary>
	/// 各数値の初期化
	/// </summary>
	private void Reset_Value()
	{
		vector = Vector3.zero;
		playerPos = null;
		TargetNumber = 0;
		Debug.Log("呼ばれたよ");
	}
}
