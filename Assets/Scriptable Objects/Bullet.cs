using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "Bullet", order = 1)]
public class Bullet : ScriptableObject
{

    public BulletDelay[] bullets;
    public BulletDelay[] focusBullets;

    private List<GameObject> InstantiateBullet(BulletDelay[] b){
        List<GameObject> checker = new List<GameObject>();
        for(int x = 0; x < b.Length;x++){
            if(b[x].CheckCurrentTime()){
                checker.Add(b[x].bullet);
                b[x].SetCurrentTime(Time.time);
            }
        }

        return checker;
    }

    public virtual List<GameObject> Fire(){
        return InstantiateBullet(bullets);
    }

    public virtual List<GameObject> FocusFire(){
        return InstantiateBullet(focusBullets);
    }
/*

--------BULLET DELAY METHODS----------

*/
    public void SetAllCurrentTime(BulletDelay[] bd, float time = 0){
        for(int x = 0; x < bd.Length;x++){
            bd[x].SetCurrentTime(time);
        }
    }
}

[System.Serializable]
public class BulletDelay
{
    public GameObject bullet;
    public float delay;
    private float currentTime;

    public void SetCurrentTime(float time = 0){
        currentTime = time;
    }

    public bool CheckCurrentTime(){
        return delay <  Time.time - currentTime;
    }
}
