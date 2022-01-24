using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet Pattern", menuName = "Bullet Pattern", order = 1)]
public class BulletPattern : ScriptableObject, iBulletPattern
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
    private float a;
    private float b;

    private BulletCoord SetBulletCoord(float angle, Vector2 center, GameObject bullet){
        //Finding The Radius from and ellipse (oval)
        double parameter1 = Mathf.Pow(a, 2) * Mathf.Pow(Mathf.Sin(angle), 2);
        double parameter2 = Mathf.Pow(b, 2) * Mathf.Pow(Mathf.Cos(angle), 2);
        float r = (a*b)/Mathf.Sqrt((float)(parameter1 + parameter2));

        //Finding the point from the radius
        Vector2 point = new Vector2((float)(r * Mathf.Cos(angle)), (float)(r * Mathf.Sin(angle)));

        return new BulletCoord(point + center, angle, bullet);
    }

    private BulletCoord[] InitializeBulletCoord (){
        int total = bulletArray + bulletSpread;
        BulletCoord[] coords = new BulletCoord[total];
        float[] angles = new float[total];

        angles[0] = startingAngle;

        int y;
        for(int x = 0; x < total; x++){
            angles[x+1] = angles[x]  + bulletArrayAngle;
            for(y = 0; y < bulletSpread;y++){
                int y1 = x + y;
                angles[y1+1] = angles[y1] + bulletSpreadAngle;
            }
            x += y;
        }

        for(int x = 0; x < angles.Length ;x++){
            angles[x] += Random.Range(-angles[x], angles[x] );
            for(int z = 0;z < centerOffsets.Length;z++){
                coords[x] = SetBulletCoord(angles[x], centerOffsets[z], bullets[currentBullet]);
            }
        }

        if(currentBullet < bullets.Length){
            currentBullet = ++currentBullet % bullets.Length;
        }

        return coords;
    }
    
    //Finding the semi major and semi minor lengths of the ellipse
    public void Initialize(){
        a = (ellipse.x > ellipse.y)?ellipse.x:ellipse.y;
        b = (ellipse.x != a)?ellipse.x:ellipse.y;
    }

    public BulletCoord[] Execute(){
        return InitializeBulletCoord();
    }   

    public bool End(){
        currentBullet = 0;
        a = 0;
        b = 0;

        return true;
    } 
}

public class BulletPointDirection{
    
}
