using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Vector3 localRotation;
    public Vector3 worldRotation;
    public bool local;
    public bool world;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.localEulerAngles);
        Debug.Log(transform.eulerAngles);
    }
}
