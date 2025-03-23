using System;
using System.Collections.Generic;

namespace EtelfutarAPI.Models;

public partial class Learaza
{
    public int EtteremId { get; set; }

    public int EtelId { get; set; }

    public int Learazas { get; set; }

    public virtual Etelek? Etel { get; set; } = null!;

    public virtual Ettermek? Etterem { get; set; } = null!;
}
