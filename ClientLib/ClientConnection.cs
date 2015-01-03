using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkCommsDotNet;
using KTVServerApp.Script.Network;
using System.Collections;
namespace ClientLib
{
    public class ClientConnection
    {
        private static ClientConnection instance =null;
        private Connection connection;
        private DataSongPool songpool;
        
        private ClientConnection()
        {
            songpool = new DataSongPool();
        }

        public Connection ThisConnection { get { return connection; } }
        public DataSongPool MainData { get { return songpool; } }

        public static ClientConnection Instance()
        {
            if (instance == null) instance = new ClientConnection();
            return instance;
        }
        public void StartConnectionToServer(string serverip, int port)
        {
            connection = S_NetworkCommunication.StartConenction(serverip, port);
        }
        public void CloseConnection()
        {
            S_NetworkCommunication.CloseConnection();
        }

        public class DataSongPool
        {
            private ArrayList production_photo;
            private ArrayList singer_photo;
            private ArrayList album_photo;

            public DataSongPool()
            {

            }

            public ArrayList ProductionImage
            {
                get { return production_photo; }
                set { production_photo = value; }
            }
            public ArrayList SingerImage
            {
                get { return singer_photo; }
                set { singer_photo = value; }
            }
            public ArrayList AlbumImage
            {
                get { return album_photo; }
                set { album_photo = value; }
            }

        }
    }
}
