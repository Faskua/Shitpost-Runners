using System.Collections.Generic;
using System;
using UnityEngine;
public class Jugador : IJugador
{
    string nombre;
    bool leToca = false;
    public int TurnosSinJugar;
    public int MemeCoin;
    List<Ficha> fichas;
    List<char> letras;
    public List<Ficha> Fichas { get => fichas; set => fichas = value;}
    public string Nombre { get => nombre; set => nombre = value; }
    public List<char> LetrasConseguidas { get => letras; set => letras = value; }
    public bool LeToca { get => leToca; set => leToca = value; }

    public Jugador(string nombre){
        fichas = new List<Ficha>();
        this.nombre = nombre;
        letras = new List<char>();
        MemeCoin = 0;
        TurnosSinJugar = 0;
    }
    public void AnadirFicha(Ficha ficha){
        Fichas.Add(ficha);
    }

    public bool Jugar(int ficha, int fila, int columna, GameController controller){ 
        if(TurnosSinJugar > 0) controller.AvanzarTurno();
        Ficha prueba = Fichas[ficha];
        int pasosDados = Math.Abs(fila - prueba.posicion.Item1 + columna - prueba.posicion.Item2);
        if(pasosDados <= prueba.velocidad){// && controller.Maze.LaberinthCSharp[fila,columna].Tipo != Casilla.Obstaculo){
            return Fichas[ficha].Jugar(fila, columna, controller);
        }
        return false;
    }
}