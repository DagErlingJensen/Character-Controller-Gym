using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float range = 100;
    public ParticleSystem MuzzleFlash;
    public GameObject HitEffect;
    Camera fpsCam;

    private void Awake()
    {
        fpsCam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Shoot();
    }

    void Shoot()
    {
        MuzzleFlash.Play();

        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            GameObject impactEffect = Instantiate(HitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactEffect, impactEffect.GetComponent<ParticleSystem>().main.duration);
            if (hit.transform.GetComponent<Target>())
                hit.transform.GetComponent<Target>().Hit();
        }
    }
}
