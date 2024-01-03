﻿using Domain.Interfaces.CQS;
using System.Diagnostics.CodeAnalysis;

namespace Application.Responses
{
    [ExcludeFromCodeCoverage]
    public class ProductResponse : IResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
