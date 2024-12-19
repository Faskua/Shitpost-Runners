using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int Turn;
    public int ObtainedLetters;
    public GameObject Players;
    public InputField Xcoord;
    public InputField Ycoord;
    public List<JugadorUN> Jugadores;
    public LaberintoUN Maze;
    public VisualController visual;
    private AudioController Music;

    
    public bool ControlarJugada(int ficha, int fila, int columna){
        if(Jugadores[Turn].jugador.Jugar(ficha, fila, columna, this)){ //mover la ficha desde la logica
            visual.MoverFicha(this, ficha, fila, columna); //moverla en el visual
            return true;
        }  
        return false;
    }

    public void AvanzarTurno(){
        if(ObtainedLetters == 8){  //Condicion de victoria 
            visual.Mondongo();
            Music.Mondongo();
            int mayor = 0;
            int ind = 0;
            for (int i = 0; i < Jugadores.Count; i++)
            {
                if(Jugadores[i].jugador.LetrasConseguidas.Count > mayor){
                    mayor = Jugadores[i].jugador.LetrasConseguidas.Count;
                    ind = i;
                }
            }
            visual.Ganador(Jugadores[ind].jugador.Nombre);
            return;
        }
        Turn++;
        if(Turn >= Jugadores.Count) Turn = 0; 
        if(Turn == 0){
            foreach (var jugador in Jugadores)
            {
                if(jugador.jugador.TurnosSinJugar > 0) jugador.jugador.TurnosSinJugar--;
                foreach (var ficha in jugador.jugador.Fichas)
                {
                    if(ficha.EnfActual > 0) ficha.EnfActual--; 
                    if(ficha.turnosSinJugar > 0) ficha.turnosSinJugar--;
                }
            }
        }
        
        visual.AvanzarTurno();
    }

    public void GenerarJugadores(){
        Jugadores = new List<JugadorUN>();
        for (int i = 0; i < Players.transform.childCount; i++)
        {
            GenerarFichas(Players.transform.GetChild(i).GetComponent<JugadorUN>()); //le genero 5 fichas al azar
            Players.transform.GetChild(i).GetComponent<JugadorUN>().GenerateFichasUN(this);  //genero las del unity
            Jugadores.Add(Players.transform.GetChild(i).GetComponent<JugadorUN>());  // lo annado a los jugadores del game controller
        }
    }

    void Awake(){
    }

    void Start(){
        Turn = 0;
        Maze = GameObject.FindGameObjectWithTag("Maze").GetComponent<LaberintoUN>();
        Music = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioController>();
        
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
            Ficha ficha = new CJ(player.jugador);
            switch(chance){
                case 2:
                    ficha = new CJ(player.jugador);
                    fichasusadas.Add(2);
                break;
                case 3:
                    ficha = new UNE(player.jugador);;
                    fichasusadas.Add(3);
                break;
                case 4:
                    ficha = new Knuckles(player.jugador);;
                    fichasusadas.Add(4);
                break;
                case 5:
                    ficha = new RickRoll(player.jugador);;
                    fichasusadas.Add(5);
                break;
                case 6:
                    ficha = new StarMan(player.jugador);;
                    fichasusadas.Add(6);
                break;
                case 7:
                    ficha = new Doge(player.jugador);;
                    fichasusadas.Add(7);
                break;
                case 8:
                    ficha = new ELChoco(player.jugador);;
                    fichasusadas.Add(8);
                break;
                default:
                    ficha = new McQueen(player.jugador);;
                    fichasusadas.Add(1);
                break;
            }
            int fila = random.Next(0,15);
            int columna = random.Next(0,15);
            while(Maze.LaberinthCSharp[fila,columna].Tipo == Casilla.Obstaculo || Maze.LaberinthCSharp[fila,columna].Tipo == Casilla.LetraMondongo || Maze.LaberinthCSharp[fila,columna].FichasEnCasilla.Count >= 1){
                fila = random.Next(0,15);
                columna = random.Next(0,15);
            }
            ficha.posicion = (fila, columna);
            player.jugador.Fichas.Add(ficha);
        }
    }
}
