﻿using FuelAcc.Domain.Commons;
using FuelAcc.Domain.Enums;

namespace FuelAcc.Application.UseCases.Authorization
{
    internal class DocumentAuthorizationPoint<ENTITY> : AuthorizationPointTmpl<ENTITY>
        where ENTITY : class, IDocumentEntity
    {
        public DocumentAuthorizationPoint() : base(ApplicationArea.Document)
        {
        }
    }
}