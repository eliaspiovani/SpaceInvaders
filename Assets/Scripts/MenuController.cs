using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject planet, spaceships;
    public float planetRotSpd = 2.0f, ssRotSpd = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("return"))
        {
            SceneManager.LoadScene(1);
        }

        planet.transform.Rotate(Vector3.up, planetRotSpd);
        spaceships.transform.Rotate(Vector3.up, ssRotSpd);
        spaceships.transform.Rotate(Vector3.right, ssRotSpd*0.2f);
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
