public class Health
{
    private int _life;

    //Set default health if unspecified
    public Health()
    {
        _life = 10;
    }

    //Set Health manually
    public Health(int life)
    {
        _life = life;
    }

    public bool GetHit(int damage)
    {
        _life -= damage;
        if (_life < 0) _life = 0;
        if (_life > 100) _life = 100;
        
        return _life > 0;
    }

    public int GetLife()
    {
        return _life;
    }

    public bool IsDead()
    {
        return _life <= 0;
    }
}