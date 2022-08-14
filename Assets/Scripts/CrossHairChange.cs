using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class CrossHairChange : MonoBehaviour
{
    public GameObject aCamera;
    public GameObject SeeThroughCrossHair;
    public GameObject TouchCrossHair;
    public GameObject ChestObject;
    public Text DrawerText;
    public Animator zombieAnimator;
    public NavMeshAgent npcAgent;
    private bool chestClosed = true;
    private Animator animator;
    private AudioSource chestSound;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        chestSound = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(aCamera.transform.position, aCamera.transform.forward, out hit))
        {
            // THIS is the chest. So we want to check if the hit object is the chest
            if ((hit.transform.gameObject == this.gameObject || hit.transform.gameObject == ChestObject.gameObject)
                 && hit.distance < 20)
            {
                // change crosshair
                if (!TouchCrossHair.active)
                {
                    SeeThroughCrossHair.SetActive(false);
                    TouchCrossHair.SetActive(true);
                }
            }
            else
            {
                // change crosshair
                if (TouchCrossHair.active)
                {
                    SeeThroughCrossHair.SetActive(true);
                    TouchCrossHair.SetActive(false);
                }
            }
            // check if we hit the chest
            if (hit.transform.gameObject == ChestObject.gameObject)
            {
                if (!DrawerText.IsActive())
                {
                    DrawerText.gameObject.SetActive(true);
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StartCoroutine(DrawerOpenClose());
                }
            }
            else
            {
                if (DrawerText.IsActive())
                {
                    DrawerText.gameObject.SetActive(false);
                }

            }
        }
    }

    // change text only after animations played
    IEnumerator DrawerOpenClose()
    {

        animator.SetBool("Open", chestClosed);
        chestClosed = !chestClosed;
        chestSound.PlayDelayed(0.8f);
        zombieAnimator.SetBool("isWalk", true); // zombie start walk
        npcAgent.enabled = true;

        yield return new WaitForSeconds(2);

        if (chestClosed)
            DrawerText.text = "Press [SPACE] to OPEN";
        else
            DrawerText.text = "Press [SPACE] to CLOSE";

    }
}
