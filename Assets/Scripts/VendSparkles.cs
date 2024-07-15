using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendSparkles : MonoBehaviour
{
    public ParticleSystem sparkles;
    public VendingMachine vending;

    public void PlayParticles()
    {
        sparkles.Play();
        vending.DisableVendingMachine(false, false);
        StartCoroutine(DisableVendingMachine());
    }


    private IEnumerator DisableVendingMachine()
    {
        yield return new WaitForSeconds(0.2f);
        vending.DisableVendingMachine(false, true);

    }
}
