using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_star_Fish : character_status
{
    Vector3 playerPos;      
    Vector3 firstPos;
    [SerializeField] float speed;   //
    float arrivalTime;      //到着時間
   public  Player1 P1;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        firstPos = transform.position;
        arrivalTime = Vector3.Distance(firstPos, playerPos) / speed;
        ///StartCoroutine(Move());
    }

    // Update is called once per frame
    new void Update()
    {
        if(P1 == null)
        {
             P1 = Obj_Storage.Storage_Data.GetPlayer().GetComponent<Player1>();
             playerPos = P1.transform.position;
             firstPos = transform.position;
        }

        base.Update();
    }

    //IEnumerator Move()
    //{
    //    float time = 0;
    //    while (true)
    //    {
    //        time += Time.deltaTime;
    //        float percentage = time / arrivalTime;
    //        transform.position = firstPos * (1 - percentage) + playerPos * percentage;
    //        if ()
    //            yield break;
    //        yield return null;
    //    }
    //}
    	private void OnTriggerExit(Collider col)
	{
                if (col.gameObject.name == "WallLeft" || col.gameObject.name == "WallTop" || col.gameObject.name == "WallUnder" || col.gameObject.name=="WallRight")
                {
                    gameObject.SetActive(false);
                }
	}
	void calcPos()
	{

	}

}
