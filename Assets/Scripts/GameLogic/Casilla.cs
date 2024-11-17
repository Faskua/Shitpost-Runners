using System.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;

public class LetraClave : ICasilla
{
    /*
        Junto a otras siete letras forman un meme tan poderoso que destruira el laberinto
    */
    char Letra { get; }
    bool visitada = false;
    List<Ficha> fichas = new List<Ficha>();

    public LetraClave(char letra){
        Letra = letra;
    }
    public bool PuedePasar => true;
    public bool Visitada { get => visitada; set => visitada = value; }

    public Casilla Tipo => Casilla.LetraClave;

    public List<Ficha> FichasEnCasilla { get => fichas; set => fichas = value; }
    public string Descripcion { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void Accion()
    {
        if(!visitada){
            fichas.Last().Propietario.LetrasConseguidas.Add(Letra);
        }
        Console.WriteLine($"Has encontrado la letra {Letra}");
        visitada = true;
    }
}

public class Vacia : ICasilla
{
    bool visitada = false;
    List<Ficha> fichas = new List<Ficha>();
    public bool PuedePasar => true;
    public bool Visitada { get => visitada; set => visitada = value; }
    public Casilla Tipo => Casilla.Vacia;

    public List<Ficha> FichasEnCasilla { get => fichas; set => fichas = value; }
    public void Accion(){
        visitada = true;
    }
}

public class Obstaculo : ICasilla //obstaculo, simplemente impide el paso
{
    /*
        Una prueba de logica salvaje se ha cruzado en tu camino y no te dejara aprobarla
        Los truhanes no te dejaran pasar
    */
    bool visitada = false;
    List<Ficha> fichas = new List<Ficha>();
    public bool PuedePasar => false;
    public bool Visitada { get => visitada; set => visitada = value; }
    public Casilla Tipo => Casilla.Obstaculo;

    public List<Ficha> FichasEnCasilla { get => fichas; set => fichas = value; }

    public void Accion()
    {
        Console.WriteLine("Regresa a tu casilla, no quieres ver es prueba de Lógica");
        visitada = true;
    }
}

public class FanDeBerserk : ICasilla
{
    /*
        Se pegara a ti explicandote el lore de berserk y reducira tu velocidad en 1 (no hace nada si solo tienes 1 de velocidad)
    */
    bool visitada = false;
    List<Ficha> fichas = new List<Ficha>();
    public bool PuedePasar => true;
    public bool Visitada { get => visitada; set => visitada = value; }
    public Casilla Tipo => Casilla.Berserk;

    public List<Ficha> FichasEnCasilla { get => fichas; set => fichas = value; }

    public void Accion()
    {
        if(fichas.Last().tipo == TipoFicha.ELChoco){
            visitada = true;
            return;
        }
        if(fichas.Last().velocidad > 1) fichas.Last().velocidad--;
        Console.WriteLine("Este tipo te va a atrasar todo el camino explicandote el lore de berserk");
        visitada = true;
    }
}

public class Abuelito : ICasilla
{
    /*
        Tu abuelito no entiende todos estos memes a su alrededor, tendras que quedarte 
        con el por un par de turnos para explicarle
    */
    bool visitada = false;
    List<Ficha> fichas = new List<Ficha>();
    public bool PuedePasar => true;
    public bool Visitada { get => visitada; set => visitada = value; }
    public Casilla Tipo => Casilla.Abuelo;

    public List<Ficha> FichasEnCasilla { get => fichas; set => fichas = value; }

    public void Accion()
    {
        if(fichas.Last().tipo == TipoFicha.ELChoco){
            visitada = true;
            return;
        }
        fichas.Last().turnosSinJugar = 2 * Laberinto.jugadores.Count;
        Console.WriteLine("Tendrás que quedarte con tu abuelito por dos turnos");
        visitada = true;
    }
}

public class Ducha : ICasilla
{
    /*
        Superalo, no te bannas, la ducha te perseguira hasta la casilla de la que vienes
    */
    bool visitada = false;
    List<Ficha> fichas = new List<Ficha>();
    public bool PuedePasar => true;
    public bool Visitada { get => visitada; set => visitada = value; }
    public Casilla Tipo => Casilla.Ducha;

    public List<Ficha> FichasEnCasilla { get => fichas; set => fichas = value; }

    public void Accion()
    {
        if(fichas.Last().tipo == TipoFicha.ELChoco){
            visitada = true;
            return;
        }
        (int,int) posicionAnt = fichas.Last().posicionAnterior;
        (int,int) posicionAct = fichas.Last().posicion;
        Laberinto.laberinto[posicionAnt.Item1, posicionAnt.Item2].FichasEnCasilla.Add(fichas.Last());
        Laberinto.laberinto[posicionAct.Item1, posicionAct.Item2].FichasEnCasilla.RemoveAt(Laberinto.laberinto[posicionAct.Item1,posicionAct.Item2].FichasEnCasilla.Count - 1);
        fichas.Last().posicion = fichas.Last().posicionAnterior;
        Console.WriteLine("Huiste de la Ducha pero a que costo");
        visitada = true;
    }
}


public class Morfeo : ICasilla
{
    /*
        Esta vez morfeo hara la desicion por ti y eligira entre
        aumentar tu velocidad o disminuirla
    */
    bool visitada = false;
    List<Ficha> fichas = new List<Ficha>();
    public bool PuedePasar => true;

    public bool Visitada { get => visitada; set => visitada = value; }    public Casilla Tipo => Casilla.Ojo;

    public List<Ficha> FichasEnCasilla { get => fichas; set => fichas = value; }

    public void Accion()
    {
        if(fichas.Last().tipo == TipoFicha.ELChoco){
            visitada = true;
            return;
        }
        Random random = new Random();
        int rnd = random.Next(1,2);
        if (rnd == 1) {
            FichasEnCasilla.Last().velocidad++;
            Console.WriteLine("Velocidad Aumentada");
        }
        else{
            FichasEnCasilla.Last().enfriamiento += 3;
            Console.WriteLine("Habilidad Suspendidad por 3 Turnos");
        }
        visitada = true;
    }
}

public class Honguito : ICasilla
{
    /*
        Come este graciosillo hongo y recupera la habilidad de la ficha(nintendo me va a demandar)
    */
    bool visitada = false;
    List<Ficha> fichas = new List<Ficha>();
    public bool PuedePasar => true;
    public bool Visitada { get => visitada; set => visitada = value; }
    public Casilla Tipo => Casilla.Honguito;
    public List<Ficha> FichasEnCasilla { get => fichas; set => fichas = value; }

    public void Accion()
    {
        if(fichas.Last().tipo == TipoFicha.ELChoco){
            visitada = true;
            return;
        }
        fichas.Last().enfriamiento = 0;
            Console.WriteLine("Habilidad Restaurada");
        visitada = true;
    }
}

public class Zorro : ICasilla
{
    /*
        El zorro te quitara una letra(si tienes) y la tirara en algun lado del laberinto(es abakua)
    */
    bool visitada;
    List<Ficha> fichas = new List<Ficha>();
    public bool PuedePasar => true;
    public bool Visitada { get => visitada; set => visitada = value; }
    public Casilla Tipo => Casilla.Zorro;
    public List<Ficha> FichasEnCasilla { get => fichas; set => fichas = value; }

    public void Accion()
    {
        if(fichas.Last().tipo == TipoFicha.ELChoco){
            visitada = true;
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
            Console.WriteLine("Zorro tiró una de tus letras al laberinto");
        }
        visitada = true;
    }
}