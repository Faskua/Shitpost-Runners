using System.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LetraClave : ICasilla
{
    char Letra { get; set; }
    
    List<Ficha> fichas = new List<Ficha>();

    public LetraClave(char letra){
        Letra = letra;
    }
    public bool PuedePasar => true;

    public Casilla Tipo => Casilla.LetraMondongo;

    public List<Ficha> FichasEnCasilla { get => fichas; set => fichas = value; }
    public string Descripcion { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public string Mensaje => "Junto a otras siete letras forman un meme tan poderoso que destruira el laberinto";

    public void Accion(GameController controller)
    {
        if(Letra != '.'){
            fichas.Last().Propietario.LetrasConseguidas.Add(Letra);
            controller.ObtainedLetters++;
            controller.visual.InstanciarLetra(Letra);
        }
        Letra = '.';
    }
}

public class Vacia : ICasilla
{
    List<Ficha> fichas = new List<Ficha>();
    public bool PuedePasar => true;
    public Casilla Tipo => Casilla.Vacia;

    public List<Ficha> FichasEnCasilla { get => fichas; set => fichas = value; }

    public string Mensaje => $"Casilla Vacia";

    public void Accion(GameController controller){
    }
}

public class Obstaculo : ICasilla //obstaculo, simplemente impide el paso
{
    /*
        Una prueba de logica salvaje se ha cruzado en tu camino y no te dejara aprobarla
        Los truhanes no te dejaran pasar
    */
    List<Ficha> fichas = new List<Ficha>();
    public bool PuedePasar => false;
    public Casilla Tipo => Casilla.Obstáculo;

    public List<Ficha> FichasEnCasilla { get => fichas; set => fichas = value; }

    public string Mensaje => "Por aquí no se pasa tanque";

    public void Accion(GameController controller){
    }
}

public class ChillGuy : ICasilla
{
    /*
        Se pegara a ti explicandote el lore de berserk y reducira tu velocidad en 1 (no hace nada si solo tienes 1 de velocidad)
    */
    List<Ficha> fichas = new List<Ficha>();
    public bool PuedePasar => true;
    public Casilla Tipo => Casilla.UnConsorteRelajao;

    public List<Ficha> FichasEnCasilla { get => fichas; set => fichas = value; }

    public string Mensaje => "Usted es un consorte relajao, no te hace falta ir mandao. Velocidad--";

    public void Accion(GameController controller)
    {
        if(fichas.Last().tipo == TipoFicha.ELChoco)  return;
        if(fichas.Last().velocidad > 1) fichas.Last().velocidad--;
    }
}

public class Abuelito : ICasilla
{
    /*
        Tu abuelito no entiende todos estos memes a su alrededor, tendras que quedarte 
        con el por un par de turnos para explicarle
    */
    List<Ficha> fichas = new List<Ficha>();
    public bool PuedePasar => true;
    public Casilla Tipo => Casilla.Abuelo;

    public List<Ficha> FichasEnCasilla { get => fichas; set => fichas = value; }

    public string Mensaje => "Vas a tener que explicarle los memes al puro. Esta ficha no puede moverse por dos turnos";

    public void Accion(GameController controller)
    {
        if(fichas.Last().tipo == TipoFicha.ELChoco){
            return;
        }
        fichas.Last().turnosSinJugar += 2;
        Debug.Log($"turnos sin jugar: {fichas.Last().turnosSinJugar}");
    }
}

public class Ducha : ICasilla
{
    /*
        Superalo, no te bannas, la ducha te perseguira hasta la casilla de la que vienes
    */
    List<Ficha> fichas = new List<Ficha>();
    public bool PuedePasar => true;
    public Casilla Tipo => Casilla.Ducha;
    public List<Ficha> FichasEnCasilla { get => fichas; set => fichas = value; }
    public string Mensaje => mensaje;
    private string mensaje;

    public void Accion(GameController controller)
    {
        if(fichas.Count != 0){
            if(fichas.Last().tipo == TipoFicha.ELChoco){
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
    List<Ficha> fichas = new List<Ficha>();
    public bool PuedePasar => true;
    public Casilla Tipo => Casilla.Morfeo;

    public List<Ficha> FichasEnCasilla { get => fichas; set => fichas = value; }

    public string Mensaje => $"Esta vez morfeo hara la desicion por ti. {mensaje}";
    private string mensaje = "";

    public void Accion(GameController controller)
    {
        if(fichas.Last().tipo == TipoFicha.ELChoco){
            return;
        }
        System.Random random = new System.Random();
        int rnd = random.Next(1,3);
        if(rnd == 1) {
            FichasEnCasilla.Last().velocidad++;
            mensaje = "Velocidad aumentada";
        }
        else{
            FichasEnCasilla.Last().enfriamiento += 3;
            mensaje = "Enfriamiento aumentado";
        }
    }
}

public class Honguito : ICasilla
{
    /*
        Come este graciosillo hongo y recupera la habilidad de la ficha(nintendo me va a demandar)
    */
    List<Ficha> fichas = new List<Ficha>();
    public bool PuedePasar => true;
    public Casilla Tipo => Casilla.Honguito;
    public List<Ficha> FichasEnCasilla { get => fichas; set => fichas = value; }

    public string Mensaje => "Come este graciosillo hongo y recupera la habilidad de la ficha(nintendo me va a demandar)";

    public void Accion(GameController controller)
    {
        fichas.Last().enfriamiento = 0;
    }
}

public class Zorro : ICasilla
{
    /*
        El zorro te quitara una letra(si tienes) y la tirara en algun lado del laberinto(es abakua)
    */
    List<Ficha> fichas = new List<Ficha>();
    public bool PuedePasar => true;
    public Casilla Tipo => Casilla.Zorro;
    public List<Ficha> FichasEnCasilla { get => fichas; set => fichas = value; }

    public string Mensaje => "Zorro ya no entiende de no te lo lleves y te quita una letra (si tienes)";

    public void Accion(GameController controller)
    {
        if(fichas.Last().tipo == TipoFicha.ELChoco){
            return;
        }
        if(fichas.Last().Propietario.LetrasConseguidas.Count > 0){
            char letra = fichas.Last().Propietario.LetrasConseguidas.Last();
            fichas.Last().Propietario.LetrasConseguidas.RemoveAt(fichas.Last().Propietario.LetrasConseguidas.Count - 1);
            System.Random random = new System.Random();
            int fila = random.Next(0,15);
            int columna = random.Next(0,15);
            while(controller.Maze.LaberinthCSharp[fila,columna] is LetraClave){
                fila = random.Next(0,15);
                columna = random.Next(0,15);
            }
            ICasilla Letra = new LetraClave(letra);
            controller.Maze.LaberinthCSharp[fila,columna] = Letra;
            controller.Maze.LabGameObj[fila,columna].GetComponent<CasillaUN>().Mondongo();
            Debug.Log($"letra eliminada");
        }
    }
}