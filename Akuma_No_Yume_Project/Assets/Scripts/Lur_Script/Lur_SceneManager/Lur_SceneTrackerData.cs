using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneTrackerData", menuName = "Scene Management/Scene Tracker Data")]
public class SceneTrackerData : ScriptableObject
{
    public int previousSceneIndex = -1; // Valor inicial negativo para indicar que no hay escena registrada.
}
