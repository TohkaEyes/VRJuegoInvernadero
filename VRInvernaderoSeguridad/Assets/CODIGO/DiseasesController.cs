using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Unity.XR.CoreUtils;
using UnityEngine;

public class DiseasesController : MonoBehaviour
{
    public List<GameObject> LISTA_ENFERMEDADES;
    public List<Diseases> LISTA_ENFERMEDADES_MODEL;

    public List<GameObject> ENFERMEDADES_ACTUALES;
    public List<Diseases> ENFERMEDADES_ACTUALES_MODEL;
    public GameObject DiseasesContainer;

    public float margin_element = 40f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ENFERMEDADES_ACTUALES = new List<GameObject>();
        ENFERMEDADES_ACTUALES_MODEL = new List<Diseases>();

        if (DiseasesContainer != null)
        {
            foreach (var item in LISTA_ENFERMEDADES)
            {
                var diseaseActual = item.GetComponent<Diseases>();
                LISTA_ENFERMEDADES_MODEL.Add(diseaseActual);
            }
        }
    }

    public void AddDisease(int index)
    {
        var enfermedad_actual = LISTA_ENFERMEDADES.ElementAt(index);
        var enfermedad_actual_obj = LISTA_ENFERMEDADES_MODEL.ElementAt(index);

        if (enfermedad_actual != null)
        {
            ENFERMEDADES_ACTUALES.Add(enfermedad_actual);
            ENFERMEDADES_ACTUALES_MODEL.Add(enfermedad_actual_obj);
        }

        var gameObject_enfermedad = Instantiate(enfermedad_actual, new Vector3(0, 0, 0), Quaternion.identity);
        gameObject_enfermedad.transform.parent = DiseasesContainer.transform;

        //posicionar rect transform
        var contador_actual = ENFERMEDADES_ACTUALES.Count();
        var rect_transform_actual = gameObject_enfermedad.GetComponent<RectTransform>();
        var posicion_y_actual = (rect_transform_actual.rect.height) * contador_actual + margin_element * (contador_actual - 1);
        rect_transform_actual.anchoredPosition = new Vector3(0f, rect_transform_actual.rect.height / 2, 0f);
    }

    public void AddDisease(GameObject obj)
    {
        var enfermedad_actual = obj.gameObject;
        var enfermedad_actual_obj = obj.GetComponent<Diseases>();

        if (enfermedad_actual != null)
        {
            ENFERMEDADES_ACTUALES.Add(enfermedad_actual);
            ENFERMEDADES_ACTUALES_MODEL.Add(enfermedad_actual_obj);
        }

        var gameObject_enfermedad = Instantiate(enfermedad_actual, new Vector3(0, 0, 0), Quaternion.identity);

        gameObject_enfermedad.transform.parent = DiseasesContainer.transform;

        //posicionar rect transform
        var rect_transform_actual = gameObject_enfermedad.GetComponent<RectTransform>();
        var alto_actual = rect_transform_actual.rect.height;
        var contador_actual = ENFERMEDADES_ACTUALES.Count();

        rect_transform_actual.offsetMin = new Vector2(0, 0);
        rect_transform_actual.offsetMax = new Vector2(0, 0);

        var posicion_y_actual = (alto_actual) * contador_actual + margin_element * (contador_actual - 1);
        rect_transform_actual.anchoredPosition = new Vector3(0f, posicion_y_actual, 0f);
        rect_transform_actual.sizeDelta = new Vector2(0, alto_actual);

    }
}
