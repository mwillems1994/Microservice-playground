using System;
using System.ComponentModel.DataAnnotations;

namespace Playground.Nl.ProductManagementAPI.Database.Common
{
    public abstract class EntityBase : EntityLog
    {
        [Required, Key] public Guid Id { get; set; } = Guid.NewGuid();
    }
}