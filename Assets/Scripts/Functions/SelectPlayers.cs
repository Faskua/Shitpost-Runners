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
            Mover(Blue);
        }
        else if(Red.GetComponent<JugadorUN>().jugador == null && Red.GetComponent<JugadorUN>().seleccionado){
            Mover(Red);
        }
        else if(Yellow.GetComponent<JugadorUN>().jugador == null && Yellow.GetComponent<JugadorUN>().seleccionado){
            Mover(Yellow);
        }
        else if(Green.GetComponent<JugadorUN>().jugador == null && Green.GetComponent<JugadorUN>().seleccionado){
            Mover(Green);
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

    public void Mover(GameObject objetivo){
        int index = drop.value;
        switch (index)
        {
            case 1:
                objetivo.GetComponent<JugadorUN>().jugador = new Principiante(Name.text);
                break;
            default:
                objetivo.GetComponent<JugadorUN>().jugador = new Jugador(Name.text);
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
