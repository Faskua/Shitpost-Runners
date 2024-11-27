using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


public class LaberintoUN : MonoBehaviour
{
    public Sprite Obstaculo;
    public Sprite ChillGuy;
    public Sprite Abuelito;
    public Sprite Ducha;
    public Sprite Morfeo;
    public Sprite Honguito;
    public Sprite Zorro;
    public Sprite Nulo;
    public Sprite Mondongo;
    public ICasilla[,] LaberinthCSharp;
    public GameObject[,] LabGameObj;
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
        ReorganiceLetters();
    }

    public void InstanciarCasillas(){
        LabGameObj = new GameObject[15,15];
        for (int fila = 0; fila < 15; fila++)
        {
            for (int col = 0; col < 15; col++)
            {
                GameObject cas = Instantiate(casillaPrefab, new Vector2(0,0), Quaternion.identity);
                cas.transform.SetParent(this.transform, false);
                cas.GetComponent<CasillaUN>().Casilla = LaberinthCSharp[fila,col];
                cas.GetComponent<CasillaUN>().Fila = fila;
                cas.GetComponent<CasillaUN>().Columna = col;
                Color color = cas.GetComponent<Image>().color;
                color.a = 0;

                if(LaberinthCSharp[fila, col] is Obstaculo){  
                    cas.GetComponent<Image>().sprite = Obstaculo; 
                }
                if(LaberinthCSharp[fila, col] is ChillGuy){  
                    cas.GetComponent<Image>().sprite = ChillGuy;
                    cas.GetComponent<Image>().color = color;
                }
                if(LaberinthCSharp[fila, col] is Abuelito){  
                    cas.GetComponent<Image>().sprite = Abuelito;  
                    cas.GetComponent<Image>().color = color;
                }
                if(LaberinthCSharp[fila, col] is Ducha){  
                    cas.GetComponent<Image>().sprite = Ducha;  
                    cas.GetComponent<Image>().color = color;
                }
                if(LaberinthCSharp[fila, col] is Morfeo){  
                    cas.GetComponent<Image>().sprite = Morfeo; 
                    cas.GetComponent<Image>().color = color;
                }
                if(LaberinthCSharp[fila, col] is Honguito){  
                    cas.GetComponent<Image>().sprite = Honguito;  
                    cas.GetComponent<Image>().color = color;
                }
                if(LaberinthCSharp[fila, col] is LetraClave){  
                    cas.GetComponent<Image>().sprite = Mondongo;
                    //cas.GetComponent<Image>().color = color;
                }
                if(LaberinthCSharp[fila, col] is Zorro){  
                    cas.GetComponent<Image>().sprite = Zorro;  
                    cas.GetComponent<Image>().color = color;
                }
                if(LaberinthCSharp[fila, col] is Cofre){  
                    cas.GetComponent<Image>().sprite = Nulo;  
                    cas.GetComponent<Image>().color = color;
                }
                LabGameObj[fila,col] = cas;
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

    ICasilla GenerarCasilla(){
        ICasilla casilla = new Cofre();
        System.Random random = new System.Random();
        int Posibilidad= random.Next(1, 8);
        switch (Posibilidad)
        {
            case 2:
                casilla = new ChillGuy();
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
                casilla = new Cofre();
            break;
        }
        return casilla;
    }

    void ReorganiceLetters(){
        char[] Letras = {'M','O','N','D','O','N','G','O'};
        for (int letra = 0; letra < Letras.Length; letra++)
        {
            System.Random random = new System.Random();
            int fila = random.Next(0,15);
            int columna = random.Next(0,15);
            while(LaberinthCSharp[fila,columna].Tipo == Casilla.Obstáculo || LaberinthCSharp[fila,columna].Tipo == Casilla.LetraMondongo){
                fila = random.Next(0,15);
                columna = random.Next(0,15);
            }
            ICasilla cas = new LetraClave(Letras[letra]);
            LaberinthCSharp[fila,columna] = cas;
        }
    }
}
