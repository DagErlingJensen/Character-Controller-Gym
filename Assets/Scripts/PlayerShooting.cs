using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public KeyCode ToggleAutoFireButton;

    public float Range = 100;
    public float HitForce = 100;
    public float FireRate = 10;
    public ParticleSystem MuzzleFlash;
    public GameObject HitEffect;

    public bool autoFire = false;
    float nextTimeToFire;
    Camera camera;

    void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Shoot();

        if (Input.GetMouseButton(0) && autoFire)
        {
            if(Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1 / FireRate;
                Shoot();
            }
        }

        if (Input.GetKeyDown(ToggleAutoFireButton))
            autoFire = !autoFire;
    }

    void Shoot()
    {
        MuzzleFlash.Play();

        RaycastHit hit;
        if(Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, Range))
        {
            Debug.Log(hit.transform.name);

            GameObject impactEffect = Instantiate(HitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            impactEffect.transform.parent = hit.transform;
            Destroy(impactEffect, impactEffect.GetComponent<ParticleSystem>().main.duration);

            if (hit.transform.TryGetComponent(out Target target))
            {                target.Hit();            }

            if (hit.transform.TryGetComponent(out Rigidbody rb))
            {                //rb.AddForce(-hit.normal * HitForce);                rb.AddForceAtPosition(-hit.normal * HitForce, hit.point);            }
        }
    }
}
