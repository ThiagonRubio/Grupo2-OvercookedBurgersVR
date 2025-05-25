using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "NewIngredientImages", menuName = "Custom/IngredientImages")]
public class IngredientImages : ScriptableObject
{
    public List<IngredientImagePair> ingredientImagesList = new List<IngredientImagePair>();
}
