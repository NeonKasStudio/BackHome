using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashLogic : MonoBehaviour
{
    public AudioSource clapping;
    public GameObject Coin;
    public Transform coinSpawnPoint;
    private void OnTriggerEnter(Collider other)
    {
      

            

            if(other.GetComponent<BaseGrabable>() as Can)
            {
                if (!clapping.isPlaying)
                    clapping.Play();

                    GameObject coin = Instantiate(Coin, coinSpawnPoint.position, coinSpawnPoint.rotation);
                    Coin g_coin = coin.GetComponent<Coin>();
                    g_coin.interactionText = InteractionManager.Instance.interactionText;
                    g_coin.PlayCoinAudio();
                    Destroy(other.gameObject);
                    InteractionManager.Instance.shouldInteract = true;
                    InteractionManager.Instance.objectHasBeenThrow = false;

                }

            }
}
