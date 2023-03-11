using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class GlobalLight : MonoBehaviour
{
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D variableLight;

    void Start()
    {
        variableLight = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
    }

    void Update()
    {
        variableLight.intensity = Mathf.PingPong(Time.time * .05f, .1f) + .2f;
    }
}
