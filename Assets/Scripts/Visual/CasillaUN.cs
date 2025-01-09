using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CasillaUN : MonoBehaviour
{
    public ICasilla Casilla;
    public int Fila;
    public int Columna;
    public Casilla Tipo => Casilla.Tipo;
    public List<GameObject> Fichas = new List<GameObject>();


    public void Accion(GameController controller){
        Visible();
        Casilla.Accion(controller);
        if(Casilla.Tipo is global::Casilla.LetraMondongo) GoGreen();
    }

    public void Visible(){
        Color color = gameObject.GetComponent<Image>().color;
        color.a = 1;
        gameObject.GetComponent<Image>().color = color;
    }

    void GoGreen(){
        Color color = gameObject.GetComponent<Image>().color;
        color.g = 120;
        color.b = 0;
        color.r = 0;
        gameObject.GetComponent<Image>().color = color;
    }
}
