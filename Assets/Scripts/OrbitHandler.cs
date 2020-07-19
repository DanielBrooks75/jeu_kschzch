using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitHandler : MonoBehaviour
{
    public bool trigoRot = false;
    [Range (1f,10f)]
    public float speed = 8f;
    [Range (4,32)]
    public int segments = 16;
    public GameObject rotCenter;

    private float [] xPath = null;
    private float [] zPath = null;

    void GenerateOrbitalPath()
    {
        ////
    }
}
