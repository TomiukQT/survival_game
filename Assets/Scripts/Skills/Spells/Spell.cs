using UnityEngine;

public enum TargetType
{
    Aim,
    Self
}

public enum CastingMode
{
    Instant,
    Charging,
    Chanelling,
    Holding
}

[CreateAssetMenu(fileName = "New Spell", menuName = "Spells/BasicSpell")]
public abstract class Spell : ScriptableObject
{

    public string Name;
    [TextArea(15,20)]
    public string Description;

    public Sprite Icon;
    
    public float ManaCost;
    public float Cooldown;
    public CastingMode CastingMode;

    public abstract void Cast(Vector3 direction, Vector3 position);


}