using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletPatternSetProp{

    public BulletPattern bp;
    public Timer startDelay; // waiting until it starts
    public Timer fireRate; //how fast does it need to fire
    public int repetition = 1; // how many times does the bullet pattern need to be executed
    public bool repInfinite; //Ignores int repetition. NOTE: it won't go to the next set if this is TRUE

    //On RunTime
    private int currentRepetition;

    public void Initialize(float timeStartDelay = 0, float timeFireRate = 0){
        bp.Initialize();
        currentRepetition = 0;
        fireRate.SetCurrentTime(timeFireRate);
        startDelay.SetCurrentTime(timeStartDelay);
    }

    public void IncreaseRepitition(){
        currentRepetition = (repInfinite)? ++currentRepetition % repetition: ++currentRepetition;
    }

    public bool CheckRepetitionIfLess(){
        return currentRepetition < repetition;
    }

    public void End(){
        bp.End();
        currentRepetition = 0;
        fireRate.SetCurrentTime();
        startDelay.SetCurrentTime();
        
    }
}
