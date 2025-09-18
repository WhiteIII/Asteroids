namespace _Project.Scripts.Core.Services.Targets
{
    internal interface ITarget
    {
    }

    internal interface IKillableTarget : ITarget
    {
        public void Kill();
    }
}