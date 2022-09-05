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
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using static Core.Utilities.Messages.Message;
using UserOperationClaim = Entities.Concrete.UserOperationClaim;

namespace Business.Concrete
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private readonly IUserOperationClaimDal _userOperationClaimDal;
        private readonly IOperationClaimService _operationClaimService;
        private readonly IUserService _userService;

        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal, IOperationClaimService operationClaimService, IUserService userService)
        {
            _userOperationClaimDal = userOperationClaimDal;
            _operationClaimService = operationClaimService;
            _userService = userService;
        }

        [ValidationAspects(typeof(UserOperationClaimValidator))]
        public IResult Add(UserOperationClaim userOperationClaim)
        {
            IResult resukt = BusinessRules.Run(IsOperationClaimExist(userOperationClaim.OperationClaimId), IsUserExist(userOperationClaim.UserId), IsOperationSetAvaliable(userOperationClaim));
            _userOperationClaimDal.Add(userOperationClaim);
            return new SuccessResult(Message.UserOperationClaim.Add());
        }

        private IResult IsOperationSetAvaliable(UserOperationClaim userOperationClaim)
        {
            var result = _userOperationClaimDal.Get(p =>
                p.UserId == userOperationClaim.UserId && p.OperationClaimId == userOperationClaim.OperationClaimId);
            if (result != null)
            {
                return new ErrorResult(Message.UserOperationClaim.SetAvaliable());
            }

            return new SuccessResult();
        }

        private IResult IsOperationClaimExist(int operationClaimId)
        {
            var result = _operationClaimService.GetById(operationClaimId).Data;
            if (result == null)
            {
                return new ErrorResult(Message.UserOperationClaim.OperationClaimNotExist());
            }

            return new SuccessResult();
        }

        private IResult IsUserExist(int userId)
        {
            var result = _userService.GetById(userId).Data;
            if (result == null)
            {
                return new ErrorResult(Message.UserOperationClaim.UserNotExist());
            }

            return new SuccessResult();
        }

        IResult IsNameAvaliableForUpdate(UserOperationClaim userOperationClaim)
        {
            var currentOperationClaim = _userOperationClaimDal.Get(oc => oc.Id == userOperationClaim.Id);
            if (currentOperationClaim.UserId != userOperationClaim.UserId || currentOperationClaim.OperationClaimId != userOperationClaim.OperationClaimId)
            {
                var result = _userOperationClaimDal.Get(p =>
                    p.UserId == userOperationClaim.UserId && p.OperationClaimId == userOperationClaim.OperationClaimId);
                if (result != null)
                {
                    return new ErrorResult(Message.UserOperationClaim.SetAvaliable());
                }
            }
            return new SuccessResult();
        }


        [ValidationAspects(typeof(UserOperationClaimValidator))]
        public IResult Update(UserOperationClaim userOperationClaim)
        {
            IResult resukt = BusinessRules.Run(IsOperationClaimExist(userOperationClaim.OperationClaimId), IsUserExist(userOperationClaim.UserId), IsNameAvaliableForUpdate(userOperationClaim));
            _userOperationClaimDal.Update(userOperationClaim);
            return new SuccessResult(Message.UserOperationClaim.Updata());
        }

        public IResult Delete(UserOperationClaim operationClaim)
        {
            _userOperationClaimDal.Delete(operationClaim);
            return new SuccessResult(Message.UserOperationClaim.Delete());
        }

        public IDataResult<List<UserOperationClaim>> GetList()
        {
            var result = _userOperationClaimDal.GetAll();
            return new SuccessDataResult<List<UserOperationClaim>>(result);
        }

        public IDataResult<UserOperationClaim> GetById(int id)
        {
            var result = _userOperationClaimDal.Get(uoc => uoc.Id == id);
            return new SuccessDataResult<UserOperationClaim>(result);
        }
    }
}
