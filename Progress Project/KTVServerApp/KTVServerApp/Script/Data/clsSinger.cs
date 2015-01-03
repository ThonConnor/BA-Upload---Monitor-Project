using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace KTVServerApp.StoreData
{
    /*
     * class to store information of singer
     */ 

    class clsSinger
    {
        #region Variables
        private string v_singer_id;     // id of each singer
        private string v_singer_name;   // name of singer
        private string v_sex;           // gender of singer
        private clsProduction v_production;    // production of singer
        private long v_popular_count;   // popular of singer
        private string v_pinyin;        // pinyin of singer
        private Image v_photo;          // photo of singer
        #endregion

        #region Constructor
        /// <summary>
        /// constructor with data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="sex"></param>
        /// <param name="production"></param>
        /// <param name="pop"></param>
        /// <param name="pinyin"></param>
        /// <param name="photo"></param>
        public clsSinger(string id,string name,string sex,clsProduction production,long pop,string pinyin,Image photo)
        {
            v_singer_id = id;
            v_singer_name = name;
            v_sex = sex;
            v_production = production;
            v_popular_count = pop;
            v_pinyin = pinyin;
            v_photo = photo;
        }

        /// <summary>
        /// default with constructor
        /// </summary>
        public clsSinger()
        {
            v_singer_id = "";
            v_singer_name = "";
            v_sex = "";
            v_production = null;
            v_popular_count = 0;
            v_pinyin = "";
            v_photo = null;
        }
        #endregion

        #region Property
        /// <summary>
        /// property of ID singer
        /// </summary>
        public string P_SingerID
        {
            get
            {
                return v_singer_id;
            }
            set
            {
                v_singer_id = value;
            }
        }
        /// <summary>
        /// Property of Singer Name
        /// </summary>
        public string P_SingerName
        {
            get
            {
                return v_singer_name;
            }
            set
            {
                v_singer_name = value;
            }
        }
        /// <summary>
        /// gender of Singer
        /// </summary>
        public string P_Gender
        {
            get
            {
                return v_sex;
            }
            set
            {
                v_sex = value;
            }
        }
        /// <summary>
        /// production of Singer
        /// </summary>
        public clsProduction P_Pro
        {
            get
            {
                return v_production;
            }
            set
            {
                v_production = value;
            }
        }
        /// <summary>
        /// view count of singer
        /// </summary>
        public long P_Popular_Count
        {
            get
            {
                return v_popular_count;
            }
            set
            {
                v_popular_count = value;
            }
        }
        /// <summary>
        /// pinyin of Singer
        /// </summary>
        public string P_Pinyin
        {
            get
            {
                return v_pinyin;
            }
            set
            {
                v_pinyin = value;
            }
        }
        /// <summary>
        /// Photo of Singer
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
        #endregion

        public override string ToString()
        {
            return v_singer_name;
        }
    }
}
