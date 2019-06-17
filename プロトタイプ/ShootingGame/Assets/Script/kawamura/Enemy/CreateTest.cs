using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTest : MonoBehaviour
{
	Quaternion rota = Quaternion.Euler(0, 0, 30.0f);
	public GameObject enemyPrefab;
	GameObject save;
	Enemy_First ef;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
		{
			save = Instantiate(enemyPrefab, this.transform.position, rota);
			ef = save.GetComponent<Enemy_First>();
			ef.SetState(1);

			save = null;
			ef = null;
		}
    }
}
