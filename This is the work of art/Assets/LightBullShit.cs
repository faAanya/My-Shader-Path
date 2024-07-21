using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBullShit : MonoBehaviour
{
    [SerializeField]
    public Light[] lights;

    public CounterClick counterClick;
    void Start()
    {

        lights[0].enabled = true;
        lights[1].enabled = false;
        lights[2].enabled = false;
        // lightR.enabled = true;
        // lightY.enabled = false;
        // lightG.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        lights[counterClick.counter].enabled = true;
    }
}
