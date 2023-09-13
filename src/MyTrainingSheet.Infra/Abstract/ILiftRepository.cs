using MyTrainingSheet.Domain;
using System.Linq.Expressions;

namespace MyTrainingSheet.Infra;
public interface ILiftRepository
{
    ///CRUD
    Task<IResponseModel> CreateAsync(Lift lift);
    Task<IResponseDataModel<Lift>> GetAsync(Expression<Func<Lift, bool>>? filter);
    Task<IResponseDataModel<IEnumerable<Lift>>> GetAllAsync(Expression<Func<Lift, bool>>? filter = null);
    Task<IResponseModel> UpdateAsync(Lift lift);
    Task<IResponseModel> DeleteAsync(int id);
}
