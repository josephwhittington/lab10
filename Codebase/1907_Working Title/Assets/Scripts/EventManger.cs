using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManger : MonoBehaviour
{   
    // Delegates
    public delegate void Pause();
    public delegate void PlayerDead();
    public delegate void Door();
    public delegate void RoomEnter();
    public delegate void DisableRoom();
    // Delegates

    // Events
    public static event Pause PauseEvent;
    public static event Pause UnPauseEvent;
    //public static event PlayerDead PlayerDeathEvent;
    //public static event Door DoorOpenEvent;
    //public static event Door DoorCloseEvent;
    //public static event RoomEnter PlayerEnterRoomEvent;
    //public static event DisableRoom DisableRoomEvent;
    // Events

    // Defaults

    void Start()
    {
        
    }

    public void HandleGenericEvent(string p_eventname)
    {
        switch (p_eventname)
        {
            case "PAUSE":
                GameState.PauseGame();
                PauseEvent?.Invoke();
                break;
            case "UNPAUSE":
                GameState.UnpauseGame();
                UnPauseEvent?.Invoke();
                break;
            default:
                break;
        }

        //Set time scale
        Time.timeScale = GameState.TimeScale;
    }

    void Update()
    {
        
    }
}
