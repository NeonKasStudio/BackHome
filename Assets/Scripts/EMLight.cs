using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMLight : MonoBehaviour
{
    public MeshRenderer mesh;
    public Material emissiveMat;

    public void EnableEMLight()
    {
        var mats = mesh.materials;
        mats[0] = emissiveMat;
        mesh.materials = mats;
    }
}
