using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet Pattern Radial", menuName = "Bullet Pattern/Radial", order = 1)]
public class BulletPatternRadial : BulletPattern
{
    public GameObject[] bullets;
    public bool isPerfectRadial;

    //Spin Variables
    public bool isSpinning;
    public bool isInvertSpin;
    public float spinAngleSkip;
    public float spinSpeed;
    public float spinMaxSpeed;
    //Circle/Ellipse Radius
    public Vector2[] centerOffsets;
    public Vector2 ellipse;

    //Number of Bullets Points
    public int bulletArray;
    public int bulletSpread;
    public float randomSpread;

    //Angle Variables
    public float startingAngle;
    public float bulletArrayAngle;
    public float bulletSpreadAngle;

    //Change On Run Time
    private int currentBullet;
    private int angleSpinCounter;

    private BulletCoord SetBulletCoord(float angle, Vector2 center, GameObject bullet){
        //Finding The Radius from and ellipse (oval)
        float parameter1 = Mathf.Pow(ellipse.x, 2) * Mathf.Pow(Mathf.Sin(angle), 2);
        float parameter2 = Mathf.Pow(ellipse.y, 2) * Mathf.Pow(Mathf.Cos(angle), 2);
        float r = (ellipse.x*ellipse.y)/Mathf.Sqrt(parameter1 + parameter2);

        //Finding the point from the radius
        float calc = (float)(angle *  Mathf.PI/180.0);
        Vector2 point = new Vector2(ellipse.x * (r * Mathf.Cos(calc)) , ellipse.y * (r * Mathf.Sin(calc)));

        return new BulletCoord(point + center, angle, bullet);
    }

    private float[] Spin(float[] angles){
        int angleSpin = (isInvertSpin)?-1 * angleSpinCounter: angleSpinCounter;
        for(int x = 0; x < angles.Length;x++){
            angles[x] = Mathf.Lerp(angles[x], angles[x] + (spinAngleSkip * angleSpin), spinSpeed * Time.deltaTime);
            Debug.Log(angles[x]);
        }
        angleSpinCounter++;
        return angles;
    }

    private BulletCoord[] InitializeBulletCoord (){
        int total = bulletArray + bulletSpread;
        BulletCoord[] coords = new BulletCoord[total * centerOffsets.Length];
        float[] angles = new float[total];

        angles[0] = startingAngle;

        int y;
        total--;
        for(int x = 0; x < total; x++){
            angles[x+1] = angles[x]  + bulletArrayAngle;
            for(y = 0; y < bulletSpread;y++){
                int y1 = x + y;
                angles[y1+1] = angles[y1] + bulletSpreadAngle;
            }
            x += y;
        }
        int z1 = 0;
        for(int x = 0; x < angles.Length ;x++){
            angles[x] += Random.Range(-randomSpread, randomSpread );
            for(int z = 0;z < centerOffsets.Length;z++){
                coords[z1] = SetBulletCoord(angles[x], centerOffsets[z], bullets[currentBullet]);
                z1++;
            }
        }

        if(isSpinning){
            angles = Spin(angles);
        }

        if(currentBullet < bullets.Length){
            currentBullet = ++currentBullet % bullets.Length;
        }

        return coords;
    }

    private BulletCoord[] InitializeBulletCoordPerfectRadial(){
        int combineArray = bulletArray + bulletSpread;
        float anglePerArray = 360/(combineArray);
        float[] angles = new float[combineArray];

        angles[0] = anglePerArray;
        for(int x = 1; x < combineArray;x++){
            angles[x] = angles[x-1] + anglePerArray;
            angles[x] += Random.Range(-randomSpread, randomSpread);
        }

        if(isSpinning){
            angles = Spin(angles);
        }

        BulletCoord[] coords = new BulletCoord[combineArray * centerOffsets.Length];

        int z = 0;
        for(int x = 0; x < combineArray;x++){
            for(int y = 0; y < centerOffsets.Length;y++){
                coords[z] = SetBulletCoord(angles[x], centerOffsets[y], bullets[currentBullet]);
                z++;
            }
        }

        if(currentBullet < bullets.Length){
            currentBullet = ++currentBullet % bullets.Length;
        }

        return coords;
    } 
    
    //Finding the semi major and semi minor lengths of the ellipse
    public override void Initialize(){
        angleSpinCounter = 0;
        currentBullet = 0;
    }

    public override BulletCoord[] Execute(){
        switch(isPerfectRadial){
            case true: return InitializeBulletCoordPerfectRadial() ;
            case false: return InitializeBulletCoord();
        }
    }   

    public override bool End(){
        currentBullet = 0;
        angleSpinCounter = 0;
        return true;
    } 
}
