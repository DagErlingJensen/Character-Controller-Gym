using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileGun : MonoBehaviour
{
    public GameObject Projectile;
    public Transform Muzzle;
    public float ProjectileForce;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(Projectile, Muzzle.position, Quaternion.LookRotation(Muzzle.forward, Muzzle.up));
        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * ProjectileForce);
        Destroy(projectile, 5);
    }
}
