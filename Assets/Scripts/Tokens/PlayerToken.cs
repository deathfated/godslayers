

using System.Diagnostics;

public class PlayerToken : Token
{

    void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();

        MaxActionPoints = 5;
        maxHp = 10;
        currHp = maxHp;
        isDamageable = true;

        StartingPosition = transform.position;

    }

    protected override void OnDeath()
    {
        base.OnDeath();

    }
}
