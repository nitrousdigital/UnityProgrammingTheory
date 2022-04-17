using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTorpedoController : AbstractExplodable
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float maxX = 1.3f;

    // Update is called once per frame
    void Update()
    {
        MoveTorpedo();
        CheckBounds();
    }

    /// <summary>
    ///  Override destruction to recycle torpedos
    /// </summary>
    protected override void OnDestroy()
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
