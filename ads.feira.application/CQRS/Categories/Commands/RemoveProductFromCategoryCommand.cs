﻿using ads.feira.domain.Entity.Categories;
using MediatR;

namespace ads.feira.application.CQRS.Categories.Commands
{
    public class RemoveProductFromCategoryCommand : IRequest<Category>
    {
        public string CategoryId { get; set; }
        public string ProductId { get; set; }
    }
}
