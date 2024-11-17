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
    public bool Visitada { get => Casilla.Visitada; set => Casilla.Visitada = value;}


    public void Accion(){
        Casilla.Accion();
    }
}
