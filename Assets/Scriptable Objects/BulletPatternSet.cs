using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet Pattern Set", menuName = "Bullet Pattern Set", order = 1)]
public class BulletPatternSet : ScriptableObject, iBulletPattern
{
    public BulletPatternSetProp[] set;
    public int repetition = 1; // number of times to repeat the set
    public bool repInfinite; // Ignores int repetition. 

    //Change On Runtime
    private int setTrav;
    private BulletPatternSetProp currentBPS;
    private int currentRep;

    public virtual void Initialize(){
        currentBPS = set[setTrav];
        currentBPS.startDelay.SetCurrentTime(Time.time);

    }

    public virtual BulletCoord[] Execute(){
        BulletCoord[] coords = null;

        if(currentRep == repetition && repInfinite == false){
            return null;
        }

        if(setTrav < set.Length && set[setTrav].startDelay.CheckTimer()){
            if(currentBPS.fireRate.CheckTimer() && currentBPS.CheckRepetitionIfLess()){
                coords = currentBPS.bp.Execute(); //gets all coords,  angles, and bullets of this bullet pattern 
                currentBPS.IncreaseRepitition();
                currentBPS.fireRate.SetCurrentTime(Time.time);
            }

            //Check if current pattern is finished assuming repinfinite is not TRUE
            if(!currentBPS.CheckRepetitionIfLess() && !currentBPS.repInfinite){
                setTrav++;
                currentBPS.End();
                currentBPS.bp.End();
                currentBPS = set[setTrav];
            }
        }

        //Check if this pattern set is done. Resets everything if repInfinite of this set is TRUE
        if(repInfinite){
            setTrav = setTrav % set.Length;
            currentRep = currentRep % repetition;
        }

        return coords;
    }

    public virtual bool End(){
        if(currentRep >= repetition && repInfinite == false){
            setTrav = 0;
            currentRep = 0;
            for(int x = 0 ; x < set.Length;x++){
                set[x].startDelay.SetCurrentTime();
                set[x].fireRate.SetCurrentTime();
            }   
            return true;
        }

        return false;
    }
}

public class BulletPatternSetProp{

    public BulletPattern bp;
    public Timer startDelay; // waiting until it starts
    public Timer fireRate; //how fast does it need to fire
    public int repetition = 1; // how many times does the bullet pattern need to be executed
    public bool repInfinite; //Ignores int repetition. NOTE: it won't go to the next set if this is TRUE

    //On RunTime
    private int currentRepetition;

    public void IncreaseRepitition(){
        currentRepetition = (repInfinite)? currentRepetition % repetition: currentRepetition++;
    }

    public bool CheckRepetitionIfLess(){
        return currentRepetition < repetition;
    }

    public void End(){
        currentRepetition = 0;
    }
}
