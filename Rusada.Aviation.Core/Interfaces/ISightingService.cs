using Rusada.Aviation.Core.Contracts.Requests;
using Rusada.Aviation.Core.Contracts.Responses;

namespace Rusada.Aviation.Core.Interface
{
    public interface ISightingService
    {
        /// <summary>
        /// Save Sighting data
        /// </summary>
        /// <param name="sightingModel"></param>
        /// <returns></returns>
        Task<SightingModel> SaveSightingAsync(SightingModel sightingModel);

        /// <summary>
        /// Detele an existing sighting by accepting Id as the inpu parameter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Update an existing sighting based on the new sighting model passed
        /// </summary>
        /// <param name="sightingModel"></param>
        /// <returns></returns>
        Task<SightingModel> UpdateAsync(SightingModel sightingModel);

        /// <summary>
        /// Get the paginated Sighting data based on the filterations
        /// </summary>
        /// <returns></returns>
        Task<DataTableResponseModel> GetPagedDataAsync(DataTableRequestModel dataTableRequest);

        /// <summary>
        /// Get the details of a sighting based on the Id passed
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SightingModel> GetByIdAsync(int id);
    }
}
