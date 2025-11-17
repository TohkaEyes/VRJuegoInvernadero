using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Diseases : MonoBehaviour
{
    public int CODIGO;
    public string NOMBRE;
    public string DESCRIPCION;
    public float PORCENTAJE;
    public string TRATAMIENTO;

    public Color COLOR_BACKGROUND;
    public Color COLOR_TEXTO;
    public Image BACKGROUND;
    public TMPro.TextMeshProUGUI TEXTO;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (COLOR_TEXTO != null)
        {
            TEXTO.color = COLOR_TEXTO;
        }

        if (COLOR_BACKGROUND != null)
        {
            BACKGROUND.color = COLOR_BACKGROUND;
        }

        TEXTO.text = NOMBRE;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
