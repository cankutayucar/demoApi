using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IUserOperationClaimService
    {
        IResult Add(UserOperationClaim operationClaim);
        IResult Update(UserOperationClaim operationClaim);
        IResult Delete(UserOperationClaim operationClaim);
        IDataResult<List<UserOperationClaim>> GetList();
        IDataResult<UserOperationClaim> GetById(int id);
    }
}
