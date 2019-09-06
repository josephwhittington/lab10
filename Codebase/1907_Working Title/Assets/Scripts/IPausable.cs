using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPausable : MonoBehaviour
{
    protected bool GamePaused = false;

    void OnEnable()
    {
        EventManger.PauseEvent += Pause;
        EventManger.UnPauseEvent += UnPause;
    }

    protected void Pause()
    {
        GamePaused = true;
    }

    protected void UnPause()
    {
        Debug.Log("up");
        GamePaused = false;
    }

    void OnDisable()
    {
        EventManger.PauseEvent -= Pause;
        EventManger.UnPauseEvent -= UnPause;
    }
}
