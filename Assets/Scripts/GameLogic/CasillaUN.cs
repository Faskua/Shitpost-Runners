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
        Casilla.Accion(controller);
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        if(Casilla.Tipo == global::Casilla.LetraMondongo) gameObject.GetComponent<CanvasGroup>().alpha = 0.3f;
    }

    public void Mondongo(){
        gameObject.GetComponent<Image>().sprite = mondongo;
    }
}
