using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogosLoader : MonoBehaviour
{
    public Animator Logos;

    void Awake(){
        Logos.SetTrigger("Iniciar");
    }
}
