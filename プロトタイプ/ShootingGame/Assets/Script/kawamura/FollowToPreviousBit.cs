using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowToPreviousBit : MonoBehaviour
{
    GameObject playerObj;
    GameObject previousBitObj;

    public float speed;             //戻るときのスピード
    float step;                     //スピードを計算して入れる

    public float offsetX;
    public float offserY;
    //public float offsetZ;

    public string myName;

    bool isMove = true;
    void Start()
    {
        myName = gameObject.name;
        //if(myName=="FollowPosFirst")
        //{
        //    previousBitObj = null;
        //}
        if (myName == "FollowPosSecond")
        {
            previousBitObj = GameObject.Find("FollowPosFirst");
        }
        else if (myName == "FollowPosThird")
        {
            previousBitObj = GameObject.Find("FollowPosSecond");

        }
        else if (myName == "FollowPosFourth")
        {
            previousBitObj = GameObject.Find("FollowPosThird");

        }

    }

    void Update()
    {

        //if (playerObj == null)
        //{
        //    playerObj = GameObject.FindGameObjectWithTag("Player");

        //}
        //スピード計算
        step = speed * Time.deltaTime;

        //Vector3 pos = playerObj.transform.position;

        if ((transform.position.x < previousBitObj.transform.position.x + offsetX && transform.position.x > previousBitObj.transform.position.x - offsetX)
            && (transform.position.y < previousBitObj.transform.position.y + offserY && transform.position.y > previousBitObj.transform.position.y - offserY))
        {
            isMove = false;
        }
        else
        {
            isMove = true;
        }

        //if ((transform.position.x > playerObj.transform.position.x + 2.5 || transform.position.x < playerObj.transform.position.x - 2.5)
        //	&&(transform.position.y > playerObj.transform.position.y +1.5 || transform.position.y < playerObj.transform.position.y - 1.5))
        //{
        //	isMove = true;	
        //}
        //else
        //{
        //	isMove = false;
        //}

        if (isMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, previousBitObj.transform.position, step);
        }
        //transform.position = new Vector3(pos.x + offsetX, pos.y + offserY, pos.z);
    }
}
