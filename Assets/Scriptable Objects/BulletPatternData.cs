using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet Pattern Data", menuName = "Bullet Pattern Data", order = 1)]
public class BulletPatternData : ScriptableObject, iBulletPattern
{
    public GameObject[] bullets;
    //Angle Variables
    public Vector2 circle;
    public int startingAngle; // the first bullet array starts here (default is 0)
    public bool toAim; //Aim at the closest opponent the entity sees

    // Bullet Array Variables
    public int bulletArray; //Number of bullet arrays
    public int bulletArraySpread; //How far (in degrees) between Bullet Arrays
    public int bulletRow; //How many rows of bullets per Bullet Array
    public int bulletSpread; // How far (in degrees) between in each bullet in a row (ref: bulletRow) in each Bullet Array.
    public int randomSpread; // How random the spread is
    public bool isPerfectRadial; //Ignores all bullet array variables (except for bulletArray, bulletArraySpread & randomSpread) 
    //and calculates to form a perfect radial
    

    //Shooting Variables
    public float fireRate; // Pew Pew Speed
    public float fireRateModifier; // Increase base Fire Rate until it reaches max fire rate.
    public Timer fireRateIncreses; // Timer to change the fire rate speed;
    public float maxFireRate; // works with fire rate modifier
    public bool toWaveFireRate; //Fire rate "lerps" back and forth from maxFireRate to fireRate and vice versa
    public bool toShoot; //to shoot or not to shoot?

    //Spin Variables
    public float spinSpeed; // base spin Speed
    public float spinModifier; // Increases the base Spin Speed overtime until it reaches Max Spin Speed
    public float maxSpinSpeed; // only works with spin modifier
    public bool toMakeWave; // after spinSpeed "lerps" to maxSpinSpeed it "lerps" to negative maxSpinSpeed. Thus making a wave

    //These Variables Change During Runtime
    private float currentSpinSpeed;
    private float currentFireRate;

    //Radius Variables (Mathematics)
    private float a;
    private float b;

    //Traversers
    private int gameObject_trav;

    private void ChangeCurrentValue(float origValue , float setValue = 0){
        origValue = setValue;
    }

    private BulletCoord SetBulletCoord(float angle){
        //Finding The Radius from and ellipse (oval)
        double parameter1 = Math.Pow(a, 2) * Math.Pow(Mathf.Sin(angle), 2);
        double parameter2 = Math.Pow(b, 2) * Math.Pow(Mathf.Cos(angle), 2);
        float r = (a*b)/Mathf.Sqrt((float)(parameter1 + parameter2));

        //Finding the point from the radius
        Vector2 point = new Vector2((float)(r * Math.Cos(angle)), (float)(r * Math.Sin(angle)));

        return new BulletCoord(point, angle);
    }

    private List<BulletCoord> InitializeCoord (){
        List<BulletCoord> coords = new List<BulletCoord>();
        coords.Add(SetBulletCoord(startingAngle));

        int y;
        for(int x = 0; x < (bulletArray + bulletRow - 1);x++){
            float incAngle = coords[x].angle.eulerAngles.z  + bulletArraySpread;
            coords.Add(SetBulletCoord(incAngle));
            for(y = 0; y < bulletRow;y++){
                int y1 = x + y;
                incAngle = coords[y1].angle.eulerAngles.z + bulletSpread;
                coords.Add(SetBulletCoord(incAngle));
            }
            x += y;
        }
        
        return coords;
    }

    public virtual void Initialize(){
        currentSpinSpeed = spinSpeed;
        currentFireRate = fireRate;
        a = (circle.x > circle.y)?circle.x:circle.y;
        b = (circle.x != a)?circle.x:circle.y;
    }

    public virtual BulletCoord[] Action(){
        List<BulletCoord> coords = InitializeCoord();

        int maxTrav = bulletRow + bulletArray;
        for(int x = 0; x < maxTrav;x++){
            coords[x].bullet = bullets[gameObject_trav];
        }

        if(bullets.Length != 1 && bullets.Length > 0){
            gameObject_trav++;

            if(gameObject_trav > bullets.Length){
                gameObject_trav = 0;
            }
        }
        
        return coords.ToArray();
    }

    public virtual void End(){
        currentFireRate = 0;
        currentSpinSpeed = 0;
        a = 0;
        b = 0;

    }
}
