using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class ParanormalEventManager : MonoBehaviour
{
    public int currentEvent = 0;
    public FluorescentLight lightToFail;
    public FluorescentLight lightToBreak;
    public List<FluorescentLight> fluorescentLigths;
    public List<EMLight> emergencyLights;
    public List<VideoCamera> videoCameras;
    public AudioSource kickSFX;
    public GameObject emergencyLightVolume;
    public AudioSource emergencyAlarm;
    public AudioSource chaseMusic;
    public GameObject lataman;
    public GameObject latamanPointLight;
    public VendingMachine disabledVendingMachine;
    public VendingMachine enabledVendingMachine;
    public TMP_Text interactionText;

    public void PlayParanormalEvent()
    {
        switch (currentEvent)
        {
            case 0:
                PlayLightFailing();
                break;

            case 1:
                PlayLightBreak();
                break;

            case 2:
                PlayCamerasEnabled();
                break;

            case 3:
                PlayKickSFX();
                break;

            case 4:
                PlayVendingMachinePowerOff();
                break;
        }

        currentEvent++;
    }

    void PlayLightFailing()
    {
        lightToFail.EnableLightFailing();
    }

    void PlayLightBreak()
    {
        lightToBreak.BreakLight();
    }

    void PlayCamerasEnabled()
    {
        foreach(VideoCamera cam in videoCameras)
        {
            cam.EnableCamera();
        }

        StartCoroutine(ShowTextoPerturbadorAseoCoroutine());
    }

    void PlayKickSFX()
    {
        kickSFX.Play();
        StartCoroutine(DropCanInDisabledMachineCoroutine());
    }

    IEnumerator DropCanInDisabledMachineCoroutine()
    {
        yield return new WaitForSeconds(0f);

        //Logica para spawnear lata en maquina desactivada
        disabledVendingMachine.ReleaseCanWithoutCoin();
    }

    void PlayEmergencyAlarm()
    {
        emergencyAlarm.Play();
        foreach (var l in emergencyLights)
        {
            l.EnableBlink();
        }
    }


    public void EnableEmergencyLights()
    {
        foreach (var l in fluorescentLigths)
        {
            l.SetLightEnabled(false);
        }

        FindObjectOfType<AudioManager>().PlayLightsOff();
        StartCoroutine(EnableEMLightsCoroutine());
        emergencyLightVolume.gameObject.SetActive(true);

        StartCoroutine(ShowTextoPerturbadorElectricidadCoroutine());
    }

    IEnumerator EnableEMLightsCoroutine()
    {
        yield return new WaitForSeconds(3.5f);
        foreach (var l in emergencyLights)
        {
            l.EnableEMLight(true);
        }
    }

    public void PlayLatamanAppareance()
    {
        FindObjectOfType<AudioManager>().LatamanAppearence();
        lataman.SetActive(true);
        latamanPointLight.gameObject.SetActive(true);
        StartCoroutine(StopPointLightCoroutine());
        StartCoroutine(EnableChaseMusicCoroutine());
    }

    public void PlayVendingMachinePowerOff()
    {
        enabledVendingMachine.DisableVendingMachine();
        StartCoroutine(ShowTextoPerturbadorMantenimientoCoroutine());
    }

    IEnumerator StopPointLightCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        latamanPointLight.gameObject.SetActive(false);

    }

    IEnumerator EnableChaseMusicCoroutine()
    {
        yield return new WaitForSeconds(0.6f);
        PlayEmergencyAlarm();
        chaseMusic.Play();
        lataman.GetComponentInChildren<Enemy>().StartChase();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            EnableEmergencyLights();

        if (Input.GetKeyDown(KeyCode.P))
            PlayEmergencyAlarm();
    }



    IEnumerator ShowTextoPerturbadorAseoCoroutine()
    {
        yield return new WaitForSeconds(2f);

        InteractionManager.Instance.shouldInteract = false;
        interactionText.text = "I'm pissing myself\nWhere's the toilet in this fucking station?";

        StartCoroutine(TimeShowingTestCoroutine());
    }

    IEnumerator ShowTextoPerturbadorMantenimientoCoroutine()
    {
        yield return new WaitForSeconds(2f);
        InteractionManager.Instance.shouldInteract = false;

        interactionText.text = "Now the fucking machine is broken.\nI think I've seen a tool room around here.";

        StartCoroutine(TimeShowingTestCoroutine());


    }

    IEnumerator ShowTextoPerturbadorElectricidadCoroutine()
    {
        yield return new WaitForSeconds(4f);
        InteractionManager.Instance.shouldInteract = false;

        interactionText.text = "Fucking Hell, I want to get out of this place.\nLet's see if I can turn on the lights and get the fuck out of here.";

        StartCoroutine(TimeShowingTestCoroutine());


    }

    IEnumerator TimeShowingTestCoroutine()
    {
        yield return new WaitForSeconds(4f);
        InteractionManager.Instance.shouldInteract = true;

    }
}
