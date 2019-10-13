using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] asteroids, itens;
    public GameObject asteroidExplosion, enemyExplosion;
    public PlayerController pC;

    AudioManager aM => GameObject.Find("AudioManager").GetComponent<AudioManager>();

    public GameObject menu;

    public float horizontal_limits = 14.0f, vertical_limits = 10.0f;

    float spawnSpeed;

    public int wave = 1, numberOfEnemies = 5;

    private IEnumerator instanciateEnemies;

    public Text scoreValue, healthValue, laserShootsValue, restartText, waveText;
    public bool createNewWave = true;

    // Start is called before the first frame update
    void Start()
    {
        spawnSpeed = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // if escape is pressed, open the menu or close it
        if (Input.GetKeyDown("escape"))
        {
            if (Time.timeScale > 0)
            {
                OpenMenu();
            }
            else
            {
                Resume();
            }
        }

        // updates the values in the UI
        scoreValue.text = pC.score.ToString();
        waveText.text = wave.ToString();

        // if the player is alive
        if (pC.health > 0)
        {
            // if the laser type is not the single one, update the value with the number of shoots left
            if (pC.laserType != PlayerController.ITEMS._1LASER) laserShootsValue.text = pC.maxShoots.ToString();
            // if it is, print the infinity symbol
            else laserShootsValue.text = "∞";

            // if the last wave has ended, create another one, with enemies spawning a little bit faster than the last one
            if (createNewWave)
            {
                instanciateEnemies = CreateWaves(spawnSpeed * (20.0f/20.0f+wave));
                StartCoroutine(instanciateEnemies);
            }
        }
        // if the player is dead, stop wave, wait until return is pressed
        else
        {
            StopCoroutine(instanciateEnemies);
            wave = 1;
            restartText.gameObject.SetActive(true);

            if (Input.GetKeyDown("return"))
            {
                createNewWave = true;
                restartText.gameObject.SetActive(false);
                pC.ResetPlayer();

                ResetAsteroids();

            }
        }

        // update health on screen
        healthValue.text = pC.health.ToString("0%");
    }

    // put all asteroid in scene out of screen
    void ResetAsteroids()
    {
        foreach (var asteroid in GameObject.FindGameObjectsWithTag("Asteroid"))
        {
            asteroid.transform.position = new Vector3(-15.0f, 0.0f, 0.0f);
        }
    }

    IEnumerator CreateWaves(float wait)
    {
        createNewWave = false;
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Instantiate(enemies[Random.Range(0, wave%5)],
                new Vector3(horizontal_limits, 0.0f,
                Random.Range(-vertical_limits, vertical_limits)),
                Quaternion.Euler(0.0f,
                Random.Range(160.0f, 200.0f), 0.0f));

            yield return new WaitForSeconds(wait);
        }
        wave++;
        createNewWave = true;
    }

    public void OpenMenu()
    {
        menu.SetActive(true);
        Time.timeScale = 0;

        aM.Pause();
    }

    public void Resume()
    {
        menu.SetActive(false);
        Time.timeScale = 1.0f;

        aM.Play();
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1.0f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);

        aM.Play();
    }
}
