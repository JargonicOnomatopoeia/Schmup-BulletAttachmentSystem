using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPatternExecutor : MonoBehaviour
{
   [SerializeField] private iBulletPattern[] bps;
   private int trav;

   private void Start(){
       for(int x = 0; x < bps.Length;x++){
           bps[x].Initialize();
       }
   }

   private void Update(){
       if(bps[trav].End()){
           trav++;
           return;
       }

       if(trav < bps.Length){
           BulletCoord[] temp = bps[trav].Execute();
           for(int x = 0; x < temp.Length;x++){
               Instantiate(temp[x].bullet, temp[x].coord, temp[x].angle);
           }
           return;
       }
   }
}
