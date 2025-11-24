using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //TODO HACER CODIGO
    public SliderBar statusBar;
    public float statusCurrent; 

    public DiseasesController diseasesController;

    public float proteccion_actual = 10f;

    public GameObject SpawnPoint;
    public GameObject Bullet;
    public GameObject BulletContainer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        statusCurrent = 0f;
        statusBar.SetMaxValue(100f);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space) )
        //{
        //    RollEnfermedad();
        //}

        //Invocar Disparo de Veneno
        if (Input.GetKeyDown(KeyCode.LeftShift) )
        {
            if (SpawnPoint != null)
            {
                if (Bullet != null)
                {
                    //Clonar Objeto Bullet 
                    var bulletActual = Instantiate(Bullet);
                    bulletActual.transform.parent = BulletContainer.transform;
                    bulletActual.transform.position = SpawnPoint.transform.position;
                }
            }
        }
    }

    public void AgregarProteccion( float proteccion_add)
    {
        proteccion_actual += proteccion_add; 
    }

    public void RollEnfermedad()
    {
        statusCurrent += UnityEngine.Random.Range(0f, 10f);
        statusBar.SetValue(statusCurrent);

        //revisar si toca enfermedad
        var max_value = statusCurrent + proteccion_actual;
        var disease_roll = UnityEngine.Random.Range(0f, max_value);

        var valor_busqueda_actual = statusCurrent - proteccion_actual;

        var disease_currents = diseasesController.LISTA_ENFERMEDADES_MODEL.Where(x => x.PORCENTAJE < valor_busqueda_actual).ToList();

        foreach (var item in disease_currents)
        {
            //tirada para ver si se enferma con alguna de las enfermedades actuales
            var actuall_roll = UnityEngine.Random.Range(0f, max_value);
            if (actuall_roll < item.PORCENTAJE)
            {
                if (diseasesController.ENFERMEDADES_ACTUALES_MODEL.Any(x => x.CODIGO == item.CODIGO) == false)
                {
                    var actual = item.gameObject;
                    diseasesController.AddDisease(actual);
                }
            }
        }
    }
}
