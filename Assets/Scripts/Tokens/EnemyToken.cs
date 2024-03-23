

public class EnemyToken : Token
{

    void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();

        MaxActionPoints = 5;
        maxHp = 8;
        currHp = maxHp;
        isDamageable = true;

        StartingPosition = transform.position;

    }

}
