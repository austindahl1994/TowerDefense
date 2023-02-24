using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script will manage all the UI as a singleton
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
