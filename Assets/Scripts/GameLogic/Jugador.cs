using System.Collections.Generic;
using System;
using UnityEngine;
public class Jugador : IJugador
{
    public int TurnosSinJugar { get; set; }
    public List<Ficha> Fichas { get; set;}
    public string Nombre { get; set; }
    public List<char> LetrasConseguidas { get; set; }

    public Jugador(string nombre){
        Fichas = new List<Ficha>();
        this.Nombre = nombre;
        LetrasConseguidas = new List<char>();
        TurnosSinJugar = 0;
    }

    public bool Jugar(int ficha, int fila, int columna, GameController controller){ 
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
    public List<char> LetrasConseguidas { get; set; }

    public Principiante(string nombre){
        Fichas = new List<Ficha>();
        this.Nombre = nombre;
        LetrasConseguidas = new List<char>();
        TurnosSinJugar = 0;
    }

    public bool Jugar(int ficha, int fila, int columna, GameController controller){ 
        if(TurnosSinJugar != 0) return false;
        System.Random random = new System.Random();
        int Ficha = random.Next(0, Fichas.Count);
        int contador = 0;
        while(Fichas[Ficha].turnosSinJugar != 0 && contador < 15){ //eligiendo la ficha a usar
            Ficha = random.Next(0, Fichas.Count);
            contador++;
        }    
        if(contador >= 15) return false;  //no se puede usar ninguna ficha
        int Fila = Fichas[Ficha].posicion.Item1;
        int Col = Fichas[Ficha].posicion.Item2;

        if(Fichas[Ficha]. EnfActual == 0){
            Fichas[Ficha].Habilidad(controller);
            controller.visual.SetHabilidad(Fichas[Ficha].HabilidadDescrp);
        } 

        do{     //eligiendo la casilla destino
            Fila = Fichas[Ficha].posicion.Item1;
            Col = Fichas[Ficha].posicion.Item2;

            int PasosRestantes = Fichas[Ficha].velocidad;
            int Vertical = random.Next(0, PasosRestantes+1);
            PasosRestantes -= Vertical;
            int Horizontal = random.Next(0, PasosRestantes+1);
            int direction = random.Next(0, 2);
            if(direction == 0) Vertical *= -1;
            direction = random.Next(0, 2);
            if(direction == 0) Horizontal *= -1;

            Fila += Vertical;
            Col += Horizontal;
        }
        while(Fila >= 15 || Col >= 15 || Fila < 0 || Col < 0 || controller.Maze.LaberinthCSharp[Fila,Col].Tipo == Casilla.Obstaculo); 

        Debug.Log($"Juega la ficha {Fichas[Ficha].tipo.ToString()} en ({Col}, {Fila})");
        return Fichas[Ficha].Jugar(Fila, Col, controller);
    }
}