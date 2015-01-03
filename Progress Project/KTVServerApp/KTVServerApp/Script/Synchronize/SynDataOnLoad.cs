using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Drawing;
namespace KTVServerApp.Script.Synchronize
{
#region OnLoad
    public class SynDataOnLoad
    {
        public ArrayList Country { get; set; }
        public ArrayList Singer { get; set; }
        public ArrayList Production { get; set; }
        public ArrayList Album { get; set; }
        public SynDataOnLoad(ArrayList lstCountry, ArrayList lstSinger, ArrayList lstProduction, ArrayList lstAlbum)
        {
            this.Country = lstCountry;
            this.Production = lstProduction;
            this.Singer = lstSinger;
            this.Album = lstAlbum;
        }
    }

    public class SynCountry
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Image Photo { get; set; }

        public SynCountry(int id, string name, Image photo)
        {
            this.ID = id;
            this.Name = name;
            this.Photo = photo;
        }
    }
    public class SynSinger
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Image Photo { get; set; }

        public SynSinger(int id, string name, Image photo)
        {
            this.ID = id;
            this.Name = name;
            this.Photo = photo;
        }
    }
    public class SynAlbum
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Image Photo { get; set; }

        public SynAlbum(int id, string name, Image photo)
        {
            this.ID = id;
            this.Name = name;
            this.Photo = photo;
        }
    }
    public class SynProduction
    {
         public int ID { get; set; }
        public string Name { get; set; }
        public Image Photo { get; set; }

        public SynProduction(int id, string name, Image photo)
        {
            this.ID = id;
            this.Name = name;
            this.Photo = photo;
        }
    }
#endregion

#region SongData
    [System.Serializable]
   public class SynSong
    {
        public string SongName { get; set; }
        public Image Production { get; set; }
        public string FilePath { get; set; }

        public SynSong(string name,Image pro, string path)
        {
            //this.ID = id;
            this.SongName = name;
            this.Production = pro;
            this.FilePath = path;
        }
    }
#endregion

}
