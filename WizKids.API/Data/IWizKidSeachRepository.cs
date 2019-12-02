using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WizKids.API.Model;

namespace WizKids.API.Data
{
    public interface IWizKidSeachRepository
    {
        List<CustomDictionary> FetchDBData(string query);
    }
}
