using App.Enums;
using App.GameEvents.UI;
using DynamicBox.EventManagement;
using Opsive.Shared.Inventory;
using Opsive.UltimateCharacterController.Character;
using Opsive.UltimateCharacterController.Inventory;
using System.Collections.Generic;
using UnityEngine;

namespace App.Controllers
{
  public class AmmoCrateController : MonoBehaviour
  {
    [Header ("Links")]
    [SerializeField] private ItemDefinitionBase bulletItem;

    [Space]
    [SerializeField] private Animation crateLidAnimation;
    [SerializeField] private AnimationClip openLidClip;
    [SerializeField] private AnimationClip closeLidClip;

    private const int maxRefillValue = 100;

    #region Unity Methods

    void OnTriggerEnter (Collider other)
    {
      if (other.gameObject.layer != LayerMask.NameToLayer ("Character"))
        return;

      ToggleLidAnimation (true);
      AdjustAmmo (other.gameObject);
      EventManager.Instance.Raise (new ShowPopupEvent (PopupType.AmmoCrateRefill));
    }

    void OnTriggerExit (Collider other)
    {
      if (other.gameObject.layer != LayerMask.NameToLayer ("Character"))
        return;

      ToggleLidAnimation (false);
    }

    #endregion

    private void ToggleLidAnimation (bool toOpen)
    {
      crateLidAnimation.clip = toOpen ? openLidClip : closeLidClip;
      crateLidAnimation.PlayQueued (crateLidAnimation.clip.name);
    }

    private void AdjustAmmo (GameObject player)
    {
      GameObject characterObject = player.gameObject.GetComponent<CapsuleColliderPositioner> ().FirstEndCapTarget.gameObject;
      Inventory inventory = characterObject.GetComponent<Inventory> ();

      List<IItemIdentifier> items = inventory.GetAllItemIdentifiers ();
      IItemIdentifier itemIdentifier = items.Find (i => i.GetItemDefinition () == bulletItem);

      int amount = inventory.GetItemIdentifierAmount (itemIdentifier);
      inventory.AdjustItemIdentifierAmount (itemIdentifier, maxRefillValue - amount);
    }
  }
}