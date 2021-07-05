public interface ICharacter : IDamagable
{
    public void ChangeSpeed(float percentage);
    public void Stun(float duration);
}