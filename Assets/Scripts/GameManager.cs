using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public int day;

    public int money;


    private void Awake()
    {
        gameManager = this;
    }

}
