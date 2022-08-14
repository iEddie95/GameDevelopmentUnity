using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INpc
{
    void Die();
    void PerformAttack();
    bool IsAlive();
}
