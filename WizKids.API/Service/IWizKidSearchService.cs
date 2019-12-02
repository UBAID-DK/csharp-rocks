using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WizKids.API.Model;

namespace WizKids.API.Service
{
   public interface IWizKidSearchService
    {
        public List<string> FetchDBData(string query);
       public List<string> FetchAPIData(string query);
    }
}
