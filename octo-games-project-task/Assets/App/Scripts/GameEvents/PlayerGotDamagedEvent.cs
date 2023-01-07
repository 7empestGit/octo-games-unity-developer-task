using DynamicBox.EventManagement;

namespace App.GameEvents
{
  public class PlayerGotDamagedEvent : GameEvent
  {
    public readonly float CurrentPlayerHealth;

    public PlayerGotDamagedEvent (float currentPlayerHealth)
    {
      CurrentPlayerHealth = currentPlayerHealth;
    }
  }
}
