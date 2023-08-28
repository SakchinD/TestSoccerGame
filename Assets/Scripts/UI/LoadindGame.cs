using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadindGame : MonoBehaviour
{
    void Start()
    {
       SceneManager.LoadSceneAsync("Game");
    }
}