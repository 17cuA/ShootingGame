﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Game_Master : MonoBehaviour
{
    public uint Frame_Count { private set; get; }   //  ゲームが開始してからの時間をカウント
    public static Game_Master MY { get; private set; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Frame_Count++;
    }
    
}
