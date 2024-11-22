using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObject : MonoBehaviour
{
    private static PersistentObject instance;

    void Awake()
    {
        if (instance == null){
            instance = this; 
            DontDestroyOnLoad(gameObject);
            Debug.Log("Objeto llevado a otra escena") ;
        }
        else  { 
            Destroy(gameObject);
            Debug.Log("Objeto eliminado");
        }
    }
}
