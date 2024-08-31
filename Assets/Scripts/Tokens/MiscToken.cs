

public class MiscToken : Token
{
    public bool isDamaging;
    public int damage;
    public bool isOccupying;

    void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();

        StartingPosition = transform.position;
    }
}
