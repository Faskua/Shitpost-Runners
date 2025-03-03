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

    public string Mensaje => "Junto a otras siete letras forman un meme tan poderoso que destruira el laberinto";

    public int Fila { get; set; }
    public int Col { get; set; }

    public LetraClave(){
        FichasEnCasilla = new List<Ficha>();
    }


    public void Accion(GameController controller)
    {
        if(Letra != '.'){
            FichasEnCasilla.Last().Propietario.LetrasConseguidas.Add(Letra);
            controller.ObtainedLetters++;
            controller.visual.InstanciarLetra(Letra);
        }
        Letra = '.';
    }
}

public class Vacia : ICasilla
{
    public Casilla Tipo => Casilla.Vacia;

    public List<Ficha> FichasEnCasilla { get; set; }

    public string Mensaje => $"Casilla Vacia";

    public int Fila { get; set; }
    public int Col { get; set; }
    public Vacia(){
        FichasEnCasilla = new List<Ficha>();
    }

    public void Accion(GameController controller){
    }
}

public class Obstaculo : ICasilla //obstaculo, simplemente impide el paso
{
    /*
        Una prueba de logica salvaje se ha cruzado en tu camino y no te dejara aprobarla
        Los truhanes no te dejaran pasar
    */
    public Casilla Tipo => Casilla.Obstaculo;

    public List<Ficha> FichasEnCasilla { get; set; }

    public string Mensaje => "Por aquí no se pasa tanque";

    public int Fila { get; set; }
    public int Col { get; set; }
    public Obstaculo(){
        FichasEnCasilla = new List<Ficha>();
    }

    public void Accion(GameController controller){
    }
}

public class ChillGuy : ICasilla
{
    /*
        Se pegara a ti explicandote el lore de berserk y reducira tu velocidad en 1 (no hace nada si solo tienes 1 de velocidad)
    */
    public Casilla Tipo => Casilla.UnConsorteRelajao;

    public List<Ficha> FichasEnCasilla { get; set; }

    public string Mensaje => "Usted es un consorte relajao, no te hace falta ir mandao. Velocidad--";
    public int Fila { get; set; }
    public int Col { get; set; }

    public ChillGuy(){
        FichasEnCasilla = new List<Ficha>();
    }

    public void Accion(GameController controller)
    {
        if(FichasEnCasilla.Last().tipo == TipoFicha.ELChoco)  return;
        if(FichasEnCasilla.Last().velocidad > 1) FichasEnCasilla.Last().velocidad--;
    }
}

public class Abuelito : ICasilla
{
    /*
        Tu abuelito no entiende todos estos memes a su alrededor, tendras que quedarte 
        con el por un par de turnos para explicarle
    */
    public Casilla Tipo => Casilla.Abuelo;

    public List<Ficha> FichasEnCasilla { get; set; }

    public string Mensaje => "Vas a tener que explicarle los memes al puro. Esta ficha no puede moverse por dos turnos";
    public int Fila { get; set; }
    public int Col { get; set; }

    public Abuelito(){
        FichasEnCasilla = new List<Ficha>();
    }

    public void Accion(GameController controller)
    {
        if(FichasEnCasilla.Last().tipo == TipoFicha.ELChoco){
            return;
        }
        FichasEnCasilla.Last().turnosSinJugar += 2;
    }
}

public class AmongUs : ICasilla
{
    /*
        Superalo, no te bannas, la ducha te perseguira hasta la casilla de la que vienes
    */
    public Casilla Tipo => Casilla.AmongUs;
    public List<Ficha> FichasEnCasilla { get; set; }
    public string Mensaje => mensaje;
    public int Fila { get; set; }
    public int Col { get; set; }
    private string mensaje;

    public AmongUs(){
        FichasEnCasilla = new List<Ficha>();
    }    
    public void Accion(GameController controller)
    {
        if(FichasEnCasilla.Count != 0){
            if(FichasEnCasilla.Last().tipo == TipoFicha.ELChoco){
                mensaje = "El Choco es un preso, sin miedo";
                return;
            }

        }
        mensaje = "Vira que esto es tremenda candela";
    }
}


public class Morfeo : ICasilla
{
    /*
        Esta vez morfeo hara la desicion por ti y eligira entre
        aumentar tu velocidad o disminuirla
    */
    public Casilla Tipo => Casilla.Morfeo;

    public List<Ficha> FichasEnCasilla { get; set; }
    public string Mensaje => $"Esta vez morfeo hara la desicion por ti. {mensaje}";
    public int Fila { get; set; }
    public int Col { get; set; }
    private string mensaje = "";

    public Morfeo(){
        FichasEnCasilla = new List<Ficha>();
    }
    public void Accion(GameController controller)
    {
        if(FichasEnCasilla.Last().tipo == TipoFicha.ELChoco){
            mensaje = "Pero no le puede hacer nada al Choco";
            return;
        }
        System.Random random = new System.Random();
        int rnd = random.Next(1,3);
        if(rnd == 1) {
            FichasEnCasilla.Last().velocidad++;
            mensaje = "Velocidad aumentada";
        }
        else{
            FichasEnCasilla.Last().EnfActual += 3;
            mensaje = "Enfriamiento aumentado";
        }
    }
}

public class Honguito : ICasilla
{
    /*
        Come este graciosillo hongo y recupera la habilidad de la ficha(nintendo me va a demandar)
    */
    public Casilla Tipo => Casilla.Honguito;
    public List<Ficha> FichasEnCasilla { get; set; }
    public string Mensaje => "Come este graciosillo hongo y recupera la habilidad de la ficha(nintendo me va a demandar)";
    public int Fila { get; set; }
    public int Col { get; set; }

    public Honguito(){
        FichasEnCasilla = new List<Ficha>();
    }
    public void Accion(GameController controller)
    {
        FichasEnCasilla.Last().EnfActual = 0;
    }
}

public class Ojo : ICasilla
{
    /*
        El zorro te quitara una letra(si tienes) y la tirara en algun lado del laberinto(es abakua)
    */
    public Casilla Tipo => Casilla.Zorro;
    public List<Ficha> FichasEnCasilla { get; set; }
    public string Mensaje => "Ojo desbloquea las casillas cercanas a ella";
    public int Fila { get; set; }
    public int Col { get; set; }

    public Ojo(){
        FichasEnCasilla = new List<Ficha>();
    }
    public void Accion(GameController controller)
    {

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