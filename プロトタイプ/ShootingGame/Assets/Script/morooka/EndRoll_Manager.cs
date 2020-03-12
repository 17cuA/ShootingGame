using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TextDisplay;

public class EndRoll_Manager : MonoBehaviour
{
	[SerializeField] private EndRoll_StringSet[] texts;
	[SerializeField] private GameObject titleObject;
	[SerializeField] private GameObject menberObject;
	private Character_Display endRoll_title;
	private Character_Display endRoll_menber;

	private float fluctuationValue;

	private void Start()
	{
		endRoll_title = new Character_Display(0, "morooka/SR", titleObject, titleObject.transform.localPosition);
		endRoll_menber = new Character_Display(0, "morooka/SS", menberObject, menberObject.transform.localPosition);

		Vector3 size = new Vector3(0.5f, 0.5f, 0.5f);
		endRoll_title.Size_Change(size);
		endRoll_menber.Size_Change(size);

		endRoll_title.Color_Change(new Color(1.0f, 1.0f, 1.0f,0.0f));
		endRoll_menber.Color_Change(new Color(1.0f, 1.0f, 1.0f, 0.0f));

		fluctuationValue = 1.0f / 60.0f;
	}
	private void Update()
	{
		//if (step == 0)
		//{
		//	if(endRoll_title.Font_Color.a  < 1.0f)
		//	{
		//		Color c = endRoll_title.Font_Color;
		//		c.a += fluctuation;
		//		endRoll_title.Color_Change(c);
		//		endRoll_menber.Color_Change(c);
		//	}
		//	else
		//	{
		//		step++;
		//	}
		//}
		//else if(step == 1)
		//{
		//	disTime += Time.deltaTime;
		//	if(disTime >= 3.0f)
		//	{
		//		step++;
		//		disTime = 0.0f;
		//	}
		//}
		//else if( step == 2)
		//{
		//	if (endRoll_title.Font_Color.a > 0.0f)
		//	{
		//		Color c = endRoll_title.Font_Color;
		//		c.a -= fluctuation;
		//		endRoll_title.Color_Change(c);
		//		endRoll_menber.Color_Change(c);
		//	}
		//	else
		//	{
		//		index++;
		//		if(index < texts.Length)
		//		{
		//			endRoll_title.CharacterSwitching(texts[index].title);
		//			endRoll_menber.CharacterSwitching(cc(texts[index].member));
		//		}
		//		step++;
		//	}
		//}
		//else if(step == 3)
		//{
		//	disTime += Time.deltaTime;
		//	if(disTime >= 0.5f)
		//	{
		//		step = 0;
		//		disTime = 0.0f;
		//	}
		//}
	}

	public void CharacterSwitching(int i)
	{
		endRoll_title.CharacterSwitching(texts[i].title);
		endRoll_menber.CharacterSwitching(cc(texts[i].member));
	}

	public void CharacterStuffing(string s)
	{
		if(s == "L" || s == "l")
		{
			endRoll_title.LeftAlign();
			endRoll_menber.LeftAlign();
		}
		else if(s == "R" || s == "r")
		{
			endRoll_title.RightAlign();
			endRoll_menber.RightAlign();
		}
		else if(s=="C"|| s == "c")
		{
			endRoll_title.Centering();
			endRoll_menber.Centering();
		}
	}

	public void Alfa_Processing(int a)
	{
		if(a < 0)
		{
			StartCoroutine("reduce_alpha");
		}
		else if(a > 0)
		{
			StartCoroutine("increase_alpha");
		}
	}

	public IEnumerator increase_alpha()
	{
		for(int i = 0; i < 60; i++)
		{
			Color color = endRoll_title.Font_Color;
			color.a += fluctuationValue;
			endRoll_title.Color_Change(color);
			endRoll_menber.Color_Change(color);

			yield return null;
		}
	}
	public IEnumerator reduce_alpha()
	{
		for(int i = 0; i < 60; i++)
		{
			Color color = endRoll_title.Font_Color;
			color.a -= fluctuationValue;
			endRoll_title.Color_Change(color);
			endRoll_menber.Color_Change(color);

			yield return null;
		}
	}

	public string cc(string[] members)
	{
		string temp = members[0];
		for(int i = 1; i < members.Length; i++)
		{
			temp += '\n' + members[i];
		}

		return temp;
	}

}

[System.Serializable]
public class EndRoll_StringSet
{
	public enum ParagraphJustification
	{
		eRightJustified,
		eLeftJustified,
	}
	public string title;
	public string[] member;
	public ParagraphJustification paragraph;
}
