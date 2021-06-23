public interface IDamagable
{

    public void TakeDamage(float amount);
    public void ChangeSpeed(float percentage);
    public void Stun(float duration);
    public void ApplyDOT(float amount,float duration);


}