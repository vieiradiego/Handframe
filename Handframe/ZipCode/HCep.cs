using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Canducci.Zip;

namespace Handframe.ZipCode
{
    public class HCep
    {
        public Boolean Find(string code)
        {
            try
            {
                ZipCodeInfo zipCodeInfo = ZipCodeLoad.Find(code);
                if (zipCodeInfo.Erro == false)
                {
                    //Zip Find
                }
                else
                {
                    //Error Find
                }
            }
            catch (ZipCodeException ex)
            {
                throw ex;
            }
            return true;
        }

        public String[] Get(string code)
        {
            string[] r = new string[6];
            try
            {
                ZipCodeInfo zipCodeInfo = ZipCodeLoad.Find(code);
                if (zipCodeInfo.Erro == false)
                {
                    
                    r[0] = zipCodeInfo.Address;
                    r[1] = zipCodeInfo.City;
                    r[2] = zipCodeInfo.Complement;
                    r[3] = zipCodeInfo.District;
                    r[4] = (zipCodeInfo.Ibge).ToString();
                    r[5] = (zipCodeInfo.Erro).ToString();
                }
                else
                {
                    r[5] = (zipCodeInfo.Erro).ToString();
                }
            }
            catch (ZipCodeException ex)
            {
                throw ex;
            }
            return r;
        }
    }
}
