using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EtelfutarAPI.Models;

public partial class Learaza
{
    public int EtteremId { get; set; }

    public int EtelId { get; set; }

    public int Learazas { get; set; }
    [JsonIgnore]
    public virtual Etelek Etel { get; set; } = null!;
    [JsonIgnore]
    public virtual Ettermek Etterem { get; set; } = null!;
}
