using UnityEngine;

public interface iBulletPattern
{
    public void Initialize();
    public BulletCoord[] Execute();
    public bool End();
}
