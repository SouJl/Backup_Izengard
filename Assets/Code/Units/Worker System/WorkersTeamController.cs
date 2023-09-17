using Controllers.Worker;
using System;
using System.Collections.Generic;
using UnityEngine;


public class WorkersTeamController: IOnUpdate, IDisposable, IOnController
{
    public WorkersTeamController(WorkersTeamConfig config)
    {
        _workerFactory = new WorkerFactory(config);

        _workersResourceTeam = new WorkersResourceTeam(config.SmokeBreakTime);
        _workersCraftTeam = new WorkersCraftTeam();
        _workersFreeTeam = new WorkersFreeTeam(_workerFactory);
    }

    public int SendWorkerToWork(Vector3 startPalce, Vector3 craftPalce,
           IWorkerPreparation preparation, IWorkerWork work)
    {
        WorkerController workerController = GetWorker();

        _workersCraftTeam.SendWorkerToWork(
            startPalce, craftPalce, workerController, preparation, work);

        return workerController.WorkerId;
    }

    public int SendWorkerToMine(Vector3 startPalce, Vector3 targetPalce,
        IWorkerPreparation preparation, IWorkerTask task)
    {
        WorkerController workerController = GetWorker();

        _workersResourceTeam.SendWorkerToMine(
            startPalce, targetPalce, workerController, preparation, task);

        return workerController.WorkerId;
    }

    public void Pause()
    {        
        _workersCraftTeam.Pause();
        _workersResourceTeam.Pause();
        _workersFreeTeam.Pause();
    }

    public void Resume()
    {
        _workersCraftTeam.Resume();
        _workersResourceTeam.Resume();
        _workersFreeTeam.Resume();
    }

    public void CancelWork(int workerId)
    {
        var worker = _workersResourceTeam.CancelWork(workerId);
        if (null == worker)
        {
            worker = _workersCraftTeam.CancelWork(workerId);
            if (null == worker)
                return;
        }

        _workersFreeTeam.FreeWorker(worker);
    }

    public void OnUpdate(float deltaTime)
    {
        _workersResourceTeam.OnUpdate(deltaTime);
        _workersCraftTeam.OnUpdate(deltaTime);
        _workersFreeTeam.OnUpdate(deltaTime);
    }

    private WorkerController GetWorker()
    {
        WorkerController worker = null;
        if (null != (worker = _workersFreeTeam.GetWorker()))
        {
            return worker;
        }
        return _workerFactory.CreateWorker();
    }

    public void Dispose()
    {
        List<WorkerController> workers = new List<WorkerController>();
        _workersResourceTeam.Dispose(workers);
        _workersCraftTeam.Dispose(workers);
        _workersFreeTeam.Dispose();

        foreach (var worker in workers)
        {
            _workerFactory.ReleaseWorker(worker.View);
            worker.Dispose();
        }
        workers.Clear();
    }

    private WorkerFactory _workerFactory;
    
    private WorkersResourceTeam _workersResourceTeam;
    private WorkersCraftTeam _workersCraftTeam;
    private WorkersFreeTeam _workersFreeTeam;
}