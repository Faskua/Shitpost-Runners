using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutos : MonoBehaviour
{
    public GameObject general;
    public GameObject casilla;
    public GameObject ficha;

    public void General(){
        casilla.SetActive(false);
        ficha.SetActive(false);
        general.SetActive(true);
    }
    public void Casilla(){
        casilla.SetActive(true);
        ficha.SetActive(false);
        general.SetActive(false);
    }
    public void Ficha(){
        casilla.SetActive(false);
        ficha.SetActive(true);
        general.SetActive(false);
    }
}
