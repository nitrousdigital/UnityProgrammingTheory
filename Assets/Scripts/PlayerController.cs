using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject torpedoPrefab;
    [SerializeField] private float missileOffsetX = 0.5f;

    float speed = 1f;

    float minY = 0.3f;
    float maxY = 1.5f;

    float minX = -1.3f;
    float maxX = 1.3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveVertical();
        MoveHorizontal();
        ClampVerticalPosition();
        ClampHorizontalPosition();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireWeapon();
        }
    }

    public void Explode()
    {
        Destroy(gameObject);
    }

    private void FireWeapon()
    {
        GameObject missile = Instantiate(torpedoPrefab);
        missile.transform.position = new Vector3(transform.position.x + missileOffsetX, transform.position.y, missile.transform.position.z);
    }

    void MoveVertical()
    {
        float vertical = Input.GetAxis("Vertical");
        transform.Translate(Vector3.up * vertical * Time.deltaTime * speed);
    }

    void MoveHorizontal()
    {
        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.forward * -horizontal * Time.deltaTime * speed);
    }

    void ClampHorizontalPosition()
    {
        if (transform.position.x < minX)
        {
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > maxX)
        {
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
        }
    }

    void ClampVerticalPosition()
    {
        if (transform.position.y < minY)
        {
            transform.position = new Vector3(transform.position.x, minY, transform.position.z);
        }
        else if (transform.position.y > maxY)
        {
            transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
        }
    }
}
