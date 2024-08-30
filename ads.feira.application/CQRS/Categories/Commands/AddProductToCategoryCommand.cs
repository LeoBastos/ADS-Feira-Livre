﻿using ads.feira.domain.Entity.Categories;
using MediatR;

namespace ads.feira.application.CQRS.Categories.Commands
{
    public class AddProductToCategoryCommand : IRequest<Category>
    {
        public int CategoryId { get; set; }
        public int ProductId { get; set; }
    }
}