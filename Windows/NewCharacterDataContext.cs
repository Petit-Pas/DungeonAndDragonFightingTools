// /***********************************************************************************
//  * ERTMS Solutions
//  ***********************************************************************************/

using DDFight.Tools;
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

        public int CA { get; set; }

        public string raw_initiative { get; set; }

        public int Initiative { get; set; }

        public string raw_max_hp { get; set; }

        public int MaxHp { get; set; }

        public string raw_hp { get; set; }

        public int Hp { get; set; }

        /// <summary>
        ///     Transforms all the string values into numeric ones
        /// </summary>
        public void FromRawToReal ()
        {
            try
            {
                CA = MyConverter.StringToInt(raw_CA);
            }
            catch (Exception )
            {
            }
            try
            {
                Initiative = MyConverter.StringToInt(raw_initiative);
            }
            catch (Exception)
            {
            }
            try
            {
                MaxHp = MyConverter.StringToInt(raw_max_hp);
            }
            catch (Exception)
            {
            }
            try
            {
                Hp = MyConverter.StringToInt(raw_hp);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        ///     transforms all the numeric values into string ones
        /// </summary>
        public void FromRealToRaw ()
        {
            raw_CA = CA.ToString();
            raw_initiative = Initiative.ToString();
            raw_max_hp = MaxHp.ToString();
            raw_hp = Hp.ToString();
        }
    }
}
