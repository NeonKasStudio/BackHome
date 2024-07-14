using System.Collections;
using System.Collections.Generic;
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
    }

    void PlayKickSFX()
    {
        kickSFX.Play();
    }

    void PlayEmergencyAlarm()
    {
        emergencyAlarm.Play();
        foreach (var l in emergencyLights)
        {
            l.EnableBlink();
        }
    }


    void EnableEmergencyLights()
    {
        foreach (var l in fluorescentLigths)
        {
            l.SetLightEnabled(false);
        }

        FindObjectOfType<AudioManager>().PlayLightsOff();
        StartCoroutine(EnableEMLightsCoroutine());
        emergencyLightVolume.gameObject.SetActive(true);
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
}
