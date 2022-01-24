using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPatternExecutor : MonoBehaviour
{
    public BulletPattern[] bps;
    private int trav;

    private void Start(){
       for(int x = 0; x < bps.Length;x++){
           bps[x].Initialize();
       }
    }

    private void Update(){
       if(trav < bps.Length && bps[trav].End()){
           trav++;
       }

       if(trav < bps.Length){
           BulletCoord[] temp = bps[trav].Execute();
           for(int x = 0;temp != null && x < temp.Length;x++){
               Instantiate(temp[x].bullet, temp[x].coord, temp[x].angle);
           }
           return;
       }
    }
}
