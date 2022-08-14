using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// USED FOR EX 3

public class NpcMotion : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    public GameObject target;
    public bool isFight = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        agent.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enabled)
        {
            agent.SetDestination(target.transform.position);
            animator.SetBool("isWalk", true);
        }

        if (!isFight)
        {
            if (Vector3.Distance(this.transform.position, target.transform.position) <= 5)
            {
                animator.SetBool("isWalk", false);
                agent.enabled = false;
            }

        }
        
    }
}
