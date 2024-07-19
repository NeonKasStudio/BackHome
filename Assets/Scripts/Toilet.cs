using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Toilet : BaseInteractable
{

    public AudioSource peeingSound;
    public Transform spawnPoint;
    public GameObject coinPrefab;
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
            
            //GameObject coin = Instantiate(coinPrefab, spawnPoint.position, spawnPoint.rotation);
            //Coin g_coin = coin.GetComponent<Coin>();
            //g_coin.interactionText = InteractionManager.Instance.interactionText;
            //g_coin.PlayCoinAudio();
            isTheFirstTime=false;   
            coinToSpawn.gameObject.SetActive(true);
            var coin = coinToSpawn.GetComponent<Coin>();
            coin.interactionText = InteractionManager.Instance.interactionText;
            coin.PlayCoinAudio();
        }
    }
}

