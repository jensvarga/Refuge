using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempKillPlayer : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
      if (other.gameObject.tag == "Player") {
        Debug.Log("Dead :(");
        SceneManager.LoadScene(0);
      }
    }
}
