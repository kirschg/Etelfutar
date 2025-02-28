using System;
using System.Collections.Generic;

namespace EtelfutarAPI.Models;

public partial class Varosok
{
    public int Id { get; set; }

    public string Nev { get; set; } = null!;

    public string IndexKep { get; set; } = null!;

    public virtual ICollection<Ettermek> Ettermeks { get; set; } = new List<Ettermek>();

    public virtual ICollection<Felhasznalok> Felhasznaloks { get; set; } = new List<Felhasznalok>();
}
