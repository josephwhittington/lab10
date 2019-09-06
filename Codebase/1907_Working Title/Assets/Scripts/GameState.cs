using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameState 
{
    public static  bool GamePaused = false;
    public static float TimeScale = 1;

    public static void SetPauseState(bool p_pauseState)
    {
        GamePaused = p_pauseState;
    }

    public static void PauseGame()
    {
        TimeScale = 0;
        SetPauseState(true);
    }

    public static void UnpauseGame()
    {
        TimeScale = 1;
        SetPauseState(false);
    }
}
