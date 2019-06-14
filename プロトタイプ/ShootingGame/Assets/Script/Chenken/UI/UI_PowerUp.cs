using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PowerUp : MonoBehaviour
{
	private Dictionary<int, Text> texts = new Dictionary<int, Text>();
	private Image current;
	private int slot;
	private void Awake()
	{
		Component[] components = transform.GetComponentsInChildren<Text>();

		for(var i = 0; i < components.Length; ++i)
		{
			texts.Add(i, components[i] as Text);
		}

		current = transform.Find("Image").GetComponent<Image>();
	}
	// Start is called before the first frame update
	private void Start()
    {
		var index = 0;
		foreach(var value in texts.Values)
		{
			value.text = PowerUpManager.Instance.GetPowerUp((PowerUpType)index).Name;
			value.transform.parent.name = PowerUpManager.Instance.GetPowerUp((PowerUpType)index).Name;
			index++;
		}
		current.name = "Cursor";
    }

    // Update is called once per frame
    void Update()
    {		
		if (PowerUpManager.Instance.IsSelect)
		{
			if (!current.gameObject.activeSelf)
				current.gameObject.SetActive(true);

			slot = PowerUpManager.Instance.CurrentSlot;

			if(current.transform.position != texts[slot].transform.position)
				current.transform.position = texts[slot].transform.position;
		
		}
		else
		{
			if(current.gameObject.activeSelf)
				current.gameObject.SetActive(false);
		}

		if (PowerUpManager.Instance.CurrentPowerUp != null)
		{
			if (PowerUpManager.Instance.CurrentPowerUp.CannotUpgrade)
			{
				if (texts[slot].text != string.Empty)
					texts[slot].text = string.Empty;
			}
		}
	}
}
