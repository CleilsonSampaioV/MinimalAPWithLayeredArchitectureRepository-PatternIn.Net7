using Microsoft.EntityFrameworkCore;
using MyTrainingSheet.Domain;
using MyTrainingSheet.Infra.Context;
using System.Linq.Expressions;

namespace MyTrainingSheet.Infra;
public class LiftRepository : ILiftRepository
{
    private readonly MyTranningSheetContext _myTranningSheet;

    public LiftRepository(MyTranningSheetContext myTranningSheetContext)
    {
        _myTranningSheet = myTranningSheetContext;
    }

    public async Task<IResponseModel> CreateAsync(Lift lift)
    {
        _myTranningSheet.lifts.Add(lift);

        return await _myTranningSheet.SaveChangesAsync() == 1 ?
            new ResponseModel { Success = true } :
            new ResponseModel
            {
                Success = false
            };
    }

    public async Task<IResponseModel> DeleteAsync(int id)
    {
        var liftData = await GetAsync(x => x.Id == id);
        if (!liftData.Success) return new ResponseModel { Success = false, Message = liftData.Message };
        _myTranningSheet.lifts.Remove(liftData.Data);
        return await _myTranningSheet.SaveChangesAsync() == 1 ?
            new ResponseModel { Success = true } :
            new ResponseModel
            {
                Success = false
            };
    }

    public async Task<IResponseDataModel<Lift>> GetAsync(Expression<Func<Lift, bool>> filter)
    {
        var data = await _myTranningSheet.lifts.SingleOrDefaultAsync(filter);
        return data != null ?
            new ResponseDataModel<Lift>
            {
                Success = true,
                Data = data
            } :
            new ResponseDataModel<Lift>
            {
                Success = false,
                Message = "Lift not found"
            };
    }

    public async Task<IResponseDataModel<IEnumerable<Lift>>> GetAllAsync(Expression<Func<Lift, bool>>? filter)
    {
        return new ResponseDataModel<IEnumerable<Lift>>
        {
            Success = true,
            Data = await _myTranningSheet.lifts.ToListAsync()
        };
    }

    public async Task<IResponseModel> UpdateAsync(Lift lift)
    {
        lift.Name = lift.Name;
        lift.Weight = lift.Weight;
        lift.Reps = lift.Reps;
        _myTranningSheet.lifts.Update(lift);
        return await _myTranningSheet.SaveChangesAsync() == 1 ?
          new ResponseModel { Success = true } :
          new ResponseModel
          {
              Success = false
          };
    }
}
