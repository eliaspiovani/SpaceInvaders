using UnityEngine;

public class LaserBlastEnemyController : MonoBehaviour
{
    public float speed = 16.0f;

    GameController gC => GameObject.Find("GameController").GetComponent<GameController>();

    // Update is called once per frame
    void LateUpdate()
    {
        // make a local copy of the tranform
        Vector3 pos = transform.localPosition;

        // calculates the new position
        pos += transform.TransformDirection(Vector3.right) * Time.deltaTime * speed;

        // if laser is out of the screen, destroy it
        if (pos.x > gC.horizontal_limits+2.0f || pos.x < -gC.horizontal_limits - 2.0f || pos.z < -gC.vertical_limits - 2.0f || pos.z > gC.vertical_limits + 2.0) {
            DestroyLaserBean();
        }

        // update position
        transform.position = pos;
    }
    
    private void DestroyLaserBean()
    {
        //sC.numberOfShoots--;
        Destroy(gameObject);
    }
}
