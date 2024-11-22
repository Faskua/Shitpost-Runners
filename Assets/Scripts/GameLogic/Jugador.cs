using System.Collections.Generic;
using System;
public class Jugador : IJugador
{
    string nombre;
    bool leToca = false;
    public int TurnosSinJugar;
    List<Ficha> fichas;
    List<char> letras;
    public List<Ficha> Fichas { get => fichas; set => fichas = value;}
    public List<FichaUN> FichasUN;
    public string Nombre { get => nombre; set => nombre = value; }
    public List<char> LetrasConseguidas { get => letras; set => letras = value; }
    public bool LeToca { get => leToca; set => leToca = value; }

    public Jugador(string nombre){
        fichas = new List<Ficha>();
        this.nombre = nombre;
        letras = new List<char>();
        TurnosSinJugar = 0;
        FichasUN = GetFichasUN();
    }
    public void AnadirFicha(Ficha ficha){
        Fichas.Add(ficha);
    }

    public bool Jugar(int ficha, int fila, int columna, GameController controller){ //esta funcion la estoy podiendo junta para no olvidarme, pero jugar es solo lo que esta en el if, el resto es para el boton
        if(TurnosSinJugar > 0) controller.AvanzarTurno();
        Ficha prueba = Fichas[ficha];
        int pasosDados = Math.Abs(fila - prueba.posicion.Item1 + columna - prueba.posicion.Item2);
        if(pasosDados <= prueba.velocidad){
            Fichas[ficha].Jugar(fila, columna, controller);
            //FinDeMiturno =  true;
            return true;
        }
        return false;
    }

    List<FichaUN> GetFichasUN(){
        List<FichaUN> output = new List<FichaUN>();
        foreach (Ficha ficha in Fichas) output.Add(new FichaUN(ficha));
        return output;
    }
}