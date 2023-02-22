using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Pins : MonoBehaviour
{
    public GameObject[] pins;
    private float thereshold = 0.6f;
    private int fallen;
    Pin_Object pin_object;

    public GameObject[] posPin;

    public TextMeshProUGUI puntuacion, tiradas;
    private float numTiradas = 0;

    public GameObject PinPrefab;

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
            numTiradas++;

            if (numTiradas < 2)
            {
                Debug.Log("Has tirado una veces");
                tiradas.text = "Tiradas : " + numTiradas.ToString();
                StartCoroutine(DestruirBolos());
            }

            if(numTiradas >= 2)
            {
                Debug.Log("Has tirado dos veces");
                numTiradas = 0;
                tiradas.text = "Tiradas : " + numTiradas.ToString();
                StartCoroutine(PosicionarBolos());
            }
            
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

        puntuacion.text = "Tu puntuacion es de " + fallen;
        pins = new GameObject[10];
        fallen = 0;


        yield return new WaitForSeconds(2f);
        for(int i = 0; i < posPin.Length; i++)
        {
            GameObject newpin = Instantiate(PinPrefab, posPin[i].transform.position, Quaternion.identity);
            pins[i] = newpin;
        }
    }
}



