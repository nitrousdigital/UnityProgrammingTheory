using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Animate the enemy torpedo on the title screen
/// </summary>
public class TitleEnemyTorpedoController : TorpedoController
{
    private TitleSceneController titleScene;
    private void Awake()
    {
        titleScene = FindObjectOfType<TitleSceneController>();
    }

    public override void SetActive(bool active)
    {
        if (!active)
        {
            Destroy(gameObject);
            titleScene.LaunchTorpedo();
        }
    }
}
