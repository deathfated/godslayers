using UnityEditor.Animations;
using UnityEngine;

public abstract class Token : MonoBehaviour
{
    public int MaxActionPoints;
    public int maxHp;
    public int currHp;
    public int atkDamage;
    protected bool isDamageable;
    public Sprite charaSprite;
    public AnimatorController animController;

    [SerializeField] protected Vector3 StartingPosition;

    protected virtual void Init()
    {
        if (MaxActionPoints == 0) MaxActionPoints = 1;
        if (maxHp == 0) maxHp = 1;
        if (currHp == 0) currHp = 1;
        isDamageable = false;

        StartingPosition = new Vector3(0,0,0);
    }

    public virtual void OnHpReduced(int damage)
    {
        currHp -= damage;
        //update UIs
        Debug.Log(this.name + " takes " + damage +" damage!");

        if (currHp <= 0) OnDeath();
    }

    protected virtual void OnDeath()
    {
        Debug.Log("RIP "+ this.name);
        this.gameObject.SetActive(false);
    }
}
