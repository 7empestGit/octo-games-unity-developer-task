using DynamicBox.EventManagement;

namespace App.GameEvents
{
  public class PlayerHealthChanged : GameEvent
  {
    public readonly float CurrentPlayerHealth;

    public PlayerHealthChanged (float currentPlayerHealth)
    {
      CurrentPlayerHealth = currentPlayerHealth;
    }
  }
}
