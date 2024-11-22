using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


public class LaberintoUN : MonoBehaviour
{
    public Sprite Obstaculo;
    public Sprite Fan;
    public Sprite Abuelito;
    public Sprite Ducha;
    public Sprite Morfeo;
    public Sprite Honguito;
    public Sprite Zorro;
    public ICasilla[,] LaberinthCSharp;
    public GameObject casillaPrefab;
    private static (int, int)[] Directions = {(0,2),(2,0),(0,-2),(-2,0)};

    void Start(){
        GenerateLaberinth();

        InstanciarCasillas();
    }
    private void ShuffleDirections(){
        System.Random random = new System.Random();
        for (int i = 0; i < Directions.Length - 1; i++)
        {
            int j = random.Next(i + 1);
            (int,int) temporal = Directions[i];
            Directions[i] = Directions[j];
            Directions[j] = temporal;
        }
    }

    private void GenerateLaberinth(){
        LaberinthCSharp = new ICasilla[15,15];
        for (int fila = 0; fila < 15; fila++)
        {
            for (int col = 0; col < 15; col++)
            {
                ICasilla obs = new Obstaculo();
                LaberinthCSharp[fila,col] = obs;
            }
        }
        AbrirCaminos(1,1);
    }

    public void InstanciarCasillas(){
        for (int fila = 0; fila < 15; fila++)
        {
            for (int col = 0; col < 15; col++)
            {
                GameObject cas = Instantiate(casillaPrefab, new Vector2(0,0), Quaternion.identity);
                cas.transform.SetParent(this.transform, false);
                cas.GetComponent<CasillaUN>().Casilla = LaberinthCSharp[fila,col];
                cas.GetComponent<CasillaUN>().Fila = fila;
                cas.GetComponent<CasillaUN>().Columna = col;
                cas.GetComponent<CasillaUN>().Visitada = false;
                if(LaberinthCSharp[fila, col] is Obstaculo){  
                    cas.GetComponent<Image>().sprite = Obstaculo;
                    cas.GetComponent<CasillaUN>().Visitada = true;  
                }
                if(LaberinthCSharp[fila, col] is FanDeBerserk){  
                    cas.GetComponent<Image>().sprite = Fan;
                    cas.GetComponent<CanvasGroup>().alpha = 0;  
                }
                if(LaberinthCSharp[fila, col] is Abuelito){  
                    cas.GetComponent<Image>().sprite = Abuelito;  
                    cas.GetComponent<CanvasGroup>().alpha = 0;
                }
                if(LaberinthCSharp[fila, col] is Ducha){  
                    cas.GetComponent<Image>().sprite = Ducha;  
                    cas.GetComponent<CanvasGroup>().alpha = 0;
                }
                if(LaberinthCSharp[fila, col] is Morfeo){  
                    cas.GetComponent<Image>().sprite = Morfeo; 
                    cas.GetComponent<CanvasGroup>().alpha = 0;
                }
                if(LaberinthCSharp[fila, col] is Honguito){  
                    cas.GetComponent<Image>().sprite = Honguito;  
                    cas.GetComponent<CanvasGroup>().alpha = 0;
                }
                if(LaberinthCSharp[fila, col] is Zorro){  
                    cas.GetComponent<Image>().sprite = Zorro;  
                    cas.GetComponent<CanvasGroup>().alpha = 0;
                }
                if(LaberinthCSharp[fila, col] is Vacia){  
                    cas.GetComponent<Image>().sprite = Zorro;  
                    cas.GetComponent<CanvasGroup>().alpha = 0;
                }
            }
        }
    }

    private void AbrirCaminos(int x, int y){
        LaberinthCSharp[x,y] = GenerarCasilla();
        ShuffleDirections();
        foreach (var direction in Directions)
        {
            int newX = x + direction.Item1;
            int newY = y + direction.Item2;

            if(newX >= 1 && newX < 14 && newY >= 1 && newY < 14 && LaberinthCSharp[newX,newY] is Obstaculo){
                LaberinthCSharp[newX - direction.Item1/2, newY - direction.Item2/2] = GenerarCasilla();
                AbrirCaminos(newX, newY);
            }
        }
    }

    private ICasilla GenerarCasilla(){
        ICasilla casilla = new Vacia();
        System.Random random = new System.Random();
        int Posibilidad= random.Next(1, 8);
        switch (Posibilidad)
        {
            case 2:
                casilla = new FanDeBerserk();
            break;
            case 3:
                casilla = new Abuelito();
            break;
            case 4:
                casilla = new Ducha();
            break;
            case 5:
                casilla = new Morfeo();
            break;
            case 6:
                casilla = new Honguito();
            break;
            case 7:
                int chance = random.Next(0,3);
                if(chance == 1) casilla = new Zorro();
            break;
            default:
                casilla = new Vacia();
            break;
        }
        return casilla;
    }
}
