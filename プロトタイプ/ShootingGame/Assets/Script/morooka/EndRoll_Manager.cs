using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TextDisplay;

public class EndRoll_Manager : MonoBehaviour
{
	[SerializeField] private EndRoll_StringSet[] texts;
	private int index;
	private int step;
	private float fluctuation;
	private Character_Display endRoll;

	private float disTime;

	private void Start()
	{
		fluctuation = 1.0f / 60.0f;
		disTime = 0.0f;
		endRoll = new Character_Display(cc(texts[0].title,texts[0].member), "morooka/SS",gameObject,transform.localPosition);
	}
	private void Update()
	{
		if (step == 0)
		{
			if(endRoll.Font_Color.a  < 1.0f)
			{
				Color c = endRoll.Font_Color;
				c.a += fluctuation;
				endRoll.Color_Change(c);
			}
			else
			{
				step++;
			}
		}
		else if(step == 1)
		{
			disTime += Time.deltaTime;
			if(disTime >= 3.0f)
			{
				step++;
				disTime = 0.0f;
			}
		}
		else if( step == 2)
		{
			if (endRoll.Font_Color.a > 0.0f)
			{
				Color c = endRoll.Font_Color;
				c.a -= fluctuation;
				endRoll.Color_Change(c);
			}
			else
			{
				index++;
				if(index < texts.Length)
				{
					endRoll.CharacterSwitching(cc(texts[index].title, texts[index].member));
				}
				step++;
			}
		}
		else if(step == 3)
		{
			disTime += Time.deltaTime;
			if(disTime >= 0.5f)
			{
				step = 0;
				disTime = 0.0f;
			}
		}
	}

	string cc(string title,string[] members)
	{
		string temp = title + '\n';
		foreach(var mem in members)
		{
			temp +=  mem + '\n';
		}
		return temp;
	}
}

[System.Serializable]
public class EndRoll_StringSet
{
	public string title;
	public string[] member;
}
