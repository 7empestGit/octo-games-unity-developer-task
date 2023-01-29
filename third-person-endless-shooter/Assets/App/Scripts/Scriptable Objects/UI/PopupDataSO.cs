using App.Models.UI;
using System.Collections.Generic;
using UnityEngine;

namespace App.ScriptableObjects.UI
{
  [CreateAssetMenu (fileName = "PopupsData", menuName = "Scriptable Objects/PopupDataSO")]
  public class PopupDataSO : ScriptableObject
  {
    [SerializeField] private List<PopupDatum> popupsData;
    public List<PopupDatum> PopupsData => popupsData;
  }
}