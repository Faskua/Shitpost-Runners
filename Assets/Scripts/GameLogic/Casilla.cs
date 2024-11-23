using System.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;

public class LetraClave : ICasilla
{
    /*
        Junto a otras siete letras forman un meme tan poderoso que destruira el laberinto
    */
    char Letra { get; set; }
    List<Ficha> fichas = new List<Ficha>();

    public LetraClave(char letra){
        Letra = letra;
    }
    public bool PuedePasar => true;

    public Casilla Tipo => Casilla.LetraClave;

    public List<Ficha> FichasEnCasilla { get => fichas; set => fichas = value; }
    public string Descripcion { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void Accion()
    {
        if(Letra != '.'){
            fichas.Last().Propietario.LetrasConseguidas.Add(Letra);
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
    public void Accion(){
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
    public Casilla Tipo => Casilla.Obstaculo;

    public List<Ficha> FichasEnCasilla { get => fichas; set => fichas = value; }

    public void Accion(){
    }
}

public class FanDeBerserk : ICasilla
{
    /*
        Se pegara a ti explicandote el lore de berserk y reducira tu velocidad en 1 (no hace nada si solo tienes 1 de velocidad)
    */
    List<Ficha> fichas = new List<Ficha>();
    public bool PuedePasar => true;
    public Casilla Tipo => Casilla.Berserk;

    public List<Ficha> FichasEnCasilla { get => fichas; set => fichas = value; }

    public void Accion()
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

    public void Accion()
    {
        if(fichas.Last().tipo == TipoFicha.ELChoco){
            return;
        }
        fichas.Last().turnosSinJugar = 2 * Laberinto.jugadores.Count;
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

    public void Accion()
    {
        if(fichas.Last().tipo == TipoFicha.ELChoco){
            return;
        }
        (int,int) posicionAnt = fichas.Last().posicionAnterior;
        (int,int) posicionAct = fichas.Last().posicion;
        Laberinto.laberinto[posicionAnt.Item1, posicionAnt.Item2].FichasEnCasilla.Add(fichas.Last());
        Laberinto.laberinto[posicionAct.Item1, posicionAct.Item2].FichasEnCasilla.RemoveAt(Laberinto.laberinto[posicionAct.Item1,posicionAct.Item2].FichasEnCasilla.Count - 1);
        fichas.Last().posicion = fichas.Last().posicionAnterior;
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
    public Casilla Tipo => Casilla.Ojo;

    public List<Ficha> FichasEnCasilla { get => fichas; set => fichas = value; }

    public void Accion()
    {
        if(fichas.Last().tipo == TipoFicha.ELChoco){
            return;
        }
        Random random = new Random();
        int rnd = random.Next(1,2);
        if (rnd == 1) {
            FichasEnCasilla.Last().velocidad++;
        }
        else{
            FichasEnCasilla.Last().enfriamiento += 3;
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

    public void Accion()
    {
        if(fichas.Last().tipo == TipoFicha.ELChoco){
            return;
        }
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

    public void Accion()
    {
        if(fichas.Last().tipo == TipoFicha.ELChoco){
            return;
        }
        if(fichas.Last().Propietario.LetrasConseguidas.Count > 0){
            char letra = fichas.Last().Propietario.LetrasConseguidas.Last();
            fichas.Last().Propietario.LetrasConseguidas.RemoveAt(fichas.Last().Propietario.LetrasConseguidas.Count - 1);
            Random random = new Random();
            int fila = random.Next(0,Laberinto.Tamanno);
            int columna = random.Next(0,Laberinto.Tamanno);
            while(Laberinto.laberinto[fila,columna] is LetraClave){
                fila = random.Next(0,Laberinto.Tamanno);
                columna = random.Next(0,Laberinto.Tamanno);
            }
            ICasilla Letra = new LetraClave(letra);
            Laberinto.laberinto[fila,columna] = Letra;
        }
    }
}