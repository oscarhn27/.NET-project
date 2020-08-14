
using Repositories.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;


namespace Services.Common
{
    public class BaseService { 

        protected IUnitOfWork _uow;

        public BaseService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }

        public IUnitOfWork getIUnitOfWork()
        {
            return _uow;

        }
    }
}
