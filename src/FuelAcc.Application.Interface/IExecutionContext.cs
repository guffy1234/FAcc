namespace FuelAcc.Application.Interface;

public interface IExecutionContext
{
    public bool IsReplicationApplying { get; set; }
}
