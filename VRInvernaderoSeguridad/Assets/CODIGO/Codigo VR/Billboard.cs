using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera camaraPrincipal;

    void Start()
    {
        camaraPrincipal = Camera.main;
    }

    void LateUpdate()
    {
        if (camaraPrincipal != null)
        {
            // Hacemos que el objeto mire hacia la cámara
            transform.LookAt(transform.position + camaraPrincipal.transform.forward);
        }
    }
}