using App.GameEvents;
using DynamicBox.EventManagement;
using Opsive.UltimateCharacterController.Character;
using System.Threading.Tasks;
using UnityEngine;
using Opsive.UltimateCharacterController.Character.Abilities.AI;
using Opsive.UltimateCharacterController.Character.Abilities.Items;
using Opsive.Shared.Game;
using Opsive.UltimateCharacterController.Traits;

namespace App.Controllers
{
  public class EnemyController : MonoBehaviour
  {
    [Header ("Links")]
    [SerializeField] private UltimateCharacterLocomotion locomotion;
    [SerializeField] private LocalLookSource localLookSource;

    private Transform player;
    private NavMeshAgentMovement navMeshAgentMovement;

    #region Unity Methods

    #endregion

    public void ActivateEnemy (Transform target)
    {
      GetComponent<Respawner> ().Respawn ();
      SetTarget (target);
      SetDestinationAsync ();
      localLookSource.Target = target;
    }

    private void SetTarget (Transform target)
    {
      player = target;
      navMeshAgentMovement = locomotion.GetAbility<NavMeshAgentMovement> ();
      navMeshAgentMovement.ArrivedDistance = 2f;
    }

    public void OnPlayerDeath ()
    {
      //gameObject.SetActive (false);
      EventManager.Instance.Raise (new CheckForAliveEnemiesEvent ());
    }

    private async void SetDestinationAsync ()
    {
      await Task.Delay (500);
      navMeshAgentMovement.SetDestination (player.position);
      Attack ();
      SetDestinationAsync ();
    }

    private void Attack ()
    {
      if (!navMeshAgentMovement.HasArrived)
        return;

      Use useAbility = locomotion.GetAbility<Use> ();
      if (useAbility != null)
      {
        // Try to start the use ability. If the ability is started it will use the currently equipped item.
        locomotion.TryStartAbility (useAbility);
      }
    }
  }
}
