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

    private const int maxRefillValue = 100;

    public void OnTriggerEnter (Collider other)
    {
      if (other.gameObject.layer != LayerMask.NameToLayer ("Character"))
        return;

      AdjustAmmo (other.gameObject);
      EventManager.Instance.Raise (new ShowPopupEvent (PopupType.AmmoCrateRefill));
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