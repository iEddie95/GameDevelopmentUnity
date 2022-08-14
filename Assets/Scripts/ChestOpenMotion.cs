using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// USED FOR EX 1

public class ChestOpenMotion : MonoBehaviour
{
    private Animator animator;
    public Animator zombieAnimator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        animator.SetBool("ChestOpen", true);
        zombieAnimator.SetInteger("Status", 1);
    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("ChestOpen", false);
    }
}
