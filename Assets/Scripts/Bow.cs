using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bow : MonoBehaviour, IWeapon
{
    private Animator animator;
    public GameObject aCamera;
    public GameObject arrow;
    public GameObject arrowSpawn;
    private Rigidbody arrowRG;
    public bool isReloadingSpecial; // exploding arrow
    public bool isReloading; //  regular arrow
    private AudioSource sound;

    void Start()
    {
        animator = GetComponent<Animator>();
        arrowRG = arrow.GetComponent<Rigidbody>();
        sound = GetComponent<AudioSource>();
        isReloading = false;
        isReloadingSpecial = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isReloading) // left click
        {
            Debug.Log("Bow Fire");
            arrow.GetComponent<Arrow>().isExplode = false;

            Vector3 direction = aCamera.transform.forward;
            arrow.GetComponent<Collider>().enabled = true;
            arrow.GetComponent<Collider>().isTrigger = true;
            arrowRG.AddForce(30f * direction, ForceMode.VelocityChange); // 10* is the power of throwing
            arrowRG.useGravity = true;
            StartCoroutine(Reload(false, 0.5f));
        }

        if (Input.GetKeyDown(KeyCode.Q) && !isReloadingSpecial)
        {
            Debug.Log("Special bow Fire");
            arrow.GetComponent<Arrow>().isExplode = true;
            Vector3 direction = aCamera.transform.forward;
            arrow.GetComponent<Collider>().enabled = true;
            arrow.GetComponent<Collider>().isTrigger = true;
            arrowRG.AddForce(30f * direction, ForceMode.VelocityChange);
            //arrowRG.velocity = direction * 30f;
            arrowRG.useGravity = true;
            StartCoroutine(Reload(true, 1.5f));
        }

    }

    public void PerformAttack()
    {
        animator.SetTrigger("Bow_Attack");
        sound.PlayDelayed(0.1f);
    }


    IEnumerator Reload(bool isSpecial, float delay)
    {
        PerformAttack();
        arrow = Instantiate(arrow, this.transform);
        arrowRG = arrow.GetComponent<Rigidbody>();
        arrowRG.useGravity = false;
        arrow.GetComponent<Collider>().enabled = false;
        arrow.GetComponent<Collider>().isTrigger = false;
        arrow.transform.SetParent(transform);

        if (isSpecial)
            isReloadingSpecial = true;
        else
            isReloading = true;


        yield return new WaitForSeconds(delay);

        if (isSpecial)
            isReloadingSpecial = false;
        else
            isReloading = false;
    }
}
