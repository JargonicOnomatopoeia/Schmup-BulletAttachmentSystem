using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer sprt;

    public void Move(Vector2 direction, float speed){
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    public void Anim(float direction = 0){
        sprt.flipX = (direction < 1 || direction == 0)? false: true; 

        if(direction == 0){
            anim.Play("idle");
            return;
        }

        if(direction != 0){
            anim.Play("left");
            return;
        }
    }
}
