using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public static BulletController bc;

    private void Awake(){
        bc = this;
    }

}
