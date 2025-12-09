using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using System.Collections;

public class SymptomSystem : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject cameraOffset;
    public Image panelComa;
    public AudioSource audioTos;

    private float intensidadMareo = 0f;
    private bool reiniciando = false;

    void Update()
    {
        if (PlayerHealth.Instance == null) return;

        float salud = PlayerHealth.Instance.vidaActual;

        //MAREO (Vida < 80)
        if (salud < 80 && salud > 0)
        {
            intensidadMareo = (80 - salud) * 0.05f;
            float x = Mathf.PerlinNoise(Time.time * 0.5f, 0) * 2 - 1;
            float y = Mathf.PerlinNoise(0, Time.time * 0.5f) * 2 - 1;

            if (cameraOffset != null)
                cameraOffset.transform.localRotation = Quaternion.Euler(x * intensidadMareo, y * intensidadMareo, 0);
        }

        //TEMBLORES (Vida < 60)
        if (salud < 60 && salud > 0)
        {
            float fuerza = (60 - salud) / 60f;
            fuerza = Mathf.Clamp01(fuerza * 0.5f);
            VibrarHardware(XRNode.LeftHand, fuerza, 0.1f);
            VibrarHardware(XRNode.RightHand, fuerza, 0.1f);
        }

        //TOS (Vida < 40)
        if (salud < 40 && salud > 0)
        {
            if (audioTos != null && !audioTos.isPlaying && Random.value < 0.01f)
            {
                audioTos.Play();
            }
        }

        //VISIÓN NEGRA / COMA (Vida < 20)
        if (panelComa != null)
        {
            float alpha = 0;
            if (salud < 20) alpha = 1 - (salud / 20f);

            Color c = panelComa.color;
            c.a = alpha;
            panelComa.color = c;
        }

        //REINICIO
        if (salud <= 0 && !reiniciando)
        {
            reiniciando = true; // Bloqueamos para que solo pase una vez
            StartCoroutine(ReiniciarSimulacion());
        }
    }

    IEnumerator ReiniciarSimulacion()
    {
        Debug.Log("¡COMA! Reiniciando en 3 segundos...");
        yield return new WaitForSeconds(3f);
        string escenaActual = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(escenaActual);
    }

    void VibrarHardware(XRNode mano, float amplitud, float duracion)
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(mano);
        if (device.isValid)
        {
            device.SendHapticImpulse(0, amplitud, duracion);
        }
    }
}