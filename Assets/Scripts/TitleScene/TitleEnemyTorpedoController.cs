// INHERITANCE

/// <summary>
///  Manages the destruction and re-creation of enemy torpedoes on the title screen
/// </summary>
public class TitleEnemyTorpedoController : TitleSceneTorpedoController
{
    // POLYMORPHISM
    // ABSTRACTION
    /// <summary>
    ///  Instruct the title scene controller to launch another enemy torpedo
    ///  when the existing torpedo is destroyed.
    /// </summary>
    protected override void OnTorpedoDestroyed()
    {
        titleScene.LaunchEnemyTorpedo();
    }
}
