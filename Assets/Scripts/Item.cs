using System.ComponentModel;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    //public TileBase tile;
    public Sprite image;
    // public ItemEffects effect;
    public int itemIndex;
}
