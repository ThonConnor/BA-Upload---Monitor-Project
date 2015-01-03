using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace KTVServerApp.StoreData
{
    /*
     * information of Album
     */ 
    class clsAlbum
    {
        #region variable
        private string v_albumid;
        private string v_albumname;
        private int v_vol;
        private clsProduction v_production;
        private Image v_photo;
        #endregion

        #region constructor
        public clsAlbum(string id, string name,int vol, Image photo, clsProduction production)
        {
            v_albumid = id;
            v_albumname = name;
            v_vol = vol;
            v_photo = photo;
            v_production = production;
        }
        public clsAlbum()
        {
            v_albumid = "";
            v_albumname = "";
            v_vol = 0;
            v_photo = null;
            v_production = null;
        }
        #endregion
        #region properties
        public string P_AlbumID
        {
            get
            {
                return v_albumid;
            }
            set
            {
                v_albumid = value;
            }
        }
        public string P_AlbumName
        {
            get
            {
                return v_albumname;
            }
            set
            {
                v_albumname = value;
            }
        }
        public int P_Vol
        {
            get
            {
                return v_vol;
            }
            set
            {
                v_vol = value;
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
        public clsProduction P_Production
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
        #endregion
    }
}
