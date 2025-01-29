using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSeguro.Settings
{
    [Serializable]
    public class ConnectionStringCollection
    {
        public string ConnectionStringSqlServer { get; set; } = null!;
    }
}
