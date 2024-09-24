using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "EnemyType")]
public class EnemyTypeSO : ScriptableObject
{
    public float Health;
    public bool isBleedImmune;
    public bool isStunningImmune;
    public bool isKnockBackImmune;
}
