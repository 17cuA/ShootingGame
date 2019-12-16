using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManagement : MonoBehaviour
{
	private List<MeshRenderer> TerrainRenderer { get; set; }

	private void Awake()
	{
		TerrainRenderer = new List<MeshRenderer>();
		gaga(transform);
	}

	void Start()
    {
        
    }

    void Update()
    {
        foreach(MeshRenderer renderer in TerrainRenderer)
		{
			if(renderer.isVisible)
			{
				renderer.enabled = true;
				Debug.Log(renderer.name + " : true");
			}
			else if (!renderer.isVisible)
			{
				renderer.enabled = false;
				Debug.Log(renderer.name + " : false");
			}
		}
	}

	void gaga(Transform transform)
	{
		foreach (Transform temp in transform)
		{
			if (temp.childCount > 0)
			{
				gaga(temp);
			}
			else
			{
				MeshRenderer temp2 = temp.GetComponent<MeshRenderer>();
				if(temp2 != null)
				{
					TerrainRenderer.Add(temp2);
				}
			}
		}
	}
}
