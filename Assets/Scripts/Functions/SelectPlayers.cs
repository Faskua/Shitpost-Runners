using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPlayers : MonoBehaviour
{
    public GameObject Blue;
    public GameObject Red;
    public GameObject Yellow;
    public GameObject Green;
    public GameObject Selected;
    public GameObject Characters;
    public InputField Name;

    public void Crear(){
        if(Blue.GetComponent<JugadorUN>().jugador == null && Blue.GetComponent<JugadorUN>().seleccionado){
            Mover(Blue);
            Blue.GetComponent<Animator>().SetTrigger("Correr");
            Blue.GetComponent<JugadorUN>().seleccionado = false;
        }
        else if(Red.GetComponent<JugadorUN>().jugador == null && Red.GetComponent<JugadorUN>().seleccionado){
            Mover(Red);
            Red.GetComponent<Animator>().SetTrigger("Correr");
            Red.GetComponent<JugadorUN>().seleccionado = false;
        }
        else if(Yellow.GetComponent<JugadorUN>().jugador == null && Yellow.GetComponent<JugadorUN>().seleccionado){
            Mover(Yellow);
            Yellow.GetComponent<Animator>().SetTrigger("Correr");
            Yellow.GetComponent<JugadorUN>().seleccionado = false;
        }
        else if(Green.GetComponent<JugadorUN>().jugador == null && Green.GetComponent<JugadorUN>().seleccionado){
            Mover(Green);
            Green.GetComponent<Animator>().SetTrigger("Correr");
            Green.GetComponent<JugadorUN>().seleccionado = false;
        }
        Name.text = "";
    }

    public void Eliminar(){
        if(Blue.GetComponent<JugadorUN>().jugador != null && Blue.GetComponent<JugadorUN>().seleccionado){
            Regresar(Blue);
            Blue.GetComponent<Animator>().SetTrigger("Caminar");
            Blue.GetComponent<JugadorUN>().seleccionado = false;
        }
        else if(Red.GetComponent<JugadorUN>().jugador != null && Red.GetComponent<JugadorUN>().seleccionado){
            Regresar(Red);
            Red.GetComponent<Animator>().SetTrigger("Caminar");
            Red.GetComponent<JugadorUN>().seleccionado = false;
        }
        else if(Yellow.GetComponent<JugadorUN>().jugador != null && Yellow.GetComponent<JugadorUN>().seleccionado){
            Regresar(Yellow);
            Yellow.GetComponent<Animator>().SetTrigger("Caminar");
            Yellow.GetComponent<JugadorUN>().seleccionado = false;
        }
        else if(Green.GetComponent<JugadorUN>().jugador != null && Green.GetComponent<JugadorUN>().seleccionado){
            Regresar(Green);
            Green.GetComponent<Animator>().SetTrigger("Caminar");
            Green.GetComponent<JugadorUN>().seleccionado = false;
        }
    }

    public void Mover(GameObject objetivo){
        objetivo.GetComponent<JugadorUN>().jugador = new Jugador(Name.text);
        objetivo.transform.SetParent(Selected.transform, false);
    }
    public void Regresar(GameObject objetivo){
        objetivo.GetComponent<JugadorUN>().jugador = null;
        objetivo.transform.SetParent(Characters.transform, false);
    }
}
