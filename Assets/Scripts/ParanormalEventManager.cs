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

    public Animator wc_door;
    public Animator mant_door;
    public Animator elec_door;

    public AudioSource wc_door_audio;
    public AudioSource mant_door_audio;
    public AudioSource elec_door_audio;

    public MeshRenderer screen1;
    public MeshRenderer screen2;
    public Material blackMaterial;

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

        var mats = screen1.materials;
        mats[1] = blackMaterial;
        screen1.materials = mats;

        var mats2 = screen2.materials;
        mats2[1] = blackMaterial;
        screen2.materials = mats2;

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
        enabledVendingMachine.DisableVendingMachine(true, true);
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


    IEnumerator ShowTextoPerturbadorAseoCoroutine()
    {
        yield return new WaitForSeconds(2f);

        //Logica texto perturbador
        StartCoroutine(OpenDoorWCCoroutine());
        InteractionManager.Instance.shouldInteract = false;
        interactionText.text = "I think I need to go to the toilet";

        StartCoroutine(TimeShowingTestCoroutine());
    }

    IEnumerator ShowTextoPerturbadorMantenimientoCoroutine()
    {
        yield return new WaitForSeconds(2f);
        InteractionManager.Instance.shouldInteract = false;

        interactionText.text = "It seems that the machine is broken";

        StartCoroutine(TimeShowingTestCoroutine());


        //Logica texto perturbador
        StartCoroutine(OpenDoorMantCoroutine());
    }

    IEnumerator ShowTextoPerturbadorElectricidadCoroutine()
    {
        yield return new WaitForSeconds(4f);
        StartCoroutine(OpenDoorElecCoroutine());

        /*InteractionManager.Instance.shouldInteract = false;

        interactionText.text = "Fucking Hell, I want to get out of this place.\nLet's see if I can turn on the lights and get the fuck out of here.";

        StartCoroutine(TimeShowingTestCoroutine());*/

    }

    IEnumerator OpenDoorWCCoroutine()
    {
        yield return new WaitForSeconds(2f);
        wc_door.enabled = true;
        wc_door_audio.Play();

    }

    IEnumerator OpenDoorMantCoroutine()
    {
        yield return new WaitForSeconds(2f);
        mant_door.enabled = true;
        mant_door_audio.Play();
    }

    IEnumerator OpenDoorElecCoroutine()
    {
        yield return new WaitForSeconds(2f);
        elec_door.enabled = true;
        elec_door_audio.Play();
    }

    public void EnableAllLights()
    {
        foreach (var l in fluorescentLigths)
        {
            l.SetLightEnabled(true);
        }
      

    }

    IEnumerator TimeShowingTestCoroutine()
    {
        yield return new WaitForSeconds(4f);
        InteractionManager.Instance.shouldInteract = true;

    }
}
