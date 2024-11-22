using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int Turn;
    public GameObject Players;
    public Text PlayerName;
    public GameObject PlayerLetters;
    public GameObject PlayerPiecesHolder;
    public GameObject FichasPrefab;
    public InputField Xcoord;
    public InputField Ycoord;
    public List<Jugador> Jugadores; //tiene que ser de JugadorUN
    public LaberintoUN Maze;

    public Sprite McQueen;
    public Sprite CJ;
    public Sprite UNE;
    public Sprite Knuckles;
    public Sprite Rick;
    public Sprite Starman;
    public Sprite Dogue;
    public Sprite Choco;
    
    public bool ControlarJugada(int ficha, int fila, int columna){
        if(Jugadores[Turn].LeToca) {
            if(Jugadores[Turn].Jugar(ficha, fila, columna, this))     return true;
        }
        return false;
    }

    public void AvanzarTurno(){
        Jugadores[Turn].LeToca = false;
        Turn++;
        if(Turn >= Jugadores.Count) Turn = 0; 
        Jugadores[Turn].LeToca = true;
        SetName();
        foreach (var jugador in Jugadores)
        {
            if(jugador.TurnosSinJugar > 0) jugador.TurnosSinJugar--;
            foreach (var ficha in jugador.Fichas)
            {
                if(ficha.enfriamiento > 0) ficha.enfriamiento--; 
            }
        }
    }

    public void SetName(){
        PlayerName.text = Jugadores[Turn].Nombre;
    }

    public void GenerarJugadores(){
        Jugadores = new List<Jugador>();
        for (int i = 0; i < Players.transform.childCount; i++)
        {
            Players.transform.GetChild(i).GetComponent<JugadorUN>().jugador.Fichas = GenerarFichas(Players.transform.GetChild(i).GetComponent<JugadorUN>()); //le genero 5 fichas al azar
            Jugadores.Add(Players.transform.GetChild(i).GetComponent<Jugador>());
        }
    }

    List<Ficha> GenerarFichas(JugadorUN player){
        List<Ficha> fichas = new List<Ficha>();
        List<int> fichasusadas = new List<int>();
        System.Random random = new System.Random();
        for (int i = 0; i < 5; i++)
        {
            int chance = random.Next(1, 9);
            while(fichasusadas.Contains(chance)) {
                chance = random.Next(1, 9);
            }
            
            switch(chance){
                case 2:
                    fichas.Add(new CJ(player.jugador));
                    fichasusadas.Add(2);
                break;
                case 3:
                    fichas.Add(new UNE(player.jugador));
                    fichasusadas.Add(3);
                break;
                case 4:
                    fichas.Add(new Knuckles(player.jugador));
                    fichasusadas.Add(4);
                break;
                case 5:
                    fichas.Add(new RickRoll(player.jugador));
                    fichasusadas.Add(5);
                break;
                case 6:
                    fichas.Add(new StarMan(player.jugador));
                    fichasusadas.Add(6);
                break;
                case 7:
                    fichas.Add(new Doge(player.jugador));
                    fichasusadas.Add(7);
                break;
                case 8:
                    fichas.Add(new ELChoco(player.jugador));
                    fichasusadas.Add(8);
                break;
                default:
                    fichas.Add(new McQueen(player.jugador));
                    fichasusadas.Add(1);
                break;
            }
        }
        return fichas;
    }

    public void InstanciarFichas(){
        foreach (var jugador in Jugadores)
        {
            foreach (var ficha in jugador.FichasUN)
            {
                GameObject instance = Instantiate(FichasPrefab, new Vector2(0,0), Quaternion.identity);
                if(ficha.Tipo == TipoFicha.Mcqueen) instance.GetComponent<Image>().sprite = McQueen;
                if(ficha.Tipo == TipoFicha.CJ) instance.GetComponent<Image>().sprite = CJ;
                if(ficha.Tipo == TipoFicha.UNE) instance.GetComponent<Image>().sprite = UNE;
                if(ficha.Tipo == TipoFicha.Knuckles) instance.GetComponent<Image>().sprite = Knuckles;
                if(ficha.Tipo == TipoFicha.RickRoll) instance.GetComponent<Image>().sprite = Rick;
                if(ficha.Tipo == TipoFicha.StarMan) instance.GetComponent<Image>().sprite = Starman;
                if(ficha.Tipo == TipoFicha.Doge) instance.GetComponent<Image>().sprite = Dogue;
                if(ficha.Tipo == TipoFicha.ELChoco) instance.GetComponent<Image>().sprite = Choco;
                instance.transform.SetParent(PlayerPiecesHolder.transform, false);
            }
        }
    }

    public void TransparentarFichas(){
        foreach (var ficha in Jugadores[Turn].FichasUN)
        {
            ficha.gameObject.GetComponent<CanvasGroup>().alpha = 1; //vuelve completamente visibles las fichas del jugador actual
        }
        for (int jugador = 0; jugador < Jugadores.Count; jugador++)
        {
            if(jugador == Turn) continue;
            foreach (var ficha in Jugadores[jugador].FichasUN)
            {
                ficha.gameObject.GetComponent<CanvasGroup>().alpha = 0.4f;//transparenta las del resto
            }
        }
    }

    void Start(){
        Turn = 0;
        Maze = GameObject.FindGameObjectWithTag("Maze").GetComponent<LaberintoUN>();
    }

}
