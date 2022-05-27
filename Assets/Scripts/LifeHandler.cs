using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeHandler : MonoBehaviour
{
    public GameObject life_1;
    public GameObject life_2;
    public GameObject life_3;
    public void Start()
    {
        activateAll();
    }

    public void removeLife(int life)
    {
        if(life == 3)
        {
            life_3.SetActive(false);
        } 
        else if(life == 2)
        {
            life_2.SetActive(false);
        }
        else 
        {
            life_1.SetActive(false);
        }
    }
    public void activateAll()
    {
        life_1.SetActive(true);
        //life_1.gameObject.SetActive(false);
        life_2.SetActive(true);
        life_3.SetActive(true);
    }
}
