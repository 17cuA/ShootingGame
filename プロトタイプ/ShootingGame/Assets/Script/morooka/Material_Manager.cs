using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Material_Manager : MonoBehaviour
{
	private Renderer[] renderers;
	private Material[] materials;

	private void OnEnable()
	{
		materials = Resources.LoadAll<Material>("morooka/Material");
		renderers = transform.GetComponentsInChildren<Renderer>(true);

		StartCoroutine("MaterialChange");
	}

	private void OnDisable()
	{
		materials = null;
		renderers = null;
	}

	IEnumerator MaterialChange()
	{
		foreach (Renderer r in renderers)
		{
			foreach (Material m in materials)
			{
				int m_p = 0;
				for(m_p = 0; m_p !=  r.materials.Length; m_p++)
				{
					if (r.materials[m_p].name == m.name + " (Instance)")
					{
						r.materials[m_p] = m;
						yield return null;
						break;
					}
				}
				yield return null;
			}
		}
		yield return null;
		this.enabled = false;
	}
}
