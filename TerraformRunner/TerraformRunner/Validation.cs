using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerraformRunner
{
    public static class Validation
    {
        public static bool formRun(string validationString, string propertyName)
        {
            if (string.IsNullOrWhiteSpace(validationString))
            {
                MessageWindow.Show("Warning", $"Make sure {propertyName} is not empty");
                return true;
            }

            return false;
        }
    }
}
