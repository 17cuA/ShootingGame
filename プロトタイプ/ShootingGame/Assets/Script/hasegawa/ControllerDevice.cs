/* 盗作 */
using System;
using System.Runtime.InteropServices;
using UnityEngine;

// --------------------------------------------
// GamePadのKeyCode指定用クラス
// --------------------------------------------
static class Code
{
	public const int pad_Dpad_Up = 0x0001;
	public const int pad_Dpad_Down = 0x0002;
	public const int pad_Dpad_Left = 0x0004;
	public const int pad_Dpad_Right = 0x0008;
	public const int pad_Start = 0x0010;
	public const int pad_Back = 0x0020;
	public const int pad_A = 0x1000;
	public const int pad_B = 0x2000;
	public const int pad_X = 0x4000;
	public const int pad_Y = 0x8000;
	public const int pad_LB = 0x0100;
	public const int pad_RB = 0x0200;
	public const int pad_LStick = 0x0040;
	public const int pad_RStick = 0x0080;
};
public enum eCode
{
	ePad_up = 0x0001,
	ePad_down = 0x0002,
	ePad_left = 0x0004,
	ePad_right = 0x0008,
	ePad_start = 0x0010,
	ePad_back = 0x0020,
	ePad_a = 0x1000,
	ePad_b = 0x2000,
	ePad_x = 0x4000,
	ePad_y = 0x8000,
	ePad_lb = 0x0100,
	ePad_rb = 0x0200,
	ePad_lStick = 0x0040,
	ePad_rStick = 0x0080,
	ePad_None = 0x0000,
}

public enum ePadNumber
{
	ePlayer1 = 0,
	ePlayer2 = 1,
	eNone,
}

public static class ControllerDevice
{
	[DllImport("DllXInput", EntryPoint = "InputJudg_Create")]
	private static extern IntPtr _Create();

	[DllImport("DllXInput", EntryPoint = "InputJudg_Destroy")]
	private static extern void _Destroy(IntPtr instance);

	[DllImport("DllXInput", EntryPoint = "InputJudg_GetGamePadState")]
	private static extern int _GetGamePadState(IntPtr instance, long padNum = 0);

	[DllImport("DllXInput", EntryPoint = "InputJudg_GetButton")]
	private static extern bool _GetButton(IntPtr instance, int judgButton = 0x00);

	[DllImport("DllXInput", EntryPoint = "InputJudg_GetButtonDown")]
	private static extern bool _GetButtonDown(IntPtr instance, int judgButton = 0x1000);

	[DllImport("DllXInput", EntryPoint = "InputJudg_GetButtonUp")]
	private static extern bool _GetButtonUp(IntPtr instance, int judgButton = 0x00);

	[DllImport("DllXInput", EntryPoint = "InputJudg_GetLeftTrigger")]
	private static extern bool _GetLeftTrigger(IntPtr instance);

	[DllImport("DllXInput", EntryPoint = "InputJudg_GetRightTrigger")]
	private static extern bool _GetRightTrigger(IntPtr instance);

	[DllImport("DllXInput", EntryPoint = "InputJudg_GetLeftAxis")]
	private static extern Vector2 _GetLeftAxis(IntPtr instance);

	[DllImport("DllXInput", EntryPoint = "InputJudg_GetRightAxis")]
	private static extern Vector2 _GetRightAxis(IntPtr instance);

	private static IntPtr _instance = _Create();
	private static string[] controllerNames;
	private static string codeToButtonName_ret = "";

	private static int[] nowTimeButtons = new int[4] { 0x00, 0x00, 0x00, 0x00 };
	private static int[] lastTimeButtons = new int[4] { 0x00, 0x00, 0x00, 0x00 };

	// 外部クラスの作成
	//public ControlerDevice()
	//{
	//	_instance = _Create();
	//	if (!IsDestroyed()) Debug.Log("作成完了");
	//}

	// 外部クラスの解放
	private static bool IsDestroyed()
	{
		return _instance == IntPtr.Zero;
	}

	// デストラクタ（外部クラスの解放）
	//~ControlerDevice()
	//{
	//	if (IsDestroyed())
	//	{
	//		return;
	//	}

	//	_Destroy(_instance);
	//	_instance = IntPtr.Zero;
	//}

	// コントローラーの状態を取得
	public static int GetGamePadState(long padNum = 0)
	{
		if (IsDestroyed())
		{
			return 0;
		}
		if (1 != _GetGamePadState(_instance, padNum)) return 0;

		return 1;
	}

