﻿
using BO;
using DalApi;
using System.Security.Cryptography.X509Certificates;

namespace BlApi;

public interface IBl
{
    public IEngineer Engineer { get; }
    public ITask Task { get; }
    public ProjectStatus ProjectStatus { get; }
    
    public DateTime? StartProject { set; get; }
    public DateTime? EndProject { set; get; }
    public void CreateSchedule();
}





