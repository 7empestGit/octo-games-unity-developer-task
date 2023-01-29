using App.GameEvents.UI;
using App.ScriptableObjects.UI;
using DynamicBox.EventManagement;
using UnityEngine;

namespace App.Managers.UI
{
  public class PopupManager : MonoBehaviour
  {
    [Header ("Links")]
    [SerializeField] private Transform popupsCanvasParent;
    [SerializeField] private PopupDataSO popupsData;

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
      GameObject targetPopupObject = popupsData.PopupsData.Find (obj => obj.PopupType.Equals (eventDetails.PopupType)).PopupObject;

      if (targetPopupObject == null)
        return;

      Instantiate (targetPopupObject, popupsCanvasParent);
    }

    #endregion
  }
}
