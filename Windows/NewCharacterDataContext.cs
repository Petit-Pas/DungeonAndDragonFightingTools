// /***********************************************************************************
//  * ERTMS Solutions
//  ***********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDFight.Windows
{
    public class NewCharacterDataContext
    {
        public string Name { get; set; }

        public string raw_CA { get; set; }

        public int CA;

        public string raw_initiative { get; set; }

        public int Initiative;

        public string raw_max_hp { get; set; }

        public int RawMaxHP;

        public string raw_hp { get; set; }

        public int MaxHp;
    }
}
