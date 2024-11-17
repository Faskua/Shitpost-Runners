using System;
public class McQueen : Ficha
{
    public McQueen(IJugador prop, string descr) : base(prop, 4, 4, descr) {}

    public override TipoFicha tipo => TipoFicha.Mcqueen;

    public override void Habilidad() //aumenta su velocidad
    {
        if(enfriamiento <= 0)
        {
            velocidad++;
            enfriamiento = 4;
        }
    }
}

public class CJ : Ficha
{
    public CJ(IJugador prop, string descr) : base(prop, 2, 7, descr) {}

    public override TipoFicha tipo => TipoFicha.CJ;

    public override void Habilidad() //roba una letra si se encuentra en la misma casilla de una ficha de un jugador con letra
    {
        if(enfriamiento <= 0)
        {
            foreach (var ficha in Laberinto.laberinto[this.posicion.Item1, this.posicion.Item2].FichasEnCasilla)
            {
                if(ficha.tipo == TipoFicha.StarMan) continue;
                if(!ficha.Propietario.Equals(this.Propietario) && ficha.Propietario.LetrasConseguidas.Count > 0){
                    this.Propietario.LetrasConseguidas.Add(ficha.Propietario.LetrasConseguidas[ficha.Propietario.LetrasConseguidas.Count - 1]);
                    ficha.Propietario.LetrasConseguidas.RemoveAt(ficha.Propietario.LetrasConseguidas.Count - 1);
                    Console.WriteLine("Letra robada");
                    return;
                }
            }
            Console.WriteLine("You just had to steal the damm letter CJ!!");
        }
    }
}

public class UNE : Ficha
{
    public UNE(IJugador prop, string descr) : base(prop, 3, 6, descr) {}

    public override TipoFicha tipo => TipoFicha.UNE;

    public override void Habilidad()
    {
        if(enfriamiento <= 0)
        {
            foreach (var jugador in Laberinto.jugadores)
            {
                foreach (var ficha in jugador.Fichas)
                {
                    if(ficha.tipo == TipoFicha.StarMan) continue;
                    ficha.turnosSinJugar = 2 * Laberinto.jugadores.Count;
                }
            }
            turnosSinJugar = 0;
            enfriamiento = 6;
        }
    }
}

public class Knuckles : Ficha
{
    public Knuckles(IJugador prop, string descr) : base(prop, 3, 4, descr) {}

    public override TipoFicha tipo => TipoFicha.Knuckles;

    public override void Habilidad()
    {
        foreach (var ficha in Propietario.Fichas)
        {
            if(ficha.enfriamiento > 1) ficha.enfriamiento -= 2;
        }
    }
}

public class RickRoll : Ficha
{
    public RickRoll(IJugador prop, string descr) : base(prop, 5, 3, descr) {}

    public override TipoFicha tipo => TipoFicha.RickRoll;

    public override void Habilidad() //envia una ficha random de un jugador random a una casilla que no sea una letra
    {
        Random random = new Random();
        int jugador = random.Next(0, Laberinto.jugadores.Count - 1);
        int fila = random.Next(0,Laberinto.Tamanno);
        int columna = random.Next(0,Laberinto.Tamanno);

        while(Laberinto.jugadores[jugador].Equals(Propietario)) jugador = random.Next(0, Laberinto.jugadores.Count - 1);//eligiendo un jugador distinto del propietario
        int ficha = random.Next(0,Propietario.Fichas.Count - 1);
        if(Laberinto.jugadores[jugador].Fichas[ficha].tipo == TipoFicha.StarMan){
            Console.WriteLine("La Habilidad no tiene efecto sobre Starman");
            return;
        }

        while(Laberinto.laberinto[fila, columna].Tipo == Casilla.LetraClave){ //eligiendo una casilla que no tenga letra
            fila = random.Next(0,Laberinto.Tamanno);
            columna = random.Next(0,Laberinto.Tamanno);
        }

        Laberinto.jugadores[jugador].Fichas[ficha].posicionAnterior = Laberinto.jugadores[jugador].Fichas[ficha].posicion; //cambiando las coordenadas
        Laberinto.jugadores[jugador].Fichas[ficha].posicion = (fila, columna); 

        (int, int) posicionAnt = Laberinto.jugadores[jugador].Fichas[ficha].posicionAnterior;
        Laberinto.laberinto[posicionAnt.Item1,posicionAnt.Item2].FichasEnCasilla.Remove(Laberinto.jugadores[jugador].Fichas[ficha]); //elimino la ficha de la posicion anterior
        Laberinto.laberinto[fila,columna].FichasEnCasilla.Add(Laberinto.jugadores[jugador].Fichas[ficha]); //la anado a la nueva
        Laberinto.laberinto[fila,columna].Accion(); //activo la casilla
    }
}

public class StarMan : Ficha
{
    public StarMan(IJugador prop, string descr) : base(prop, 4, 0, descr) {}

    public override TipoFicha tipo => TipoFicha.StarMan;

    public override void Habilidad()
    {
        //es inmune a todas las habilidades del resto de fichas
    }
}

public class Doge : Ficha
{
    public Doge(IJugador prop, string descr) : base(prop, 3, 5, descr) {}

    public override TipoFicha tipo => TipoFicha.Doge;

    public override void Habilidad() //de un galletazo le baja la velocidad a todas las fichas en su misma fila(incluyendo aliados)
    {
        int fila = posicion.Item1;
        for (int columna = 0; columna < 10; columna++)
        {
            foreach (var ficha in Laberinto.laberinto[fila,columna].FichasEnCasilla)
            {
                if(ficha.tipo == TipoFicha.StarMan) continue;
                if(ficha.velocidad > 1) ficha.velocidad--;
            }
        }
        velocidad++;
    }
}

public class ELChoco : Ficha
{
    public ELChoco(IJugador prop, string descr) : base(prop, 4, 0, descr) {}
    public override TipoFicha tipo => TipoFicha.ELChoco;

    public override void Habilidad()
    {
        //Es inmune a todas las trampas del juego
    }
}