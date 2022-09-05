using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.ValidationRules.FluentValid;
using Core.Aspects.Validation;
using Core.Utilities.Business;
using Core.Utilities.Messages;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class OperationClaimManager : IOperationClaimService
    {
        private readonly IOperationClaimDal _operationClaimDal;

        public OperationClaimManager(IOperationClaimDal operationClaimDal)
        {
            _operationClaimDal = operationClaimDal;
        }

        [ValidationAspects(typeof(OperationClaimValidator))]
        public IResult Add(OperationClaim operationClaim)
        {
            IResult result = BusinessRules.Run(IsNameAvaliable(operationClaim.Name));
            if (result.Success)
            {
                _operationClaimDal.Add(operationClaim);
                return new SuccessResult(Message.OperationClaim.Add());
            }
            return result;
        }

        [ValidationAspects(typeof(OperationClaimValidator))]
        public IResult Update(OperationClaim operationClaim)
        {
            IResult result = BusinessRules.Run(IsNameAvaliableForUpdate(operationClaim));
            if (result.Success)
            {
                _operationClaimDal.Update(operationClaim);
                return new SuccessResult(Message.OperationClaim.Update());
            }
            return result;
        }

        public IResult Delete(OperationClaim operationClaim)
        {
            _operationClaimDal.Delete(operationClaim);
            return new SuccessResult(Message.OperationClaim.Delete());
        }

        public IDataResult<List<OperationClaim>> GetList()
        {
            return new SuccessDataResult<List<OperationClaim>>(_operationClaimDal.GetAll());
        }

        public IDataResult<OperationClaim> GetById(int id)
        {
            return new SuccessDataResult<OperationClaim>(_operationClaimDal.Get(oc => oc.Id == id));
        }

        IResult IsNameAvaliable(string name)
        {
            var result = _operationClaimDal.Get(d => d.Name == name);
            if (result!=null)
            {
                return new ErrorResult(Message.OperationClaim.NameIsNotAvaliable());
            }
            return new SuccessResult();
        }

        IResult IsNameAvaliableForUpdate(OperationClaim operationClaim)
        {
            var currentOperationClaim = _operationClaimDal.Get(oc => oc.Id == operationClaim.Id);
            if(currentOperationClaim.Name != operationClaim.Name)
            {
                var result = _operationClaimDal.Get(d => d.Name == currentOperationClaim.Name);
                if (result != null)
                {
                    return new ErrorResult(Message.OperationClaim.NameIsNotAvaliable());
                }
            }
            return new SuccessResult();
        }
    }
}
