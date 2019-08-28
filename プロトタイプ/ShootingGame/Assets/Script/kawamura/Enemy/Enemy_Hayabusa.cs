//作成者：川村良太
//ハヤブサエネミーの挙動

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Hayabusa : character_status
{
    public enum Status
    {
        Up,
        Down,
    }

    public Status eStatus;

    Vector3 velocity;

    public float speedX;
    public float speedY;
    public float speedZ;

    public float rotaX;
    public float saveRotaX;
    public float rotaX_ChangeValue;
    public float turnDelayCnt;
    public float turnDelayMax;         //曲がるときにまっすぐ進む間隔

    public bool isFirstTurn;
    public bool isTurn;
    public bool isDelayStart;
    new void Start()
    {
        isFirstTurn = true;
        rotaX = transform.eulerAngles.x;
        saveRotaX = rotaX;

        HP_Setting();
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        velocity = gameObject.transform.rotation * new Vector3(0, 0, -speedZ);
        gameObject.transform.position += velocity * Time.deltaTime;

        if(isFirstTurn)
        {
            if (transform.position.x < -15)
            {
                isFirstTurn = false;
                isTurn = true;
            }
        }

        if(isTurn)
        {
            Turn_RightAngle();
        }

        if(isDelayStart)
        {
            if (turnDelayCnt >= turnDelayMax)
            {
                isTurn = true;
            }
            else
            {
                turnDelayCnt++;
            }
        }
    }

    //曲がる方向と曲がる間隔(90度回転した後まっすぐ進む間隔)の設定
    public void SetStatus(Status setStatus, float set)
    {
        eStatus = setStatus;
        turnDelayMax = set;
    }

    //90度曲がる(自分の向きを変える)関数
    void Turn_RightAngle()
    {
        switch (eStatus)
        {
            case Status.Up:
                rotaX -= rotaX_ChangeValue;
                if (rotaX < saveRotaX - 90)
                {
                    saveRotaX -= 90;
                    rotaX = saveRotaX;
                    isTurn = false;
                    isDelayStart = true;
                }
                transform.rotation = Quaternion.Euler(rotaX, -90, 90);

                break;

            case Status.Down:
                rotaX += rotaX_ChangeValue;
                if (rotaX < saveRotaX + 90)
                {
                    saveRotaX += 90;
                    rotaX = saveRotaX;
                    isTurn = false;
                    isDelayStart = true;
                }
                transform.rotation = Quaternion.Euler(rotaX, transform.eulerAngles.y, transform.eulerAngles.z);
                break;
        }
    }
}
