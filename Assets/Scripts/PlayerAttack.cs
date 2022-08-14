using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject weapon;
    private IWeapon equippedWeapon { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        equippedWeapon = weapon.GetComponent<IWeapon>();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //        PerformWeaponAttack();
    //}

    //public void PerformWeaponAttack()
    //{
    //    equippedWeapon.PerformAttack(Random.Range(5, 20));
    //}

    public void EquipWeapon(IWeapon weaponToEquip)
    {
        equippedWeapon = weaponToEquip;
        Debug.Log("EquipWeapon " + weaponToEquip.ToString());

    }
}
