using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Target : MonoBehaviour
{
    public void Hit()
    {
        Invoke("OnHit", 0);
    }

    [Serializable]
    public class OnHitEvent : UnityEvent { }

    public OnHitEvent OnHit;
}
