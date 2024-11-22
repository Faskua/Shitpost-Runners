using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FichaUN : MonoBehaviour
{
    public Ficha ficha;
    public TipoFicha Tipo => ficha.tipo;

    public FichaUN(Ficha ficha){ this.ficha = ficha; }
    public void Habilidad(GameController controller){
        ficha.Habilidad(controller);
    }
}
