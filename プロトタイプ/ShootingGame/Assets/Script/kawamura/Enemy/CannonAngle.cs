using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonAngle : MonoBehaviour
{
    public GameObject playerObj;   //プレイヤー（向く対象）情報を入れる変数
    Vector3 playerPos;      //プレイヤー（向く対象）の座標を入れる変数
    Vector3 myPos;     //自分の座標を入れる変数
    Vector3 dif;            //対象と自分の座標の差を入れる変数



    public bool imasuyo = false;
    public bool isPlayerActive = true;
    float radian;                   //ラジアン
    public float degree;            //角度
    public float positiveDegree;    //正の数で表した角度	
    public float standardDegree;    //最初の向き（これを基準に左右90度まで回転）
    public float standardDig_high;  //最初の向きから90足した数
    public float standardDig_low;   //最初の向きから90引いた数

    private void Awake()
    {
        standardDegree = transform.rotation.z;

        standardDig_high = standardDegree + 90.0f;
        //角度を直す
        if (standardDig_high > 360)
        {
            standardDig_high -= 360.0f;
        }

        standardDig_low = standardDegree - 90.0f;
        //角度を直す
        if (standardDig_low < 0)
        {
            standardDig_low += 360.0f;
        }
    }
    void Start()
    {

    }

    void Update()
    {
        //プレイヤー（向く対象）情報が入っていないなら入れる
        if (playerObj == null && isPlayerActive)
        {
            playerObj = GameObject.FindGameObjectWithTag("Player");
        }

        if (playerObj)
        {
            if (isPlayerActive)
            {
                if (playerObj.activeInHierarchy)
                {
                    //imasuyo = true;
                }
                else
                {
                    playerObj = null;
                    isPlayerActive = false;
                }

            }
        }
        //プレイヤー（向く対象）の座標を入れる
        if (playerObj)
        {
            playerPos = playerObj.transform.position;
        }
        //自分の座標を入れる
        myPos = this.transform.position;

        //角度計算の関数呼び出し
        DegreeCalculation();

        if (positiveDegree < 0)
        {
            positiveDegree += 360.0f;
        }

        //if(degree>0)
        //{
        //	midBossDig = degree;
        //}
        //else if(degree<=0)
        //{
        //	midBossDig = degree;
        //}

        //自分を対象の方向へ向かせる
        this.transform.rotation = Quaternion.Euler(0, 0, positiveDegree);

    }

    //角度を求める関数
    void DegreeCalculation()
    {
        //座標の差を入れる
        dif = playerPos - myPos;

        //ラジアンを求める
        radian = Mathf.Atan2(dif.y, dif.x);

        //角度を求める
        degree = radian * Mathf.Rad2Deg;
        positiveDegree = degree;
    }
}
