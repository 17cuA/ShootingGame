using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Board_Parent : MonoBehaviour
{
	public GameObject quarterObj;

	public GameObject saveQuaterObj;

	public Quaternion randRota_TopLeft;
	public Quaternion randRota_TopRight;
	public Quaternion randRota_BottomLeft;
	public Quaternion randRota_BottomRight;

	public Enemy_Board_Parent ebp;



	Vector3 velocity;
	public int divisionCnt = 0;
	public float speedX;

	string myName;

	float rotaZ;

	public bool isDead = false;
	public bool isCreate = false;
	private void Awake()
	{
		myName = gameObject.name;
		//speedX = 20;
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
			//randRota_TopLeft = new Quaternion(0, 0, Random.Range(0, 360), 0);
			//Instantiate(quarterObj, transform.position, randRota_TopLeft);
			//randRota_TopLeft = new Quaternion(0, 0, Random.Range(0, 360), 0);
			//Instantiate(quarterObj, transform.position, randRota_TopRight);
			//randRota_TopLeft = new Quaternion(0, 0, Random.Range(0, 360), 0);
			//Instantiate(quarterObj, transform.position, randRota_BottomLeft);
			//randRota_TopLeft = new Quaternion(0, 0, Random.Range(0, 360), 0);
			//Instantiate(quarterObj, transform.position, randRota_BottomRight);

			//右上に生成
			saveQuaterObj = Instantiate(quarterObj, transform.position, transform.rotation);
			ebp = saveQuaterObj.GetComponent<Enemy_Board_Parent>();
			saveQuaterObj.transform.rotation = Quaternion.Euler(0,0, Random.Range(180, 270));
			ebp.divisionCnt = 1;
			ebp.isCreate = true;
			ebp = null;
			if (divisionCnt == 0)
			{
				saveQuaterObj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
			}
			else if(divisionCnt == 1)
			{
				saveQuaterObj.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
			}
			saveQuaterObj = null;

			//左上に生成
			saveQuaterObj = Instantiate(quarterObj, transform.position, transform.rotation);
			ebp = saveQuaterObj.GetComponent<Enemy_Board_Parent>();
			ebp.divisionCnt = 1;
			ebp.isCreate = true;
			ebp = null;
			saveQuaterObj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(270, 360));
			if (divisionCnt == 0)
			{
				saveQuaterObj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
			}
			else if (divisionCnt == 1)
			{
				saveQuaterObj.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
			}
			saveQuaterObj = null;

			//左下に生成
			saveQuaterObj = Instantiate(quarterObj, transform.position, transform.rotation);
			ebp = saveQuaterObj.GetComponent<Enemy_Board_Parent>();
			ebp.divisionCnt = 1;
			ebp.isCreate = true;
			ebp = null;
			saveQuaterObj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 90));
			if (divisionCnt == 0)
			{
				saveQuaterObj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
			}
			else if (divisionCnt == 1)
			{
				saveQuaterObj.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
			}
			saveQuaterObj = null;

			//右下に生成
			saveQuaterObj = Instantiate(quarterObj, transform.position, transform.rotation);
			ebp = saveQuaterObj.GetComponent<Enemy_Board_Parent>();
			ebp.divisionCnt = 1;
			ebp.isCreate = true;
			ebp = null;
			saveQuaterObj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(90, 180));
			if (divisionCnt == 0)
			{
				saveQuaterObj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
			}
			else if (divisionCnt == 1)
			{
				saveQuaterObj.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
			}
			saveQuaterObj = null;

			isDead = false;

			gameObject.SetActive(false);
		}
    }
}
