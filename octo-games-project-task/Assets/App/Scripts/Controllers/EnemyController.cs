using App.GameEvents;
using DynamicBox.EventManagement;
using Opsive.UltimateCharacterController.Character;
using System.Threading.Tasks;
using UnityEngine;
using Opsive.UltimateCharacterController.Character.Abilities.AI;

namespace App.Controllers
{
  public class EnemyController : MonoBehaviour
  {
    [Header ("Links")]
    [SerializeField] private UltimateCharacterLocomotion locomotion;

    private Transform player;

    #region Unity Methods

    #endregion

    public void SetTarget (Transform target)
    {
      player = target;
    }

    public void OnPlayerDeath ()
    {
      EventManager.Instance.Raise (new CheckForAliveEnemiesEvent ());
    }

    public async void SetDestinationAsync ()
    {
      await Task.Delay (500);
      locomotion.GetAbility<NavMeshAgentMovement> ().SetDestination (player.position);
      SetDestinationAsync ();
    }
  }
}
