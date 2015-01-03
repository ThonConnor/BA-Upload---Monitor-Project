using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
namespace K
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
#endregionTVServerApp.Script.Synchronize

#region SongData
    [System.Serializable]
    public class SynSong : INotifyPropertyChanged
    {
        //event property
        public event PropertyChangedEventHandler PropertyChanged;

        private string songname;
        private Image production;
        private byte[] productionbyte;
        private string filepath;

        public SynSong(string name, Image pro, string path)
        {
            //this.ID = id;
            this.songname = name;
            this.production = pro;
            this.filepath = path;
        }

        public SynSong(string name, byte[] pro, string path)
        {
            this.songname = name;
            this.productionbyte = pro;
            this.filepath = path;
        }
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public byte[] ImageSoure
        {
            get
            {
                return productionbyte;
            }
            set
            {
                productionbyte = value; 
            }
        }
        public string SongName
        {
            get { return songname; }
            set
            {
                songname = value;
                NotifyPropertyChanged("SONGNAME");
            }
        }
        public string FilePath
        {
            get { return filepath; }
            set
            {
                filepath = value;
                NotifyPropertyChanged("FILEPATH");
            }
        }
        public Image Production
        {
            get { return production; }
            set
            {
                production = value;
                NotifyPropertyChanged("PRODUCTION");
            }
        }
    }
#endregion

}
