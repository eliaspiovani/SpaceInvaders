using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{

    public float speed = 8.0f, maxSpeed = 8.0f;
    //PlayerController pC => GameObject.Find("Spaceship").GetComponent<PlayerController>();
    GameController gC => GameObject.Find("GameController").GetComponent<GameController>();

    BoxCollider bxCol => GetComponent<BoxCollider>();

    public int score = 20;

    GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        explosion = gC.asteroidExplosion;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos = transform.position;

        pos += Vector3.right * Time.deltaTime * -speed;

        if (pos.x < -gC.horizontal_limits - 2.0f)
        {
            pos.x = 20;
            pos.z = Random.Range(-gC.vertical_limits, gC.vertical_limits);
            speed = maxSpeed * Random.Range(0.2f, 1.0f);
            transform.localScale = Vector3.one * Random.Range(1.0f, 2.0f);
            transform.rotation = Quaternion.Euler(Random.value * 360.0f, Random.value * 360.0f, Random.value * 360.0f);
        }

        transform.position = pos;
        if(Time.timeScale > 0) transform.Rotate(Vector3.forward, 2.0f);
    }

    public void DestroyAsteroid()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        if (Random.value > 0.96f)
        {
            Instantiate(gC.itens[Random.Range(0, gC.itens.Length)], transform.position, Quaternion.identity);
        }
        //Destroy(gameObject);
        transform.position = new Vector3(-gC.horizontal_limits-2.0f, 0.0f, 0.0f);
    }

    private void OnDestroy()
    {

    }

}
