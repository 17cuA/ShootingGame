using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Two_Boss : character_status
{
	[SerializeField] private GameObject core;
	private Two_Boss_Parts Core { get; set; }

	// Start is called before the first frame update
	private new void Start()
	{
		Core = core.GetComponent<Two_Boss_Parts>();
		base.Start();
	}

	// Update is called once per frame
	private new void Update()
	{
		if (Core.hp < 1)
		{
			base.Died_Judgment();
			base.Died_Process();
		}
	}
}
