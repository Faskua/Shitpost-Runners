using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FichaUN : MonoBehaviour
{
    public Ficha ficha;
    public TipoFicha Tipo => ficha.tipo;
    public bool Seleccionada => ficha.seleccionada;
    private Text habilidadDesc;

    public void Habilidad(){
        if(Input.GetMouseButtonUp(1)){
            GameController controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            ficha.Habilidad(controller);
            controller.visual.SetHabilidad(ficha.HabilidadDescrp);
        }
    }

    public void Seleccionar(){
        foreach (var ficha in ficha.Propietario.Fichas)
        {
            ficha.seleccionada = false;
        }
        ficha.seleccionada = true;
    }

    void Start(){
        habilidadDesc = GameObject.Find("FichaHabilidad").GetComponent<Text>();
    }
}
