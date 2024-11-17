using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowInfo : MonoBehaviour
{
    public GameObject TextPrefab;
    private GameObject Text;

    public void OnMoseEnter(){
        TextPrefab.transform.GetChild(0).GetComponent<Text>().text = gameObject.GetComponent<FichaUN>().ficha.Descripcion;
        Text = Instantiate(TextPrefab, new Vector2(-300,0), Quaternion.identity);
    }

    public void OnMouseExit(){
        if(Text != null) Destroy(Text.gameObject);
    }
}
