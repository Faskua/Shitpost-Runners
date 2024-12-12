using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public interface IJugador
{
    string Nombre { get; set; }
    bool LeToca { get; set; }
    List<char> LetrasConseguidas { get; set; }
    List<Ficha> Fichas { get; set; }
}

public interface ICasilla
{
    bool PuedePasar { get; }
    string Mensaje { get; }
    Casilla Tipo { get; }
    List<Ficha> FichasEnCasilla { get; set; }

    void Accion(GameController controller);
}

public enum Casilla
{
    LetraMondongo,     Vacia,  
    Obstáculo,      UnConsorteRelajao,
    Abuelo,         Ducha,  
    Morfeo,            Zorro,
    Honguito
}

public enum TipoFicha
{
    Mcqueen,        CJ,
    UNE,            Knuckles,
    RickRoll,       StarMan,
    Doge,           ELChoco
}

public abstract class Ficha
{
    public int enfriamiento = 0;
    public int EnfActual = 0;
    public int velocidad = 0;
    public int turnosSinJugar;
    public bool seleccionada;
    public string HabilidadDescrp = "";
    public virtual string Descripcion { get; }
    public (int, int) posicion;
    public (int, int) posicionAnterior;
    public Jugador Propietario;
    public abstract TipoFicha tipo { get; }

    private int[] dFil = new int[] { 1, 0, -1, 0};
    private int[] dCol = new int[] { 0, 1, 0, -1};

    public Ficha(Jugador propietario, int vel, int enf){
        Propietario = propietario;
        velocidad = vel;
        enfriamiento = enf;
        turnosSinJugar = 0;
    }

    public abstract void Habilidad(GameController controller);

    public bool Jugar(int fila, int columna, GameController controller){
        if(turnosSinJugar == 0)
        {
            int minimo = int.MaxValue;
            Caminar(posicion.Item1, posicion.Item2, fila, columna, controller, 0, velocidad, ref minimo);
            Debug.Log($"El minimo es: {minimo}");
            if(minimo <= velocidad){
                if(controller.Maze.LaberinthCSharp[fila,columna] is Ducha && tipo != TipoFicha.ELChoco){
                    controller.Maze.LabGameObj[fila, columna].GetComponent<CasillaUN>().Accion(controller); 
                    return true;
                }
                posicionAnterior = posicion;
                controller.Maze.LaberinthCSharp[posicionAnterior.Item1,posicionAnterior.Item2].FichasEnCasilla.Remove(this); 
                posicion = (fila,columna);
                controller.Maze.LaberinthCSharp[fila,columna].FichasEnCasilla.Add(this);
                controller.Maze.LabGameObj[fila, columna].GetComponent<CasillaUN>().Accion(controller); 
                return true;
            }
            // if(controller.Maze.LaberinthCSharp[fila,columna] is Ducha && tipo != TipoFicha.ELChoco){
            //     controller.Maze.LabGameObj[fila, columna].GetComponent<CasillaUN>().Accion(controller); 
            //     return true;
            // }
            // posicionAnterior = posicion;
            // controller.Maze.LaberinthCSharp[posicionAnterior.Item1,posicionAnterior.Item2].FichasEnCasilla.Remove(this); 
            // posicion = (fila,columna);
            // controller.Maze.LaberinthCSharp[fila,columna].FichasEnCasilla.Add(this);
            // controller.Maze.LabGameObj[fila, columna].GetComponent<CasillaUN>().Accion(controller); 
            // return true;
        }   
        return false;
    }

    private void Caminar(int filaAct, int colAct, int fila, int col, GameController controller, int pasos, int velocidad, ref int minimo){
        if(filaAct == fila && colAct == col){
            if(pasos <= minimo) minimo = pasos;
        } 
        else{
            for (int i = 0; i < 4; i++)
            {
                int sigFila = filaAct+dFil[i];
                int sigCol = colAct+dCol[i];
                if(filaAct >= 15 || colAct >= 15 || controller.Maze.LaberinthCSharp[filaAct,colAct].Tipo == Casilla.Obstáculo || pasos == velocidad) return;
                Caminar(sigFila, sigCol, fila, col, controller, pasos+1, velocidad, ref minimo);
            }
        }
    }
}