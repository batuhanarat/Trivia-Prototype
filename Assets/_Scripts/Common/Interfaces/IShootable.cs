using UnityEngine;

public interface IShootable
{
    void Shoot(Vector2 force);
    void ResetBall(ref bool hasShot);
    public bool HasScored();
    public bool HasHitGround();
}
