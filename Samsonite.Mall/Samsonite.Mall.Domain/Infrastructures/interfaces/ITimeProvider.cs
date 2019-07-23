using AutoMapper;
using Samsonite.Mall.Domain.Identity;
using NLog;
using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;

namespace Samsonite.Mall.Domain.Infrastructures {
    public interface ITimeProvider
    {
        DateTime Now { get; }
    }
}