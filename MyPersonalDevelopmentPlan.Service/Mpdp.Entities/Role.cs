﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mpdp.Entities
{
  public class Role : IEntityBase
  {
    public int Id { get; set; }
    public string Name { get; set; }
  }
}
