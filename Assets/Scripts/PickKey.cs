using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// USED FOR EX 2

public class PickKey : MonoBehaviour
{
    public GameObject keyInChest;
    public GameObject keyInHand;
    private AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && keyInChest.active)
        {
            sound.PlayDelayed(0.5f);
            keyInChest.SetActive(false);
            keyInHand.SetActive(true);
        }

    }

    private void OnMouseDown()
    {
        keyInChest.SetActive(false);
        keyInHand.SetActive(true);
    }
}
