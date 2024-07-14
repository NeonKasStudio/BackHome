using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatamanHideTrigger : MonoBehaviour
{
    public Animator anim;
    public bool triggerIsEnabled = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggerIsEnabled)
            return;

        if(other.CompareTag("Player"))
        {
            triggerIsEnabled = false;
            FindObjectOfType<AudioManager>().PlayHorrorHit();
            anim.SetTrigger("hide");
            StartCoroutine(PlayTenseSoundCoroutine());
        }
    }

    IEnumerator PlayTenseSoundCoroutine()
    {
        yield return new WaitForSeconds(0.35f);
        FindObjectOfType<AudioManager>().PlayTenseSound();
    }
}
