using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using R3;

namespace _Project.Scripts.ViewModel.Implementation
{
    public class LoadingWindowViewModel : IViewModel
    {
        public readonly ReactiveProperty<float> LoadingProgress = new();
        
        public async UniTask StartLoadingAsync(UniTask[] tasks)
        {
            List<UniTask> tasksList = new(tasks);
            int tasksCount = tasksList.Count;
            float succeededTasksCount = 0;
            
            while (tasksList.Count > 0)
            {
                if (CheckUniTaskSuccess(tasksList, out List<UniTask> succeededTasks))
                {
                    RemoveSucceededTasksFromTasks(tasksList, succeededTasks);
                    succeededTasksCount += succeededTasks.Count;
                    LoadingProgress.Value = tasksCount / succeededTasksCount;
                }
                await UniTask.Yield();
            }
        }

        private void RemoveSucceededTasksFromTasks(List<UniTask> tasks, IEnumerable<UniTask> succeededTasks)
        {
            foreach (UniTask task in succeededTasks)
                tasks.Remove(task);
        }
        
        private bool CheckUniTaskSuccess(IEnumerable<UniTask> tasks, out List<UniTask> succeededTasks)
        {
            List<UniTask> succeededTaskList = new();
            foreach (UniTask task in tasks)
            {
                if (task.Status == UniTaskStatus.Succeeded)
                    succeededTaskList.Add(task);
            }

            if (succeededTaskList.Count == 0)
            {
                succeededTasks = null;
                return false;
            }
            
            succeededTasks = succeededTaskList;
            return true;
        }
        
        public void ResetLoadingProgress() => 
            LoadingProgress.Value = 0;
    }
}