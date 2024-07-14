using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluorescentLight : MonoBehaviour
{
    public MeshRenderer mesh;
    public AudioSource ambienceLoop;
    public AudioSource lightFailing;
    public AudioSource lightBreak;

    public Material lightEnabledMaterial;
    public Material lightDisabledMaterial;

    bool isLightFailing = false;
    public float lightSwitchMinTime = 0.1f;
    public float lightSwitchMaxTime = 0.6f;
    public float lightFailingDuration = 15f;

    public bool lightCanFail = false;
    public bool lightCanBreak = false;

    bool isLightBroken = false;
    public float startLightFailingTest = 10f;
    public float startLightBreakTest = 25f;


    public void EnableLightFailing()
    {
        if (isLightBroken)
            return;

        isLightFailing = true;
        lightFailing.Play();
        SetLightEnabled(false);
        StartCoroutine(StopLightFailing());
    }

    IEnumerator StopLightFailing()
    {
        yield return new WaitForSeconds(lightFailingDuration);
        isLightFailing = false;
        lightFailing.Stop();
        SetLightEnabled(true);
    }

    public void SetLightEnabled(bool p_enabled)
    {
        var mats = mesh.materials;
        mats[1] = p_enabled ? lightEnabledMaterial : lightDisabledMaterial;
        mesh.materials = mats;

        if (isLightFailing)
            StartCoroutine(SetLightEnabledCoroutine(!p_enabled, Random.Range(lightSwitchMinTime, lightSwitchMaxTime)));
        else
            ambienceLoop.Stop();

    }

    IEnumerator SetLightEnabledCoroutine(bool p_enabled, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if(isLightFailing)
            SetLightEnabled(p_enabled);
    }

    public void BreakLight()
    {
        lightBreak.Play();
        SetLightEnabled(false);
    }

    IEnumerator StartLightBreakTest()
    {
        yield return new WaitForSeconds(startLightBreakTest);
        isLightBroken = true;
        BreakLight();
    }
}
