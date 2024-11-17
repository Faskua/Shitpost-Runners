using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorUN : MonoBehaviour
{
    public string Nombre { get => jugador.Nombre; set => jugador.Nombre = value; }
    public bool seleccionado;
    public Jugador jugador;

    public void Seleccionar(){
        seleccionado = true;
    }
}
