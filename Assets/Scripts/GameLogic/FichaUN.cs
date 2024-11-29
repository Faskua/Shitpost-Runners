using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FichaUN : MonoBehaviour
{
    public Ficha ficha;
    public TipoFicha Tipo => ficha.tipo;
    private Text habilidadDesc;

    public void Habilidad(){
        if(Input.GetMouseButtonUp(1)){
            GameController controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            ficha.Habilidad(controller);
            habilidadDesc.text = ficha.HabilidadDescrp;
        }
    }
    public void Seleccionar(){
        ficha.seleccionada = true;
    }

    void Start(){
        habilidadDesc = GameObject.Find("FichaHabilidad").GetComponent<Text>();
    }
}
