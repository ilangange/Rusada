using AutoMapper;
using Rusada.Aviation.Core.Contracts.Requests;
using Rusada.Aviation.Core.Contracts.Responses;
using Rusada.Aviation.Core.Entities;
using Rusada.Aviation.Core.Interface;

namespace Rusada.Aviation.Core.Service
{
    public class SightingService : ISightingService
    {
        private readonly IGenericRepository<Sighting> _sightingRepository;
        private readonly IMapper _mapper;

        public SightingService(IGenericRepository<Sighting> sightingRepository, IMapper mapper)
        {
            _sightingRepository = sightingRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sighting = (await _sightingRepository.GetAll()).Where(a => a.Id == id && !a.IsDeleted).FirstOrDefault();
            if (sighting != null)
            {
                sighting.IsDeleted = true;
            }
            await _sightingRepository.Update(sighting);
            await _sightingRepository.Save();
            return sighting == null ? false : true;
        }

        public async Task<SightingModel> GetByIdAsync(int id)
        {
            var sighting = (await _sightingRepository.GetAll()).Where(a => a.Id == id && !a.IsDeleted).FirstOrDefault();
            return sighting == null ? null : _mapper.Map<SightingModel>(sighting);
        }

        public async Task<DataTableResponseModel> GetPagedDataAsync(DataTableRequestModel dataTableRequest)
        {
            DataTableResponseModel dataTableResponseModel = null;
            var query = (await _sightingRepository.GetAll()).Where(a => !a.IsDeleted);

            if (dataTableRequest.Columns[0].Search.Value != "") {
                query = query.Where(a => a.Make.Contains(dataTableRequest.Columns[0].Search.Value));
            }
            if (dataTableRequest.Columns[1].Search.Value != "")
            {
                query = query.Where(a => a.Model.Contains(dataTableRequest.Columns[1].Search.Value));

            }
            if (dataTableRequest.Columns[2].Search.Value != "")
            {
                query = query.Where(a => a.Registration.Contains(dataTableRequest.Columns[2].Search.Value));
            }

            var sightingList = query.Skip(dataTableRequest.Start).Take(dataTableRequest.Length).ToList();
            if (sightingList != null) {
                dataTableResponseModel = new DataTableResponseModel();
                dataTableResponseModel.Data = _mapper.Map<List<SightingModel>>(sightingList);
            }
            dataTableResponseModel.Draw = dataTableRequest.Draw;
            dataTableResponseModel.RecordsFiltered = sightingList == null ? 0 : sightingList.Count;
            dataTableResponseModel.RecordsTotal = (await _sightingRepository.GetAll()).Where(a => !a.IsDeleted).Count();

            return dataTableResponseModel;
        }

        public async Task<SightingModel> SaveSightingAsync(SightingModel sightingModel)
        {
            var sighting = _mapper.Map<Sighting>(sightingModel);
            await _sightingRepository.Insert(sighting);
            await _sightingRepository.Save();
            return _mapper.Map<SightingModel>(sighting);
        }

        public async Task<SightingModel> UpdateAsync(SightingModel sightingModel)
        {
            var sighting = (await _sightingRepository.GetAll()).Where(a => a.Id == (int)sightingModel.Id).FirstOrDefault();

            if (sighting != null) {
                sighting.Make = sightingModel.Make;
                sighting.Model = sightingModel.Model;
                sighting.Registration = sightingModel.Registration;
                sighting.Location = sightingModel.Location;
                sighting.Date = sightingModel.Date;
                sighting.FileData = sightingModel.FileData;

                await _sightingRepository.Update(sighting);
                await _sightingRepository.Save();
            }

            return sighting == null ? null : _mapper.Map<SightingModel>(sighting);
        }
    }
}
