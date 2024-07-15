using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : BaseInteractable
{

    public AudioSource InsertingCoinSound;
    public AudioSource KickSound;
    public Transform spawnPoint;
    public GameObject canPrefab;
    public GameObject sign;

    //Disable vending machine
    public MeshRenderer vendMesh;
    public MeshRenderer neonKasMesh;
    public Material mat1NonEmmissive;
    public Material mat2NonEmmissive;
    public Material mat3NonEmmissive;
    public AudioSource powerOff;

    private bool releasingCan = false;
    private float signCooldownTime = 3.0f;

    public bool disabledMachine = false;

    int maxNumberOfThrowingCansBeforeMachineBroken = 5;
    int maxNumberOfThrowingCansAfterMachineBroken = 12;
    int currentThrowedCans;
    bool isMachineBroken = false;

    public override void DisplayInteractionText()
    {
        if(!disabledMachine)
        interactionText.text = "E | Interact";
    }

    public override void PerformAction()
    {
        Debug.Log(isMachineBroken);
        if (isMachineBroken)
            return;

        if (!releasingCan )
        {

            if (InteractionManager.Instance.GetCurrentGrabable() as Coin)
            {

                InteractionManager.Instance.DestroyCurrentGrabable();

                if (!InsertingCoinSound.isPlaying)
                    InsertingCoinSound.Play();

                
                    StartCoroutine(ReleaseCan(5f));
            }
            else if(InteractionManager.Instance.GetCurrentGrabable() as Wrench)
            {
                if (!KickSound.isPlaying)
                    KickSound.Play();


                StartCoroutine(ReleaseAndThrow(1.5f));
            }
            else
            {
                StartCoroutine(ActivateSign());
            }


        }
    }

    public void ReleaseCanWithoutCoin()
    {
        if (!InsertingCoinSound.isPlaying)
            InsertingCoinSound.Play();

        StartCoroutine(ReleaseCan(5f));
    }

    private IEnumerator ReleaseCan(float secondsWaiting)
    {
        InteractionManager.Instance.shouldInteract = false;
        interactionText.text = string.Empty;
        releasingCan = true;

        yield return new WaitForSeconds(secondsWaiting);

        InteractionManager.Instance.shouldInteract = true;
        GameObject can = Instantiate(canPrefab, spawnPoint.position, spawnPoint.rotation);
        BaseGrabable g_can = can.GetComponent<BaseGrabable>();
        g_can.interactionText = InteractionManager.Instance.interactionText;
        releasingCan = false;
        Debug.Log("instancio");

    }
    private IEnumerator ReleaseAndThrow(float secondsWaiting)
    {
        InteractionManager.Instance.shouldInteract = false;
        interactionText.text = string.Empty;
        releasingCan = true;
    
        yield return new WaitForSeconds(secondsWaiting);

        GameObject can = Instantiate(canPrefab, spawnPoint.position, spawnPoint.rotation);
        BaseGrabable g_can = can.GetComponent<BaseGrabable>();
        g_can.Throw(5f);
        g_can.interactionText = InteractionManager.Instance.interactionText;
        StartCoroutine(TimeDesactivatingInteractions(1f));
       
        releasingCan = false;
        currentThrowedCans++;

        if (!isMachineBroken && currentThrowedCans >= maxNumberOfThrowingCansBeforeMachineBroken)
            BreakVendingMachine();

        if(isMachineBroken)
        {
            if(currentThrowedCans <= maxNumberOfThrowingCansAfterMachineBroken)
                StartCoroutine(ReleaseAndThrow(0.5f));
            else
            {
                DisableVendingMachine();
                StartCoroutine(EnableEmergencyLightsCoroutine());
            }
        }
    }

    private IEnumerator EnableEmergencyLightsCoroutine()
    {
        yield return new WaitForSeconds(2f);
        FindObjectOfType<ParanormalEventManager>().EnableEmergencyLights();

    }

    public void BreakVendingMachine()
    {
        isMachineBroken = true;
        StartCoroutine(ReleaseAndThrow(0.2f));
    }

    private IEnumerator TimeDesactivatingInteractions(float secondsWaiting)
    {
        yield return new WaitForSeconds(secondsWaiting);
        InteractionManager.Instance.shouldInteract = true;

    }

    private IEnumerator ActivateSign()
    {
        // Activar el objeto
        sign.SetActive(true);

        // Esperar por el tiempo de cooldown
        yield return new WaitForSeconds(signCooldownTime);

        // Desactivar el objeto
        sign.SetActive(false);
    }

    public void DisableVendingMachine()
    {
        if (disabledMachine)
            return;

        disabledMachine = true;

        var mats = vendMesh.materials;
        mats[1] = mat1NonEmmissive;
        mats[5] = mat2NonEmmissive;
        vendMesh.materials = mats;

        var mats2 = neonKasMesh.materials;
        mats2[0] = mat3NonEmmissive;
        neonKasMesh.materials = mats2;

        powerOff.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            DisableVendingMachine();
    }
}
