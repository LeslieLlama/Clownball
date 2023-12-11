using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffectsManager : MonoBehaviour
{
    public AudioSource guitarTwang;

    private void OnEnable()
    {
        RopeScript.OnSlingshotRelease += PlayGuitarSound;
    }
    private void OnDisable()
    {
        RopeScript.OnSlingshotRelease -= PlayGuitarSound;
    }

    void PlayGuitarSound(float pullStrength)
    {
        print(pullStrength);
        guitarTwang.pitch = pullStrength;
        guitarTwang.Play();
    }
}
