using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public static BulletController bc;
    public List<Bullet> bullets;

    private void Awake(){
        bc = this;
    }

    private void OnApplicationQuit(){
        bullets.ForEach(i => {
            i.SetAllCurrentTime(i.bullets);
            i.SetAllCurrentTime(i.focusBullets);
        });
    }

    public static void AddBullet(Bullet b){
        bc.bullets.Add(b);
    }

}
