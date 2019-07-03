using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Create : MonoBehaviour {

    public GameObject Laser_prefab;//Laser_prefabをインスペクターに用意
    public static float x;//クリック位置x
    public static float y;//クリック位置y
    public static float z;//クリック位置z
   
    void Start () {
		
	}
	
	void Update () {

        if (Input.GetMouseButton(0))//マウスを左クリックした時
        {
            Vector3 touchScreenPosition = Input.mousePosition;//マウス座標を代入

            touchScreenPosition.z = 10.0f;//ScreenToWorldPointを使うための呪文

            Camera gameCamera = Camera.main;//カメラ取得
            Vector3 touchWorldPosition = gameCamera.ScreenToWorldPoint(touchScreenPosition);//マウス座標をワールド座標に変換
            x = touchWorldPosition.x;//クリック位置代入
            y = touchWorldPosition.y;
            z = touchWorldPosition.z;

            Debug.Log("LeftClick:" + touchWorldPosition);//取得座標表示
           
            Instantiate(Laser_prefab, new Vector3(0f, 0f, 0f), Quaternion.identity);//0,0,0にレーザー生成


        }
    }
}
