using DynamicBox.EventManagement;
using App.GameEvents;
using UnityEngine;
using App.GameEvents.UI;

namespace App.Controllers.UI
{
  public class MainMenuPanelUIController : MonoBehaviour
  {
    [Header ("Links")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject howToPlayPanel;

    #region Unity Methods

    void OnEnable ()
    {
      EventManager.Instance.AddListener<OpenMainMenuGameEvent> (OpenMainMenuGameEventHandler);
    }

    void OnDisable ()
    {
      EventManager.Instance.RemoveListener<OpenMainMenuGameEvent> (OpenMainMenuGameEventHandler);
    }

    #endregion

    #region Event Handlers

    private void OpenMainMenuGameEventHandler (OpenMainMenuGameEvent eventDetails)
    {
      HideAllPanels ();
      mainMenuPanel.SetActive (true);
    }

    #endregion

    #region Button Methods

    public void StartGameClicked ()
    {
      HideAllPanels ();
      EventManager.Instance.Raise (new StartGameEvent ());
    }

    public void ReturnToMainMenu ()
    {
      HideAllPanels ();
      mainMenuPanel.SetActive (true);
    }

    public void OpenHowToPlayPanel ()
    {
      HideAllPanels ();
      howToPlayPanel.SetActive (true);
    }

    public void QuitGame ()
    {
      Application.Quit ();
    }

    #endregion

    private void HideAllPanels ()
    {
      mainMenuPanel.SetActive (false);
      howToPlayPanel.SetActive (false);
    }
  }
}
