public static class Laberinto
{
    public static bool Resuelto = false;
    public static List<bool> Mascara;
    public static List<char> Letras;
    public static ICasilla[,] laberinto;
    public static List<IJugador> jugadores = new List<IJugador>();
    static int Tamanno;

    public static void Generar(){
        Tamanno = 10;
        laberinto = new ICasilla[10, 10]; //crea el laberinto
        for (int fila = 0; fila < Tamanno; fila++) // lo deja vacio completo
        {
            for (int columna = 0; columna < Tamanno; columna++)
            {
                ICasilla vacia = new Vacia();
                laberinto[fila,columna] = vacia;
            }
        }
        Letras = ['M','O','N','D','O','N','G','O'];
        Mascara = [false, false, false, false, false, false, false, false];
    }

    public static void EstaResuelto() => Resuelto = Mascara.All(x => x);

    public static void Reorganizar(){
        Random random = new Random();
        for (int fila = 0; fila < Tamanno; fila++)
        {
            for (int columna = 0; columna < Tamanno; columna++)
            {
                int Posibilidad= random.Next(1, 8);
                switch (Posibilidad)
                {
                    case 2:
                        if( EsPosible(fila, columna, Tamanno) && //si es posible moverse sin salir de la matriz
                            laberinto[fila-1,columna].PuedePasar && laberinto[fila+1,columna].PuedePasar && //si ningun adyacente es obstaculo
                            laberinto[fila,columna-1].PuedePasar && laberinto[fila,columna+1].PuedePasar &&
                            laberinto[fila-1,columna-1].PuedePasar && laberinto[fila-1,columna+1].PuedePasar &&
                            laberinto[fila+1,columna-1].PuedePasar && laberinto[fila+1,columna+1].PuedePasar){
                            
                            ICasilla obstaculo = new Obstaculo();
                            laberinto[fila,columna] = obstaculo;
                        }
                        else{
                            ICasilla vacia = new Vacia();
                            laberinto[fila,columna] = vacia;
                        }
                    break;
                    case 3:
                        ICasilla berserk = new FanDeBerserk();
                        laberinto[fila,columna] = berserk;
                    break;
                    case 4:
                        ICasilla abuelito = new Abuelito();
                        laberinto[fila,columna] = abuelito;
                    break;
                    case 5:
                        ICasilla ducha = new Ducha();
                        laberinto[fila,columna] = ducha;
                    break;
                    case 6:
                        ICasilla morfeo = new Morfeo();
                        laberinto[fila,columna] = morfeo;
                    break;
                    case 7:
                        ICasilla honguito = new Honguito();
                        laberinto[fila,columna] = honguito;
                    break;
                    case 8:
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

    public static bool EsPosible(int fila, int columna, int size) => fila > 0 && fila < size - 1 && columna > 0 && columna < size - 1;
}