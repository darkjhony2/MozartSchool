﻿namespace ColegioMozart.Domain.Common;

public abstract class AuditableEntity<TKeyType> : KeyedEntity<TKeyType>
{
    public DateTime Created { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public string? LastModifiedBy { get; set; }


}
