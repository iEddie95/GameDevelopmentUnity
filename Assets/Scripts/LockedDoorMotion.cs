using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

// USED FOR EX2

public class LockedDoorMotion : MonoBehaviour
{
    private Animator animator;
    private AudioSource sound;
    public GameObject keyInHand;
    public GameObject guard;
    public Text DoorText;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Animator guardAnimator = guard.GetComponent<Animator>();
        NavMeshAgent guardAgent = guard.GetComponent<NavMeshAgent>();

        if (keyInHand.active)
        {
           animator.SetBool("DoorIsOpen", true);
           sound.PlayDelayed(1f);
           guardAgent.enabled = true;
        }
        else
        {
            guardAnimator.SetInteger("Status", 2);
            DoorText.gameObject.SetActive(true);
            DoorText.text = "You need a key to open this door";
            guardAgent.enabled = false;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        Animator guardAnimator = guard.GetComponent<Animator>();
        NavMeshAgent guardAgent = guard.GetComponent<NavMeshAgent>();

        animator.SetBool("DoorIsOpen", false);
        //sound.Play();
        sound.PlayDelayed(1f);
        DoorText.gameObject.SetActive(false);
        guardAnimator.SetInteger("Status", 0);
        guardAgent.enabled = false;

    }
}
