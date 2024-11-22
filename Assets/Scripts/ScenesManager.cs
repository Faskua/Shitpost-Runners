using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public Animator SceneLoader;

    public void AnimGame(){
        SceneLoader.SetTrigger("Start");
        Invoke("Game", 1);
    }
    public void AnimMenu(){
        SceneLoader.SetTrigger("Start");
        Invoke("Menu", 1);
    }

    public void Exit(){
        Application.Quit();
    }

    void Game(){
        SceneManager.LoadScene(1);
    }

    void Menu(){
        SceneManager.LoadScene(0);
    }

    
}
