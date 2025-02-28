using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EtelfutarAPI.Models;

public partial class Ettermek
{
    public int Id { get; set; }

    public string Cim { get; set; } = null!;

    public int ChainId { get; set; }

    public int VarosId { get; set; }

    public string Indexkep { get; set; } = null!;
    [JsonIgnore]
    public virtual Chain Chain { get; set; } = null!;

    public virtual ICollection<Ertekelesek> Ertekeleseks { get; set; } = new List<Ertekelesek>();

    public virtual ICollection<Learaza> Learazas { get; set; } = new List<Learaza>();
    [JsonIgnore]
    public virtual Varosok Varos { get; set; } = null!;

    public virtual ICollection<Etelek> Etels { get; set; } = new List<Etelek>();
}
