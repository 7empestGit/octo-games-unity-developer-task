using App.GameEvents.UI;
using DynamicBox.EventManagement;
using UnityEngine;

namespace App.Managers.UI
{
  public class UIAudioManager : MonoBehaviour
  {
    [Header ("Links")]
    [SerializeField] private AudioSource onHoverAudioSource;
    [SerializeField] private AudioSource onClickAudioSource;

    #region Unity Methods

    void OnEnable ()
    {
      EventManager.Instance.AddListener<ButtonAudioClipEvent> (ButtonAudioClipEventHandler);
    }

    void OnDisable ()
    {
      EventManager.Instance.RemoveListener<ButtonAudioClipEvent> (ButtonAudioClipEventHandler);
    }

    #endregion

    #region Event Handlers

    private void ButtonAudioClipEventHandler (ButtonAudioClipEvent eventDetails)
    {
      if (eventDetails.OnHover)
        onHoverAudioSource.Play ();
      else
        onClickAudioSource.Play ();
    }

    #endregion
  }
}