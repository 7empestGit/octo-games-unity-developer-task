using UnityEngine;
using App.Enums;
using System;

namespace App.Models.UI
{
  [Serializable]
  public class PopupDatum
  {
    public PopupType PopupType;
    public GameObject PopupObject;
  }
}