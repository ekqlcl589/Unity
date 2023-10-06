using UnityEngine;

// 좀비 생성시 사용할 셋업 데이터
[CreateAssetMenu(menuName = "Scriptable/ZombieData", fileName = "Zombie Data")]
public class ZombieData : ScriptableObject
{
    [SerializeField] private float health = 100f; // 체력
    public float Health { get { return health; } set { health = value; } }
    [SerializeField] private float damage = 20f; // 공격력

    public float Damage { get { return damage; } set { damage = value; } }
    [SerializeField] private float speed = 2f; // 이동 속도

    public float Speed { get { return speed; } set { speed = value; } }
    [SerializeField] private Color skinColor = Color.white; // 피부색

    public Color SkinColor { get { return skinColor; } set { skinColor = value; } }

}
