using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private BulletActionSet bas;

    private void Update(){
        bas.Execute(transform);
    }
    
}
