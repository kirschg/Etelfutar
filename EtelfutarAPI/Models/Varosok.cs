using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EtelfutarAPI.Models;

public partial class Varosok
{
    public int Id { get; set; }

    public string Nev { get; set; } = null!;

    public string IndexKep { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Ettermek> Ettermeks { get; set; } = new List<Ettermek>();
    [JsonIgnore]
    public virtual ICollection<Felhasznalok> Felhasznaloks { get; set; } = new List<Felhasznalok>();
}
