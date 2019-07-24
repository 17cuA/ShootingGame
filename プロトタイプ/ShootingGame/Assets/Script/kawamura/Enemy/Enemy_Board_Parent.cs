using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Board_Parent : MonoBehaviour
{
	public GameObject quarterObj;					//4分の1オブジェクト
	public GameObject quarter_OneSixteenthObj;		//16分の1オブジェクト

	public GameObject saveQuaterObj;

	public Quaternion randRota_TopLeft;
	public Quaternion randRota_TopRight;
	public Quaternion randRota_BottomLeft;
	public Quaternion randRota_BottomRight;

	public Enemy_Board_Parent ebp;



	Vector3 velocity;
	public int divisionCnt = 0;
	public float speedX;

	public string myName;

	float rotaZ;

	public bool isDead = false;
	public bool isCreate = false;
	private void Awake()
	{
		myName = gameObject.name;
		//speedX = 15;
	}
	void Start()
    {
        
    }

    void Update()
    {
		if(isCreate)
		{
			velocity = gameObject.transform.rotation * new Vector3(-speedX, 0, 0);
			gameObject.transform.position += velocity * Time.deltaTime;
			speedX -= 1.0f;
			if (speedX < 0)
			{
				speedX = 0;
				isCreate = false;
			}


		}
		randRota_TopLeft = new Quaternion(0, 0, Random.Range(180, 270), 0);
		randRota_TopRight = new Quaternion(0, 0, Random.Range(270, 360), 0);
		randRota_BottomLeft = new Quaternion(0, 0, Random.Range(0, 90), 0);
		randRota_BottomRight = new Quaternion(0, 0, Random.Range(90, 180), 0);
		if (isDead)
		{
			for (int i = 0; i < 4; i++)
			{
				switch(i)
				{
					case 0:
						if (myName == "Enemy_Board")
						{
							saveQuaterObj = Instantiate(quarterObj, transform.position, transform.rotation);
							ebp = saveQuaterObj.GetComponent<Enemy_Board_Parent>();
							saveQuaterObj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(180, 270));
							//ebp.divisionCnt = 1;
							ebp.isCreate = true;
							ebp.speedX = 15;
							ebp = null;
							saveQuaterObj = null;

						}
						else if (myName == "Enemy_Board_Quarter(Clone)")
						{
							saveQuaterObj = Instantiate(quarter_OneSixteenthObj, transform.position, transform.rotation);
							ebp = saveQuaterObj.GetComponent<Enemy_Board_Parent>();
							saveQuaterObj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(180, 270));
							//ebp.divisionCnt = 1;
							ebp.isCreate = true;
							ebp.speedX = 15;
							ebp = null;
							saveQuaterObj = null;
							//isDead = false;

							//gameObject.SetActive(false);

						}
						else if (myName == "Enemy_Board_OneSixteenth(Clone)")
						{
							//なにもしない
						}

						break;
					case 1:
						//左上に生成
						if (myName == "Enemy_Board")
						{
							saveQuaterObj = Instantiate(quarterObj, transform.position, transform.rotation);
							ebp = saveQuaterObj.GetComponent<Enemy_Board_Parent>();
							//ebp.divisionCnt = 1;
							ebp.isCreate = true;
							ebp.speedX = 15;
							ebp = null;
							saveQuaterObj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(270, 360));
							saveQuaterObj = null;

						}
						else if (myName == "Enemy_Board_Quarter(Clone)")
						{
							saveQuaterObj = Instantiate(quarter_OneSixteenthObj, transform.position, transform.rotation);
							ebp = saveQuaterObj.GetComponent<Enemy_Board_Parent>();
							//ebp.divisionCnt = 1;
							ebp.isCreate = true;
							ebp.speedX = 15;
							ebp = null;
							saveQuaterObj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(270, 360));
							saveQuaterObj = null;

						}
						else if (myName == "Enemy_Board_OneSixteenth(Clone)")
						{
							//なにもしない
						}

						break;
					case 2:
						//左下に生成
						if (myName == "Enemy_Board")
						{
							saveQuaterObj = Instantiate(quarterObj, transform.position, transform.rotation);
							ebp = saveQuaterObj.GetComponent<Enemy_Board_Parent>();
							//ebp.divisionCnt = 1;
							ebp.isCreate = true;
							ebp.speedX = 15;
							ebp = null;
							saveQuaterObj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 90));
							saveQuaterObj = null;

						}
						else if (myName == "Enemy_Board_Quarter(Clone)")
						{
							saveQuaterObj = Instantiate(quarter_OneSixteenthObj, transform.position, transform.rotation);
							ebp = saveQuaterObj.GetComponent<Enemy_Board_Parent>();
							//ebp.divisionCnt = 1;
							ebp.isCreate = true;
							ebp.speedX = 15;
							ebp = null;
							saveQuaterObj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 90));
							saveQuaterObj = null;
						}
						else if (myName == "Enemy_Board_OneSixteenth(Clone)")
						{
							//なにもしない
						}

						break;
					case 3:
						//右下に生成
						if (myName == "Enemy_Board")
						{
							saveQuaterObj = Instantiate(quarterObj, transform.position, transform.rotation);
							ebp = saveQuaterObj.GetComponent<Enemy_Board_Parent>();
							//ebp.divisionCnt = 1;
							ebp.isCreate = true;
							ebp.speedX = 15;
							ebp = null;
							saveQuaterObj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(90, 180));
							saveQuaterObj = null;

						}
						else if (myName == "Enemy_Board_Quarter(Clone)")
						{
							saveQuaterObj = Instantiate(quarter_OneSixteenthObj, transform.position, transform.rotation);
							ebp = saveQuaterObj.GetComponent<Enemy_Board_Parent>();
							//ebp.divisionCnt = 1;
							ebp.isCreate = true;
							ebp.speedX = 15;
							ebp = null;
							saveQuaterObj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(90, 180));
							saveQuaterObj = null;
						}
						else if (myName == "Enemy_Board_OneSixteenth(Clone)")
						{
							//なにもしない
						}
						//ebp = saveQuaterObj.GetComponent<Enemy_Board_Parent>();

						break;

				}

			}

			//for文じゃないほう
			//右上に生成
			//if(myName=="Enemy_Board")
			//{
			//	saveQuaterObj = Instantiate(quarterObj, transform.position, transform.rotation);
			//	ebp = saveQuaterObj.GetComponent<Enemy_Board_Parent>();
			//	saveQuaterObj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(180, 270));
			//	//ebp.divisionCnt = 1;
			//	ebp.isCreate = true;
			//	ebp = null;
			//	saveQuaterObj = null;

			//}
			//else if(myName== "Enemy_Board_Quarter(Clone)")
			//{
			//	saveQuaterObj = Instantiate(quarter_OneSixteenthObj, transform.position, transform.rotation);
			//	ebp = saveQuaterObj.GetComponent<Enemy_Board_Parent>();
			//	saveQuaterObj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(180, 270));
			//	//ebp.divisionCnt = 1;
			//	ebp.isCreate = true;
			//	ebp = null;
			//	saveQuaterObj = null;

			//}
			//else if (myName == "Enemy_Board_OneSixteenth(Clone)")
			//{
			//	//なにもしない
			//}
			////ebp = saveQuaterObj.GetComponent<Enemy_Board_Parent>();
			////saveQuaterObj.transform.rotation = Quaternion.Euler(0,0, Random.Range(180, 270));
			//////ebp.divisionCnt = 1;
			////ebp.isCreate = true;
			////ebp = null;
			////if (divisionCnt == 0)
			////{
			////	saveQuaterObj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
			////}
			////else if(divisionCnt == 1)
			////{
			////	saveQuaterObj.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
			////}
			////saveQuaterObj = null;

			////左上に生成
			//if (myName == "Enemy_Board")
			//{
			//	saveQuaterObj = Instantiate(quarterObj, transform.position, transform.rotation);
			//	ebp = saveQuaterObj.GetComponent<Enemy_Board_Parent>();
			//	//ebp.divisionCnt = 1;
			//	ebp.isCreate = true;
			//	ebp = null;
			//	saveQuaterObj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(270, 360));
			//	saveQuaterObj = null;

			//}
			//else if (myName == "Enemy_Board_Quarter(Clone)")
			//{
			//	saveQuaterObj = Instantiate(quarter_OneSixteenthObj, transform.position, transform.rotation);
			//	ebp = saveQuaterObj.GetComponent<Enemy_Board_Parent>();
			//	//ebp.divisionCnt = 1;
			//	ebp.isCreate = true;
			//	ebp = null;
			//	saveQuaterObj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(270, 360));
			//	saveQuaterObj = null;

			//}
			//else if(myName== "Enemy_Board_OneSixteenth(Clone)")
			//{
			//	//なにもしない
			//}
			////ebp = saveQuaterObj.GetComponent<Enemy_Board_Parent>();
			//////ebp.divisionCnt = 1;
			////ebp.isCreate = true;
			////ebp = null;
			////saveQuaterObj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(270, 360));
			////if (divisionCnt == 0)
			////{
			////	saveQuaterObj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
			////}
			////else if (divisionCnt == 1)
			////{
			////	saveQuaterObj.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
			////}
			////saveQuaterObj = null;

			////左下に生成
			//if (myName == "Enemy_Board")
			//{
			//	saveQuaterObj = Instantiate(quarterObj, transform.position, transform.rotation);
			//	ebp = saveQuaterObj.GetComponent<Enemy_Board_Parent>();
			//	//ebp.divisionCnt = 1;
			//	ebp.isCreate = true;
			//	ebp = null;
			//	saveQuaterObj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 90));
			//	saveQuaterObj = null;

			//}
			//else if (myName == "Enemy_Board_Quarter(Clone)")
			//{
			//	saveQuaterObj = Instantiate(quarter_OneSixteenthObj, transform.position, transform.rotation);
			//	ebp = saveQuaterObj.GetComponent<Enemy_Board_Parent>();
			//	//ebp.divisionCnt = 1;
			//	ebp.isCreate = true;
			//	ebp = null;
			//	saveQuaterObj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 90));
			//	saveQuaterObj = null;

			//}
			//else if (myName == "Enemy_Board_OneSixteenth(Clone)")
			//{
			//	//なにもしない
			//}
			////ebp = saveQuaterObj.GetComponent<Enemy_Board_Parent>();
			//////ebp.divisionCnt = 1;
			////ebp.isCreate = true;
			////ebp = null;
			////saveQuaterObj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 90));
			////if (divisionCnt == 0)
			////{
			////	saveQuaterObj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
			////}
			////else if (divisionCnt == 1)
			////{
			////	saveQuaterObj.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
			////}
			////saveQuaterObj = null;

			////右下に生成
			//if (myName == "Enemy_Board")
			//{
			//	saveQuaterObj = Instantiate(quarterObj, transform.position, transform.rotation);
			//	ebp = saveQuaterObj.GetComponent<Enemy_Board_Parent>();
			//	//ebp.divisionCnt = 1;
			//	ebp.isCreate = true;
			//	ebp = null;
			//	saveQuaterObj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(90, 180));
			//	saveQuaterObj = null;

			//}
			//else if (myName == "Enemy_Board_Quarter(Clone)")
			//{
			//	saveQuaterObj = Instantiate(quarter_OneSixteenthObj, transform.position, transform.rotation);
			//	ebp = saveQuaterObj.GetComponent<Enemy_Board_Parent>();
			//	//ebp.divisionCnt = 1;
			//	ebp.isCreate = true;
			//	ebp = null;
			//	saveQuaterObj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(90, 180));
			//	saveQuaterObj = null;

			//}
			//else if (myName == "Enemy_Board_OneSixteenth(Clone)")
			//{
			//	//なにもしない
			//}
			//ebp = saveQuaterObj.GetComponent<Enemy_Board_Parent>();
			////ebp.divisionCnt = 1;
			//ebp.isCreate = true;
			//ebp = null;
			//saveQuaterObj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(90, 180));
			//if (divisionCnt == 0)
			//{
			//	saveQuaterObj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
			//}
			//else if (divisionCnt == 1)
			//{
			//	saveQuaterObj.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
			//}
			//saveQuaterObj = null;

			isDead = false;

			gameObject.SetActive(false);
		}
    }
}
