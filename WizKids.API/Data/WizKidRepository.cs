using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WizKids.API.Model;

namespace WizKids.API.Data
{
    public class WizKidRepository : IWizKidSeachRepository
    {
       List<CustomDictionary> IWizKidSeachRepository.FetchDBData(string query)
        {
            return Globals._globals.GetAllRows(query).ToList();
        }

     }
}
