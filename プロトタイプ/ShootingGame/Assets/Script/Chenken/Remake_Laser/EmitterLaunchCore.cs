using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterLaunchCore
{
	private LaunchDevice currentDevice;
	public LaunchDevice CurrentDevice
	{
		get
		{
			return currentDevice;
		}
	}

	public EmitterLaunchCore(LaunchDevice defaultLaunchDevice)
	{
		currentDevice = defaultLaunchDevice;
	}

	public void SetDevice(LaunchDevice newLaunchDevice)
	{
		currentDevice = newLaunchDevice;
	}

	public void GenerateLine()
	{
		currentDevice.GenerateLine();
	}

	public void LaunchNode()
	{
		currentDevice.LaunchNode();
	}
}
