using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Inspectable Data")]
public class InspectableData : ScriptableObject
{
    public string objectName;
    [TextArea] public string description;
}
