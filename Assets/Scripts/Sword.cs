using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Sword : MonoBehaviour, IWeapon
{
    private Animator animator;
    private AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // left click
        {
            PerformAttack();
        }

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            col.GetComponent<Combat>().TakeDamage(Random.Range(5, 20));
        }
    }

    public void PerformAttack()
    {
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        sound.PlayDelayed(0.5f);
        animator.SetTrigger("Sword_Attack");
        yield return new WaitForSeconds(0.1f);
    }


}
