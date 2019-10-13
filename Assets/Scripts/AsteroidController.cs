using UnityEngine;

public class AsteroidController : MonoBehaviour
{

    public float speed = 8.0f, maxSpeed = 8.0f;

    GameController gC => GameObject.Find("GameController").GetComponent<GameController>();

    BoxCollider bxCol => GetComponent<BoxCollider>();

    public int score = 20;

    GameObject explosion;

    void Start()
    {
        // Get the explosion set for asteroids from Game Controller
        explosion = gC.asteroidExplosion;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // make a local copy of the tranform
        Vector3 pos = transform.position;

        // calculates the new position
        pos += Vector3.right * Time.deltaTime * -speed;

        // position is out of the bounds of the screen, restart it in a random position, speed, scale, rotation
        if (pos.x < -gC.horizontal_limits - 2.0f)
        {
            pos.x = 20;
            pos.z = Random.Range(-gC.vertical_limits, gC.vertical_limits);
            speed = maxSpeed * Random.Range(0.2f, 1.0f);
            transform.localScale = Vector3.one * Random.Range(1.0f, 2.0f);
            transform.rotation = Quaternion.Euler(Random.value * 360.0f, Random.value * 360.0f, Random.value * 360.0f);
        }

        // update the position of the object
        transform.position = pos;

        // if the game is paused, stop rotating
        if(Time.timeScale > 0) transform.Rotate(Vector3.forward, 2.0f);
    }

    public void DestroyAsteroid()
    {
        // When the object is destroyed by something instantiate and explosion
        Instantiate(explosion, transform.position, Quaternion.identity);

        // randomly instantiate one of an item in a range
        if (Random.value > 0.96f)
        {
            Instantiate(gC.itens[Random.Range(0, gC.itens.Length)], transform.position, Quaternion.identity);
        }

        // instead of destroying the object, move it out of the bounds of the screen, so it will be repositioned in the scene
        transform.position = new Vector3(-gC.horizontal_limits-2.0f, 0.0f, 0.0f);
    }

}
