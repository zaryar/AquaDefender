using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSelfdestruct : MonoBehaviour
{
    [SerializeField] float Timer = 2;

    private void Awake()
    {
        Destroy(gameObject, Timer);
    }
}