	// 指定したボタンが押されているかの判定
	public static bool GetButton(int judgButton = 0x1000, ePadNumber padNumber = ePadNumber.eNone)
	{
		if (IsDestroyed())
		{
			return false;
		}
		GetGamePadState((long)padNumber);
		controllerNames = Input.GetJoystickNames();
		bool judge = controllerNames.Length > (int)padNumber;

		if ((judge && controllerNames[(int)padNumber] != "Controller (Gamepad F310)") && Input.GetButton(CodeToButtonName((eCode)judgButton, padNumber))) { return true; }
		if (_GetButton(_instance, judgButton)) { return true; }
		return false;
	}
	public static bool GetButton(eCode judgButton = eCode.ePad_None, ePadNumber padNumber = ePadNumber.eNone)
	{
		if (IsDestroyed())
		{
			return false;
		}
		GetGamePadState((long)padNumber);
		controllerNames = Input.GetJoystickNames();
		bool judge = controllerNames.Length > (int)padNumber;

		if ((judge && controllerNames[(int)padNumber] != "Controller (Gamepad F310)") && Input.GetButton(CodeToButtonName(judgButton, padNumber))) { return true; }
		if (_GetButton(_instance, (int)judgButton)) { return true; }
		return false;
	}

	// 指定したボタンが押されたかの判定
	public static bool GetButtonDown(int judgButton = 0x1000, ePadNumber padNumber = ePadNumber.eNone)
	{
		if (IsDestroyed())
		{
			return false;
		}
		GetGamePadState((long)padNumber);
		controllerNames = Input.GetJoystickNames();
		bool judge = controllerNames.Length > (int)padNumber;

		if ((judge && controllerNames[(int)padNumber] != "Controller (Gamepad F310)") && Input.GetButtonDown(CodeToButtonName((eCode)judgButton, padNumber))) { return true; }

		if ((lastTimeButtons[(int)padNumber] & judgButton) == 0 && (nowTimeButtons[(int)padNumber] & judgButton) == judgButton) { return true; }
		//if (_GetButtonDown(_instance, judgButton)) { return true; }
		return false;
	}
	public static bool GetButtonDown(eCode judgButton = eCode.ePad_None, ePadNumber padNumber = ePadNumber.eNone)
	{
		if (IsDestroyed())
		{
			return false;
		}
		GetGamePadState((long)padNumber);
		controllerNames = Input.GetJoystickNames();
		bool judge = controllerNames.Length > (int)padNumber;

		if ((judge && controllerNames[(int)padNumber] != "Controller (Gamepad F310)") && Input.GetButtonDown(CodeToButtonName(judgButton, padNumber))) { return true; }

		if ((lastTimeButtons[(int)padNumber] & (int)judgButton) == 0 && (nowTimeButtons[(int)padNumber] & (int)judgButton) == (int)judgButton) { return true; }
		//if (_GetButtonDown(_instance, (int)judgButton)) { return true; }
		return false;
	}
	private static bool GetButtonDown()
	{
		return false;
	}

	// 指定したボタンが放されたかの判定
	public static bool GetButtonUp(int judgButton = 0x1000, ePadNumber padNumber = ePadNumber.eNone)
	{
		if (IsDestroyed())
		{
			return false;
		}
		GetGamePadState((long)padNumber);
		controllerNames = Input.GetJoystickNames();
		bool judge = controllerNames.Length > (int)padNumber;

		if ((judge && controllerNames[(int)padNumber] != "Controller (Gamepad F310)") && Input.GetButtonUp(CodeToButtonName((eCode)judgButton, padNumber))) { return true; }

		if ((lastTimeButtons[(int)padNumber] & judgButton) == judgButton && (nowTimeButtons[(int)padNumber] & judgButton) == 0) { return true; }
		if (_GetButtonUp(_instance, judgButton)) { return true; }
		return false;
	}
	public static bool GetButtonUp(eCode judgButton = eCode.ePad_None, ePadNumber padNumber = ePadNumber.eNone)
	{
		if (IsDestroyed())
		{
			return false;
		}
		GetGamePadState((long)padNumber);
		controllerNames = Input.GetJoystickNames();
		bool judge = controllerNames.Length > (int)padNumber;

		if ((judge && controllerNames[(int)padNumber] != "Controller (Gamepad F310)") && Input.GetButtonUp(CodeToButtonName(judgButton, padNumber))) { return true; }

		if ((lastTimeButtons[(int)padNumber] & (int)judgButton) == (int)judgButton && (nowTimeButtons[(int)padNumber] & (int)judgButton) == 0) { return true; }
		if (_GetButtonUp(_instance, (int)judgButton)) { return true; }
		return false;
	}

