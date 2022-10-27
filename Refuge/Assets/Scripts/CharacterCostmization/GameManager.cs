/* Copyright (C) - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Jens Varga, 2022
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
  private Color[] allSkinColors = { new Color(0.99f, 0.86f, 0.75f, 1.0f),
                                    new Color(0.99f, 0.82f, 0.65f, 1.0f),
                                    new Color(0.91f, 0.69f, 0.47f, 1.0f),
                                    new Color(0.69f, 0.49f, 0.27f, 1.0f),
                                    new Color(0.45f, 0.31f, 0.17f, 1.0f),
                                    new Color(0.20f, 0.13f, 0.06f, 1.0f) };

  private Color[] allHairColors = { new Color(0.99f, 0.86f, 0.75f, 1.0f),
                                    new Color(0.99f, 0.86f, 0.35f, 1.0f),
                                    new Color(0.70f, 0.57f, 0.37f, 1.0f),
                                    new Color(0.33f, 0.23f, 0.14f, 1.0f),
                                    new Color(0.86f, 0.29f, 0.39f, 1.0f),
                                    new Color(0.16f, 0.09f, 0.03f, 1.0f),
                                    new Color(0.45f, 0.68f, 0.26f, 1.0f),
                                    new Color(0.26f, 0.64f, 0.87f, 1.0f),
                                    new Color(0.71f, 0.36f, 0.79f, 1.0f),
                                    new Color(0.72f, 0.72f, 0.72f, 1.0f) };

  public GameObject[] maleHairstyles;
  public GameObject[] femaleHairstyles;
  public GameObject[] beardstyles;
  private GameObject[] hairstyles;

  public GameObject maleCharacter;
  public GameObject femaleCharacter;
  private GameObject character;

  public Transform maleCharacterScaleObject;
  public Transform femaleCharacterScaleObject;
  private Transform characterScaleObject;

  private Renderer renderer;
  private bool male;

  public Slider heightSlider;
  public Slider widthSlider;
  public GameObject beardParameter;
  public GameObject boobParameter;

  private static int defaultSkinColorIndex = 2;
  private static int defaultHairStyleIndex = 0;
  private static int defaultHairColorIndex = 2;
  private static int defaultBeardIndex = -1;

  private int skinColorIndex = defaultSkinColorIndex;
  private int hairStyleIndex = defaultHairStyleIndex;
  private int hairColorIndex = defaultHairColorIndex;
  private int beardIndex = defaultBeardIndex;

  private void Start() {
    // Inactivate female hair hairstyles
    foreach (GameObject hairstyle in femaleHairstyles) {
      hairstyle.SetActive(false);
    }
    // Default male
    femaleCharacterScaleObject.gameObject.SetActive(false);
    male = true;
    characterScaleObject = maleCharacterScaleObject;
    character = maleCharacter;
    if (male) {
      boobParameter.SetActive(false);
      beardParameter.SetActive(true);
    }
    if (maleHairstyles != null) hairstyles = maleHairstyles;

    if (hairstyles != null) {
      foreach (GameObject hairstyle in hairstyles) {
        var hairRenderer = hairstyle.GetComponent<Renderer>();
        hairRenderer.material.color = allHairColors[hairColorIndex];
        hairstyle.SetActive(false);
      }
    }

    if (beardstyles != null && male) {
      foreach (GameObject beardstyle in beardstyles) {
        var beardRenderer = beardstyle.GetComponent<Renderer>();
        beardRenderer.material.color = allHairColors[hairColorIndex];
        beardstyle.SetActive(false);
      }
    }

    hairstyles[hairStyleIndex].SetActive(true);
    renderer = character.GetComponent<Renderer>();
    renderer.material.color = allSkinColors[skinColorIndex];
    ChangeHeight();
    ChangeWidth();
  }

  public void NextSkinColor() {
      if (skinColorIndex < (allSkinColors.Length - 1)) {
        skinColorIndex++;
      } else {
        skinColorIndex = 0;
      }

      renderer.material.color = allSkinColors[skinColorIndex];
  }

  public void PreviousSkinColor() {
    if (skinColorIndex > 0) {
      skinColorIndex--;
    } else {
      skinColorIndex = allSkinColors.Length - 1;
    }

    renderer.material.color = allSkinColors[skinColorIndex];
  }

  public void NextHairStyle() {
    if (hairStyleIndex > -1 && hairStyleIndex < hairstyles.Length) {
      hairstyles[hairStyleIndex].SetActive(false);
    }
    if (hairStyleIndex < (hairstyles.Length - 1)) {
      hairStyleIndex++;
      hairstyles[hairStyleIndex].SetActive(true);
    } else if (hairStyleIndex == (hairstyles.Length - 1)) {
      // Bald
      hairStyleIndex++;
    } else {
      hairStyleIndex = 0;
      hairstyles[hairStyleIndex].SetActive(true);
    }
  }

  public void PreviousHairStyle() {
    if (hairStyleIndex > -1 && hairStyleIndex < hairstyles.Length) {
      hairstyles[hairStyleIndex].SetActive(false);
    }
    if (hairStyleIndex > 0) {
      hairStyleIndex--;
      hairstyles[hairStyleIndex].SetActive(true);
    } else if (hairStyleIndex == 0) {
      // Bald
      hairStyleIndex--;
    } else {
      hairStyleIndex = hairstyles.Length - 1;
      hairstyles[hairStyleIndex].SetActive(true);
    }
  }

  public void NextHairColor() {
    if (hairColorIndex < (allHairColors.Length - 1)) {
      hairColorIndex++;
    } else {
      hairColorIndex = 0;
    }

    if (hairstyles != null) {
      foreach (GameObject hairstyle in hairstyles) {
        var hairRenderer = hairstyle.GetComponent<Renderer>();
        hairRenderer.material.color = allHairColors[hairColorIndex];
      }
    }
    if (beardstyles != null && male) {
      foreach (GameObject beardstyle in beardstyles) {
        var beardRenderer = beardstyle.GetComponent<Renderer>();
        beardRenderer.material.color = allHairColors[hairColorIndex];
      }
    }
  }

  public void PreviousHairColor() {
    if (hairColorIndex > 0) {
      hairColorIndex--;
    } else {
      hairColorIndex = allHairColors.Length - 1;
    }

    if (hairstyles != null) {
      foreach (GameObject hairstyle in hairstyles) {
        var hairRenderer = hairstyle.GetComponent<Renderer>();
        hairRenderer.material.color = allHairColors[hairColorIndex];
      }
    }
    if (beardstyles != null && male) {
      foreach (GameObject beardstyle in beardstyles) {
        var beardRenderer = beardstyle.GetComponent<Renderer>();
        beardRenderer.material.color = allHairColors[hairColorIndex];
      }
    }
  }

  public void NextBeard() {
    if (beardIndex > -1 && beardIndex < beardstyles.Length) {
      beardstyles[beardIndex].SetActive(false);
    }
    if (beardIndex < (beardstyles.Length - 1)) {
      beardIndex++;
      beardstyles[beardIndex].SetActive(true);
    } else if (beardIndex == (beardstyles.Length - 1)) {
      // No beard
      beardIndex++;
    } else {
      beardIndex = 0;
      beardstyles[beardIndex].SetActive(true);
    }
  }

  public void PreviousBeard() {
    if (beardIndex > -1 && beardIndex < beardstyles.Length) {
      beardstyles[beardIndex].SetActive(false);
    }
    if (beardIndex > 0) {
      beardIndex--;
      beardstyles[beardIndex].SetActive(true);
    } else if (beardIndex == 0) {
      // No beard
      beardIndex--;
    } else {
      beardIndex = (beardstyles.Length - 1);
      beardstyles[beardIndex].SetActive(true);
    }
  }

  public void ChangeHeight() {
    Vector3 lTemp = characterScaleObject.localScale;
    lTemp.y = 0.8f + (heightSlider.value / 2.5f);
    characterScaleObject.localScale = lTemp;
  }

  public void ChangeWidth() {
    Vector3 wTemp = characterScaleObject.localScale;
    wTemp.x = 0.8f + (widthSlider.value / 2.5f);
    wTemp.z = 0.8f + (widthSlider.value / 2.5f);
    characterScaleObject.localScale = wTemp;
  }

  public void SelectFemale() {
    male = false;
    // Switch model
    femaleCharacterScaleObject.gameObject.SetActive(true);
    maleCharacterScaleObject.gameObject.SetActive(false);
    characterScaleObject = femaleCharacterScaleObject;
    character = femaleCharacter;

    // Reset hair style
    hairstyles[hairStyleIndex].SetActive(false);
    hairstyles = femaleHairstyles;
    hairStyleIndex = defaultHairStyleIndex;
    hairstyles[hairStyleIndex].SetActive(true);
    if (hairstyles != null) {
      foreach (GameObject hairstyle in hairstyles) {
        var hairRenderer = hairstyle.GetComponent<Renderer>();
        hairRenderer.material.color = allHairColors[hairColorIndex];
      }
    }

    if (beardIndex != -1) beardstyles[beardIndex].SetActive(false);
    beardParameter.SetActive(false);
    boobParameter.SetActive(true);
    setUpNewModel();
  }

  public void SelectMale() {
    male = true;
    // Switch model
    femaleCharacterScaleObject.gameObject.SetActive(false);
    maleCharacterScaleObject.gameObject.SetActive(true);
    characterScaleObject = maleCharacterScaleObject;
    character = maleCharacter;

    // Reset hair style
    hairstyles[hairStyleIndex].SetActive(false);
    hairstyles = maleHairstyles;
    hairStyleIndex = defaultHairStyleIndex;
    hairstyles[hairStyleIndex].SetActive(true);

    if (beardIndex != -1) beardstyles[beardIndex].SetActive(true);
    boobParameter.SetActive(false);
    beardParameter.SetActive(true);
    setUpNewModel();
  }

  public void setUpNewModel() {
    renderer = character.GetComponent<Renderer>();
    renderer.material.color = allSkinColors[skinColorIndex];
    hairStyleIndex = defaultHairStyleIndex;
    hairstyles[hairStyleIndex].SetActive(true);
    ChangeHeight();
    ChangeWidth();
  }

  public void StartGame() {
    
  }

}
