using UnityEngine;

public class BulletCoord
{
    public GameObject bullet;
    public Vector2 coord;
    public Quaternion angle;

    public BulletCoord(Vector2 _coord, float _angle, GameObject _bullet){
        coord = _coord;
        angle = Quaternion.Euler(0, 0, _angle);
        bullet = _bullet;
    }
}
