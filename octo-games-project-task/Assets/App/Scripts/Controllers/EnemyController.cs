using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
  [SerializeField] private NavMeshAgent agent;

  private Transform player;

  void OnEnable ()
  {
    SetDestinationAsync ();
  }

  public void SetTarget (Transform target)
  {
    player = target;
  }

  private async void SetDestinationAsync ()
  {
    await Task.Delay (500);
    agent.SetDestination (player.position);
    SetDestinationAsync ();
  }
}
