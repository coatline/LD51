using UnityEngine;

public class Weapon : Item
{
    [Header("Projectile Properties")]
    [SerializeField] Vector2 attackLinearDrag;
    [SerializeField] float attackGravity;
    [SerializeField] float attackForce;
    [SerializeField] float attackCount;

    [Header("Fade Out")]
    [SerializeField] bool fadeOut;
    [SerializeField] float fadeOutMagnitude;
    [SerializeField] float maxLifeTime;

    [Header("Attack Properties")]
    [SerializeField] WeaponType weaponType;
    [SerializeField] float spread;
    [SerializeField] float damage;
    [SerializeField] float knockBack;

    [Header("Burst")]
    [SerializeField] bool burst;
    [SerializeField] int attacksPerBurst;
    [SerializeField] bool canRollInBurst;
    [SerializeField] float timeBetweenAttacks;

    [Header("Multi-Shot")]
    [SerializeField] bool parellelBullets;
    [SerializeField] float attackSpacing = .3f;

    public WeaponType WeaponType => weaponType;
    public float Damage => damage;
    public float AttackForce => attackForce;
    public float AttackGravity => attackGravity;
    public float AttackCount => attackCount;
    public Vector2 AttackLinearDrag => attackLinearDrag;
    public float Knockback => knockBack;
    public bool CanRollInBurst => canRollInBurst;
    public bool FadeOut => fadeOut;
    public float FadeOutMagnitude => fadeOutMagnitude;
    public float MaxLifeTime => maxLifeTime;

    public float Spread => spread;
    public bool Burst => burst;
    public int AttacksPerBurst => attacksPerBurst;
    public float TimeBetweenAttacks => timeBetweenAttacks;
    public bool ParellelBullets => parellelBullets;
    public float AttackSpacing => attackSpacing;

}

public enum WeaponType
{
    Slayer,
    Support,
    Anchor
}