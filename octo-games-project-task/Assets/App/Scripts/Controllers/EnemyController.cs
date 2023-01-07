using App.GameEvents;
using DynamicBox.EventManagement;
using Opsive.UltimateCharacterController.Character;
using System.Threading.Tasks;
using UnityEngine;
using Opsive.UltimateCharacterController.Character.Abilities.AI;
using Opsive.UltimateCharacterController.Character.Abilities.Items;
using Opsive.UltimateCharacterController.Traits;
using System.Linq;
using System.Collections;

namespace App.Controllers
{
  public class EnemyController : MonoBehaviour
  {
    [Header ("Links")]
    [SerializeField] private UltimateCharacterLocomotion locomotion;

    private Transform player;
    private NavMeshAgentMovement navMeshAgentMovement;

    private readonly float setDestinationDelay = 0.2f;

    public void ActivateEnemy (Transform target)
    {
      player = target;
      SetTarget ();
      StartCoroutine (SetDestinationAsync ());
    }

    public void DeactivateEnemy ()
    {
      StopCoroutine (SetDestinationAsync ());
    }

    public void DisableEnemy ()
    {
      gameObject.SetActive (false);
    }

    private void SetTarget ()
    {
      navMeshAgentMovement = locomotion.GetAbility<NavMeshAgentMovement> ();
      navMeshAgentMovement.ArrivedDistance = 1f;
    }

    public void OnPlayerDeath ()
    {
      EventManager.Instance.Raise (new EnemyIsDeadEvent ());
    }

    private IEnumerator SetDestinationAsync ()
    {
      yield return new WaitForSeconds (setDestinationDelay);
      navMeshAgentMovement.SetDestination (player.position);

      if (gameObject.activeInHierarchy)
        StartCoroutine(SetDestinationAsync ());

      if (navMeshAgentMovement.HasArrived)
        Attack ();
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
