using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class PlayerMovement : MonoBehaviour
{
    public static Transform p_Transform;
    [SerializeField] private float speed;
    [SerializeField] private float speedFocus;
    private Movement p_move;

    private void Awake(){
        p_Transform = transform;
        p_move = GetComponent<Movement>();
    }

    private void Update(){
        float speedTemp = (Input.GetButton("Fire2"))?speedFocus:speed;
        p_move.Move(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")), speedTemp);
        if(((Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal")) && !Input.GetButton("Fire2")) || Input.GetButtonUp("Fire2")){
            p_move.Anim(Input.GetAxisRaw("Horizontal"));
            return;
        }
        if(Input.GetButtonDown("Fire2")){
            p_move.Anim();
            return;
        }
    }
}
