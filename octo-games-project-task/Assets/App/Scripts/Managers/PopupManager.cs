using App.GameEvents.UI;
using App.ScriptableObjects.UI;
using DynamicBox.EventManagement;
using UnityEngine;

namespace App.Managers.UI
{
  public class PopupManager : MonoBehaviour
  {
    #region Unity Methods

    void OnEnable ()
    {
      EventManager.Instance.AddListener<ShowPopupEvent> (ShowPopupEventHandler);
    }

    void OnDisable ()
    {
      EventManager.Instance.RemoveListener<ShowPopupEvent> (ShowPopupEventHandler);
    }

    #endregion

    #region Event Handlers

    private void ShowPopupEventHandler (ShowPopupEvent eventDetails)
    {

    }

    #endregion
  }
}
