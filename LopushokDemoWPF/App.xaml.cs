using LopushokDemoWPF.ADO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LopushokDemoWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly LopushokEntities _connection = new LopushokEntities();
        public static LopushokEntities Connection => _connection;
    }
}
