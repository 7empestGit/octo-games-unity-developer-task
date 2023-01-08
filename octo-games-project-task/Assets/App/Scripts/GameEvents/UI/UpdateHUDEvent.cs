using DynamicBox.EventManagement;

namespace App.GameEvents.UI
{
  public class UpdateHUDEvent : GameEvent
  {
    public readonly int CurrentKilledEnemies;

    public UpdateHUDEvent (int currentKilledEnemies)
    {
      CurrentKilledEnemies = currentKilledEnemies;
    }
  }
}