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
        SceneManager.LoadScene("02_Pacman");
        PlayerPrefs.SetInt("nivel", 1);
    }
}
