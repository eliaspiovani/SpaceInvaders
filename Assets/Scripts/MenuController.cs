using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject planet, spaceships;

    public float planetRotSpd = 2.0f, ssRotSpd = 2.0f;
    
    // Update is called once per frame
    void Update()
    {
        // If Return is pressed the game starts
        if (Input.GetKeyDown("return"))
        {
            SceneManager.LoadScene(1);
        }

        // Rotates the basic elements in the menus (Planet and spaceships)
        planet.transform.Rotate(Vector3.up, planetRotSpd * Time.deltaTime);
        spaceships.transform.Rotate(Vector3.up, ssRotSpd * Time.deltaTime);
        spaceships.transform.Rotate(Vector3.right, ssRotSpd*0.2f * Time.deltaTime);
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

}
