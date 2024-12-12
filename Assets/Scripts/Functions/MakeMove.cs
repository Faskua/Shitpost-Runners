using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeMove : MonoBehaviour
{
    public InputField x;
    public InputField y;
    private GameController controller;
    public void Make(){
        for (int ficha = 0; ficha < 5; ficha++)
        {
            if(controller.Jugadores[controller.Turn].jugador.Fichas[ficha].seleccionada){
                if(!controller.ControlarJugada(ficha, int.Parse(y.text), int.Parse(x.text))){
                    gameObject.GetComponent<Animator>().SetTrigger("Temblar");
                    controller.Jugadores[controller.Turn].jugador.Fichas[ficha].seleccionada = false;
                }
                else {
                    controller.Jugadores[controller.Turn].jugador.Fichas[ficha].seleccionada = false;
                    return;
                }
            }    
        }
    }

    void Start(){
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
}
