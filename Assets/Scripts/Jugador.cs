
public class Jugador : IJugador
{
    string nombre;
    bool leToca = false;
    bool finDeMiturno = false;
    List<Ficha> fichas;
    List<char> letras;
    public List<Ficha> Fichas { get => fichas; }
    public string Nombre { get => nombre; set => nombre = value; }
    public List<char> LetrasConseguidas { get => letras; set => letras = value; }
    public bool LeToca { get => leToca; set => leToca = value; }
    public bool FinDeMiturno { get => finDeMiturno; set => finDeMiturno = value; }

    public Jugador(string nombre){
        fichas = new List<Ficha>();
        this.nombre = nombre;
        letras = new List<char>();
    }
    public void AnadirFicha(Ficha ficha){
        Fichas.Add(ficha);
    }

    public void Jugar(int ficha, int fila, int columna){ //esta funcion la estoy podiendo junta para no olvidarme, pero jugar es solo lo que esta en el if, el resto es para el boton
        Ficha prueba = Fichas[ficha];
        int pasosDados = Math.Abs(fila - prueba.posicion.Item1 + columna - prueba.posicion.Item2);
        if(pasosDados <= prueba.velocidad){
            Fichas[ficha].Jugar(fila, columna);
            FinDeMiturno =  true;
            return;
        }
        throw new InvalidOperationException("Velocidad insuficiente");
    }
}