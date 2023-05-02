using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Skybox", menuName = "ScriptableObjects/SkyboxScriptableObject", order = 2)]
public class SkyboxSettings : ScriptableObject
{
    [SerializeField] public Material Material;
    [SerializeField] public Color FogColor;
}
