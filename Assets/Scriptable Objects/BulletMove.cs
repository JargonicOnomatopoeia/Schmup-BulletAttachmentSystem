using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAction : ScriptableObject
{
    // Start is called before the first frame update
    public virtual void Initialize(Transform transform){

    }

    public virtual void Action(Transform transform){
        
    }

    public virtual bool End(Transform transform){
        return true;
    }
}
