using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextDisplay;
using UnityEngine.SceneManagement;

public class Scene_last : MonoBehaviour
{
	[SerializeField,Header("表示文字")]public string string_to_display;

	private Character_Display game_last;
	private GameObject game_last_parent;
	public Vector3 game_last_pos;
	public float game_last_size;

	private Character_Display score;
	private GameObject score_parent;
	public Vector3 score_pos;
	public float score_size;

	private Character_Display please_push_button;
	private GameObject please_push_button_parent;
	public Vector3 please_push_button_pos;
	public float please_push_button_size;

	private int d;
	private Color mu;
	private Color ari;

	private void Start()
	{
		game_last_parent = new GameObject();
		game_last_parent.transform.parent = transform;
		game_last = new Character_Display(string_to_display.Length, "morooka/SS", game_last_parent, game_last_pos);
		game_last.Character_Preference(string_to_display);
		game_last.Size_Change(new Vector3(game_last_size, game_last_size, game_last_size));

		score_parent = new GameObject();
		score_parent.transform.parent = transform;
		score = new Character_Display(10, "morooka/SS", score_parent, score_pos);
		score.Character_Preference(Game_Master.MY.display_score.ToString("D10"));
		score.Size_Change(new Vector3(score_size, score_size, score_size));

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