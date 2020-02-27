using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderMonitoring : MonoBehaviour
{
	public TerrainManagement.OriginalVector4 v4;

	void OnBecameVisible()
	{
		v4.set_inX(transform.position.x);
		v4.set_inY(transform.position.y);
	}
	void OnBecameInvisible()
	{
		v4.set_outX(transform.position.x);
		v4.set_outY(transform.position.y);
	}
}
