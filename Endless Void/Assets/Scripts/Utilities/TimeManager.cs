using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeManager {

    // Put a reference in Time.deltaTime lines that should be paused when the player freezes action
    public static int softTimeSpeedModifier = 1;
    // Put a reference in Time.deltaTime lines that should be paused when the pause menu is opened
    public static int hardTimeSpeedModifier = 1;
	
    public static void freezeAction()
    {
        softTimeSpeedModifier = 0;
    }

    public static void unfreezeAction()
    {
        softTimeSpeedModifier = 1;
    }

    public static void pauseGame()
    {
        softTimeSpeedModifier = 0;
        hardTimeSpeedModifier = 0;
    }

    public static void unpauseGame()
    {
        softTimeSpeedModifier = 1;
        hardTimeSpeedModifier = 1;
    }
}
