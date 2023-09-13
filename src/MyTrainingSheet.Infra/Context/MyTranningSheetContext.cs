using Microsoft.EntityFrameworkCore;
using MyTrainingSheet.Domain;

namespace MyTrainingSheet.Infra.Context;

public class MyTranningSheetContext: DbContext
{
    public MyTranningSheetContext(DbContextOptions<MyTranningSheetContext> options) : base(options) { }

    public DbSet<Lift> Lifts => Set<Lift>();
}
