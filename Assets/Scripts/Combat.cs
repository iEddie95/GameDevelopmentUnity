using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    private bool alive = true;
    public int hp;
    public HealthBar healthBar;

    void Start()
    {
        if (healthBar!=null)
            healthBar.SetHealth(hp);
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        if (healthBar != null)
            healthBar.SetHealth(hp);
        if (hp <= 0)
            alive = false;
    }

    public bool IsAlive()
    {
        return alive;
    }

}