	// 左トリガーの判定
	public static bool GetLeftTrigger(ePadNumber padNumber = ePadNumber.eNone)
	{
		if (IsDestroyed())
		{
			return false;
		}
		GetGamePadState((long)padNumber);
		return _GetLeftTrigger(_instance);
	}

	// 右トリガーの判定
	public static bool GetRightTrigger(ePadNumber padNumber = ePadNumber.eNone)
	{
		if (IsDestroyed())
		{
			return false;
		}
		GetGamePadState((long)padNumber);
		return _GetRightTrigger(_instance);
	}

	// 左スティックの判定
	public static Vector2 GetLeftAxis(ePadNumber padNumber = ePadNumber.eNone)
	{
		if (IsDestroyed())
		{
			return new Vector2(0.0f, 0.0f);
		}
		GetGamePadState((long)padNumber);
		return _GetLeftAxis(_instance);
	}

	// 右スティックの判定
	public static Vector2 GetRightAxis(ePadNumber padNumber = ePadNumber.eNone)
	{
		if (IsDestroyed())
		{
			return new Vector2(0.0f, 0.0f);
		}
		GetGamePadState((long)padNumber);
		return _GetRightAxis(_instance);
	}

	/// <summary>
	/// 機能的にはGetAxisRaw
	/// </summary>
	/// <param name="axisName"></param>
	/// <returns></returns>
	public static float GetAxis(string axisName = "", ePadNumber padNumber = ePadNumber.eNone)
	{
		controllerNames = Input.GetJoystickNames();
		bool judge = controllerNames.Length > (int)padNumber;
		if (judge && controllerNames[(int)padNumber] != "Controller (Gamepad F310)") { return Input.GetAxis(axisName); }

		if (axisName == "Horizontal" || axisName == "P2_Horizontal") { return GetLeftAxis(padNumber).x; }
		if (axisName == "Vertical" || axisName == "P2_Vertical") { return GetLeftAxis(padNumber).y; }
		Debug.LogError("名前が違うヨ☆");
		return 0f;
	}

	/// <summary>
	/// LateUpdateでよべ💩
	/// </summary>
	public static void Update()
	{
		foreach (ePadNumber pn in Enum.GetValues(typeof(ePadNumber)))
		{
			_GetGamePadState(_instance, (long)pn);
			lastTimeButtons[(int)pn] = nowTimeButtons[(int)pn];
			foreach (eCode code in Enum.GetValues(typeof(eCode)))
			{
				if (_GetButton(_instance, (int)code))
				{
					nowTimeButtons[(int)pn] |= (int)code;
				}
				else
				{
					nowTimeButtons[(int)pn] &= ~(int)code;
				}
			}
		}
	}

	static string CodeToButtonName(eCode code = eCode.ePad_None, ePadNumber padNum = ePadNumber.eNone)
	{
		codeToButtonName_ret = "";
		switch (padNum)
		{
			case ePadNumber.ePlayer1:
				break;
			case ePadNumber.ePlayer2:
				codeToButtonName_ret += "P2_";
				break;
			default:
				break;
		}
		switch (code)
		{
			case eCode.ePad_a:
				codeToButtonName_ret += "A";
				break;
			case eCode.ePad_b:
				codeToButtonName_ret += "B";
				break;
			case eCode.ePad_x:
				codeToButtonName_ret += "X";
				break;
			case eCode.ePad_y:
				codeToButtonName_ret += "Y";
				break;
			case eCode.ePad_lb:
				codeToButtonName_ret += "LB";
				break;
			case eCode.ePad_rb:
				codeToButtonName_ret += "RB";
				break;
			case eCode.ePad_lStick:
				codeToButtonName_ret += "LStick";
				break;
			case eCode.ePad_rStick:
				codeToButtonName_ret += "RStick";
				break;
			case eCode.ePad_start:
				codeToButtonName_ret += "Start";
				break;
			case eCode.ePad_back:
				codeToButtonName_ret += "Back";
				break;
			default:
				break;
		}
		return codeToButtonName_ret;
	}
}
