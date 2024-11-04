
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

    public void Jugar(int ficha, int fila, int columna){
        Fichas[ficha].Jugar(fila, columna);
        FinDeMiturno =  true;
    }
}