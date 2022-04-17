using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTorpedoController : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float maxX = 1.3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveTorpedo();
        CheckBounds();
    }

    public void Explode()
    {
        Destroy(gameObject);
    }

    private void CheckBounds()
    {
        if (transform.position.x > maxX)
        {
            Destroy(gameObject);
        }
    }

    private void MoveTorpedo() {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
