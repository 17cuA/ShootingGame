using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateItem : MonoBehaviour
{
    public GameObject item;

	public int childNum;
    public int remainingEnemiesCnt;

    public Transform itemTransform;
    public Vector3 itemPos;

    public bool isDead = false;
    void Start()
    {
        item = Resources.Load("Item/Item_Test") as GameObject;
        childNum = transform.childCount;
        remainingEnemiesCnt = childNum;
    }

    void Update()
    {
        if (remainingEnemiesCnt == 0)
        {
            isDead = true;
            gameObject.SetActive(false);

            //Died_Process();
        }
    }
    private void OnDisable()
    {
        if(isDead)
        {
            //Instantiate(item, itemTransform.position, transform.rotation);
            //Instantiate(item, itemPos, this.transform.rotation);

            itemPos = new Vector3(0, 0, 0);
            itemTransform = null;
            remainingEnemiesCnt = childNum;

            isDead = false;
        }

    }
}
