using App.GameEvents;
using DynamicBox.EventManagement;
using Opsive.UltimateCharacterController.Character;
using System.Threading.Tasks;
using UnityEngine;
using Opsive.UltimateCharacterController.Character.Abilities.AI;
using Opsive.UltimateCharacterController.Character.Abilities.Items;
using Opsive.UltimateCharacterController.Traits;
using System.Linq;

namespace App.Controllers
{
  public class EnemyController : MonoBehaviour
  {
    [Header ("Links")]
    [SerializeField] private UltimateCharacterLocomotion locomotion;

    private Transform player;
    private NavMeshAgentMovement navMeshAgentMovement;

    #region Unity Methods

    #endregion

    public void ActivateEnemy (Transform target)
    {
      player = target;
      SetTarget ();
      SetDestinationAsync ();
    }

    private void SetTarget ()
    {
      navMeshAgentMovement = locomotion.GetAbility<NavMeshAgentMovement> ();
      navMeshAgentMovement.ArrivedDistance = 2f;
    }

    public void OnPlayerDeath ()
    {
      //gameObject.SetActive (false);
      EventManager.Instance.Raise (new EnemyIsKilledEvent ());
    }

    private async void SetDestinationAsync ()
    {
      await Task.Delay (500);
      navMeshAgentMovement.SetDestination (player.position);

      if (navMeshAgentMovement.HasArrived)
        Attack ();

      if (gameObject.activeInHierarchy)
        SetDestinationAsync ();
    }

    private void Attack ()
    {
      Use useAbility = locomotion.GetAbility<Use> ();
      if (useAbility != null)
      {
        // Try to start the use ability. If the ability is started it will use the currently equipped item.
        locomotion.TryStartAbility (useAbility);
      }
    }
  }
}
