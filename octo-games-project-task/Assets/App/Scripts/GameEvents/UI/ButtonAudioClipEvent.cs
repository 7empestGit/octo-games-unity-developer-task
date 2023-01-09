using DynamicBox.EventManagement;

namespace App.GameEvents.UI
{
  public class ButtonAudioClipEvent : GameEvent
  {
    public readonly bool OnHover;

    public ButtonAudioClipEvent (bool onHover)
    {
      OnHover = onHover;
    }
  }
}