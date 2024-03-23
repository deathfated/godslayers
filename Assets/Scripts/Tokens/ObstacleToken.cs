

public class ObstacleToken : Token
{
    
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
