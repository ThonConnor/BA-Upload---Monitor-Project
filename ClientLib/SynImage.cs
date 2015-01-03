using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Drawing;
using NetworkCommsDotNet;
using KTVServerApp.Script.Network;

namespace ClientLib
{
    public class SynImage
    {
        public class StoreImage
        {
            private string id;
            private Image image;

            public StoreImage(string id, Image image)
            {
                this.id = id;
                this.image = image;
            }

            public string ID
            {
                get
                {
                    return id;
                }
                set
                {
                    id = value;
                }
            }
            public Image Image
            {
                get
                {
                    return image;
                }
                set
                {
                    image = value;
                }
            }
        }

        public delegate void RecieveHandler(ArrayList data);
        public event RecieveHandler OnRecieveImage = delegate { };

        public void Init()
        {
            S_NetworkCommunication.RecieveIncomingPacket<byte[]>("LoadImageClient", (type, connection, message) =>
            {
                ArrayList data = S_NetworkCommunication.RecieveIncomingObject<ArrayList>(message);
                if (OnRecieveImage != null)
                {
                    OnRecieveImage(data);
                }
            });
        }

        public void SendRequest(Connection connection, string sql)
        {
            S_NetworkCommunication.SendMessage<string>("LoadImageServer", connection, sql);
        }
    }
}
