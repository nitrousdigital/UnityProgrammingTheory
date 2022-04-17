using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTorpedoController : MonoBehaviour, Explodable
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float maxX = 1.3f;

    /// <summary>
    ///  The explosion prefab to be instantiated when the enemy is destroyed.
    /// </summary>
    [SerializeField] private GameObject explosionPrefab;

    // Update is called once per frame
    void Update()
    {
        MoveTorpedo();
        CheckBounds();
    }

    public GameObject GetExplosionPrefab()
    {
        return explosionPrefab;
    }

    public void Explode()
    {
        // hide the torpedo, making it available for re-use.
        gameObject.SetActive(false);
    }

    private void CheckBounds()
    {
        if (transform.position.x > maxX)
        {
            // hide the torpedo, making it available for re-use.
            gameObject.SetActive(false);
        }
    }

    private void MoveTorpedo() {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
