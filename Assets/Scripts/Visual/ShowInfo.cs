using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowInfo : MonoBehaviour
{
    public GameObject TextPrefab;
    private GameObject canvas;
    private GameObject Text;

    public void OnMoseEnter(){
        TextPrefab.transform.GetChild(0).GetComponent<Text>().text = gameObject.GetComponent<FichaUN>().ficha.Descripcion;
        Text = Instantiate(TextPrefab, new Vector2(gameObject.transform.position.x + 200, gameObject.transform.position.y), Quaternion.identity);
        Text.transform.SetParent(canvas.transform, true);
    }

    public void OnMouseExit(){
        if(Text != null) Destroy(Text.gameObject);
    }

    void Start(){
        canvas = GameObject.FindGameObjectWithTag("Canvas");
    }
}
