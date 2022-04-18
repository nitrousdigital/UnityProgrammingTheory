using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTorpedoController : AbstractExplodable
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float maxX = 1.3f;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
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
    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
        gameManager.UpdateAmmoHUD();
    }

    /// <summary>
    ///  Override destruction to recycle torpedos
    /// </summary>
    protected override void OnDestroy()
    {
        // hide the torpedo, making it available for re-use.
        SetActive(false);
    }

    /// <summary>
    ///  Deactivate the torpedo if it flies out of bounds
    /// </summary>
    private void CheckBounds()
    {
        if (transform.position.x > maxX)
        {
            // hide the torpedo, making it available for re-use.
            SetActive(false);
        }
    }

    /// <summary>
    ///  Move the torpedo forward
    /// </summary>
    private void MoveTorpedo() {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
