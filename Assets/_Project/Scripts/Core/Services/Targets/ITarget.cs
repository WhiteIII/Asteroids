namespace _Project.Scripts.Core.Services.Targets
{
    public interface ITarget
    {
    }

    public interface IKillableTarget : ITarget
    {
        public void Kill();
    }
}