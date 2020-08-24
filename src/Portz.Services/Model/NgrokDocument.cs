using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Portz.Services.Model
{
    public class NgrokDocument
    {
        [Required]
        public IList<Tunnel> tunnels { get; set; }
    }
}
