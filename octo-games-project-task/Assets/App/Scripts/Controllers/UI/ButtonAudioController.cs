using App.GameEvents.UI;
using DynamicBox.EventManagement;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAudioController : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
  public void OnPointerClick (PointerEventData eventData)
  {
    EventManager.Instance.Raise (new ButtonAudioClipEvent (false));
  }

  public void OnPointerEnter (PointerEventData eventData)
  {
    EventManager.Instance.Raise (new ButtonAudioClipEvent (true));
  }
}
