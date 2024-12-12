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
    public Sprite mondongo;


    public void Accion(GameController controller){
        Color color = gameObject.GetComponent<Image>().color;
        color.a = 1;
        gameObject.GetComponent<Image>().color = color;
        Casilla.Accion(controller);
    }

    public void Mondongo(){
        gameObject.GetComponent<Image>().sprite = mondongo;
    }
}
