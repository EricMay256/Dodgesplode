using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "Scriptable Objects/Projectile")]
public class Projectile : ScriptableObject
{
    public Sprite sprite;
    public Color color = Color.white;
    public float speed = 10f;
    public int damage = 1;
    public float lifetime = 5f;
    public float scale = 1f;
    public GameObject impactEffect;
    public AudioClip shootSound;
    public AudioClip impactSound;
}
