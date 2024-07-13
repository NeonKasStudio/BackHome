using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoCamera : MonoBehaviour
{
    PlayerMovement player;
    public Transform cameraTransform;
    public MeshRenderer cameraMesh;
    public Material cameraEmission;
    bool isCameraEnabled = false;
    public float startEnableCameraTime = 10f;
    public AudioSource cameraSwitch;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    private void Start()
    {
        //StartCoroutine(EnableCameraCoroutine());
    }

    private void Update()
    {
        if (!isCameraEnabled)
            return;

        cameraTransform.LookAt(player.transform);
    }

    IEnumerator EnableCameraCoroutine()
    {
        yield return new WaitForSeconds(startEnableCameraTime);
        EnableCamera();
    }

    public void EnableCamera()
    {
        isCameraEnabled = true;
        var mats = cameraMesh.materials;
        mats[2] = cameraEmission;
        cameraMesh.materials = mats;
        cameraSwitch.Play();
    }
}
