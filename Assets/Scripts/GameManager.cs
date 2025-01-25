using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int player;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && player < 5)
        {
            player++;
        }
    }
}
