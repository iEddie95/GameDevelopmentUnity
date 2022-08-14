using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcArrow : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(enemytag))
        {
            {
                Debug.Log("arrow hit ");
                sound.Play();
                col.GetComponent<Combat>().TakeDamage(Random.Range(10, 15));
                Destroy(gameObject);
            }

        }
    }
}
