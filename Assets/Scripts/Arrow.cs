using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody rb;
    public bool isExplode;
    public GameObject explosion;
    private AudioSource sound;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        sound = GetComponent<AudioSource>();
    }

    private void Update()
    {
       
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            if (!isExplode)
            {
                Debug.Log("Arrow hit ");
                col.GetComponent<Combat>().TakeDamage(Random.Range(10, 15));
                Destroy(gameObject);
            }
            else
            {
                StartCoroutine(Explode());
            }


        }
    }

    IEnumerator Explode()
    {
        // add explosion influence on other objects
        Collider[] objectsCollider = Physics.OverlapSphere(transform.position, 20);
        explosion.transform.position = transform.position;
        explosion.SetActive(true);
        sound.Play();

        for (int i = 0; i < objectsCollider.Length; i++)
        {
            Combat p = objectsCollider[i].GetComponent<Combat>();
            if (p != null && objectsCollider[i].tag == "Enemy")
            {
                Debug.Log("Explode Arrow hit ");

                p.TakeDamage(Random.Range(10, 15));
            }
        }
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        explosion.SetActive(false);


    }

}
