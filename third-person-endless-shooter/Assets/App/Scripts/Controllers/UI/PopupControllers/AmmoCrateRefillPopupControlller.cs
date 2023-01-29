using System.Threading.Tasks;
using UnityEngine;

namespace App.Controllers
{

  public class AmmoCrateRefillPopupControlller : MonoBehaviour
  {
    [Header ("Parameters")]
    [SerializeField] private int delaySecondsBeforeAnimation;

    [Header ("Links")]
    [SerializeField] private Animation textAnimation;

    async void Start ()
    {
      await Task.Delay (delaySecondsBeforeAnimation * 100);
      textAnimation.Play ();
    }

    public void AnimationEnded ()
    {
      Destroy (gameObject);
    }
  }
}