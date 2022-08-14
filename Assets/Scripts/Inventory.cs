using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private PlayerAttack playerAttack;
    public GameObject[] weapons;
    public GameObject weapon;
    private IWeapon equippedWeapon;

    // Start is called before the first frame update
    void Start()
    {
        playerAttack = GetComponentInParent<PlayerAttack>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weapon.SetActive(false);
            weapon = weapons[0];
            weapon.SetActive(true);
            equippedWeapon = weapon.GetComponent<IWeapon>();
            playerAttack.EquipWeapon(equippedWeapon);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weapon.SetActive(false);
            weapon = weapons[1];
            weapon.SetActive(true);
            equippedWeapon = weapon.GetComponent<IWeapon>();
            playerAttack.EquipWeapon(equippedWeapon);
        }
    }
}
