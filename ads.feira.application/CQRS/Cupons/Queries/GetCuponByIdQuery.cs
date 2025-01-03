﻿using ads.feira.domain.Entity.Cupons;
using MediatR;

namespace ads.feira.application.CQRS.Cupons.Queries
{
    public class GetCuponByIdQuery : IRequest<Cupon>
    {
        public string Id { get; set; }

        public GetCuponByIdQuery(string id)
        {
            Id = id;
        }
    }
}
