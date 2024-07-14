using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMLight : MonoBehaviour
{
    public MeshRenderer mesh;
    public Material emissiveMat;
    public Material nonEmissiveMat;
    public AudioSource audioSource;
    float blinkTimeInterval = 0.4f;
    bool lightEnabled = false;
    bool isBlinking = false;

    public void EnableEMLight(bool p_enable)
    {
        var mats = mesh.materials;
        mats[0] = p_enable ? emissiveMat : nonEmissiveMat;
        mesh.materials = mats;
        if(!isBlinking)
            audioSource.Play();
        lightEnabled = p_enable;

        if (isBlinking)
            StartCoroutine(BlinkCoroutine());
    }

    public void EnableBlink()
    {
        isBlinking = true;
        EnableEMLight(false);
    }

    IEnumerator BlinkCoroutine()
    {
        yield return new WaitForSeconds(blinkTimeInterval);
        EnableEMLight(!lightEnabled);
    }
}
