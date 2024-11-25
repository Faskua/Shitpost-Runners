using System.Collections.Generic;
using System.Linq;
using System;
public static class Laberinto
{
    public static bool Resuelto = false;
    public static List<bool> Mascara;
    public static List<char> Letras;
    public static ICasilla[,] laberinto;
    public static List<IJugador> jugadores = new List<IJugador>();
    public static int Tamanno;

    public static void Generar(int tamanno){
        Tamanno = tamanno - 1;
        laberinto = new ICasilla[tamanno, tamanno]; //crea el laberinto
        for (int fila = 0; fila < tamanno; fila++) // lo deja vacio completo
        {
            for (int columna = 0; columna < tamanno; columna++)
            {
                ICasilla vacia = new Vacia();
                laberinto[fila,columna] = vacia;
            }
        }
        Letras = new List<char> {'M','O','N','D','O','N','G','O'};
        Mascara = new List<bool> {false, false, false, false, false, false, false, false};

        //Aqui me aegurare en el futuro de instanciar los obstaculos (osea es el mismo pero en varias casillas por referencia)
    }

    public static void EstaResuelto() => Resuelto = Mascara.All(x => x);

    public static void Reorganizar(){
        Random random = new Random();
        for (int fila = 0; fila <= Tamanno; fila++)
        {
            for (int columna = 0; columna <= Tamanno; columna++)
            {
                int Posibilidad= random.Next(1, 7);
                if(laberinto[fila,columna] is Obstaculo) continue;
                switch (Posibilidad)
                {
                    case 2:
                        ICasilla berserk = new ChillGuy();
                        laberinto[fila,columna] = berserk;
                    break;
                    case 3:
                        ICasilla abuelito = new Abuelito();
                        laberinto[fila,columna] = abuelito;
                    break;
                    case 4:
                        ICasilla ducha = new Ducha();
                        laberinto[fila,columna] = ducha;
                    break;
                    case 5:
                        ICasilla morfeo = new Morfeo();
                        laberinto[fila,columna] = morfeo;
                    break;
                    case 6:
                        ICasilla honguito = new Honguito();
                        laberinto[fila,columna] = honguito;
                    break;
                    case 7:
                        ICasilla zorro = new Zorro();
                        laberinto[fila,columna] = zorro;
                    break;
                    default:
                        ICasilla CasillaVacia = new Vacia();
                        laberinto[fila, columna] = CasillaVacia;
                    break;
                }
            }
        }
        foreach (char Letra in Letras) //reordenando las letras
        {
            int fila = random.Next(0,7);
            int columna = random.Next(0,7);
            while (laberinto[fila,columna] is LetraClave)
            {
                fila = random.Next(0,7);
                columna = random.Next(0,7);
            }
            ICasilla letra = new LetraClave(Letra);
            laberinto[fila, columna] = letra;
        }
    }

}