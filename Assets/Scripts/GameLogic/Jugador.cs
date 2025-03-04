using System.Collections.Generic;
using System;
using UnityEngine;
using System.ComponentModel.Design;
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
        int[] dfila = {1, 0, -1, 0};
        int[] dcol = {0, 1, 0, -1};
        for (int i = pasos; i > 0 ; i--)
        {
            Util.ShuffleDirection(ref dfila, ref dcol);
            for (int j = 0; j < 4; j++)
            {
                filaAct = fila + dfila[j];
                colAct = col + dcol[j];
                if(Util.Valid(filaAct, colAct, controller)){ //se toma la primera que sea valida
                    fila = filaAct;
                    col = colAct;
                    break;
                }
            }
            
        }
    }
}

class Intermedio : IJugador
{
    public int TurnosSinJugar { get; set; }
    public List<Ficha> Fichas { get; set;}
    public string Nombre { get; set; }
    public TipoJugador Tipo { get; set; }
    public List<char> LetrasConseguidas { get; set; }

    public Intermedio(string nombre, TipoJugador tipo){
        Fichas = new List<Ficha>();
        Nombre = nombre;
        Tipo = tipo;
        LetrasConseguidas = new List<char>();
        TurnosSinJugar = 0;
    }

    public bool Jugar(ref int ficha, ref int fila, ref int columna, GameController controller)
    {
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
        ActivarHabilidad(ficha, controller);
        
        return Fichas[ficha].Jugar(fila, columna, controller);
    }

    public void ElegirCasilla(int pasos, GameController controller, ref int fila, ref int col){
        System.Random random = new System.Random();
        int filaAct = fila;
        int colAct = col;
        int[] dfila = {1, 0, -1, 0};
        int[] dcol = {0, 1, 0, -1};
        for (int i = pasos; i > 0 ; i--)
        {
            Util.ShuffleDirection(ref dfila, ref dcol);
            bool SinVisitar = false;
            for (int j = 0; j < 4; j++) //revisando todas las direcciones
            {
                filaAct = fila + dfila[j];
                colAct = col + dcol[j];
                if(Util.Valid(filaAct, colAct, controller) && !controller.Maze.LaberinthCSharp[filaAct, colAct].Visitada){ //si hay alguna que no se haya visitado se elige y para 
                    fila = filaAct;
                    col = colAct;
                    SinVisitar = true; //se marca que ya se encontro una sin visitar
                    break;
                }
            }
            if(!SinVisitar){ // si ninguna esta sin visitar
                for (int j = 0; j < 4; j++)
                {
                    filaAct = fila + dfila[j];
                    colAct = col + dcol[j];
                    if(Util.Valid(filaAct, colAct, controller)){ //se toma la primera que sea valida
                        fila = filaAct;
                        col = colAct;
                        break;
                    }
                }
            }
        }
    }

    public void ActivarHabilidad(int ficha, GameController controller){
        if(Fichas[ficha].EnfActual != 0) return;

        switch(Fichas[ficha].tipo){
            case TipoFicha.Mcqueen:
                Fichas[ficha].Habilidad(controller);
                controller.visual.SetHabilidad(Fichas[ficha].HabilidadDescrp);
            break;
            case TipoFicha.CJ:
                bool letra = false;
                foreach (var item in controller.Maze.LaberinthCSharp[Fichas[ficha].posicion.Item1, Fichas[ficha].posicion.Item2].FichasEnCasilla)
                {
                    if(item.Propietario.Nombre == Fichas[ficha].Propietario.Nombre) continue;
                    else if(item.Propietario.LetrasConseguidas.Count > 0) letra = true;
                }
                if(letra){
                    Fichas[ficha].Habilidad(controller);
                    controller.visual.SetHabilidad(Fichas[ficha].HabilidadDescrp);
                }
                
            break;
            case TipoFicha.Doge:
                int aliado = 0;
                int enemigo = 0;
                for (int col = 0; col < 15; col++)
                {
                    if(controller.Maze.LaberinthCSharp[Fichas[ficha].posicion.Item1, col].FichasEnCasilla.Count == 0) continue;

                    foreach (var item in controller.Maze.LaberinthCSharp[Fichas[ficha].posicion.Item1, col].FichasEnCasilla)
                    {
                        if(item.Propietario.Nombre == Fichas[ficha].Propietario.Nombre) aliado++;
                        else enemigo++;
                    }
                }

                if(aliado == 0 && enemigo > 1){
                    Fichas[ficha].Habilidad(controller);
                    controller.visual.SetHabilidad(Fichas[ficha].HabilidadDescrp);
                }
            break;
            case TipoFicha.Knuckles:
                int aliados = 0;
                foreach (var item in Fichas[ficha].Propietario.Fichas)
                {
                    if(item.EnfActual > 2) aliados++;
                }
                if(aliados > 1){
                    Fichas[ficha].Habilidad(controller);
                    controller.visual.SetHabilidad(Fichas[ficha].HabilidadDescrp);
                }
            break;
            case TipoFicha.RickRoll:
                Fichas[ficha].Habilidad(controller);
                controller.visual.SetHabilidad(Fichas[ficha].HabilidadDescrp);
            break;
            case TipoFicha.UNE:
                Fichas[ficha].Habilidad(controller);
                controller.visual.SetHabilidad(Fichas[ficha].HabilidadDescrp);
            break;
            default:
            break;
        }
    }
}

public static class Util
{
    public static void ShuffleDirection(ref int[] fila, ref int[] col){
        System.Random random = new System.Random();
        for (int i = 0; i < 4; i++)
        {
            int j = random.Next(0, 4);
            int aux = fila[i];
            fila[i] = fila[j];
            fila[j] = aux;

            aux = col[j];
            col[i] = col[j];
            col[j] = aux;
        }
    }

    public static bool Valid(int fila, int col, GameController controller) => fila >= 0 && fila < 15 && col >= 0 && col < 15 && controller.Maze.LaberinthCSharp[fila, col].Tipo != Casilla.Obstaculo;
}