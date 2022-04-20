// INHERITANCE

/// <summary>
///  Manages the destruction and re-creation of player torpedoes on the title screen
/// </summary>
public class TitlePlayerTorpedoController : TitleSceneTorpedoController
{
    // POLYMORPHISM
    /// <summary>
    ///  Instruct the title scene controller to launch another player torpedo
    ///  when the existing torpedo is destroyed.
    /// </summary>
    protected override void OnTorpedoDestroyed()
    {
        titleScene.LaunchPlayerTorpedo();
    }
}
