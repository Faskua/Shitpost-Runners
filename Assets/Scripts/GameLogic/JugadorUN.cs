using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class JugadorUN : MonoBehaviour
{
    public string Nombre { get => jugador.Nombre; set => jugador.Nombre = value; }
    public GameObject FichasPrefab;
    public GameObject PlayerPiecesHolder;
    public Jugador jugador { get; set; }
    public bool seleccionado;
    public List<GameObject> FichasUN;

    public Sprite McQueen;
    public Sprite CJ;
    public Sprite UNE;
    public Sprite Knuckles;
    public Sprite Rick;
    public Sprite Starman;
    public Sprite Dogue;
    public Sprite Choco;

    public void Seleccionar(){
        seleccionado = true;
    }

    public void DannoAnim(){
        gameObject.GetComponent<Animator>().SetTrigger("Danno");
    }

    public void GenerateFichasUN(){
        FichasUN = new List<GameObject>();
        foreach (Ficha ficha in jugador.Fichas) {
            GameObject instance = Instantiate(FichasPrefab, new Vector2(0,0), Quaternion.identity);
            if(ficha.tipo == TipoFicha.Mcqueen) instance.GetComponent<Image>().sprite = McQueen;
            if(ficha.tipo == TipoFicha.CJ) instance.GetComponent<Image>().sprite = CJ;
            if(ficha.tipo == TipoFicha.UNE) instance.GetComponent<Image>().sprite = UNE;
            if(ficha.tipo == TipoFicha.Knuckles) instance.GetComponent<Image>().sprite = Knuckles;
            if(ficha.tipo == TipoFicha.RickRoll) instance.GetComponent<Image>().sprite = Rick;
            if(ficha.tipo == TipoFicha.StarMan) instance.GetComponent<Image>().sprite = Starman;
            if(ficha.tipo == TipoFicha.Doge) instance.GetComponent<Image>().sprite = Dogue;
            if(ficha.tipo == TipoFicha.ELChoco) instance.GetComponent<Image>().sprite = Choco;
            instance.GetComponent<FichaUN>().ficha = ficha; 
            instance.transform.SetParent(PlayerPiecesHolder.transform, false);

            FichasUN.Add(instance);
        }
    }
}
