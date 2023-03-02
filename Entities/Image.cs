using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QemuUtil.Entities
{
    class Image
    {
        public int id { get; set; } = 0;
        public string Name { get; set; }
        public string url { get; set; }
        public string Description { get; set; }

        public Image(int id, string name, string url)
        {
            this.id=id;
            Name = name;
            this.url = url;
            Global.ps.StandardInput.WriteLine(name);
            //IMGs.Items.Add(Name);

                //MessageBox.Show(++id + " " + Name + " " + this.url);
        }
    }
}
