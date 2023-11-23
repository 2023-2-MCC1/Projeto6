using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemCollector : MonoBehaviour
{
    private int Thermometer = 0;
    [SerializeField] private Text ThermometerText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Thermometer"))
        {
            Destroy(collision.gameObject);
            Thermometer++;
            ThermometerText.text = "" + Thermometer;
        }
    }
}
