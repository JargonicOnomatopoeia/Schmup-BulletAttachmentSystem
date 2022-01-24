using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet Pattern Radial", menuName = "Bullet Pattern/Radial", order = 1)]
public class BulletPatternRadial : BulletPattern
{
    public GameObject[] bullets;
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

    private BulletCoord SetBulletCoord(float angle, Vector2 center, GameObject bullet){
        //Finding The Radius from and ellipse (oval)
        float parameter1 = Mathf.Pow(ellipse.x, 2) * Mathf.Pow(Mathf.Sin(angle), 2);
        float parameter2 = Mathf.Pow(ellipse.y, 2) * Mathf.Pow(Mathf.Cos(angle), 2);
        float r = (ellipse.x*ellipse.y)/Mathf.Sqrt(parameter1 + parameter2);

        //Finding the point from the radius
        Vector2 point = new Vector2(ellipse.x * (r * Mathf.Cos((float)(angle *  Mathf.PI/180.0))) , ellipse.y * (r * Mathf.Sin((float)(angle *  Mathf.PI/180.0))));

        return new BulletCoord(point + center, angle, bullet);
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

        if(currentBullet < bullets.Length){
            currentBullet = ++currentBullet % bullets.Length;
        }

        return coords;
    }
    
    //Finding the semi major and semi minor lengths of the ellipse
    public override void Initialize(){
        //a = (ellipse.x > ellipse.y)?ellipse.x:ellipse.y;
        //b = (ellipse.x != a)?ellipse.x:ellipse.y;
    }

    public override BulletCoord[] Execute(){
        return InitializeBulletCoord();
    }   

    public override bool End(){
        currentBullet = 0;

        return true;
    } 
}
