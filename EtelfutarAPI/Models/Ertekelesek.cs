using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EtelfutarAPI.Models;

public partial class Ertekelesek
{
    public int Id { get; set; }

    public int FelhasznaloId { get; set; }

    public int EtteremId { get; set; }

    public string Szoveg { get; set; } = null!;

    public int Ertekeles { get; set; }

    public virtual Ettermek? Etterem { get; set; } = null!;

    public virtual Felhasznalok? Felhasznalo { get; set; } = null!;
}
