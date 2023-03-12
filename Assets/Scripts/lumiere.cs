using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lumiere : MonoBehaviour
{
    [SerializeField] private Light variableLight;

    void Start()
    {
        variableLight = GetComponent<Light>();
    }

    void Update()
    {
        variableLight.intensity = Mathf.PingPong(Time.time * .5f, 1f);
    }
}
