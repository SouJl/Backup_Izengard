using Controllers.Worker;
using System.Collections.Generic;
using UnityEngine;


public class WorkersCraftTeam : IOnUpdate, IOnController
{
    public WorkersCraftTeam()
    {
        _model = new WorkersTeamModel();

        _workersAreGoingToWork = new Dictionary<int, WorkerCraftWork>();
        _readyWorkersToWork = new List<int>();

        _workingWorkers = new Dictionary<int, WorkerCraftWork> ();
    }

    public int SendWorkerToWork(Vector3 startPalce, Vector3 targetPalce,
         WorkerController workerController, 
         IWorkerPreparation preparation, IWorkerWork work)
    {
        if (null == work || null == workerController)
            return -1;

        var workingWorker = new WorkerCraftWork() { 
            Worker = workerController, Preparation = preparation, Work = work 
        };

        _workersAreGoingToWork.Add(workerController.WorkerId, workingWorker);

        workerController.OnMissionCompleted += OnReadyToWork;
        workerController.GoToPlace(startPalce, targetPalce);
        return workerController.WorkerId;
    }

    public void Pause()
    {
        if (_model.IsPaused)
            return;

        _model.IsPaused = true;
        foreach (var kvp in _workersAreGoingToWork)
            kvp.Value.Worker.Pause();
    }

    public void Resume()
    {
        if (!_model.IsPaused)
            return;

        foreach (var kvp in _workersAreGoingToWork)
            kvp.Value.Worker.Resume();

        _model.IsPaused = false;
    }

    public WorkerController CancelWork(int workerId)
    {
        WorkerCraftWork workingWorker = null;
        if (_workersAreGoingToWork.TryGetValue(workerId,
            out workingWorker))
        {
            _workersAreGoingToWork.Remove(workingWorker.Worker.WorkerId);
        }
        else if (_workingWorkers.TryGetValue(workerId,
            out workingWorker))
        {
            _workingWorkers.Remove(workerId);
        }
        else
            return null;

        if (null != workingWorker.Preparation)
            workingWorker.Preparation.AfterWork();

        var worker = workingWorker.Worker;

        workingWorker.Preparation = null;
        workingWorker.Work = null;
        workingWorker.Worker = null;

        worker.CancelWork();
        return worker;
    }

    public void OnUpdate(float deltaTime)
    {
        if (_model.IsPaused)
            return;

        foreach (var worker  in _workersAreGoingToWork)
            worker.Value.Worker.OnUpdate(deltaTime);

        CheckReadyWorkers();

        foreach (var work in _workingWorkers)
            work.Value.Work.Produce(deltaTime);
    }

    private void CheckReadyWorkers()
    {
        for (int i = 0; i < _readyWorkersToWork.Count; ++i)
        {
            int workerId = _readyWorkersToWork[i];
            if (_workersAreGoingToWork.TryGetValue(workerId, out WorkerCraftWork work))
            {
                work.Worker.OnMissionCompleted -= OnReadyToWork;
                
                if (null != work.Preparation)
                    work.Preparation.BeforWork();

                _workersAreGoingToWork.Remove(workerId);
                _workingWorkers.Add(workerId, work);
            }
        }
        _readyWorkersToWork.Clear();
    }

    private void OnReadyToWork(WorkerController workerController)
    {
        _readyWorkersToWork.Add(workerController.WorkerId);
    }

    private void ClearWorks(Dictionary<int, WorkerCraftWork> works, 
        List<WorkerController> workers)
    {
        
        foreach (var worker in works)
        {
            WorkerCraftWork work = worker.Value;
            work.Worker.OnMissionCompleted -= OnReadyToWork;
            work.Preparation = null;
            work.Work.Dispose();
            work.Work = null;

            workers.Add(work.Worker);
            work.Worker = null;
        }
        works.Clear();
    }

    public void Dispose(List<WorkerController> workers)
    {
        ClearWorks(_workersAreGoingToWork, workers);
        ClearWorks(_workingWorkers, workers);
    }

    class WorkerCraftWork
    {
        public WorkerController Worker;
        public IWorkerWork Work;
        public IWorkerPreparation Preparation;
    }

    private WorkersTeamModel _model;

    private Dictionary<int, WorkerCraftWork> _workersAreGoingToWork;
    private List<int> _readyWorkersToWork;

    private Dictionary<int, WorkerCraftWork> _workingWorkers;
}
