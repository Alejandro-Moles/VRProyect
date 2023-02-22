using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Texto_Camara : MonoBehaviour
{
    private Camera cameraLookAt;

    void Start()
    {
        cameraLookAt = Camera.main;
    }

    void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(new Vector3(cameraLookAt.transform.forward.x, 1, cameraLookAt.transform.forward.z));
    }
}
