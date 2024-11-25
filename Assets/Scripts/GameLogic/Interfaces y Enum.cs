using System.Collections;
using System.Collections.Generic;
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
    public int velocidad = 0;
    public int turnosSinJugar;
    public bool seleccionada;
    public virtual string Descripcion { get; }
    public (int, int) posicion;
    public (int, int) posicionAnterior;
    public IJugador Propietario;
    public abstract TipoFicha tipo { get; }

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
            posicionAnterior = posicion;
            controller.Maze.LaberinthCSharp[posicionAnterior.Item1,posicionAnterior.Item2].FichasEnCasilla.Remove(this); 
            posicion = (fila,columna);
            controller.Maze.LaberinthCSharp[fila,columna].FichasEnCasilla.Add(this);
            controller.Maze.LaberinthCSharp[fila, columna].Accion(controller); 
            return true;
        }   
        return false;
    }
}