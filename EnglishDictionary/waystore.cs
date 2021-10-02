using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace EnglishDictionary
{
    
    class waystore
    {
        
        bool insert = true;
        public void store (string word, string mean)
        {
            //******************************************************//
            //When the text boxs are empty.This message will be show//
            //******************************************************//

            if (word == "" && mean == "")
            {
                insert = false;
                string message = "Insert a values";
                MessageBox.Show(message);   
            }
            //******************************************************//
            //When enter the word and meaning this will execute     //
            //******************************************************//
            if (insert == true)
            {
                
                WoMe wmset = new WoMe();
                wmlistword.WMlist.Add(wmset);


                int i = 0;
                while (i < wmlistword.WMlist.Count)
                {    //***********************************************//
                    // When click the addbtn. add new word and meaning//
                    //************************************************//
                    if (wmlistword.WMlist[i].wordset == null && wmlistword.WMlist[i].Meanset == null)
                        {
                            wmlistword.WMlist[i].wordset = word;
                            wmlistword.WMlist[i].meanlist(mean);
                        }

                    //***************************************************//
                    //When the word already exit . this will execute     //
                    //***************************************************//
                    else if (wmlistword.WMlist[i].wordset == word)
                    {
                        DialogResult result = MessageBox.Show("Word already exit. Do you want to replace ", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            wmlistword.WMlist[i].wordset = word;

                        }
                        else if (result == DialogResult.No)
                        {

                        }
      
                          //***************************************************//
                          //If word is already their . It will check the mean. //
                          //***************************************************//
                            if (wmlistword.WMlist[i].wordset == word && wmlistword.WMlist[i].Meanset.Contains(mean) == true)
                        {
                            DialogResult re = MessageBox.Show("Word already exit.  ", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (re == DialogResult.Yes)
                            {
                                
                                wmlistword.WMlist[i].Meanset[wmlistword.WMlist[i].Meanset.IndexOf(mean)] = mean;
                            }
                            else if (result == DialogResult.No)
                            {

                            }
                            break;
                         //*********************************************//
                         // When add a new meaning . this will execute. //
                         //*********************************************//
                        }
                        if(wmlistword.WMlist[i].wordset == word && wmlistword.WMlist[i].Meanset.Contains(mean) ==false)
                        {
                            DialogResult re = MessageBox.Show("Do you want to add this new meaning? ", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (re == DialogResult.Yes)
                            {
                                wmlistword.WMlist[i].Meanset.Add(mean);
                                string wordnew = wmlistword.WMlist[i].WordAndMean + "," + mean + " ";
                                wmlistword.WMlist[i].WordAndMean = wordnew;

                            }
                            else if (result == DialogResult.No)
                            {

                            }
                            
                        }
                        break;
                    }
                    i++;
                }
                

            }


        }


    }
}
