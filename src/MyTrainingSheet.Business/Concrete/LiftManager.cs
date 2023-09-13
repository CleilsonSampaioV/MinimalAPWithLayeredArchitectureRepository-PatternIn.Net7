using MyTrainingSheet.Domain;
using MyTrainingSheet.Infra;

namespace MyTrainingSheet.Business
{
    public class LiftManager : ILiftService
    {
        private readonly ILiftRepository _liftRepository;

        public LiftManager(ILiftRepository liftRepository)
        {
            _liftRepository = liftRepository;
        }

        public async Task<IResponseModel> CreateAsync(Lift lift)
        {
            var result = await _liftRepository.CreateAsync(lift);
            return result.Success.Equals(true) ?
                new ResponseModel { Success = true } :
                new ResponseModel { Success = false };
        }

        public async Task<IResponseModel> DeleteAsync(int id)
        {
            var liftResult = await _liftRepository.GetAllAsync(null);
            if (!liftResult.Success) return new ResponseModel { Success = false, Message = liftResult.Message };

            var result = await _liftRepository.DeleteAsync(id);
            return result.Success.Equals(true) ?
                new ResponseModel { Success = true } :
                new ResponseModel { Success = false };
        }

        public async Task<IResponseDataModel<Lift>> GetAsync(Lift lift)
        {
            var result = await _liftRepository.GetAsync(x => x.Id == lift.Id);
            return result.Success.Equals(true) ?
                new ResponseDataModel<Lift> { Success = true, Data = result.Data } :
                new ResponseDataModel<Lift> { Success = false };
        }

        public async Task<IResponseDataModel<IEnumerable<Lift>>> GetAllAsync()
        {
            var result = await _liftRepository.GetAllAsync();
            return result.Success.Equals(true) ?
                new ResponseDataModel<IEnumerable<Lift>> { Success = true, Data = result.Data } :
                new ResponseDataModel<IEnumerable<Lift>> { Success = false };
        }

        public async Task<IResponseModel> UpdateAsync(Lift lift)
        {
            var result = await _liftRepository.UpdateAsync(lift);
            return result.Success.Equals(true) ?
                new ResponseModel { Success = true } :
                new ResponseModel { Success = false };
        }

        public async Task<IResponseDataModel<Lift>> GetByLiftName(LiftName name)
        {
            var result = await _liftRepository.GetAsync(x => x.Name == name);

            return result.Success.Equals(true) ?
                new ResponseDataModel<Lift> { Success = true, Data = result.Data } :
                new ResponseDataModel<Lift> { Success = false };
        }
    }
}