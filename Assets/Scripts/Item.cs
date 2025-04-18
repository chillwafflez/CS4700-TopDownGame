using System.ComponentModel;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public Sprite image;
    public int itemIndex;
}
