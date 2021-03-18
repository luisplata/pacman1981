﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class InicioDeJuego : MonoBehaviour
{
    public void CargarJuego()
    {
        PlayerPrefs.DeleteKey("vidas");
        PlayerPrefs.DeleteKey("score");
#if UNITY_ANDROID
        SceneManager.LoadScene("021_pacmanMovil");
#else
        SceneManager.LoadScene(1);
        //SceneManager.LoadScene("02_Pacman");
#endif
        PlayerPrefs.SetInt("nivel", 1);
    }
}
