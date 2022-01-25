using System;
using System.Collections.Generic;
using UnityEngine;

/*
    BULLET PATTERN SET COMBINE
    A scriptable object that combines all bullet patterns into one
*/
[CreateAssetMenu(fileName = "Bullet Pattern Set Combine", menuName = "Bullet Pattern/Set Combine", order = 1)]
public class BulletPatternSetCombine : BulletPattern
{
    //
    public BulletPattern[] set; //Bullet Patterns

    public override void Initialize()
    {
        for(int x = 0; x < set.Length;x++){
            set[x].Initialize();
        }
    }

    public override BulletCoord[] Execute()
    {

        List<BulletCoord> coords = new List<BulletCoord>();

        //Getting all of the bullets, angles, and coordinats from all bullet patterns 
        for(int x = 0; x < set.Length;x++){
            //Add the bullets, angles, and coordinates from this BulletPatternSetCombineProp
            coords.AddRange(set[x].Execute());
        }

        return coords.ToArray();
    }

    public override bool End()
    {
        //Calls End to all Bullet Pattern Props
        for(int x = 0; x < set.Length;x++){
            set[x].End();
        }
        return true;
    }
}
