using BookMaster34.Models;
using System.Configuration;
using System.Data;
using System.Windows;

namespace BookMaster34
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Bookmaster34Context _context;
        public static Bookmaster34Context Get34Context()
        {
            if (_context == null)
            {
                _context = new Bookmaster34Context();
            }
            return _context;
        }
    }

}
