using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float CooldownDuration = 10;
    public GameController gameController;

    public float CooldownCollider = 2f;
    public bool ColliderIsAvailable = true;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(StartCooldown());
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnTriggerStay(Collider other)
    {
        Debug.Log("Colisionando trigger con -> " + other.transform.tag);
        if (other.transform.tag == "Player")
        {
            if (ColliderIsAvailable)
            {
                StartCoroutine (StartCooldownCollider());
            }
        }
    }

    public IEnumerator StartCooldown()
    {

        yield return new WaitForSeconds(CooldownDuration); //esperar N segundos

        Destroy(this.gameObject);
    }

    public IEnumerator StartCooldownCollider()
    {
        Debug.Log("Coroutine Collider ");
        ColliderIsAvailable = false;
        gameController.RollEnfermedad();
        yield return new WaitForSeconds(CooldownCollider); //esperar N segundos
        ColliderIsAvailable = true;
    }
}
