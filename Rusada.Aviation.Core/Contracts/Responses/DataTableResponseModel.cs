using Newtonsoft.Json;
using Rusada.Aviation.Core.Contracts.Requests;

namespace Rusada.Aviation.Core.Contracts.Responses
{
    public class DataTableResponseModel
    {
        [JsonProperty(PropertyName = "draw")]
        public int Draw { get; set; }

        [JsonProperty(PropertyName = "recordsFiltered")]
        public int RecordsFiltered { get; set; }

        [JsonProperty(PropertyName = "recordsTotal")]
        public int RecordsTotal { get; set; }

        [JsonProperty(PropertyName = "data")]
        public List<SightingModel> Data { get; set; }

        public DataTableResponseModel()
        {
            Data = new List<SightingModel>();
        }
    }
}
