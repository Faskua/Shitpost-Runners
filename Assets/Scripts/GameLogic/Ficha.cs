using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class McQueen : Ficha
{
    public McQueen(IJugador prop) : base(prop, 4, 4) {}

    public override string Descripcion => $"McQUEEN \nVelocidad: {this.velocidad}, Enfriamiento: {this.EnfActual}. Aumenta su propia velocidad en 1. Cuchau";

    public override TipoFicha tipo => TipoFicha.Mcqueen;

    public override void Habilidad(GameController controller) //aumenta su velocidad
    {
        if(EnfActual <= 0)
        {
            velocidad++;
            EnfActual = 4;
            HabilidadDescrp = "Velocidad aumentada";
        }
        else HabilidadDescrp = $"Habilidad gastada, espera {EnfActual} turnos";
    }
}

public class CJ : Ficha
{
    public CJ(IJugador prop) : base(prop, 2, 7) {}

    public override string Descripcion => $"CJ \n Velocidad: {this.velocidad}, Enfriamiento: {this.EnfActual}. Si comparte casilla con otra ficha le roba una letra a su propietario";

    public override TipoFicha tipo => TipoFicha.CJ;

    public override void Habilidad(GameController controller) //roba una letra si se encuentra en la misma casilla de una ficha de un jugador con letra
    {
        if(EnfActual <= 0)
        {
            foreach (var ficha in controller.Maze.LaberinthCSharp[this.posicion.Item1, this.posicion.Item2].FichasEnCasilla)
            {
                if(ficha.tipo == TipoFicha.StarMan) continue;
                if(!ficha.Propietario.Equals(this.Propietario) && ficha.Propietario.LetrasConseguidas.Count > 0){
                    this.Propietario.LetrasConseguidas.Add(ficha.Propietario.LetrasConseguidas[ficha.Propietario.LetrasConseguidas.Count - 1]);
                    ficha.Propietario.LetrasConseguidas.RemoveAt(ficha.Propietario.LetrasConseguidas.Count - 1);
                    HabilidadDescrp = $"Letra {Propietario.LetrasConseguidas.Last()} robada";
                    controller.visual.InstanciarLetra(Propietario.LetrasConseguidas.Last());
                    return;
                }
                EnfActual = enfriamiento;
            }
            HabilidadDescrp = "You just had to steal the damm letter CJ!!";
        }
        else HabilidadDescrp = $"Habilidad gastada, espera {EnfActual} turnos";
    }
}

public class UNE : Ficha
{
    public UNE(IJugador prop) : base(prop, 3, 6) {}

    public override string Descripcion => $"UNE \n Velocidad: {this.velocidad}, Enfriamiento: {this.EnfActual}. Le quita la luz a un jugador al azar y no podra jugar por dos turnos (incluyendo a Starman)";

    public override TipoFicha tipo => TipoFicha.UNE;

    public override void Habilidad(GameController controller)
    {
        if(EnfActual <= 0)
        {
            System.Random random = new System.Random();
            int objetivo = random.Next(0, controller.Jugadores.Count);
            while(Propietario.Nombre == controller.Jugadores[objetivo].Nombre && controller.Jugadores.Count > 1)  objetivo = random.Next(0, controller.Jugadores.Count);
            controller.Jugadores[objetivo].jugador.TurnosSinJugar += 2;
            EnfActual = enfriamiento;
            HabilidadDescrp = $"{controller.Jugadores[objetivo].jugador.Nombre} no va a tener luz por 2 turnos";
        }
        else HabilidadDescrp = $"Habilidad gastada, espera {EnfActual} turnos";
    }
}

public class Knuckles : Ficha
{
    public Knuckles(IJugador prop) : base(prop, 3, 4) {}

    public override string Descripcion => $"KNUCKLES \n Velocidad: {this.velocidad}, Enfriamiento: {this.EnfActual}. Le resta dos turnos de enfriamiento al resto de fichas de su propietario";

    public override TipoFicha tipo => TipoFicha.Knuckles;

    public override void Habilidad(GameController controller)
    {
        if(EnfActual <= 0){
            foreach (var ficha in Propietario.Fichas)
            {
                if(ficha.EnfActual > 1) ficha.EnfActual -= 2;
                else if(ficha.EnfActual > 0) ficha.EnfActual --;
            }
            EnfActual = enfriamiento;
            HabilidadDescrp = $"Las fichas de {Propietario.Nombre} tienen hasta dos turnos menos de enfriamiento";
        }
        else HabilidadDescrp = $"Habilidad gastada, espera {EnfActual} turnos";
    }
}

public class RickRoll : Ficha
{
    public RickRoll(IJugador prop) : base(prop, 5, 3) {}

    public override string Descripcion => $"RICKROLL \n Velocidad: {this.velocidad}, Enfriamiento: {this.EnfActual}. Toma una ficha al azar de un jugador al azar y la manda a un lugar al azar :)";

    public override TipoFicha tipo => TipoFicha.RickRoll;

