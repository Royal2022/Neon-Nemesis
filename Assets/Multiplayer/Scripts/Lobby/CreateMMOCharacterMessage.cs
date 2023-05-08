using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public struct CreateMMOCharacterMessage : NetworkMessage
{
    public int SkinNumber;
    public Race race;
    public string name;
    public Color hairColor;
    public Color eyeColor;
}
public enum Race
{
    None,
    Elvish,
    Dwarvish,
    Human
}