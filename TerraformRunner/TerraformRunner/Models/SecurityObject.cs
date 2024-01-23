using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerraformRunner
{
    public class SecurityObject
    {
        public SecurityObject(string securityId, string label)
        {
            SecurityId = securityId;
            Label = label;
        }
        public string SecurityId { get; internal set; }
        public string Label { get; internal set; }
    }
}