    public override void Habilidad(GameController controller) //envia una ficha random de un jugador random a una casilla que no sea una letra
    {
        if(EnfActual <= 0){
            System.Random random = new System.Random();
            int jugador = random.Next(0, controller.Jugadores.Count);
            while(controller.Jugadores[jugador].Nombre == Propietario.Nombre && controller.Jugadores.Count > 1){   jugador = random.Next(0, controller.Jugadores.Count - 1);   }
            int fila = random.Next(0,15);
            int columna = random.Next(0,15);
            int ficha = random.Next(0,5);

            if(controller.Jugadores[jugador].jugador.Fichas[ficha].tipo == TipoFicha.StarMan){
                HabilidadDescrp = "La Habilidad no tiene efecto sobre Starman";
                return;
            }

            while(controller.Maze.LaberinthCSharp[fila, columna].Tipo == Casilla.LetraMondongo || controller.Maze.LaberinthCSharp[fila, columna].Tipo == Casilla.Obstaculo){ //eligiendo una casilla 
                fila = random.Next(0,15);
                columna = random.Next(0,15);
            }

            controller.Maze.LaberinthCSharp[posicion.Item1,posicion.Item2].FichasEnCasilla.Remove(controller.Jugadores[jugador].jugador.Fichas[ficha]); //elimino la ficha de la posicion anterior
            controller.Jugadores[jugador].jugador.Fichas[ficha].posicion = (fila, columna); 
            controller.Maze.LaberinthCSharp[fila,columna].FichasEnCasilla.Add(controller.Jugadores[jugador].jugador.Fichas[ficha]); //la anado a la nueva

            controller.Jugadores[jugador].FichasUN[ficha].transform.SetParent(controller.Maze.LabGameObj[fila,columna].transform, true); //moviendola en lo visual
            controller.Jugadores[jugador].FichasUN[ficha].transform.position = controller.Maze.LabGameObj[fila,columna].transform.position;

            controller.Maze.LabGameObj[fila,columna].GetComponent<CasillaUN>().Accion(controller); //activo la casilla
            controller.visual.CasillaNombre.text = controller.Maze.LaberinthCSharp[fila,columna].Mensaje;

            EnfActual = enfriamiento;
            HabilidadDescrp = $"La ficha {controller.Jugadores[jugador].jugador.Fichas[ficha].tipo} de {controller.Jugadores[jugador].jugador.Nombre} ha sido llevada a la casila ({columna},{fila})";
        }
        else HabilidadDescrp = $"Habilidad gastada, espera {EnfActual} turnos";  
    }
}

public class StarMan : Ficha
{
    public StarMan(IJugador prop) : base(prop, 4, 0) {}

    public override string Descripcion => $"STARMAN \n Velocidad: {this.velocidad}, Enfriamiento: {this.EnfActual}. Es inmune a las habilidades del resto de fichas";

    public override TipoFicha tipo => TipoFicha.StarMan;

    public override void Habilidad(GameController controller)
    {
        HabilidadDescrp = "Este hombre es inmune a todas las habilidades";
    }
}

public class Doge : Ficha
{
    public Doge(IJugador prop) : base(prop, 3, 5) {}

    public override string Descripcion => $"DOGUE \n Velocidad: {this.velocidad}, Enfriamiento: {this.EnfActual}. De una galleta le baja hasta 2 puntos en velocidad al resto fichas en su fila";

    public override TipoFicha tipo => TipoFicha.Doge;

    public override void Habilidad(GameController controller) //de un galletazo le baja la velocidad a todas las fichas en su misma fila(incluyendo aliados)
    {
        if(EnfActual <= 0){
            int fila = posicion.Item1;
            int vel = this.velocidad;
            for (int columna = 0; columna < 15; columna++)
            {
                if(controller.Maze.LaberinthCSharp[fila,columna].FichasEnCasilla.Count > 0){
                    foreach (var ficha in controller.Maze.LaberinthCSharp[fila,columna].FichasEnCasilla)
                    {
                        if(ficha.tipo == TipoFicha.StarMan) continue;
                        if(ficha.velocidad > 2) ficha.velocidad -=2;
                        else if(ficha.velocidad > 1) ficha.velocidad --;
                    }
                }
            }
            this.velocidad = vel;
            EnfActual = enfriamiento;
            HabilidadDescrp = $"A todas las fichas en la fila {fila} se les ha disminuido hasta 2 puntos en velocidad";
        }
        else HabilidadDescrp = $"Habilidad gastada, espera {EnfActual} turnos";
    }
}

public class ELChoco : Ficha
{
    public ELChoco(IJugador prop) : base(prop, 4, 0) {}

    public override string Descripcion => $"EL CHOCO \n Velocidad: {this.velocidad}, Enfriamiento: {this.EnfActual}. Al Rey del reparto las trampas no le hacen nada, todavia no tiene la boca llena de hormigas";

    public override TipoFicha tipo => TipoFicha.ELChoco;

    public override void Habilidad(GameController controller)
    {
        HabilidadDescrp = "Al Rey del reparto las trampas no le hacen na";
    }
}