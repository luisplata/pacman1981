using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorDeVolumen : MonoBehaviour
{
    public AudioSource audio;
    // Start is called before the first frame update
    private void Start()
    {
        if (PlayerPrefs.HasKey("volumenGeneral"))
        {
            audio.volume = PlayerPrefs.GetFloat("volumenGeneral");
        }
    }
}
