using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultStone : MonoBehaviour
{

    Rigidbody rb;
    public string enemytag;
    private AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sound = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            {
                col.GetComponent<Combat>().TakeDamage(Random.Range(10, 15));
                StartCoroutine(stoneHit());
            }
        }      
    }

    IEnumerator stoneHit()
    {
        sound.Play();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

}
