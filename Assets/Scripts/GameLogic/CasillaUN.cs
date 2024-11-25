using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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
        Casilla.Accion(controller);
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
    }
}
