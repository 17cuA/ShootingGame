using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BattleshipType_EF : MonoBehaviour
{
	public Vector3 pulus_Pos;
	public Vector3 plus_rota;
	public bool delete_flag;
	private Vector3 Initial_Position;
	private Vector3 Initial_Rota;
	private PlayableDirector PD;

	private void Start()
	{
		PD = GetComponent<PlayableDirector>();
		PD.playOnAwake = true;
		Initial_Position = transform.position;
		Initial_Rota = transform.eulerAngles;
	}

	void Update()
    {
		transform.position = Initial_Position + pulus_Pos;
		transform.rotation = Quaternion.Euler(Initial_Rota + plus_rota);

		if(delete_flag)
		{
			Destroy(gameObject);
		}
    }
}
