using FuelAcc.Application.Interface;

namespace FuelAcc.Application.UseCases
{
    public class ExecutionContext : IExecutionContext
    {
        public bool IsReplicationApplying { get; set; }
    }
}