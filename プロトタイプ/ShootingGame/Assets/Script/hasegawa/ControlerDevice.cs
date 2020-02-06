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

public static class ControlerDevice
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
		string buttonName = CodeToButtonName((eCode)judgButton, padNumber);
		return _GetButton(_instance, judgButton) || Input.GetButton(buttonName);
	}
	public static bool GetButton(eCode judgButton = eCode.ePad_None, ePadNumber padNumber = ePadNumber.eNone)
	{
		if (IsDestroyed())
		{
			return false;
		}
		GetGamePadState((long)padNumber);
		string buttonName = CodeToButtonName(judgButton, padNumber);
		bool unko = _GetButton(_instance, (int)judgButton);
		unko = Input.GetButton(buttonName);
		return _GetButton(_instance, (int)judgButton) || Input.GetButton(buttonName);
	}

	// 指定したボタンが押されたかの判定
	public static bool GetButtonDown(int judgButton = 0x1000, ePadNumber padNumber = ePadNumber.eNone)
	{
		if (IsDestroyed())
		{
			return false;
		}
		GetGamePadState((long)padNumber);
		string buttonName = CodeToButtonName((eCode)judgButton, padNumber);
		return _GetButtonDown(_instance, judgButton) || Input.GetButtonDown(buttonName);
	}
	public static bool GetButtonDown(eCode judgButton = eCode.ePad_None, ePadNumber padNumber = ePadNumber.eNone)
	{
		if (IsDestroyed())
		{
			return false;
		}
		GetGamePadState((long)padNumber);
		string buttonName = CodeToButtonName(judgButton, padNumber);
		return _GetButtonDown(_instance, (int)judgButton) || Input.GetButtonDown(buttonName);
	}

	// 指定したボタンが放されたかの判定
	public static bool GetButtonUp(int judgButton = 0x1000, ePadNumber padNumber = ePadNumber.eNone)
	{
		if (IsDestroyed())
		{
			return false;
		}
		GetGamePadState((long)padNumber);
		string buttonName = CodeToButtonName((eCode)judgButton, padNumber);
		return _GetButtonUp(_instance, judgButton) || Input.GetButtonUp(buttonName);
	}
	public static bool GetButtonUp(eCode judgButton = eCode.ePad_None, ePadNumber padNumber = ePadNumber.eNone)
	{
		if (IsDestroyed())
		{
			return false;
		}
		GetGamePadState((long)padNumber);
		string buttonName = CodeToButtonName(judgButton, padNumber);
		return _GetButtonUp(_instance, (int)judgButton) || Input.GetButtonUp(buttonName);
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
		Input.GetAxis("unchi");
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

	static string CodeToButtonName(eCode code = eCode.ePad_None, ePadNumber padNum = ePadNumber.eNone)
	{
		string ret = "";
		switch (padNum)
		{
			case ePadNumber.ePlayer1:
				break;
			case ePadNumber.ePlayer2:
				ret += "P2_";
				break;
			default:
				break;
		}
		switch (code)
		{
			case eCode.ePad_a:
				ret += "A";
				break;
			case eCode.ePad_b:
				ret += "B";
				break;
			case eCode.ePad_x:
				ret += "X";
				break;
			case eCode.ePad_y:
				ret += "Y";
				break;
			case eCode.ePad_lb:
				ret += "LB";
				break;
			case eCode.ePad_rb:
				ret += "RB";
				break;
			case eCode.ePad_lStick:
				ret += "LStick";
				break;
			case eCode.ePad_rStick:
				ret += "RStick";
				break;
			case eCode.ePad_start:
				ret += "Start";
				break;
			case eCode.ePad_back:
				ret += "Back";
				break;
			default:
				break;
		}
		return ret;
	}
}
