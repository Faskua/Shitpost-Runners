using System;
public class McQueen : Ficha
{
    public McQueen(Jugador prop) : base(prop, 4, 4) {}

    public override string Descripcion => $"McQUEEN \nVelocidad: {this.velocidad}, Enfriamiento: {this.enfriamiento}. Aumneta su propia velocidad en 1. Cuchau";

    public override TipoFicha tipo => TipoFicha.Mcqueen;

    public override void Habilidad(GameController controller) //aumenta su velocidad
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
    public CJ(Jugador prop) : base(prop, 2, 7) {}

    public override string Descripcion => $"CJ \n Velocidad: {this.velocidad}, Enfriamiento: {this.enfriamiento}. Si comparte casilla con otra ficha le roba una letra a su propietario";

    public override TipoFicha tipo => TipoFicha.CJ;

    public override void Habilidad(GameController controller) //roba una letra si se encuentra en la misma casilla de una ficha de un jugador con letra
    {
        if(enfriamiento <= 0)
        {
            foreach (var ficha in controller.Maze.LaberinthCSharp[this.posicion.Item1, this.posicion.Item2].FichasEnCasilla)
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
    public UNE(Jugador prop) : base(prop, 3, 6) {}

    public override string Descripcion => $"UNE \n Velocidad: {this.velocidad}, Enfriamiento: {this.enfriamiento}. Le quita la luz a un jugador al azar y no podra jugar por dos turnos (incluyendo a Starman)";

    public override TipoFicha tipo => TipoFicha.UNE;

    public override void Habilidad(GameController controller)
    {
        if(enfriamiento <= 0)
        {
            System.Random random = new System.Random();
            int objetivo = random.Next(0, controller.Jugadores.Count);
            while(Propietario.Nombre == controller.Jugadores[objetivo].Nombre)  objetivo = random.Next(0, controller.Jugadores.Count);
            controller.Jugadores[objetivo].jugador.TurnosSinJugar += 2;
        }
    }
}

public class Knuckles : Ficha
{
    public Knuckles(Jugador prop) : base(prop, 3, 4) {}

    public override string Descripcion => $"KNUCKLES \n Velocidad: {this.velocidad}, Enfriamiento: {this.enfriamiento}. Le resta dos turnos de enfriamiento al resto de fichas de su propietario";

    public override TipoFicha tipo => TipoFicha.Knuckles;

    public override void Habilidad(GameController controller)
    {
        foreach (var ficha in Propietario.Fichas)
        {
            if(ficha.enfriamiento > 1) ficha.enfriamiento -= 2;
        }
    }
}

public class RickRoll : Ficha
{
    public RickRoll(Jugador prop) : base(prop, 5, 3) {}

    public override string Descripcion => $"RICKROLL \n Velocidad: {this.velocidad}, Enfriamiento: {this.enfriamiento}. Toma una ficha al azar de un jugador al azar y la manda a un lugar al azar :)";

    public override TipoFicha tipo => TipoFicha.RickRoll;

    public override void Habilidad(GameController controller) //envia una ficha random de un jugador random a una casilla que no sea una letra
    {
        Random random = new Random();
        int jugador = random.Next(0, Laberinto.jugadores.Count - 1);
        int fila = random.Next(0,Laberinto.Tamanno);
        int columna = random.Next(0,Laberinto.Tamanno);

        while(controller.Jugadores[jugador].Equals(Propietario)) jugador = random.Next(0, controller.Jugadores.Count - 1);//eligiendo un jugador distinto del propietario
        int ficha = random.Next(0,Propietario.Fichas.Count);
        if(controller.Jugadores[jugador].jugador.Fichas[ficha].tipo == TipoFicha.StarMan){
            Console.WriteLine("La Habilidad no tiene efecto sobre Starman");
            return;
        }

        while(controller.Maze.LaberinthCSharp[fila, columna].Tipo == Casilla.LetraMondongo){ //eligiendo una casilla que no tenga letra
            fila = random.Next(0,Laberinto.Tamanno);
            columna = random.Next(0,Laberinto.Tamanno);
        }

        controller.Jugadores[jugador].jugador.Fichas[ficha].posicionAnterior = controller.Jugadores[jugador].jugador.Fichas[ficha].posicion; //cambiando las coordenadas
        controller.Jugadores[jugador].jugador.Fichas[ficha].posicion = (fila, columna); 

        (int, int) posicionAnt = controller.Jugadores[jugador].jugador.Fichas[ficha].posicionAnterior;
        controller.Maze.LaberinthCSharp[posicionAnt.Item1,posicionAnt.Item2].FichasEnCasilla.Remove(Laberinto.jugadores[jugador].Fichas[ficha]); //elimino la ficha de la posicion anterior
        controller.Maze.LaberinthCSharp[fila,columna].FichasEnCasilla.Add(controller.Jugadores[jugador].jugador.Fichas[ficha]); //la anado a la nueva
        controller.Maze.LaberinthCSharp[fila,columna].Accion(controller); //activo la casilla
    }
}

public class StarMan : Ficha
{
    public StarMan(Jugador prop) : base(prop, 4, 0) {}

    public override string Descripcion => $"STARMAN \n Velocidad: {this.velocidad}, Enfriamiento: {this.enfriamiento}. Es inmune a las habilidades del resto de fichas";

    public override TipoFicha tipo => TipoFicha.StarMan;

    public override void Habilidad(GameController controller)
    {
        //es inmune a todas las habilidades del resto de fichas
    }
}

public class Doge : Ficha
{
    public Doge(Jugador prop) : base(prop, 3, 5) {}

    public override string Descripcion => $"DOGUE \n Velocidad: {this.velocidad}, Enfriamiento: {this.enfriamiento}. De una galleta le baja la velocidad al resto fichas en su fila";

    public override TipoFicha tipo => TipoFicha.Doge;

    public override void Habilidad(GameController controller) //de un galletazo le baja la velocidad a todas las fichas en su misma fila(incluyendo aliados)
    {
        int fila = posicion.Item1;
        for (int columna = 0; columna < 10; columna++)
        {
            foreach (var ficha in controller.Maze.LaberinthCSharp[fila,columna].FichasEnCasilla)
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
    public ELChoco(Jugador prop) : base(prop, 4, 0) {}

    public override string Descripcion => $"EL CHOCO \n Velocidad: {this.velocidad}, Enfriamiento: {this.enfriamiento}. Al choco no hay trampa que le haga daño";

    public override TipoFicha tipo => TipoFicha.ELChoco;

    public override void Habilidad(GameController controller)
    {
        //Es inmune a todas las trampas del juego
    }
}