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
    public InputField Xcoord;
    public InputField Ycoord;
    public List<JugadorUN> Jugadores;
    public LaberintoUN Maze;
    
    public bool ControlarJugada(int ficha, int fila, int columna){
        if(Jugadores[Turn].jugador.LeToca) {
            if(Jugadores[Turn].jugador.Jugar(ficha, fila, columna, this))     return true;
        }
        return false;
    }

    public void AvanzarTurno(){
        Jugadores[Turn].jugador.LeToca = false;
        Turn++;
        if(Turn >= Jugadores.Count) Turn = 0; 
        Jugadores[Turn].jugador.LeToca = true;
        SetName();
        TransparentarFichas();
        foreach (var jugador in Jugadores)
        {
            if(jugador.jugador.TurnosSinJugar > 0) jugador.jugador.TurnosSinJugar--;
            foreach (var ficha in jugador.jugador.Fichas)
            {
                if(ficha.enfriamiento > 0) ficha.enfriamiento--; 
                if(ficha.turnosSinJugar > 0) ficha.turnosSinJugar--;
            }
        }
    }

    public void SetName(){
        PlayerName.text = Jugadores[Turn].Nombre;
    }

    public void GenerarJugadores(){
        Jugadores = new List<JugadorUN>();
        for (int i = 0; i < Players.transform.childCount; i++)
        {
            GenerarFichas(Players.transform.GetChild(i).GetComponent<JugadorUN>()); //le genero 5 fichas al azar
            Players.transform.GetChild(i).GetComponent<JugadorUN>().GenerateFichasUN();  //genero las del unity
            Jugadores.Add(Players.transform.GetChild(i).GetComponent<JugadorUN>());  // lo annado a los jugadores del game controller
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
                ficha.gameObject.GetComponent<CanvasGroup>().alpha = 0.2f;//transparenta las del resto
            }
        }
    }


    void Start(){
        Turn = 0;
        Maze = GameObject.FindGameObjectWithTag("Maze").GetComponent<LaberintoUN>();
    }

    void GenerarFichas(JugadorUN player){
        player.jugador.Fichas.Clear();
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
                    player.jugador.Fichas.Add(new CJ(player.jugador));
                    fichasusadas.Add(2);
                break;
                case 3:
                    player.jugador.Fichas.Add(new UNE(player.jugador));
                    fichasusadas.Add(3);
                break;
                case 4:
                    player.jugador.Fichas.Add(new Knuckles(player.jugador));
                    fichasusadas.Add(4);
                break;
                case 5:
                    player.jugador.Fichas.Add(new RickRoll(player.jugador));
                    fichasusadas.Add(5);
                break;
                case 6:
                    player.jugador.Fichas.Add(new StarMan(player.jugador));
                    fichasusadas.Add(6);
                break;
                case 7:
                    player.jugador.Fichas.Add(new Doge(player.jugador));
                    fichasusadas.Add(7);
                break;
                case 8:
                    player.jugador.Fichas.Add(new ELChoco(player.jugador));
                    fichasusadas.Add(8);
                break;
                default:
                    player.jugador.Fichas.Add(new McQueen(player.jugador));
                    fichasusadas.Add(1);
                break;
            }
        }
    }

}
