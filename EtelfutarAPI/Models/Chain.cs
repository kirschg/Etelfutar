using EtelfutarAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EtelfutarAPI.Models;

public partial class Chain
{
    public int Id { get; set; }

    public string Nev { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Etelek> Eteleks { get; set; } = new List<Etelek>();
    [JsonIgnore]
    public virtual ICollection<Ettermek> Ettermeks { get; set; } = new List<Ettermek>();
}
