using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet Pattern Perfect Radial", menuName = "Bullet Pattern/Perfect Radial", order = 1)]
public class BulletPatternPerfectRadial : BulletPattern
{
    public GameObject[] bullets;
    public int bulletArrays;
    public float randomSpread;

    //On Run Time
    private float angle;


}
