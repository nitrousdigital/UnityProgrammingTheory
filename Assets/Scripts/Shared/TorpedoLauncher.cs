using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoLauncher : ExplodableController
{
    /// <summary>
    ///  The audio clip to be played when launching a torpedo
    /// </summary>
    [SerializeField] private AudioClip torpedoLaunchSound;

    /// <summary>
    ///  The audio level for the sound played when launching a torpedo
    /// </summary>
    [SerializeField] private float torpedoLaunchVolume = 0.5f;

    /// <summary>
    ///  The horizontal offset to be used to position the torpedo relative to the GameObject when launching.
    /// </summary>
    [SerializeField] private float torpedoOffsetX = 0f;

    /// <summary>
    ///  The vertical offset to be used to position the torpedo relative to the GameObject when launching.
    /// </summary>
    [SerializeField] private float torpedoOffsetY = 0f;

    protected AudioSource audioPlayer;
    protected TorpedoManager torpedoManager;

    // Start is called before the first frame update
    protected void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    /// <summary>
    ///  Set the TorpedoManager to be used to launch torpedos.
    /// </summary>
    protected void SetTorpedoManager(TorpedoManager manager)
    {
        torpedoManager = manager;
    }

    /// <summary>
    ///  Launch a torpedo, if one is available.
    ///  Returns the torpedo that is being launched.
    ///  Returns null if no inactive torpedos are found.
    /// </summary>
    public GameObject LaunchTorpedo()
    {
        return LaunchTorpedo(transform.position.x, transform.position.y);
    }

    /// <summary>
    ///  Launch a torpedo, if one is available.
    ///  Returns the torpedo that is being launched.
    ///  Returns null if no inactive torpedos are found.
    /// </summary>
    public GameObject LaunchTorpedo(float speed)
    {
        return LaunchTorpedo(transform.position.x, transform.position.y, speed);
    }

    /// <summary>
    ///  Launch a torpedo, if one is available.
    ///  Returns the torpedo that is being launched.
    ///  Returns null if no inactive torpedos are found.
    /// </summary>
    public GameObject LaunchTorpedo(float x, float y)
    {
        GameObject torpedo = torpedoManager.FireTorpedo(x + torpedoOffsetX, y + torpedoOffsetY);
        OnTorpedoLaunched(torpedo);
        return torpedo;
    }

    /// <summary>
    ///  Launch a torpedo, if one is available and set its speed.
    ///  Returns the torpedo that is being launched.
    ///  Returns null if no inactive torpedos are found.
    /// </summary>
    public GameObject LaunchTorpedo(float x, float y, float speed)
    {
        GameObject torpedo = torpedoManager.FireTorpedo(x + torpedoOffsetX, y + torpedoOffsetY, speed);
        OnTorpedoLaunched(torpedo);
        return torpedo;
    }

    /// <summary>
    ///  If the specified torpedo is non-null, play the launch sound.
    /// </summary>
    private void OnTorpedoLaunched(GameObject torpedo)
    {
        if (torpedo != null)
        {
            audioPlayer.PlayOneShot(torpedoLaunchSound, torpedoLaunchVolume);
        }
    }

}
