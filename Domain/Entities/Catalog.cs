using System.Collections.Generic;

namespace Domain.Entities;

public class Catalog
{
    // public int Id { get; set; }
    // public string Name { get; set; }
    public List<Category> Categories { get; set; }
}