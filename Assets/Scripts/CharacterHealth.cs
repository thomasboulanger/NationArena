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

    //Take damage, and return true if the character survived, false otherwise
    public bool GetHit(int damage)
    {
        _life -= damage;
        if (_life < 0)
            _life = 0;
        return _life > 0;
    }

    //Accessor for life
    public int GetLife()
    {
        return _life;
    }

    //Return whether life is below or at 0
    public bool IsDead()
    {
        return _life == 0;
    }
}