using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KTVServerApp.StoreData
{
    /*
     * information of song category
     */ 
    class clsSongCategory
    {
        #region variables
        private string v_categoryid;    // id of category
        private string v_categoryname;  // name of category
        private long v_count;           // popular count
        #endregion

        #region constructor
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="count"></param>
        public clsSongCategory(string id, string name, long count)
        {
            v_categoryid = id;
            v_categoryname = name;
            v_count = count;
        }
        /// <summary>
        /// default constructor
        /// </summary>
        public clsSongCategory()
        {
            v_categoryid = "";
            v_categoryname = "";
            v_count = 0;
        }
        #endregion
        #region properties
        /// <summary>
        /// get or set id of category
        /// </summary>
        public string P_CategoryId
        {
            get
            {
                return v_categoryid;
            }
            set
            {
                v_categoryid = value;
            }
        }
        /// <summary>
        /// get or set name of category
        /// </summary>
        public string P_CategoryName
        {
            get
            {
                return v_categoryname;
            }
            set
            {
                v_categoryname = value;
            }
        }
        /// <summary>
        /// get or set popular count
        /// </summary>
        public long P_PopularCount
        {
            get
            {
                return v_count;
            }
            set
            {
                v_count = value;
            }
        }
        #endregion
    }
}
