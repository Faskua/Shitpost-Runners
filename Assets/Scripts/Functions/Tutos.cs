using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutos : MonoBehaviour
{
    public GameObject descripcion;
    public GameObject general;
    public GameObject general2;
    public GameObject casilla;
    public GameObject ficha;

    public void Descripcion(){
        descripcion.SetActive(true);
        casilla.SetActive(false);
        ficha.SetActive(false);
        general.SetActive(true);
        general2.SetActive(false);
    }

    public void General(){
        casilla.SetActive(false);
        ficha.SetActive(false);
        general.SetActive(true);
        general2.SetActive(false);
    }

    public void General2(){
        casilla.SetActive(false);
        ficha.SetActive(false);
        general.SetActive(false);
        general2.SetActive(true);
    }
    public void Casilla(){
        descripcion.SetActive(false);
        casilla.SetActive(true);
        ficha.SetActive(false);
        general.SetActive(false);
        general2.SetActive(false);
    }
    public void Ficha(){
        descripcion.SetActive(false);
        casilla.SetActive(false);
        ficha.SetActive(true);
        general.SetActive(false);
        general2.SetActive(false);
    }
}
