using System;
using System.Collections.Generic;

class Program
{
    // public static List<Jugador> jugadores = new List<Jugador>();

    // public static void Main(){
    //     jugadores.Clear();
    //     Laberinto.Generar(15);
    //     Laberinto.Reorganizar();
    //     Console.WriteLine("Cuantos jugadores son");
    //     int cant = int.Parse(Console.ReadLine());
    //     // for (int i = 1; i <= cant; i++)
    //     // {
    //     //     Console.WriteLine("Escribe el nombre del jugador: " + i);
    //     //     AnadirJugador(Console.ReadLine());
    //     // }
    //     int turno = 0;
    //     while(!Laberinto.Resuelto){
    //         if(turno >= jugadores.Count) turno = 0; //reiniciando los turnos
    //         jugadores[turno].LeToca = true; //dando permiso al jugador para jugar
    //         //En consola pediria las coordenadas con un console readline;
    //         while (!jugadores[turno].FinDeMiturno) {} //esperando a que termine
    //         jugadores[turno].FinDeMiturno = false; //reiniciando el bool
    //         Laberinto.EstaResuelto(); //preguntando si ya se encontraron todas las letras
    //         PasaTurno(); //reducir los turnos sin jugar
    //     }
    //     string NombreGanador = "";
    //     int Mayor = 0;
    //     foreach (var jugador in jugadores)
    //     {
    //         Mayor = Math.Max(Mayor, jugador.LetrasConseguidas.Count);
    //         if(Mayor == jugador.LetrasConseguidas.Count) NombreGanador = jugador.Nombre;
    //     }
    //     Console.WriteLine($"El Ganador es {NombreGanador}");
    // }

    // public static void AnadirJugador(Jugador jugador){
    //     jugadores.Add(jugador);
    // }

    // public static void PasaTurno(){
    //     foreach (var jugador in jugadores)
    //     {
    //         foreach (var ficha in jugador.Fichas)
    //         {
    //             if(ficha.turnosSinJugar > 0) ficha.turnosSinJugar--;
    //             if(ficha.enfriamiento > 0) ficha.enfriamiento--; 
    //         }
    //     }
    // }
}