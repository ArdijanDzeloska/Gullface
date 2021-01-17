using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainController : MonoBehaviour
{
    public ParticleSystem fountainEffect;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time % 10 < 5)
        {
            fountainEffect.Play();
        }
        else
        {
            fountainEffect.Stop();
        }
    }
}
