using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.Enums
{
    public enum ModerationType
    {
        [Description("Political propoganda")]
        Political,
        [Description("Offensive langugage")]
        Language,
        [Description("Drug references")]
        Drugs,
        [Description("Threatening langugage")]
        Threatening,
        [Description("Sexual content")]
        Sexual
    }
}
