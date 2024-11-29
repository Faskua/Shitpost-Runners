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
    public GameObject Mondongo;
    public Text PlayerName;
    public Text HabilidadDescr;
    public Text CasillaNombre;
    public GameObject LetterPrefab;
    public GameObject PlayerLetters;
    public InputField Xcoord;
    public InputField Ycoord;
    public List<JugadorUN> Jugadores;
    public LaberintoUN Maze;

    
    public bool ControlarJugada(int ficha, int fila, int columna){
        if(Jugadores[Turn].jugador.Jugar(ficha, fila, columna, this)){ //mover la ficha desde la logica
            CasillaNombre.text = Maze.LaberinthCSharp[fila,columna].Mensaje;
            if(Maze.LaberinthCSharp[fila,columna].Tipo != Casilla.LetraMondongo){
                Color color = Maze.LabGameObj[fila, columna].GetComponent<Image>().color;
                color.a = 1;
                Maze.LabGameObj[fila, columna].GetComponent<Image>().color = color;
            }

            if(Maze.LaberinthCSharp[fila,columna] is Ducha && Jugadores[Turn].jugador.Fichas[ficha].tipo != TipoFicha.ELChoco) return true;

            Jugadores[Turn].FichasUN[ficha].transform.SetParent(Maze.LabGameObj[fila,columna].transform, true);
            Jugadores[Turn].FichasUN[ficha].transform.position = Maze.LabGameObj[fila,columna].transform.position; //mover la ficha de forma visual
            
            return true;
        }  
        return false;
    }

    public void AvanzarTurno(){
        if(ObtainedLetters == 8){  //Condicion de victoria 
            Mondongo.SetActive(true);
            //Mondongo.GetComponent<AudioSource>().Play();
            int mayor = 0;
            int ind = 0;
            for (int i = 0; i < Jugadores.Count; i++)
            {
                if(Jugadores[i].jugador.LetrasConseguidas.Count > mayor){
                    mayor = Jugadores[i].jugador.LetrasConseguidas.Count;
                    ind = i;
                }
            }
            Debug.Log($"EL ganador es {Jugadores[ind].jugador.Nombre}");
            return;
        }
        Turn++;
        if(Turn >= Jugadores.Count) Turn = 0; 
        SetName();
        TransparentarFichas();
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
        
        for (int hijo = 0; hijo < PlayerLetters.transform.childCount; hijo++){  Destroy(PlayerLetters.transform.GetChild(hijo).gameObject);  }//destruir las letras del jugador anterior
        for (int letter = 0; letter < Jugadores[Turn].jugador.LetrasConseguidas.Count; letter++)
        { //instanciar las del jugador actual
            GameObject letra = Instantiate(LetterPrefab, new Vector2(0,0), Quaternion.identity);
            letra.GetComponent<Text>().text = Jugadores[Turn].jugador.LetrasConseguidas[letter].ToString();
            letra.transform.SetParent(PlayerLetters.transform, false);
        }

        HabilidadDescr.text = "";
        CasillaNombre.text = "";
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
                ficha.gameObject.GetComponent<CanvasGroup>().alpha = 0.2f;//transparenta las del resto
            }
        }
    }


    void Start(){
        Turn = 0;
        Maze = GameObject.FindGameObjectWithTag("Maze").GetComponent<LaberintoUN>();
        Mondongo.SetActive(false);
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
