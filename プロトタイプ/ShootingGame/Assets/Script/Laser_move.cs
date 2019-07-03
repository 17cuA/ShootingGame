using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_move : MonoBehaviour {
    
    private float DivNum = 40;//分割数
    private float Counter = 0f;//レーザー進行フレーム用
    private float u = 0f;//ベジェ曲線位置用
    private float P0x = 0f;
    private float P0y = 0f;
    private float P0z = 0f;

    private float P1x = 0f;
    private float P1y = 0f;
    private float P1z = 0f;

    private float P2x = 0f;
    private float P2y = 0f;
    private float P2z = 0f;


    private float P3x = 0f;
    private float P3y = 0f;
    private float P3z = 0f;


    private float P01x = 0f;
    private float P01y = 0f;
    private float P01z = 0f;

    private float P12x = 0f;
    private float P12y = 0f;
    private float P12z = 0f;

    private float P23x = 0f;
    private float P23y = 0f;
    private float P23z = 0f;

    private float P02x = 0f;
    private float P02y = 0f;
    private float P02z = 0f;


    private float P13x = 0f;
    private float P13y = 0f;
    private float P13z = 0f;

    private float P03x = 0f;
    private float P03y = 0f;
    private float P03z = 0f;

    public void Create_P3xyz(float x, float y, float z)//目標座標生成
    {
        P3x = x;
        P3y = y;
        P3z = z;
    }
 
    void Start()
    {
        transform.rotation = Quaternion.Euler(90, 0, 0);//画像回転

        P0x = 0f;//ベジェ曲線発生位置x
        P0z = 0f;
        P0y = 0f;


        P1x = Random.Range(-13.0f, 13.0f);//ベジェ曲線下側の目標座標x
        P1y = Random.Range(-13.0f, 13.0f);
        P1z = Random.Range(-13.0f, 13.0f);


        P2x = Random.Range(-13.0f, 13.0f);//ベジェ曲線真ん中付近の目標座標x
        P2y = Random.Range(-13.0f, 13.0f);
        P2z = Random.Range(-13.0f, 13.0f);

        Create_P3xyz(Laser_Create.x, Laser_Create.y, Laser_Create.z);//目標座標（クリックした位置）

    }

    void Update()
    {
        u = (1.0f / DivNum) * Counter;//ベジェ曲線の位置を移動させる

        P01x = (1.0f - u) * P0x + u * P1x;
        P01z = (1.0f - u) * P0z + u * P1z;
        P01y = (1.0f - u) * P0y + u * P1y;


        P12x = (1.0f - u) * P1x + u * P2x;
        P12z = (1.0f - u) * P1z + u * P2z;
        P12y = (1.0f - u) * P1y + u * P2y;

        P23x = (1.0f - u) * P2x + u * P3x;
        P23z = (1.0f - u) * P2z + u * P3z;
        P23y = (1.0f - u) * P2y + u * P3y;

        P02x = (1.0f - u) * P01x + u * P12x;
        P02z = (1.0f - u) * P01z + u * P12z;
        P02y = (1.0f - u) * P01y + u * P12y;

        P13x = (1.0f - u) * P12x + u * P23x;
        P13z = (1.0f - u) * P12z + u * P23z;
        P13y = (1.0f - u) * P12y + u * P23y;

        P03x = (1.0f - u) * P02x + u * P13x;
        P03z = (1.0f - u) * P02z + u * P13z;
        P03y = (1.0f - u) * P02y + u * P13y;

        Vector3 pos = transform.position;
        pos.x = P03x;
        pos.y = P03y;
        pos.z = P03z;

        transform.position = pos;//レーザー表示座標

        Counter += 0.5f;//レーザーの速さ；

       // if (transform.position.y > 11f || transform.position.y < -11f || transform.position.x > 10f || transform.position.x < -10f)
        if (u>=1.0f)
            {
            transform.DetachChildren();
            Destroy(gameObject);
        }
    }
}
