using DynamicBox.EventManagement;
using App.GameEvents;
using UnityEngine;

namespace App.Controllers.UI
{
  public class MainMenuPanelUIController : MonoBehaviour
  {
    [Header ("Links")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject howToPlayPanel;

    #region Button Methods

    public void StartGameClicked ()
    {
      gameObject.SetActive (false);
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
