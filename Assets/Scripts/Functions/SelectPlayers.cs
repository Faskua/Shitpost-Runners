using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectPlayers : MonoBehaviour
{
    public GameObject Blue;
    public GameObject Red;
    public GameObject Yellow;
    public GameObject Green;
    public GameObject Characters;
    public GameObject Players;
    public InputField Name;
    public TMP_Dropdown drop;

    public void Crear(){
        if(Blue.GetComponent<JugadorUN>().jugador == null && Blue.GetComponent<JugadorUN>().seleccionado){
            Mover(Blue, TipoJugador.Azul);
        }
        else if(Red.GetComponent<JugadorUN>().jugador == null && Red.GetComponent<JugadorUN>().seleccionado){
            Mover(Red, TipoJugador.Rojo);
        }
        else if(Yellow.GetComponent<JugadorUN>().jugador == null && Yellow.GetComponent<JugadorUN>().seleccionado){
            Mover(Yellow, TipoJugador.Amarillo);
        }
        else if(Green.GetComponent<JugadorUN>().jugador == null && Green.GetComponent<JugadorUN>().seleccionado){
            Mover(Green, TipoJugador.Verde);
        }
        Name.text = "";
    }

    public void Eliminar(){
        if(Blue.GetComponent<JugadorUN>().jugador != null && Blue.GetComponent<JugadorUN>().seleccionado){
            Regresar(Blue);
        }
        else if(Red.GetComponent<JugadorUN>().jugador != null && Red.GetComponent<JugadorUN>().seleccionado){
            Regresar(Red);
        }
        else if(Yellow.GetComponent<JugadorUN>().jugador != null && Yellow.GetComponent<JugadorUN>().seleccionado){
            Regresar(Yellow);
        }
        else if(Green.GetComponent<JugadorUN>().jugador != null && Green.GetComponent<JugadorUN>().seleccionado){
            Regresar(Green);
        }
    }

    public void Mover(GameObject objetivo, TipoJugador tipo){
        int index = drop.value;
        switch (index)
        {
            case 1:
                objetivo.GetComponent<JugadorUN>().jugador = new Principiante(Name.text, tipo);
            break;
            case 2:
                objetivo.GetComponent<JugadorUN>().jugador = new Intermedio(Name.text, tipo);
            break;
            default:
                objetivo.GetComponent<JugadorUN>().jugador = new Jugador(Name.text, tipo);
            break;
        }
        
        objetivo.transform.SetParent(Players.transform, false);
        objetivo.GetComponent<JugadorUN>().seleccionado = false;
        objetivo.GetComponent<Animator>().SetTrigger("Correr");

    }
    public void Regresar(GameObject objetivo){
        objetivo.GetComponent<JugadorUN>().jugador = null;
        objetivo.transform.SetParent(Characters.transform, false);
        objetivo.GetComponent<JugadorUN>().seleccionado = false;
        objetivo.GetComponent<Animator>().SetTrigger("Caminar");
    }
}
