using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WizKids.API.Data;
using WizKids.API.Model;

namespace WizKids.API.Service
{
    public class WizKidSearchService : IWizKidSearchService
    {
        private readonly IWizKidSeachRepository _wizKidSeachRepository;
        public WizKidSearchService(IWizKidSeachRepository wizKidSeachRepository)
        {
            _wizKidSeachRepository = wizKidSeachRepository;
        }

        List<string> IWizKidSearchService.FetchDBData(string query)
        {
            var result= _wizKidSeachRepository.FetchDBData(query).Select(x=>x.Name).ToList();
            return result;
        }

        public List<string> FetchAPIData(string query)
        {
            CultureInfo culture = CultureInfo.CurrentCulture;
            RestServiceClient restServiceClient = new RestServiceClient
            {
                endPoint = "https://services.lingapps.dk/misc/getPredictions?locale="+ (culture.Name switch
                {
                    "en-US" => "en-GB",
                    "en-GB" => "en-GB",
                    "en-DK" => "da-DK",
                    _ => "en-GB",
                }) + "&text=I%20like%20" + query,
                TokenBearer = "MjAxOS0xMS0yOQ==.dXJAaWZzLmFlcm8=.MmQyMjBhZDE2ZmMyZDc0MjY3ZDBkZmFkNjViZWQzMWE="
            };
            var list = JsonConvert.DeserializeObject<List<string>>(restServiceClient.makeRequest<string>(true));
                       
            if (list.Count() <= 0)
                return null;
               else
                return list;
        }


    }
}
