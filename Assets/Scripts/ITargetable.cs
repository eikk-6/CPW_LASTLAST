using UnityEngine;

public interface ITargetable
{
    void TakeDamage(int amount);
    Transform GetTransform();
}
