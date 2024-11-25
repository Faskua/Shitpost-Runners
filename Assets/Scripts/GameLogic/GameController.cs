using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int Turn;
    public int ObtainedLetters;
    public GameObject Players;
    public Text PlayerName;
    public GameObject LetterPrefab;
    public GameObject PlayerLetters;
    public InputField Xcoord;
    public InputField Ycoord;
    public List<JugadorUN> Jugadores;
    public LaberintoUN Maze;
    public Text CasillaNombre;

    
    public bool ControlarJugada(int ficha, int fila, int columna){
        if(Jugadores[Turn].jugador.Jugar(ficha, fila, columna, this)){ //mover la ficha desde la logica
            
            Jugadores[Turn].FichasUN[ficha].transform.position = Maze.LabGameObj[fila,columna].transform.position; //mover la ficha de forma visual
            
            CasillaNombre.text = Maze.LaberinthCSharp[fila,columna].Tipo.ToString();
            Maze.LabGameObj[fila, columna].GetComponent<CanvasGroup>().alpha = 1;
            return true;
        }  
        return false;
    }

    public void AvanzarTurno(){
        if(ObtainedLetters == 8) return; //Condicion de victoria no implementada aun, cambio a la escena de victoria con el mondongo
        Turn++;
        if(Turn >= Jugadores.Count) Turn = 0; 
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
        for (int hijo = 0; hijo < PlayerLetters.transform.childCount; hijo++){  Destroy(PlayerLetters.transform.GetChild(hijo).gameObject);  }
        for (int letter = 0; letter < Jugadores[Turn].jugador.LetrasConseguidas.Count; letter++)
        {
            GameObject letra = Instantiate(LetterPrefab, new Vector2(0,0), Quaternion.identity);
            letra.GetComponent<Text>().text = Jugadores[Turn].jugador.LetrasConseguidas[letter].ToString();
            letra.transform.SetParent(PlayerLetters.transform, false);
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
            Players.transform.GetChild(i).GetComponent<JugadorUN>().GenerateFichasUN(this);  //genero las del unity
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
                ficha.gameObject.GetComponent<CanvasGroup>().alpha = 0.1f;//transparenta las del resto
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
            while(Maze.LaberinthCSharp[fila,columna].Tipo == Casilla.Obstáculo || Maze.LaberinthCSharp[fila,columna].Tipo == Casilla.LetraMondongo || Maze.LaberinthCSharp[fila,columna].FichasEnCasilla.Count >= 1){
                fila = random.Next(0,15);
                columna = random.Next(0,15);
            }
            ficha.posicion = (fila, columna);
            player.jugador.Fichas.Add(ficha);
        }
    }
}
