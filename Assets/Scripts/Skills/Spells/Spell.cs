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
    public float CastringModeParameter;

    public abstract void Cast(Vector3 direction, Vector3 position,float power = 1f);


}