using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace EnglishDictionary
{
    class WoMe : IComparable
    {
        
        public string wordset { get; set; }
        public List<string> Meanset { get; set; }
        public string WordAndMean { get; set; }

       
        public void meanlist(string mean)
        {   //*****************************************************************//
            //This mean list is use to display the words and menaing to listbox//
            //*****************************************************************//
            if (Meanset == null)
            {
                Meanset = new List<string> { mean }; 
                WordAndMean ="Word:-" + " '" + wordset + "'" +  " Word's Meaning :-  " + " '" + mean + "'";
            }

        }
        //********************************************//
        //The CompareTo method is compare the wordset.//
        //********************************************//
        public int CompareTo(object obj)
        {
            WoMe wmset = (WoMe)obj;
            return wordset.CompareTo(wmset.wordset);
        }
    }
}
