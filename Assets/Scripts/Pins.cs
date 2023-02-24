using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class Pins : MonoBehaviour
{
    public GameObject[] pins;
    private float thereshold = 0.6f;
    private int fallen;
    Pin_Object pin_object;

    public GameObject[] posPin;

    public TextMeshProUGUI puntuacion, tiradas;
    private float numTiradas = 0;

    public GameObject PinPrefab, newPosBola;

    private MeshRenderer mesh;
    private Rigidbody rb; 


    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        DetectarBolos();
    }

    public void DetectarBolos()
    {
        foreach (GameObject pin in pins)
        {
            if(pin != null)
            {
                if (pin.GetComponent<Transform>().up.y < thereshold)
                {
                    pin_object = pin.GetComponent<Pin_Object>();

                    if (!pin_object.isPinFall)
                    {
                        pin_object.isPinFall = true;
                        fallen++;
                        puntuacion.text = "Tus Puntos: " + fallen.ToString();
                    }
                }
            }
        }  
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.CompareTag("TriggerBolos"))
        {
            SumPoints();
            StartCoroutine(InstanciateBola(3f));
        }

        if (other.gameObject.CompareTag("ReturnBola"))
        {
            SumPoints();
            StartCoroutine(InstanciateBola(1f));
        }

        if (other.gameObject.CompareTag("Limite"))
        {
            Debug.Log("Impulso");
            rb.AddForce(0,0,-700, ForceMode.Impulse);
        }
    }

    private IEnumerator DestruirBolos()
    {
        yield return new WaitForSeconds(5f);
        foreach (GameObject pin in pins)
        {
            if (pin.GetComponent<Pin_Object>().isPinFall)
            {
                Destroy(pin, 0.5f);
            }
 
        }
    }

    private IEnumerator PosicionarBolos()
    {
        yield return new WaitForSeconds(1f);
        foreach (GameObject pin in pins)
        {
            Destroy(pin);
        }

        pins = new GameObject[10];
        fallen = 0;


        yield return new WaitForSeconds(2f);
        numTiradas = 0;
        tiradas.text = "Tiradas : " + numTiradas.ToString();
        puntuacion.text = "Tus Puntos: " + fallen.ToString();
        for (int i = 0; i < posPin.Length; i++)
        {
            GameObject newpin = Instantiate(PinPrefab, posPin[i].transform.position, Quaternion.identity);
            pins[i] = newpin;
        }
    }

    private void SumPoints()
    {
        numTiradas++;

        if (numTiradas < 2)
        {
            tiradas.text = "Tiradas : " + numTiradas.ToString();
            StartCoroutine(DestruirBolos());
        }

        if (numTiradas >= 2)
        {
            tiradas.text = "Tiradas : " + numTiradas.ToString();
            StartCoroutine(PosicionarBolos());
        }
    }

    private IEnumerator InstanciateBola(float time)
    {
        yield return new WaitForSeconds(time);
        mesh.enabled = false;

        yield return new WaitForSeconds(1f);
        this.transform.position = newPosBola.transform.position;
        mesh.enabled = true;
    }
}



