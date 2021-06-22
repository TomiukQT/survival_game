using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Skill")]
public class Skill : ScriptableObject
{

    public string _name;
    [TextArea(15,20)]
    public string _description;

    public GameObject _projectile;

}