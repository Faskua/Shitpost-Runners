using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorUN : MonoBehaviour
{
    public string Nombre { get => jugador.Nombre; set => jugador.Nombre = value; }
    public Jugador jugador { get; set; }
    public bool seleccionado;

    public void Seleccionar(){
        seleccionado = true;
    }

    public void DannoAnim(){
        gameObject.GetComponent<Animator>().SetTrigger("Danno");
    }
}
