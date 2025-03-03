using System.Collections.Generic;
using System;
using UnityEngine;
public class Jugador : IJugador
{
    public int TurnosSinJugar { get; set; }
    public List<Ficha> Fichas { get; set;}
    public string Nombre { get; set; }
    public TipoJugador Tipo { get; set; }
    public List<char> LetrasConseguidas { get; set; }

    public Jugador(string nombre, TipoJugador tipo){
        Fichas = new List<Ficha>();
        Nombre = nombre;
        Tipo = tipo;
        LetrasConseguidas = new List<char>();
        TurnosSinJugar = 0;
    }

    public bool Jugar( ref int ficha, ref  int fila, ref int columna, GameController controller){ 
        if(TurnosSinJugar == 0){
            return Fichas[ficha].Jugar(fila, columna, controller);
        }
        return false;
    }
}

class Principiante : IJugador
{
    public int TurnosSinJugar { get; set; }
    public List<Ficha> Fichas { get; set;}
    public string Nombre { get; set; }
    public TipoJugador Tipo { get; set; }
    public List<char> LetrasConseguidas { get; set; }

    public Principiante(string nombre, TipoJugador tipo){
        Fichas = new List<Ficha>();
        Nombre = nombre;
        Tipo = tipo;
        LetrasConseguidas = new List<char>();
        TurnosSinJugar = 0;
    }

    public bool Jugar( ref int ficha, ref  int fila, ref int columna, GameController controller){ 
        if(TurnosSinJugar != 0) return false;
        System.Random random = new System.Random();
        ficha = random.Next(0, Fichas.Count);
        int contador = 0;
        while(Fichas[ficha].turnosSinJugar != 0 && contador < 15){ //eligiendo la ficha a usar
            ficha = random.Next(0, Fichas.Count);
            contador++;
        }    
        if(contador >= 15) return false;  //no se puede usar ninguna ficha

        fila = Fichas[ficha].posicion.Item1;
        columna = Fichas[ficha].posicion.Item2;
        int vel = random.Next(1, Fichas[ficha].velocidad+1);

        ElegirCasilla(vel, controller, ref fila, ref columna);

        if(Fichas[ficha]. EnfActual == 0){
            Fichas[ficha].Habilidad(controller);
            controller.visual.SetHabilidad(Fichas[ficha].HabilidadDescrp);
        } 
        
        return Fichas[ficha].Jugar(fila, columna, controller);
    }

    public void ElegirCasilla(int pasos, GameController controller, ref int fila, ref int col){
        System.Random random = new System.Random();
        int filaAct = fila;
        int colAct = col;
        for (int i = pasos; i > 0 ; i--)
        {
            do{
                int direction = random.Next(0, 4);
                filaAct = fila;
                colAct = col;
                switch (direction)
                {
                    case 0:
                        filaAct++;
                        break;
                    case 1:
                        colAct++;
                        break;
                    case 2:
                        filaAct--;
                        break;
                    default:
                        colAct--;
                        break;
                }
                //Debug.Log($"({colAct}, {filaAct})");
            }
            while(filaAct > 14 || filaAct < 0 || colAct > 14 || colAct < 0 || controller.Maze.LaberinthCSharp[filaAct,colAct].Tipo == Casilla.Obstaculo);
            fila = filaAct;
            col = colAct;
        }
    }
}