using App.Enums;
using DynamicBox.EventManagement;

namespace App.GameEvents.UI
{
  public class ShowPopupEvent : GameEvent
  {
    public readonly PopupType PopupType;

    public ShowPopupEvent (PopupType popupType)
    {
      PopupType = popupType;
    }
  }
}
