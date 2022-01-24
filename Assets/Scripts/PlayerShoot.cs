using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    //[SerializeField] private Bullet baseBullet;

    private void Start(){
        //BulletController.AddBullet(baseBullet);
    }

    private void Update(){
        if((Input.GetButtonDown("Fire1") && Input.GetButton("Fire2")) || (Input.GetButton("Fire1") && Input.GetButtonDown("Fire2"))){
            //BulletController.bc.bullets.ForEach(i => i.SetAllCurrentTime(i.focusBullets, Time.time));
        }
        /*if(Input.GetButton("Fire1") && Input.GetButton("Fire2")){
            foreach (Bullet i in BulletController.bc.bullets)
            {
                List<GameObject> temp = i.FocusFire();
                foreach (GameObject x in temp)
                {
                    Instantiate(x, transform.position, Quaternion.identity);
                }
            }
            return;
        }

        if(Input.GetButtonDown("Fire1")){
            BulletController.bc.bullets.ForEach(i => i.SetAllCurrentTime(i.bullets, Time.time));
        }

        if(Input.GetButton("Fire1")){
            foreach (Bullet i in BulletController.bc.bullets)
            {
                List<GameObject> temp = i.Fire();
                foreach (GameObject x in temp)
                {
                    Instantiate(x, transform.position, Quaternion.identity);
                }
            }
            return;
        }*/
    }
}
