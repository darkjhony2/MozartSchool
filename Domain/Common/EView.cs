namespace ColegioMozart.Domain.Common;

public class EView
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string DisplayName { get; set; }

    public bool AllowCreate { get; set; }
    public bool AllowDetails { get; set; }
    public bool AllowDelete { get; set; }
    public bool AllowUpdate { get; set; }
    public EEntity Entity { get; set; }

}


public class EEntity
{
    public int Id { get; set; }

    public string EntityFullName { get; set; }

    public string EntityDtoFullName { get; set; }

    public List<EEntityFields> EntityFields { get; set; }
}


public class EEntityFields
{
    public int Id { get; set; }

    public EEntity Entity { get; set; }

    public bool AllowUpdate { get; set; }
    public bool AllowInsert { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public bool IsRequired { get; set; }
}
