using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogosLoader : MonoBehaviour
{
    public Animator Logos;
    public Animator LogosBackground;

    void Awake(){
        Logos.SetTrigger("Iniciar");
        LogosBackground.SetTrigger("Iniciar");
    }
}
