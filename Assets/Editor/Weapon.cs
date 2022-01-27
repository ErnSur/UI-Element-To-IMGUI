using UnityEngine;

[CreateAssetMenu]
public class Weapon : ScriptableObject
{
    public int a;
    [Range(2,3)]
    public int b;
    [Header("hmm")]
    public int c;
    [Ve]
    public int d;
    public int looongAssNameForTheField;

}