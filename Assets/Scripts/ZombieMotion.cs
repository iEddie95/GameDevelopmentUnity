using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// USED FOR EX. 3- NOT USED IN THE FINAL PROJECT

public class ZombieMotion : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // sets the new path for motion
        if (agent.enabled && animator.GetBool("isWalk"))
            agent.SetDestination(target.transform.position);

    }

  
}
