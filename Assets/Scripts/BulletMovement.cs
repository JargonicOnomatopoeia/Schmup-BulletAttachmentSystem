using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float deathTimer;
    private float currentTimer;

    private void Start(){
        currentTimer = Time.time;
    }

    private void Update(){
        transform.Translate(new Vector2(0, 1) * speed * Time.deltaTime, Space.Self);
        if(deathTimer < Time.time - currentTimer){
            Destroy(this.gameObject);
        }
    }
    
}
