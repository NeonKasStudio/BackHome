using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Toilet : BaseInteractable
{

    public AudioSource peeingSound;
    public Transform spawnPoint;
    public GameObject Coin;
    public AudioSource spawnCoinSound;
    public bool isTheFirstTime = true;
    public GameObject coinToSpawn;

    // Start is called before the first frame update
    public override void DisplayInteractionText()
    {

        interactionText.text = "E | Interact";

    }

    // Update is called once per frame
    public override void PerformAction()
    {

        if(!peeingSound.isPlaying) {
            peeingSound.Play();
        }
        if(isTheFirstTime)
        {
            if (!spawnCoinSound.isPlaying)
                spawnCoinSound.Play();

            coinToSpawn.gameObject.SetActive(true);
        }
    }
}

