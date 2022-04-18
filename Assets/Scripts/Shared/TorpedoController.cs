using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Responsible for moving a torpedo and for setting it inactive when it either explodes or travels out of bounds
/// </summary>
public class TorpedoController : ExplodableController
{

    [SerializeField] private float speed = 1f;
    [SerializeField] private float maxX = 1.3f;
    [SerializeField] private float minX = -2.2f;

    /// <summary>
    ///  Set the speed of the torpedo
    /// </summary>
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        MoveTorpedo();
        CheckBounds();
    }

    /// <summary>
    ///  Activate or deactivate this torpedo and update the ammo HUD
    /// </summary>
    public virtual void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    /// <summary>
    ///  Override destruction to recycle torpedos
    /// </summary>
    protected override void OnExploded()
    {
        // hide the torpedo, making it available for re-use.
        SetActive(false);
    }

    /// <summary>
    ///  Deactivate the torpedo if it flies out of bounds
    /// </summary>
    private void CheckBounds()
    {
        if (transform.position.x > maxX ||
            transform.position.x < minX)
        {
            // hide the torpedo, making it available for re-use.
            SetActive(false);
        }
    }

    /// <summary>
    ///  Move the torpedo forward
    /// </summary>
    private void MoveTorpedo()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
