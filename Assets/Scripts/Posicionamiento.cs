using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Posicionamiento : MonoBehaviour
{
    public Transform StartPoint;
    public Transform PlayPoint;
    public Transform XROrigin;


    private void Start()
    {
        XROrigin.position = StartPoint.position;
    }

    public void goPlayPoint()
    {
        XROrigin.position = PlayPoint.position; 
    }
}
