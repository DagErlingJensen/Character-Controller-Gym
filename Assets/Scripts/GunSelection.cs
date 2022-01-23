using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSelection : MonoBehaviour
{
    PlayerShooting _gun1Script;
    PlayerProjectileGun _gun2Script;

    void Start()
    {
        _gun1Script = GetComponent<PlayerShooting>();
        _gun2Script = GetComponent<PlayerProjectileGun>();

        ActivateGun1();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivateGun1();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivateGun2();
        }
    }

    void ActivateGun1()
    {
        _gun1Script.enabled = true;
        _gun2Script.enabled = false;
    }

    void ActivateGun2()
    {
        _gun2Script.enabled = true;
        _gun1Script.enabled = false;
    }
}
