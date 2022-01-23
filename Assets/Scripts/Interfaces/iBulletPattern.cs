using UnityEngine;

public interface iBulletPattern
{
    public void Initialize();
    public BulletCoord[] Action();
    public void End();
}
