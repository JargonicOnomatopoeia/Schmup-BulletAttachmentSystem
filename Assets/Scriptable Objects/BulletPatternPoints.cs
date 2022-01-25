using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet Pattern Points", menuName = "Bullet Pattern/Points", order = 1)]
public class BulletPatternPoints : BulletPattern
{
    [System.Serializable]
    public class BPpoint{
        public Vector2 coord;
        public float angle;
    }

    public GameObject[] bullets; // bullets to shoot (sequentially)
    public BPpoint[] points; // points where the bullets will be instantiated relative to the center

    //Changes on Runtime
    private int currentBullet;

    public override void Initialize()
    {
        currentBullet = 0;
    }

    public override BulletCoord[] Execute()
    {
        BulletCoord[] coords = new BulletCoord[points.Length];

        for(int x = 0; x < points.Length;x++){
            coords[x] = new BulletCoord(points[x].coord, points[x].angle, bullets[currentBullet]);
        }

        currentBullet = ++currentBullet % bullets.Length;

        return coords;
    }

    public override bool End()
    {
        currentBullet = 0;
        return true;
    }
}
