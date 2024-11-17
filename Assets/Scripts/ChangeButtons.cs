using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButtons : MonoBehaviour
{
    public Sprite gray;
    public Sprite green;

    public void ToGreen(){
        gameObject.GetComponent<Image>().sprite = green;
    }

    public void ToGray(){
        gameObject.GetComponent<Image>().sprite = gray;
    }
}
