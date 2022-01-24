using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iBulletPatternSet
{
    public void Initialize();
    public BulletPattern Execute();
    public bool End();
}
