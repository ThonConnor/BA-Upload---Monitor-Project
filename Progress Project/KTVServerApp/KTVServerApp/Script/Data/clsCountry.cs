using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace KTVServerApp.StoreData
{
    /*
     * data of country
     */ 
    class clsCountry
    {
        #region variables
        private string v_countryid;     // id of country
        private string v_englishname;   // represent name
        private string v_realname;      // real name
        private Image v_photo;          // photo
        private string v_shortcut;
        #endregion

        #region construtor
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="engname"></param>
        /// <param name="realname"></param>
        /// <param name="photo"></param>
        public clsCountry(string id, string engname, string realname, Image photo,string shortcut)
        {
            v_countryid = id;
            v_englishname = engname;
            v_realname = realname;
            v_photo = photo;
            v_shortcut = shortcut;
        }
        /// <summary>
        /// default constructor
        /// </summary>
        public clsCountry()
        {
            v_countryid = "";
            v_englishname = "";
            v_realname = "";
            v_photo = null;
            v_shortcut = "";
        }
        #endregion
        #region properties
        /// <summary>
        /// get or set country id
        /// </summary>
        public string P_CountryId
        {
            get
            {
                return v_countryid;
            }
            set
            {
                v_countryid = value;
            }
        }
        /// <summary>
        /// get or set represent name
        /// </summary>
        public string P_RepresentName
        {
            get
            {
                return v_englishname;
            }
            set
            {
                v_englishname = value;
            }
        }
        /// <summary>
        /// get or set photo
        /// </summary>
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
        /// <summary>
        /// get or set real name
        /// </summary>
        public string P_RealName
        {
            get
            {
                return v_realname;
            }
            set
            {
                v_realname = value;
            }
        }
        public string P_Shortcut
        {
            get
            {
                return v_shortcut;
            }
            set
            {
                v_shortcut = value;
            }
        }
        #endregion

        public override string ToString()
        {
            return v_englishname;
        }
    }
}
