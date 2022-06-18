using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Theme", menuName = "Level Generation/Theme", order = 1)]
public class LevelTheme : ScriptableObject
{
    public Material FloorMaterial;
    public Material HallwayMaterial;

    public GameObject[] LevelAssets;
}
