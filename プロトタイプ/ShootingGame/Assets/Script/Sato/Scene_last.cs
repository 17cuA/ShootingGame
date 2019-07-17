using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextDisplay;
using UnityEngine.SceneManagement;

public class Scene_last : MonoBehaviour
{
	private Character_Display game_clear;
	private GameObject game_clear_parent;
	public Vector3 game_clear_pos;
	public float game_clear_size;

	private Character_Display please_push_button;
	private GameObject please_push_button_parent;
	public Vector3 please_push_button_pos;
	public float please_push_button_size;

	private int d;
	private Color mu;
	private Color ari;

	private void Start()
	{
		game_clear_parent = new GameObject();
		game_clear_parent.transform.parent = transform;
		game_clear = new Character_Display(10, "morooka/SS", game_clear_parent, game_clear_pos);
		game_clear.Character_Preference("GAME_CLEAR");
		game_clear.Size_Change(new Vector3(game_clear_size, game_clear_size, game_clear_size));

		please_push_button_parent = new GameObject();
		please_push_button_parent.transform.parent = transform;
		please_push_button = new Character_Display(16, "morooka/SS", please_push_button_parent, please_push_button_pos);
		please_push_button.Character_Preference("PRESS_ANY_BUTTON");
		please_push_button.Size_Change(new Vector3(please_push_button_size, please_push_button_size, please_push_button_size));

		mu = new Color(1.0f,1.0f,1.0f,0.0f);
		ari = new Color(1.0f, 1.0f, 1.0f, 1.0f);

	}

	void Update()
	{
		d++;
        if(d == 30)
		{
			please_push_button.Color_Change(mu);
		}
		else if(d == 60)
		{
			please_push_button.Color_Change(ari);
			d = 0;
		}


		if (Input.GetKeyDown(KeyCode.L)|| Input.GetKeyDown("joystick button 0") || Input.GetButton("Fire1") || Input.GetKeyDown(KeyCode.Space))
		{
			Scene_Manager.Manager.Screen_Transition_To_Title();
		}
	}
}