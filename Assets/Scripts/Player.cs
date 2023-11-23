using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private WeaponBase myWeapon;
    [SerializeField] private ProjectileWeapon proWeap;


    private bool weaponShootToggle;

    void Start()
    {
        InputManager.Init(this);
        Debug.Log("Fired in Player");
        InputManager.EnableInGame();
    }




    public void Shoot()
    {
        print("I shot: " + InputManager.GetCameraRay());
        weaponShootToggle = !weaponShootToggle;
        if (weaponShootToggle)
        {
            myWeapon.StartShooting();
        }
        else
        {
            myWeapon.StopShooting();
        }
    }

    public void Reload()
    {
        myWeapon.Refresh();
        Debug.Log("Reloaded in player");
    }


    public void Change()
    {
        myWeapon.Chan();
    }

}
