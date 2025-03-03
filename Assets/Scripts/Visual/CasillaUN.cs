using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CasillaUN : MonoBehaviour
{
    public ICasilla Casilla;
    public int Fila;
    public int Columna;
    public Casilla Tipo => Casilla.Tipo;


    public void Accion(GameController controller){
        Visible();
        if(Casilla.Tipo is global::Casilla.LetraMondongo){
            if((Casilla as LetraClave).Letra != '.')    ChangeColor();
        } 
        Casilla.Accion(controller);
        
    }

    public void Visible(){
        Color color = gameObject.GetComponent<Image>().color;
        color.a = 1;
        gameObject.GetComponent<Image>().color = color;
    }

    void ChangeColor(){
        Color color = gameObject.GetComponent<Image>().color;
        color.g = 0;
        color.b = 0;
        color.r = 0;
        switch(Casilla.FichasEnCasilla[Casilla.FichasEnCasilla.Count-1].Propietario.Tipo){
            case TipoJugador.Rojo:
                color.r = 255;
            break;
            case TipoJugador.Azul:
                color.b = 255;
            break;
            case TipoJugador.Amarillo:
                color.r = 255;
                color.g = 255;
            break;
            default:
                color.g = 120;
            break;
        }
        gameObject.GetComponent<Image>().color = color;
    }
}
