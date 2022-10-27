using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameInputField : MonoBehaviour
{
  string text;
  public TextMeshProUGUI name;

  void Start() {
    text = name.text;
  }

    void Update () {
      string correctedName = "";
      foreach (char chr in text) {
        if ( (chr < 'a' || chr > 'z') && (chr < 'A' || chr > 'Z') && (chr < '0' || chr > '9') ) {

        } else {
          correctedName.Insert(correctedName.Length, chr.ToString());
        }
      }

      name.text = correctedName;
    }
}
