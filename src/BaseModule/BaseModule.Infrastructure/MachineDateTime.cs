using CoreModule.Common;

namespace BaseModule.Infrastructure;

public class MachineDateTime : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
}
