using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// USED FOR EX. 3- NOT USED IN THE FINAL PROJECT

public class NpcFight : MonoBehaviour
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
        float distance = Vector3.Distance(this.transform.position, target.transform.position);
        Animator targetAnimator = target.GetComponent<Animator>();
        NavMeshAgent enenmyAgent = target.GetComponent<NavMeshAgent>();

        if (distance < 7.2)
        {
            animator.SetInteger("Status", 1);

            // stop enemy motion
            targetAnimator.SetInteger("Status", 1);

            enenmyAgent.enabled = false;

            agent.enabled = false;

        }     
            
        }
}
