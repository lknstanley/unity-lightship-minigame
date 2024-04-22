namespace Core.ObjectPool
{
    public interface IPoolable
    {
        public void Spawn();
        public void Despawn();
    }
}