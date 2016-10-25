using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mainful.AdminUI.Shared.Entities
{
    public class MetaInfoEntity
    {
        public int DataFound { get; set; }
        public int DataPerPage { get; set; }
        public int TotalPages { get; set; }
        public int Page { get; set; }
    }
}
