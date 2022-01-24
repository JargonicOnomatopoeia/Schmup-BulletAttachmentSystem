using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet Action Forward", menuName = "Bullet Action/Move Forward", order = 1)]
public class BulletMoveForward : ScriptableObject, iBulletAction
{
    public float distance; // how far does the bullet need to go?
    public bool disInfinite; // Ignores distance.
    public float speed; // bullet speed

    //On Runtime
    private Vector2 distancePos;

    public void Initialize(Transform transform){
        distancePos = transform.position;
        distancePos.x += distance;
    }

    public Transform Action(Transform transform ){
        if(transform != null){
            transform.Translate(new Vector2( distance, 0f), Space.Self);
        }

        return transform;
    }   

    public bool End(Transform transform){
        if(transform != null && (Vector2)transform.position == distancePos && disInfinite != true){
            distancePos = Vector2.zero;
            return true;
        }
        
        return false;
    }
}
