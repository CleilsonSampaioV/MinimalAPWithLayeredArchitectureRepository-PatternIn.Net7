using MyTrainingSheet.Domain;

namespace MyTrainingSheet.Business;
public interface ILiftService
{
    Task<IResponseModel> CreateAsync(Lift lift);
    Task<IResponseDataModel<Lift>> GetAsync(Lift lift);
    Task<IResponseDataModel<IEnumerable<Lift>>> GetAllAsync();
    Task<IResponseModel> UpdateAsync(Lift lift);
    Task<IResponseModel> DeleteAsync(int id);
    Task<IResponseDataModel<Lift>> GetByLiftName(LiftName name);
}
