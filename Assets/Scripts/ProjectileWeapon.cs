using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : WeaponBase
{

    [SerializeField] private Rigidbody fireBullet;
    [SerializeField] private Rigidbody waterBullet;
    [SerializeField] private Rigidbody poisonBullet;


    [SerializeField] private float force;
    [SerializeField] private bool secondary;

    private Rigidbody currentBullet;


    protected override void Attack(float percent)
    {

        if(BulletType == EProjectileType.Fire)
        {
            currentBullet = fireBullet;
        }
        else if(BulletType == EProjectileType.Water) 
        { 
            currentBullet = waterBullet; 
        }
        else if (BulletType == EProjectileType.Poison)
        {
            currentBullet = poisonBullet;
        }




        if (bulletCount > 0)
        {
            print("My weapon attacked: " + percent);
            Ray camRay = InputManager.GetCameraRay(); // gets the mouse position
            Rigidbody rb = Instantiate(currentBullet, camRay.origin, transform.rotation); //clones the bullet and sets the origin to the mouse position
            rb.AddForce(force * camRay.direction, ForceMode.Impulse); //fires the bullet
            Destroy(rb.gameObject, 5); //destroys the bullet after 5 seconds

            gunShotSource.Play();
            

            bulletCount -= 1;
            Debug.Log(bulletCount);
        }
        else
        {
            Debug.Log("No bullets :c");
        }

        if(((int)BulletType & (int)EProjectileType.Fire) == (int)EProjectileType.Fire)
        {
            print("FIRE");
        }
        else
        {
            print("NOT FIRE");
        }
        
    }

    
    


}
