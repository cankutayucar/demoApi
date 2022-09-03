using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        public static IResult Run(params IResult[] locigs)
        {
            foreach (var logic in locigs)
            {
                if (!logic.Success)
                {
                    return logic;
                }
            }
            return new SuccessResult();
        }
    }
}
