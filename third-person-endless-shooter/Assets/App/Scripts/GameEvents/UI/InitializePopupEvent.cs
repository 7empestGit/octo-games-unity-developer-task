using App.Enums;
using DynamicBox.EventManagement;
using System.Collections.Generic;

namespace App.GameEvents.UI
{
  public class InitializePopupEvent : GameEvent
  {
    public readonly PopupType PopupType;
    public readonly List<object> PopupParameters = new List<object> ();

    public InitializePopupEvent (PopupType popupType, List<object> popupParameters)
    {
      PopupType = popupType;
      PopupParameters = popupParameters;
    }
  }
}