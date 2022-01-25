using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Bullet Pattern Set Combine", menuName = "Bullet Pattern/Set Combine", order = 1)]
public class BulletPatternSetCombine : BulletPattern
{
    public BulletPatternSetProp[] set;
    
    public bool repInfinite;
    public int repetition = 1;

    //On RunTime
    public int currRep;


    public override void Initialize()
    {
        currRep = 0;
        for(int x = 0; x < set.Length;x++){
            set[x].Initialize(Time.time);
        }
    }

    public override BulletCoord[] Execute()
    {
        List<BulletCoord> coords = new List<BulletCoord>();

        for(int x = 0; x < set.Length;x++){
            if(set[x].fireRate.CheckTimer() && set[x].CheckRepetitionIfLess()){
                coords.AddRange(set[x].bp.Execute());
                set[x].IncreaseRepitition();
                set[x].fireRate.SetCurrentTime(Time.time);
            }
        }

        currRep = (repInfinite)?++currRep % repetition: ++currRep;

        return coords.ToArray();
    }

    public override bool End()
    {
        if(currRep >= repetition){
            currRep = 0;
            for(int x = 0; x < set.Length;x++){
                set[x].End();
            }
            return true;
        }
        return false;
    }
}
