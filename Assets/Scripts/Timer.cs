using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Timer
{
    public float maxTime;
    private float currentTime;

    public bool CheckTimer(){
        return maxTime < Time.time - currentTime;
    }

    public void SetCurrentTime(float time = 0){
        currentTime = time;
    }
}
