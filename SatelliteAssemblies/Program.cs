using System;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading;

[assembly: NeutralResourcesLanguageAttribute("en-US")]

namespace SatelliteAssemblies
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] cultures = { "en-CA", "en-US", "fr-FR", "de-DE" };
            string culture = args.Length > 0 ? args[0] : "en-US";

            if (!cultures.Contains(culture))
            {
                Console.WriteLine("Not a supported culture.");
                Environment.Exit(1);
            }

            CultureInfo startCulture = Thread.CurrentThread.CurrentCulture;

            try
            {
                CultureInfo argCulture = new CultureInfo(culture);
                Thread.CurrentThread.CurrentCulture = argCulture;
                Thread.CurrentThread.CurrentUICulture = argCulture;

                ResourceManager rm = new ResourceManager("Resource", typeof(Program).Assembly);

                Console.WriteLine("The current culture is {0}.", Thread.CurrentThread.CurrentUICulture.Name);
                Console.WriteLine(rm.GetString("Hello"));
                Console.WriteLine(rm.GetString("Goodbye"));
            }
            catch (CultureNotFoundException e)
            {
                Console.WriteLine("Cannot instantiate specified culture: {0}", e.InvalidCultureName);
                Environment.Exit(1);
            }
            finally
            {
                Thread.CurrentThread.CurrentUICulture = startCulture;
                Thread.CurrentThread.CurrentCulture = startCulture;
            }
        }
    }
}
