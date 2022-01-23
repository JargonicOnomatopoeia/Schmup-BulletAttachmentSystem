using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPattern : MonoBehaviour
{
   [SerializeField] private List<BulletPatternTimer> bulletPatterns;
}


[System.Serializable]
public class BulletPatternTimer{
    public BulletPatternData bulletPatternData;
    public Timer timer;
}
