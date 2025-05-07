using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "ScriptaleObjects/Product")]
[Serializable]
public class ProductData : ScriptableObject
{

    public int price;

    public int amount;

    public string description;

    public string title;

    public Sprite sprite;
}
