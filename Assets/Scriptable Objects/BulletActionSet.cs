using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet Action Set", menuName = "Bullet Action Set", order = 1)]
public class BulletActionSet : ScriptableObject
{
    public iBulletAction[] actions;
    //Change On RunTime
    private int trav;
    private bool repInfinite;

    public Transform Execute(Transform transform = null){
        Transform newPos = null;
        if(trav < actions.Length){
            newPos = actions[trav].Action(transform);
        }
        if(trav < actions.Length && actions[trav].End()){
            trav = (repInfinite)?++trav % actions.Length: ++trav;
        }

        return newPos;
    }

    public bool End(Transform transform = null){
        if(trav >= actions.Length){
            trav = 0;
            return true;
        }

        return false;
    }
    
}
