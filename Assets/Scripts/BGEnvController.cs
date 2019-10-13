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

    void LateUpdate()
    {
        // make a local copy of the tranform
        Vector3 pos = transform.position;

        // calculates the new position
        pos += Vector3.right * Time.deltaTime * -speed;

        // position is out of the bounds of the screen, restart it in a random position, speed, scale, rotation and color
        if (pos.x < -gC.horizontal_limits - 6.0f)
        {
            pos.x = 20;
            pos.z = Random.Range(-gC.vertical_limits, gC.vertical_limits);
            speed = maxSpeed * Random.Range(0.2f, 1.0f);
            transform.localScale = Vector3.one * Random.Range(scaleMinMax.x, scaleMinMax.y);
            transform.rotation = Quaternion.Euler(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f));
            if (changeMaterialColor) rend.material.color = new Color(Random.value, Random.value, Random.value);
        }

        // update the position of the object
        transform.position = pos;


        rotateObj.transform.Rotate(axis, rotSpeed);
    }
}
