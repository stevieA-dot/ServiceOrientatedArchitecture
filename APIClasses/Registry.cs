using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CsvHelper.Configuration;

namespace APIClasses
{
    public class Registry
    {
        public class EndpointData
        {
            public string Name;
            public string Description;
            public string APIEndpoint;
            public int NumOfOperands;
            public string OperandType;

            public EndpointData()
            {
                Name = "";
                Description = "";
                APIEndpoint = "";
                NumOfOperands = 0;
                OperandType = "";
            }
            
            public override string ToString()
            {
                return $"{Name} \n{Description} \n{APIEndpoint} \n{NumOfOperands} \n{OperandType}";
            }
        }

        public class SearchData
        {
            public string SearchStr;

            public override string ToString()
            {
                return SearchStr;
            }

        }

        public sealed class EndpointDataMap : ClassMap<EndpointData>
        {
            public EndpointDataMap()
            {
                Map(m => m.Name);
                Map(m => m.Description);
                Map(m => m.APIEndpoint);
                Map(m => m.NumOfOperands);
                Map(m => m.OperandType);
            }
        }
    }
}
