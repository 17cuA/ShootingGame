using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Game_Master : MonoBehaviour
{
    private uint frame_Count;   //  ゲームが開始してからの時間をカウント
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        frame_Count++;
    }
}
