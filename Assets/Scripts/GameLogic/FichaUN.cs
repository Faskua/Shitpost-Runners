using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FichaUN : MonoBehaviour
{
    public Ficha ficha;
    public TipoFicha Tipo => ficha.tipo;

    public void Habilidad(){
        ficha.Habilidad();
    }
}
