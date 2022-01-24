using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet Pattern Set", menuName = "Bullet Pattern Set", order = 1)]
public class BulletPatternSet : BulletPattern
{
    public BulletPatternSetProp[] set;
    public int repetition = 1; // number of times to repeat the set
    public bool repInfinite; // Ignores int repetition. 

    //Change On Runtime
    [SerializeField] private int setTrav;
    private BulletPatternSetProp currentBPS;
    [SerializeField] private int currentRep;

    public override void Initialize(){
        setTrav = 0;
        currentRep = 0;
        currentBPS = set[setTrav];
        currentBPS.Initialize(Time.time);
    }

    public override BulletCoord[] Execute(){
        if(currentRep >= repetition && repInfinite == false){
            return null;
        }

        BulletCoord[] coords = null;

        if(setTrav < set.Length && set[setTrav].startDelay.CheckTimer()){
            if(currentBPS.fireRate.CheckTimer() && currentBPS.CheckRepetitionIfLess()){
                coords = currentBPS.bp.Execute(); //gets all coords,  angles, and bullets of this bullet pattern 
                currentBPS.IncreaseRepitition();
                currentBPS.fireRate.SetCurrentTime(Time.time);
            }

            //Check if current pattern is finished assuming repinfinite is not TRUE
            if(!currentBPS.CheckRepetitionIfLess()){
                setTrav++; //Next Pattern
                //End Current Pattern
                currentBPS.End();
                if(setTrav >= set.Length){
                    //Repetition increase by one if all patterns are done
                    currentRep = (repInfinite)?++currentRep % repetition: ++currentRep;
                    setTrav = setTrav % set.Length;
                }

                if(setTrav < set.Length){
                    currentBPS = set[setTrav];
                    currentBPS.Initialize(Time.time);
                }
            }
        }

        return coords;
    }

    public override bool End(){

        if(currentRep >= repetition){
            setTrav = 0;
            currentRep = 0;
            currentBPS = null;
            for(int x = 0 ; x < set.Length;x++){
                set[x].End();
            }   
            return true;
        }

        return false;
    }
}
