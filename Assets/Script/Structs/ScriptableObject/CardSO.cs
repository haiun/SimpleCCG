using UnityEngine;

//[CreateAssetMenu(fileName = "CardSO", menuName = "ScriptableObjects/CardSO", order = 1)]
public class CardSO : ScriptableObject
{
    public int CardId = 0;
    public Sprite Icon = null;
    public int Tier = 0;

    public int Attack = 0;
    public int Defense = 0;
}
