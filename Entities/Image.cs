using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QemuUtil.Entities
{
    class Image
    {
        public int Counter { get; set; } = 0;

        public int id { get; set; }
        public string url { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Image(int id, string url, string name, string description)
        {
            this.id = id;
            this.url = url;
            this.Name = name;
            this.Description = description;
        }

        public void DataSeeder()
        {
            new Image(id=++Counter, "https://sourceforge.net/projects/mx-linux/files/Final/Xfce/MX-21.3_x64.iso", "MX-21.3_x64.iso", "some blabla");
            new Image(id=++Counter, "https://mirror.alpix.eu/endeavouros/iso/EndeavourOS_Cassini_neo_22_12.iso", "EndeavourOS_Cassini_neo_22_12.iso", "some more blabla");
            new Image(id=++Counter, "https://mirror.crexio.com/linuxmint/isos/stable/21.1/linuxmint-21.1-cinnamon-64bit.iso", "linuxmint-21.1-cinnamon-64bit.iso", "some more blabla");
            new Image(id=++Counter, "https://download.manjaro.org/kde/22.0.3/manjaro-kde-22.0.3-230213-linux61.iso", "manjaro-kde-22.0.3-230213-linux61.iso", "some more blabla");
            new Image(id=++Counter, "https://download.manjaro.org/gnome/22.0.3/manjaro-gnome-22.0.3-230213-linux61.iso", "manjaro-gnome-22.0.3-230213-linux61.iso", "some more blabla");
            //new Image(++Counter, "", ".iso", "some more blabla");
            //new Image(++Counter, "", ".iso", "some more blabla");
            //new Image(++Counter, "", ".iso", "some more blabla");
            //new Image(++Counter, "", ".iso", "some more blabla");
            //new Image(++Counter, "", ".iso", "some more blabla");
            //new Image(++Counter, "", ".iso", "some more blabla");
            //new Image(++Counter, "", ".iso", "some more blabla");
            //new Image(++Counter, "", ".iso", "some more blabla");
            //new Image(++Counter, "", ".iso", "some more blabla");
            //new Image(++Counter, "", ".iso", "some more blabla");
            //new Image(++Counter, "", ".iso", "some more blabla");
            //new Image(++Counter, "", ".iso", "some more blabla");
            //new Image(++Counter, "", ".iso", "some more blabla");
            //new Image(++Counter, "", ".iso", "some more blabla");
            //new Image(++Counter, "", ".iso", "some more blabla");
            //new Image(++Counter, "", ".iso", "some more blabla");
            //new Image(++Counter, "", ".iso", "some more blabla");
            //new Image(++Counter, "", ".iso", "some more blabla");
            //new Image(++Counter, "", ".iso", "some more blabla");

        }
    }
}
