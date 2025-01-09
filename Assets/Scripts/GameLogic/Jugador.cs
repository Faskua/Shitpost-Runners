using System.Collections.Generic;
using System;
using UnityEngine;
public class Jugador 
{
    string nombre;
    public int TurnosSinJugar;
    List<Ficha> fichas;
    List<char> letras;
    public List<Ficha> Fichas { get => fichas; set => fichas = value;}
    public string Nombre { get => nombre; set => nombre = value; }
    public List<char> LetrasConseguidas { get => letras; set => letras = value; }

    public Jugador(string nombre){
        fichas = new List<Ficha>();
        this.nombre = nombre;
        letras = new List<char>();
        TurnosSinJugar = 0;
    }

    public bool Jugar(int ficha, int fila, int columna, GameController controller){ 
        if(TurnosSinJugar == 0){
            return Fichas[ficha].Jugar(fila, columna, controller);
        }
        return false;
    }
}