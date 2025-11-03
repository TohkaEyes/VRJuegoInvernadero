using UnityEngine;

public class GameController : MonoBehaviour
{
    //TODO HACER CODIGO
    public SliderBar statusBar;
    public float statusCurrent; 


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        statusCurrent = 0f;
        statusBar.SetMaxValue(100f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            statusCurrent += UnityEngine.Random.Range(0f, 10f);
            statusBar.SetValue(statusCurrent);
        }
    }
}
