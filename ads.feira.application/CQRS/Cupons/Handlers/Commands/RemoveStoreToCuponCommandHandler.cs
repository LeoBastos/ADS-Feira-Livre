﻿using ads.feira.application.CQRS.Cupons.Commands;
using ads.feira.domain.Entity.Cupons;
using ads.feira.domain.Interfaces.Cupons;
using ads.feira.domain.Interfaces.Stores;
using ads.feira.domain.Interfaces.UnitOfWorks;
using MediatR;

namespace ads.feira.application.CQRS.Cupons.Handlers.Commands
{
    public class RemoveStoreToCuponCommandHandler : IRequestHandler<RemoveStoreFromCuponCommand, Cupon>
    {
        private readonly ICuponRepository _cuponRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveStoreToCuponCommandHandler(ICuponRepository cuponRepository, IStoreRepository storeRepository, IUnitOfWork unitOfWork)
        {
            _cuponRepository = cuponRepository;
            _storeRepository = storeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Cupon> Handle(RemoveStoreFromCuponCommand request, CancellationToken cancellationToken)
        {
            var cupon = await _cuponRepository.GetByIdAsync(request.CuponId);
            if (cupon == null)
            {
                throw new InvalidOperationException($"Cupon with ID {request.CuponId} not found.");
            }

            var store = await _storeRepository.GetByIdAsync(request.StoreId);
            if (store == null)
            {
                throw new InvalidOperationException($"Product with ID {request.StoreId} not found.");
            }

            cupon.RemoveStore(store);

            await _cuponRepository.UpdateAsync(cupon);
            await _unitOfWork.Commit();

            return cupon;
        }
    }
}
