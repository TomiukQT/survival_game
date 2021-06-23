using UnityEngine;
using UnityEngine.Serialization;

public class PlayerSkills : MonoBehaviour
{

    //TEMP
    //[SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Spell _tempSpell;

    public Spell GetSpell()
    {
        return _tempSpell;
    }
    
}