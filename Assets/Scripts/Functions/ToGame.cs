using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToGame : MonoBehaviour
{
    public GameObject Creador;
    public GameObject Players;
    public GameObject PlayersHolder;
    private GameController controller;

    void Start(){
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    public void Game(){
        Players.transform.SetParent(PlayersHolder.transform, true);
        Players.transform.position = PlayersHolder.transform.position;
        Creador.SetActive(false);
        controller.GenerarJugadores();
        controller.SetName();
        //controller.InstanciarFichas();
        controller.TransparentarFichas();
    }
}
