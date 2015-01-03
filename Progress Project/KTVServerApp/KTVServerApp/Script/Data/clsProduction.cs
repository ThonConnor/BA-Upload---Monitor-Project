using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace KTVServerApp.StoreData
{
    /*
     * information of Production
     */ 
     class clsProduction
    {
        #region variable
        private string v_productionid;
        private string v_productionname;
        private clsCountry v_country;
        private Image v_photo;
        #endregion

        #region constructor
        public clsProduction(string id, string name, Image photo, clsCountry country)
        {
            v_productionid = id;
            v_productionname = name;
            v_photo = photo;
            v_country = country;
        }
        public clsProduction()
        {
            v_productionid = "";
            v_productionname = "";
            v_photo = null;
            v_country = null;
        }
        #endregion
        #region properties
        public string P_ProductionID
        {
            get
            {
                return v_productionid;
            }
            set
            {
                v_productionid = value;
            }
        }
        public string P_ProductionName
        {
            get
            {
                return v_productionname;
            }
            set
            {
                v_productionname = value;
            }
        }
        public Image P_Photo
        {
            get
            {
                return v_photo;
            }
            set
            {
                v_photo = value;
            }
        }
        public clsCountry P_Country
        {
            get
            {
                return v_country;
            }
            set
            {
                v_country = value;
            }
        }
        #endregion

        public override string ToString()
        {
            return P_ProductionName;
        }
    }
}
