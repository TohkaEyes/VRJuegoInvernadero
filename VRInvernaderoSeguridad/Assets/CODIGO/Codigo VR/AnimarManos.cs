using UnityEngine;
using UnityEngine.InputSystem;

public class AnimarMano : MonoBehaviour
{
    public InputActionProperty triggerValue;
    public InputActionProperty gripValue;
    // Para leer los valores de los controles del Oculus
    public Animator handAnimator;
    // Simular el movimiento de manos
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float trigger = triggerValue.action.ReadValue<float>();
        float grip = gripValue.action.ReadValue<float>();

        handAnimator.SetFloat("Trigger", trigger);
        handAnimator.SetFloat("Grip", grip);

    }
}
