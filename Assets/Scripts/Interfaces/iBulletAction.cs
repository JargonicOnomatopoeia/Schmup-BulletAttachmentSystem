using UnityEngine;

public interface iBulletAction
{
    public void Initialize(Transform transform = null);
    public Transform Action(Transform transform = null);
    public bool End(Transform transform = null);

}
