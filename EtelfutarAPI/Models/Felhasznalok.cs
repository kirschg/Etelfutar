using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EtelfutarAPI.Models;

public partial class Felhasznalok
{
    public int Id { get; set; }

    public string FelhasznaloNev { get; set; } = null!;

    public string TeljesNev { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int VarosId { get; set; }

    public string Lakcim { get; set; } = null!;

    public string Hash { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public int Jogosultsag { get; set; }

    public int Aktiv { get; set; }
    [JsonIgnore]
    public virtual ICollection<Ertekelesek> Ertekeleseks { get; set; } = new List<Ertekelesek>();

    public virtual Rendeles? Rendeles { get; set; }

    public virtual Varosok? Varos { get; set; } = null!;
}
