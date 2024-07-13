using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParanormalEventManager : MonoBehaviour
{
    public int currentEvent = 0;
    public FluorescentLight lightToFail;
    public FluorescentLight lightToBreak;
    public List<VideoCamera> videoCameras;
    public AudioSource kickSFX;

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
}
