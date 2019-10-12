using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGEnvController : MonoBehaviour
{
    GameController gC => GameObject.Find("GameController").GetComponent<GameController>();

    public float speed = 2.0f, maxSpeed = 4.0f;
    public Vector2 scaleMinMax;
    public float rotSpeed = 2.0f;

    Renderer rend => GetComponentInChildren<Renderer>();
    public GameObject rotateObj;
    public Vector3 axis;
    bool changeMaterialColor = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos = transform.position;

        pos += Vector3.right * Time.deltaTime * -speed;

        if (pos.x < -gC.horizontal_limits - 6.0f)
        {
            pos.x = 20;
            pos.z = Random.Range(-gC.vertical_limits, gC.vertical_limits);
            speed = maxSpeed * Random.Range(0.2f, 1.0f);
            transform.localScale = Vector3.one * Random.Range(scaleMinMax.x, scaleMinMax.y);
            transform.rotation = Quaternion.Euler(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f));
            if (changeMaterialColor) rend.material.color = new Color(Random.value, Random.value, Random.value);
        }

        transform.position = pos;

        //transform.Rotate(Vector3.up, rotSpeed);
        rotateObj.transform.Rotate(axis, rotSpeed);
    }
}
