using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet Action Set", menuName = "Bullet Action Set", order = 1)]
public class BulletActionSet : ScriptableObject
{
    [SerializeField] private BulletAction[] actions;
    [SerializeField] private bool repInfinite;
    //Change On RunTime
    private int trav;
    

    public void Execute(Transform transform = null){
        if(trav < actions.Length){
            actions[trav].Action(transform);
        }
        if(trav < actions.Length && actions[trav].End(transform)){
            trav = (repInfinite)?++trav % actions.Length: ++trav;
        }
    }

    public bool End(Transform transform = null){
        if(trav >= actions.Length){
            trav = 0;
            return true;
        }

        return false;
    }
    
}
