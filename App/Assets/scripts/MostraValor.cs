using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostraValor : MonoBehaviour
{
    List<int> valorLista= new List<int>() {0, 0, 0, 0, 0, 0, 0};
    int vez;
    
    void Start()
    {    
        for (int i=0; i<7; i++)
                {
                    int val= PlayerPrefs.GetInt("Lista" +i);
                    valorLista[i]=val;
                }
    }
    void OnMouseDown()
        {
            Debug.Log("funciona");
        }
}
