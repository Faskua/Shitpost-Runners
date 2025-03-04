using System.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LetraClave : ICasilla
{
    public char Letra { get; set; }

    public LetraClave(char letra){
        Letra = letra;
    }

    public Casilla Tipo => Casilla.LetraMondongo;

    public List<Ficha> FichasEnCasilla { get; set; }
    public string Descripcion { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public string Mensaje { get => "Junto a otras siete letras forman un meme tan poderoso que destruira el laberinto"; set => Mensaje = value;}

    public int Fila { get; set; }
    public int Col { get; set; }
    public bool Visitada{ get; set; }

    public LetraClave(){
        Visitada = false;
        FichasEnCasilla = new List<Ficha>();
    }


    public void Accion(GameController controller)
    {
        if(!Visitada){
            FichasEnCasilla.Last().Propietario.LetrasConseguidas.Add(Letra);
            controller.ObtainedLetters++;
            controller.visual.InstanciarLetra(Letra);
        }
        Visitada = true;
    }
}

public class Vacia : ICasilla
{
    public Casilla Tipo => Casilla.Vacia;

    public List<Ficha> FichasEnCasilla { get; set; }

    public string Mensaje { get => $"Casilla Vacia"; set => Mensaje = value; }

    public int Fila { get; set; }
    public int Col { get; set; }
    public bool Visitada{ get; set; }
    public Vacia(){
        Visitada = false;
        FichasEnCasilla = new List<Ficha>();
    }

    public void Accion(GameController controller){ Visitada = true;
    }
}

public class Obstaculo : ICasilla //obstaculo, simplemente impide el paso
{
    public Casilla Tipo => Casilla.Obstaculo;

    public List<Ficha> FichasEnCasilla { get; set; }

    public string Mensaje { get => "Por aquí no se pasa tanque"; set => Mensaje = value; }

    public int Fila { get; set; }
    public int Col { get; set; }
    public bool Visitada{ get; set; }
    public Obstaculo(){
        Visitada = false;
        FichasEnCasilla = new List<Ficha>();
    }

    public void Accion(GameController controller){  Visitada = true;
    }
}

public class ChillGuy : ICasilla
{
    public Casilla Tipo => Casilla.UnConsorteRelajao;

    public List<Ficha> FichasEnCasilla { get; set; }

    public string Mensaje { get; set; }
    public int Fila { get; set; }
    public int Col { get; set; }
    public bool Visitada{ get; set; }

    public ChillGuy(){
        Visitada = false;
        FichasEnCasilla = new List<Ficha>();
    }

    public void Accion(GameController controller)
    {
        Visitada = true;
        if(FichasEnCasilla.Last().tipo == TipoFicha.ELChoco){
            Mensaje = "El Choco no tiene freno";
            return;
        } 
        else Mensaje =  "Usted es un consorte relajao, no te hace falta ir mandao. Velocidad--";
        if(FichasEnCasilla.Last().velocidad > 1) FichasEnCasilla.Last().velocidad--;
    }
}

public class Abuelito : ICasilla
{
    public Casilla Tipo => Casilla.Abuelo;

    public List<Ficha> FichasEnCasilla { get; set; }

    public string Mensaje { get; set; }
    public int Fila { get; set; }
    public int Col { get; set; }
    public bool Visitada{ get; set; }

    public Abuelito(){
        Visitada = false;
        FichasEnCasilla = new List<Ficha>();
    }

    public void Accion(GameController controller)
    {
        Visitada = true;
        if(FichasEnCasilla.Last().tipo == TipoFicha.ELChoco){
            Mensaje = "El Choco no pierde el tiempo con gente de la tercera edad";
            return;
        }
        else Mensaje = "Vas a tener que explicarle los memes al puro. Esta ficha no puede moverse por dos turnos";
        FichasEnCasilla.Last().turnosSinJugar += 2;
    }
}

public class AmongUs : ICasilla
{
    public Casilla Tipo => Casilla.AmongUs;
    public List<Ficha> FichasEnCasilla { get; set; }
    public string Mensaje { get; set; }
    public int Fila { get; set; }
    public int Col { get; set; }
    public bool Visitada{ get; set; }

    public AmongUs(){
        Visitada = false;
        FichasEnCasilla = new List<Ficha>();
    }    
    public void Accion(GameController controller)
    {
        Visitada = true;
        if(FichasEnCasilla.Count != 0){
            if(FichasEnCasilla.Last().tipo == TipoFicha.ELChoco){
                Mensaje = "El Choco es un preso, sin miedo";
                return;
            }
            
        }
        Mensaje = "Vira que esto es tremenda candela";
    }
}


public class Morfeo : ICasilla
{
    public Casilla Tipo => Casilla.Morfeo;

    public List<Ficha> FichasEnCasilla { get; set; }
    public string Mensaje { get; set; }
    public int Fila { get; set; }
    public int Col { get; set; }
    public bool Visitada{ get; set; }

    public Morfeo(){
        Visitada = false;
        FichasEnCasilla = new List<Ficha>();
    }
    public void Accion(GameController controller)
    {
        Visitada = true;
        if(FichasEnCasilla.Last().tipo == TipoFicha.ELChoco){
            Mensaje = "El Choco no cree en pastillita ni na de eso";
            return;
        }
        System.Random random = new System.Random();
        int rnd = random.Next(1,3);
        if(rnd == 1) {
            FichasEnCasilla.Last().velocidad++;
            Mensaje = "Morfeo ha aumentado tu velocidad";
        }
        else{
            FichasEnCasilla.Last().EnfActual += 3;
            Mensaje = "Morfeo ha aumentado el timpo de enfriamiento de tu habilidad";
        }
    }
}

public class Honguito : ICasilla
{
    public Casilla Tipo => Casilla.Honguito;
    public List<Ficha> FichasEnCasilla { get; set; }
    public string Mensaje { get => "Come este graciosillo hongo y recupera la habilidad de la ficha(nintendo me va a demandar)"; set => Mensaje = value; }
    public int Fila { get; set; }
    public int Col { get; set; }
    public bool Visitada{ get; set; }

    public Honguito(){
        Visitada = false;
        FichasEnCasilla = new List<Ficha>();
    }
    public void Accion(GameController controller)
    {
        Visitada = true;
        FichasEnCasilla.Last().EnfActual = 0;
    }
}

public class Ojo : ICasilla
{
    public Casilla Tipo => Casilla.Zorro;
    public List<Ficha> FichasEnCasilla { get; set; }
    public string Mensaje { get => "Ojo desbloquea las casillas cercanas a ella"; set => Mensaje = value; }
    public int Fila { get; set; }
    public int Col { get; set; }
    public bool Visitada{ get; set; }

    public Ojo(){
        Visitada = false;
        FichasEnCasilla = new List<Ficha>();
    }
    public void Accion(GameController controller)
    {
        Visitada = true;
        int[] x = {0, 1, 0, -1};
        int[] y = {1, 0, -1, 0};

        for (int i = 0; i < 4; i++)
        {
            int fila = this.Fila + y[i];
            int col = this.Col + x[i];
            if(fila < 15 && fila >=0 && col < 15 && col >= 0) controller.Maze.LabGameObj[fila, col].GetComponent<CasillaUN>().Visible();
        }
    }
}