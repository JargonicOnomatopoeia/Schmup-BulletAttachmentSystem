using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPattern : ScriptableObject
{
    public virtual void Initialize(){

    }
    public virtual BulletCoord[] Execute(){
        return null;
    }
    public virtual bool End(){
        return true;
    }
}
