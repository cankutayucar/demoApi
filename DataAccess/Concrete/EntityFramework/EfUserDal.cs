using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Dal.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepository<User, AppDbContext>, IUserDal
    {
        public List<OperationClaim> UserOperationClaims(int userId)
        {
            using (var contex = new AppDbContext())
            {
                var result =
                    (from uop in contex.UserOperationClaims
                        join op in contex.OperationClaims on uop.OperationClaimId equals op.Id
                        where uop.UserId == userId
                        select new OperationClaim
                        {
                            Id = op.Id,
                            Name = op.Name
                        });
                return result.OrderBy(p=>p.Name).ToList();
            }
        }
    }
}
