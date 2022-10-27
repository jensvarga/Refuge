using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandeler : MonoBehaviour
{
  [SerializeField] private GameObject weaponLogic;
  [SerializeField] private GameObject kickLogic;

  public void EnableWapon()
  {
    weaponLogic.SetActive(true);
  }

  public void DisableWapon()
  {
    weaponLogic.SetActive(false);
  }

  public void EnableKick()
  {
    kickLogic.SetActive(true);
  }

  public void DisableKick()
  {
    kickLogic.SetActive(false);
  }

}
