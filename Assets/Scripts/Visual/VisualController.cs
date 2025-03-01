using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualController : MonoBehaviour
{
    public GameObject mondongo;
    public Text PlayerName;
    public Text HabilidadDescr;
    public Text CasillaNombre;
    public GameObject ganador;
    public GameObject LetterPrefab;
    public GameObject PlayerLetters;
    public GameController controller;
    private int Ficha;
    private int Fila;
    private int Columna;

    public VisualController(GameController contr){
        controller = contr;
    }

    public void MoverFicha(GameController controller, int ficha, int fila, int columna){
        this.controller = controller;
        Ficha = ficha;
        Fila = fila;
        Columna = columna;
        CasillaNombre.text = controller.Maze.LaberinthCSharp[fila,columna].Mensaje;

        if(controller.Maze.LaberinthCSharp[fila,columna] is AmongUs && controller.Jugadores[controller.Turn].jugador.Fichas[ficha].tipo != TipoFicha.ELChoco) return;

        controller.Jugadores[controller.Turn].FichasUN[ficha].GetComponent<Animator>().SetTrigger("Animation");
        controller.Jugadores[controller.Turn].FichasUN[ficha].GetComponent<AudioSource>().Play();
        Invoke("movimiento", 0.2f);        
    }

    public void movimiento(){
        controller.Jugadores[controller.Turn].FichasUN[Ficha].transform.SetParent(controller.Maze.LabGameObj[Fila,Columna].transform, false);
        controller.Jugadores[controller.Turn].FichasUN[Ficha].transform.position = controller.Maze.LabGameObj[Fila,Columna].transform.position;
    }

    public void AvanzarTurno(){
        SetName();
        TransparentarFichas();
        ActualizarLetra();
        CasillaNombre.text = "";
        HabilidadDescr.text = "";
    }

    public void Mondongo(){
        mondongo.SetActive(true);
    }

    public void Ganador(string nombre){
        ganador.GetComponent<Text>().text = $"{nombre} ha ganado";
        ganador.SetActive(true);
    }

    public void SetName(){
        PlayerName.text = controller.Jugadores[controller.Turn].Nombre;
    }

    public void SetHabilidad(string text){
        HabilidadDescr.text = text;
    }

    public void TransparentarFichas(){
        foreach (var ficha in controller.Jugadores[controller.Turn].FichasUN)
        {
            ficha.gameObject.GetComponent<CanvasGroup>().alpha = 1; //vuelve completamente visibles las fichas del jugador actual
            ficha.gameObject.GetComponent<CanvasGroup>().interactable = true;
            ficha.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        for (int jugador = 0; jugador < controller.Jugadores.Count; jugador++)
        {
            if(jugador == controller.Turn) continue;
            foreach (var ficha in controller.Jugadores[jugador].FichasUN)
            {
                ficha.gameObject.GetComponent<CanvasGroup>().alpha = 0.2f;//transparenta las del resto
                ficha.gameObject.GetComponent<CanvasGroup>().interactable = false;
                ficha.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }
    }

    public void InstanciarLetra(char Letra){
        GameObject letra = Instantiate(LetterPrefab, new Vector2(0,0), Quaternion.identity);
        letra.GetComponent<Text>().text = Letra.ToString();
        letra.transform.SetParent(PlayerLetters.transform, false);
    }

    public void ActualizarLetra(){
        for (int hijo = 0; hijo < PlayerLetters.transform.childCount; hijo++){  Destroy(PlayerLetters.transform.GetChild(hijo).gameObject);  }//destruir las letras del jugador anterior
        for (int letter = 0; letter < controller.Jugadores[controller.Turn].jugador.LetrasConseguidas.Count; letter++)
        { //instanciar las del jugador actual
            InstanciarLetra(controller.Jugadores[controller.Turn].jugador.LetrasConseguidas[letter]);
        }
    }
    
    void Start()
    {
        mondongo.SetActive(false);
        ganador.GetComponent<Text>().text = "";
        ganador.SetActive(false);
    }
}
